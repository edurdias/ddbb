using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Contracts.ViewModels;

namespace ddbb.App
{
	public class Bootstrapper : BootstrapperBase
	{
		private CompositionContainer container;
		private static readonly WindowManager WindowManager = new WindowManager();
		private static readonly EventAggregator EventAggregator = new EventAggregator();

		public Bootstrapper()
		{
			Initialize();
		}

		public CompositionContainer Container
		{
			get
			{
				if (container == null)
				{
					var catalog = new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
					
					
					container = new CompositionContainer(catalog);

					var batch = new CompositionBatch();
					batch.AddExportedValue<IWindowManager>(WindowManager);
					batch.AddExportedValue<IEventAggregator>(EventAggregator);
					batch.AddExportedValue(container);

					container.Compose(batch);
				}
				return container;
			}
		}

		#region IoC with MEF

		protected override object GetInstance(Type service, string key)
		{
			var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
			var exports = Container.GetExportedValues<object>(contract).ToArray();
			if (exports.Any())
				return exports.First();
			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override void BuildUp(object instance)
		{
			Container.SatisfyImportsOnce(instance);
		}

		#endregion

		#region Configuration, Loading Assemblies and Startup

		protected override void Configure()
		{
			MapNamespacesFor<IShell>();
			//TODO: load view model contracts dynamically
			MapNamespacesFor<IToolbarViewModel>();
			MapNamespacesFor<IDbExplorerViewModel>();
			MapNamespacesFor<IViewManagerViewModel>();
			MapNamespacesFor<IConnectionManagerViewModel>();
			MapNamespacesFor<ICollectionEditorViewModel>();
		}

		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			var assemblies = base.SelectAssemblies().ToList();
			assemblies.Add(typeof (IShell).Assembly);
			//TODO: load view model contracts dynamically
			assemblies.AddRange(GetAssembliesFor<IShell>());
			assemblies.AddRange(GetAssembliesFor<IToolbarViewModel>());
			assemblies.AddRange(GetAssembliesFor<IDbExplorerViewModel>());
			assemblies.AddRange(GetAssembliesFor<IViewManagerViewModel>());
			assemblies.AddRange(GetAssembliesFor<IConnectionManagerViewModel>());
			assemblies.AddRange(GetAssembliesFor<ICollectionEditorViewModel>());
			return assemblies;
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<IShell>();
		}

		#endregion


		#region Private

		private void MapNamespacesFor<T>()
		{
			var namespaces = new List<string>();
			Container.GetExportedValues<T>()
				.ToList()
				.ForEach(instance => namespaces.Add(instance.GetType().Namespace));

			var targets = namespaces.Distinct().ToArray();

			ViewLocator.AddNamespaceMapping(string.Empty, targets);
			ViewModelLocator.AddNamespaceMapping(string.Empty, targets);
		}

		private IEnumerable<Assembly> GetAssembliesFor<T>()
		{
			var assemblies = new List<Assembly>();
			foreach (var value in Container.GetExportedValues<T>())
			{
				if (value != null)
					assemblies.Add(value.GetType().Assembly);
			}
			return assemblies.Distinct();
		}

		#endregion

	}
}

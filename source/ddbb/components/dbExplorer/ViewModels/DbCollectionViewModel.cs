using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ddbb.App.Contracts.Domain;

namespace ddbb.App.Components.DbExplorer.ViewModels
{
	public class DbCollectionViewModel : TreeViewViewModel
	{
		private readonly IDbCollection _collection;
		private IObservableCollection<DbTriggerViewModel> _triggers;
		private IObservableCollection<DbFunctionViewModel> _functions;
		private IObservableCollection<DbStoredProcViewModel> _storedProcs;

		public DbCollectionViewModel(IDbCollection collection)
		{
			_collection = collection ?? new DummyCollection();

			Triggers = new BindableCollection<DbTriggerViewModel>();
			Functions = new BindableCollection<DbFunctionViewModel>();
			StoredProcedures = new BindableCollection<DbStoredProcViewModel>();

			if (_collection.StoredProcedures != null && _collection.StoredProcedures.Any()) {
				StoredProcedures.AddRange(_collection.StoredProcedures.Select(sp => new DbStoredProcViewModel(sp)));
			}
			else {
				StoredProcedures.AddRange(new[] { new DbStoredProcViewModel(null) });
			}

			if (_collection.Functions != null && _collection.Functions.Any()) {
				Functions.AddRange(_collection.Functions.Select(f => new DbFunctionViewModel(f)));
			}
			else {
				Functions.AddRange(new[] { new DbFunctionViewModel(null) });
			}

			if (_collection.Triggers != null && _collection.Triggers.Any()) {
				Triggers.AddRange(_collection.Triggers.Select(t => new DbTriggerViewModel(t)));
			}
			else {
				Triggers.AddRange(new[] { new DbTriggerViewModel(null) });
			}
		}

		public string Name
		{
			get
			{
				return _collection.Name;
			}
			set
			{
				if (Equals(value, _collection.Name)) {
					return;
				}

				_collection.Name = value;
				NotifyOfPropertyChange(() => Name);
				Refresh();
			}
		}

		public IObservableCollection<DbStoredProcViewModel> StoredProcedures
		{
			get
			{
				return _storedProcs;
			}
			set
			{
				if (Equals(value, _storedProcs)) {
					return;
				}

				_storedProcs = value;
				NotifyOfPropertyChange(() => StoredProcedures);
				Refresh();
			}
		}

		public IObservableCollection<DbTriggerViewModel> Triggers
		{
			get
			{
				return _triggers;
			}
			set
			{
				if (Equals(value, _triggers)) {
					return;
				}

				_triggers = value;
				NotifyOfPropertyChange(() => Triggers);
				Refresh();
			}
		}

		public IObservableCollection<DbFunctionViewModel> Functions
		{
			get
			{
				return _functions;
			}
			set
			{
				if (Equals(value, _functions)) {
					return;
				}

				_functions = value;
				NotifyOfPropertyChange(() => Triggers);
				Refresh();
			}
		}

		protected override void ToggleExpansion(bool value)
		{
			
		}

		class DummyCollection : IDbCollection
		{
			public string Name
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}

			public IEnumerable<IDbSproc> StoredProcedures { get; set; }

			public IEnumerable<IDbTrigger> Triggers { get; set; }

			public IEnumerable<IDbFunction> Functions { get; set; }
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using ddbb.App.Components.ConnectionManager;
using ddbb.App.Contracts.Domain;
using ddbb.App.Contracts.Events;
using ddbb.App.Contracts.Repositories;
using ddbb.App.Contracts.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ddbb.Components.ConnectionManager.Tests
{
	[TestClass]
	public class ConnectionManagerViewModelTest
	{
		private Mock<IWindowManager> windowManager;
		private Mock<IConnectionRepository> repository;
		private Mock<IEventAggregator> eventAggregator;
		private Mock<IDocumentDbService> documentDb;

		[TestInitialize]
		public void Initialize()
		{
			windowManager = new Mock<IWindowManager>();
			repository = new Mock<IConnectionRepository>();
			eventAggregator = new Mock<IEventAggregator>();
			documentDb = new Mock<IDocumentDbService>();
		}

		[TestMethod]
		public void LoadedConnectionsProperly()
		{
			var expectedConnections = new List<IConnection>
			{
				new Mock<IConnection>().Object
			};

			repository.Setup(r => r.All()).Returns(expectedConnections);
			
			var viewModel = CreateModel();

			Assert.AreEqual(expectedConnections.Count, viewModel.Connections.Count);

			for (var i = 0; i < expectedConnections.Count; i++)
				Assert.AreEqual(expectedConnections[i], viewModel.Connections[i]);
		}

		[TestMethod]
		public void EmptyConnectionsList()
		{
			repository.Setup(r => r.All()).Returns(new List<IConnection>());
			var viewModel = CreateModel();
			Assert.AreEqual(0, viewModel.Connections.Count);
		}


		[TestMethod]
		public void ValidSelectedConnection()
		{
			var expectedConnection = SetupConnections();

			var viewModel = CreateModel();

			Assert.IsNull(viewModel.SelectedConnection);
			Assert.IsFalse(viewModel.CanConnect);
			Assert.IsFalse(viewModel.CanCopy);
			Assert.IsFalse(viewModel.CanModify);
			Assert.IsFalse(viewModel.CanRemove);


			viewModel.SelectedConnection = viewModel.Connections.FirstOrDefault();

			Assert.IsNotNull(viewModel.SelectedConnection);
			Assert.AreEqual(expectedConnection, viewModel.SelectedConnection);
			Assert.IsTrue(viewModel.CanConnect);
			Assert.IsTrue(viewModel.CanCopy);
			Assert.IsTrue(viewModel.CanModify);
			Assert.IsTrue(viewModel.CanRemove);

		}

		[TestMethod]
		public void ConnectingToSelectedConnection()
		{
			var expectedConnection = SetupConnections();

			documentDb
				.Setup(ddb => ddb.Connect(It.IsAny<IConnection>()))
				.ReturnsAsync(expectedConnection);

			eventAggregator
				.Setup(e => e.Publish(It.IsAny<object>(), It.IsAny<Action<System.Action>>()))
				.Callback<object, Action<System.Action>>((m, action) =>
				{
					var message = m as IConnectionEstablishedEvent;
					Assert.IsNotNull(message);
					Assert.IsNotNull(message.Connection);
					Assert.AreEqual(expectedConnection, message.Connection);
				}).Verifiable();

			var viewModel = CreateModelWithSelectedConnection(expectedConnection);
			
			viewModel.Connect();

			eventAggregator.Verify();

		}

		[TestMethod]
		public void OpeningCopyDialog()
		{
			IoC.BuildUp = o => { };

			var expectedConnection = SetupConnections();

			windowManager
				.Setup(w => w.ShowDialog(It.IsAny<CopyConnectionViewModel>(), null, It.IsAny<IDictionary<string, object>>()))
				.Callback<object, object, IDictionary<string, object>>((model, context, options) =>
				{
					var copyViewModel = model as CopyConnectionViewModel;
					Assert.IsNotNull(copyViewModel);
					Assert.AreEqual(expectedConnection, copyViewModel.Connection);
				})
				.Returns(() => true)
				.Verifiable();

			var viewModel = CreateModelWithSelectedConnection(expectedConnection);

			Assert.IsTrue(viewModel.CanCopy);

			viewModel.Copy();

			windowManager.Verify();
		}

		[TestMethod]
		public void OpeningCreateDialog()
		{
			IoC.BuildUp = o => { };

			var expectedConnection = SetupConnections();

			windowManager
				.Setup(w => w.ShowDialog(It.IsAny<ViewConnectionViewModel>(), null, It.IsAny<IDictionary<string, object>>()))
				.Callback<object, object, IDictionary<string, object>>((model, context, options) =>
				{
					var viewConnectionViewModel = model as ViewConnectionViewModel;
					Assert.IsNotNull(viewConnectionViewModel);
					Assert.IsNull(viewConnectionViewModel.Connection);
					Assert.AreEqual(ViewConnectionMode.Create, viewConnectionViewModel.Mode);
				})
				.Returns(() => true)
				.Verifiable();

			var viewModel = CreateModelWithSelectedConnection(expectedConnection);

			viewModel.Create();

			windowManager.Verify();
		}

		[TestMethod]
		public void OpeningModifyDialog()
		{
			IoC.BuildUp = o => { };

			var expectedConnection = SetupConnections();

			windowManager
				.Setup(w => w.ShowDialog(It.IsAny<ViewConnectionViewModel>(), null, It.IsAny<IDictionary<string, object>>()))
				.Callback<object, object, IDictionary<string, object>>((model, context, options) =>
				{
					var viewConnectionViewModel = model as ViewConnectionViewModel;
					Assert.IsNotNull(viewConnectionViewModel);
					Assert.AreEqual(expectedConnection, viewConnectionViewModel.Connection);
					Assert.AreEqual(ViewConnectionMode.Modify, viewConnectionViewModel.Mode);
				})
				.Returns(() => true)
				.Verifiable();

			var viewModel = CreateModelWithSelectedConnection(expectedConnection);

			Assert.IsTrue(viewModel.CanModify);
			
			viewModel.Modify();

			windowManager.Verify();
		}


		[TestMethod]
		public void RemovingConnection()
		{
			var messageService = new Mock<IMessageService>();
			messageService
				.Setup(m => m.Show(It.IsAny<string>(), null, MessageBoxButton.OKCancel, MessageBoxImage.Question))
				.Returns(() => MessageBoxResult.OK);

			IoC.GetInstance = (type, name) => type.IsInstanceOfType(messageService.Object)
				? messageService.Object
				: null;

			var expectedConnection = SetupConnections();

			repository
				.Setup(r=>r.Remove(expectedConnection))
				.Callback<IConnection>(c =>
				{
					Assert.IsNotNull(c);
					Assert.AreEqual(expectedConnection, c);
				})
				.Verifiable();


			var viewModel = CreateModelWithSelectedConnection(expectedConnection);

			Assert.IsTrue(viewModel.CanRemove);

			viewModel.Remove();

			repository.Verify();

			Assert.AreEqual(0, viewModel.Connections.Count);
			Assert.IsNull(viewModel.SelectedConnection);
		}

		private ConnectionManagerViewModel CreateModelWithSelectedConnection(IConnection selectedConnection)
		{
			var viewModel = CreateModel();
			viewModel.SelectedConnection = selectedConnection;
			return viewModel;
		}

		private ConnectionManagerViewModel CreateModel()
		{
			return new ConnectionManagerViewModel(windowManager.Object, repository.Object, eventAggregator.Object, documentDb.Object);
		}


		private IConnection SetupConnections()
		{
			var expectedConnection = new Mock<IConnection>().Object;

			repository
				.Setup(r => r.All())
				.Returns(new List<IConnection>
				{
					expectedConnection
				});
			return expectedConnection;
		}
	}
}

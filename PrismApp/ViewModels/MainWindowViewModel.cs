﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismApp.Events;
using PrismApp.Models;
using PrismApp.Regions;
using Unity;

namespace PrismApp.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        [Dependency]
        public IContentRegionManager Region { get; set; }

        [Dependency]
        public IEventPublisher Event { get; set; }

        [Dependency]
        public OrdersFunction Orders { get; set; }

        [Dependency]
        public AppFunction App { get; set; }

        #region Title property
        private string _title = "Demo";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        #endregion

        #region NavigateCommand property
        private DelegateCommand<string>? _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ??= new DelegateCommand<string>(Navigate, CanNavigate);

        private void Navigate(string path)
        {
            Region.Navigate(path);
        }

        private bool CanNavigate(string path)
        {
            return true;
        }
        #endregion

        #region GoBackCommand property
        private DelegateCommand? _goBackCommand;
        public DelegateCommand GoBackCommand => _goBackCommand ??= new DelegateCommand(GoBack, CanGoBack);

        private void GoBack()
        {
            Region.GoBack();
        }

        private bool CanGoBack()
        {
            return Region.CanGoBack();
        }
        #endregion

        #region RefreshCommand property
        private DelegateCommand? _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand ??= new DelegateCommand(Refresh, CanRefresh);

        private void Refresh()
        {
            (Region.DataContext as IOperationCommands)?.Refresh();
        }

        private bool CanRefresh()
        {
            return (Region.DataContext as IOperationCommands)?.CanRefresh ?? false;
        }
        #endregion

        #region AddNewItemCommand property
        private DelegateCommand? _addNewItemCommand;
        public DelegateCommand AddNewItemCommand => _addNewItemCommand ??= new DelegateCommand(AddNewItem, CanAddNewItem);

        private void AddNewItem()
        {
            (Region.DataContext as IOperationCommands)?.AddNewItem();
        }

        private bool CanAddNewItem()
        {
            return (Region.DataContext as IOperationCommands)?.CanAddNewItem ?? false;
        }
        #endregion

        #region EditItemCommand property
        private DelegateCommand? _editItemCommand;
        public DelegateCommand EditItemCommand => _editItemCommand ??= new DelegateCommand(EditItem, CanEditItem);

        private void EditItem()
        {
            (Region.DataContext as IOperationCommands)?.EditItem();
        }

        private bool CanEditItem()
        {
            return (Region.DataContext as IOperationCommands)?.CanEditItem ?? false;
        }
        #endregion

        #region DeleteItemCommand property
        private DelegateCommand? _deleteItemCommand;
        public DelegateCommand DeleteItemCommand => _deleteItemCommand ??= new DelegateCommand(DeleteItem, CanDeleteItem);

        private void DeleteItem()
        {
            (Region.DataContext as IOperationCommands)?.DeleteItem();
        }

        private bool CanDeleteItem()
        {
            return (Region.DataContext as IOperationCommands)?.CanDeleteItem ?? false;
        }
        #endregion

        public MainWindowViewModel(IEventAggregator ea)
        {
            ea.GetEvent<SituationChangedEvent>().Subscribe(RefreshCommands);
        }

        private void RefreshCommands()
        {
            NavigateCommand.RaiseCanExecuteChanged();
            GoBackCommand.RaiseCanExecuteChanged();
            RefreshCommand.RaiseCanExecuteChanged();
            AddNewItemCommand.RaiseCanExecuteChanged();
            EditItemCommand.RaiseCanExecuteChanged();
            DeleteItemCommand.RaiseCanExecuteChanged();
        }
    }
}

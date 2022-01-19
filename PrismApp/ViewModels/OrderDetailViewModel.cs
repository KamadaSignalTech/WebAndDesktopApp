﻿using Data;
using Data.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismApp.Controllers;
using PrismApp.Events;
using System.Diagnostics;
using System.Windows;
using Unity;

namespace PrismApp.ViewModels
{
    public class OrderDetailViewModel : BindableBase, INavigationAware, IRibbon
    {
        [Dependency]
        public IRegionManager? RegionManager { get; set; }

        [Dependency]
        public IEventAggregator? EventAggregator { get; set; }

        #region Order property
        private Order? order;
        public Order? Order
        {
            get => order;
            set
            {
                if (SetProperty(ref order, value))
                {
                    PublishSituationChangedEvent();
                }
            }
        }
        #endregion

        private async void Initialize(int? id)
        {
            Order = null;

            var order = id.HasValue ? await OrderController.GetOrderAsync(id.Value) : null;
            if (order is null)
            {
                MessageBox.Show("レコードが見つかりません", "警告", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                PublishGoBackEvent();
                return;
            }

            Order = order;
        }

        private void PublishGoBackEvent()
        {
            EventAggregator?.GetEvent<GoBackEvent>().Publish();
        }

        private void PublishSituationChangedEvent()
        {
            EventAggregator?.GetEvent<SituationChangedEvent>().Publish();
        }

        private void NavigateToOrderEdit(int? id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("id", id);
            RegionManager?.RequestNavigate(RegionNames.ContentRegion, "OrderEdit", parameters);
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var id = (int?)navigationContext.Parameters["id"];
            Initialize(id);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        #endregion

        #region IRibbon
        public bool CanRefresh => Order != null;
        public void Refresh()
        {
            if (CanRefresh)
            {
                Initialize(Order?.Id);
            }
        }

        public bool CanAddNewItem => false;
        public void AddNewItem()
        {
            throw new System.NotImplementedException();
        }

        public bool CanEditItem => Order != null && !Order.IsClosed;
        public void EditItem()
        {
            if (CanEditItem)
            {
                NavigateToOrderEdit(Order?.Id);
            }
        }

        public bool CanDeleteItem => Order != null && !Order.IsClosed;
        public async void DeleteItem()
        {
            if (CanDeleteItem)
            {
                if (MessageBox.Show("削除しますか？", "確認", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Debug.Assert(Order != null);
                    await OrderController.DeleteOrderAsync(Order.Id);
                    PublishGoBackEvent();
                }
            }
        }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrismApp.Views
{
    /// <summary>
    /// Orders.xaml の相互作用ロジック
    /// </summary>
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
        }

        /// <summary>
        /// リストビューのヘッダーをクリックしてデータをソートします。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainList_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader header)
            {
                ListViewUtils.SortByProperty(MainList, header);
                MainList.UnselectAll();
            }
        }
    }
}

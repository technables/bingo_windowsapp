using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Bingo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            cbFirstRow.IsChecked = DataHolder.FirstRow;
            cbSecondRow.IsChecked = DataHolder.SecondRow;
            cbThirdRow.IsChecked = DataHolder.ThirdRow;
            cbFourCorner.IsChecked = DataHolder.FourCorners;
            cbFullHouse.IsChecked = DataHolder.FullHouse;
            cbUnlucky.IsChecked = DataHolder.Unlucky;
            if (DataHolder.IsAppInstalled)
            {
                btnCancel.Visibility = Visibility.Visible;
                txtConfigurationMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnCancel.Visibility = Visibility.Collapsed;
                txtConfigurationMessage.Visibility = Visibility.Visible;
                txtConfigurationMessage.Text = StaticMessage.FirstTimeUserMessage;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbFirstRow.IsChecked = false;
            cbSecondRow.IsChecked = false;
            cbThirdRow.IsChecked = false;
            cbFourCorner.IsChecked = false;
            cbFullHouse.IsChecked = false;
            cbUnlucky.IsChecked = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool firstRow = cbFirstRow.IsChecked.Value;
            bool secondRow = cbSecondRow.IsChecked.Value;
            bool thirdRow = cbThirdRow.IsChecked.Value;
            bool fourCorner = cbFourCorner.IsChecked.Value;
            bool fullHouse = cbFullHouse.IsChecked.Value;
            bool unlucky = cbUnlucky.IsChecked.Value;

            DataHolder.FirstRow = firstRow;
            DataHolder.SecondRow = secondRow;
            DataHolder.ThirdRow = thirdRow;
            DataHolder.FourCorners = fourCorner;
            DataHolder.FullHouse = fullHouse;
            DataHolder.Unlucky = unlucky;

            grdMain.IsHitTestVisible = false;
            DataHolder.IsAppInstalled = true;
            txtMessage.Text = StaticMessage.ConfigurationSavedMessage;

            MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
            if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;


        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BingoDraw), null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
    }
}

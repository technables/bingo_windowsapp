using Bingo.UserControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bingo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BingoDraw : Page
    {
        public BingoDraw()
        {
            //DataHolder.IsAppInstalled = false;
            this.InitializeComponent();

        }

        int columnCount = 0;
        int rowCount = 0;
        int currentRow = 0;
        int currentColumn = 0;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            btnDraw.IsEnabled = true;
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (DataHolder.LstBingoNumber.Count > 0)
                {
                    Initiate();
                    AddPreviousCell();
                }
            }
            else
            {

                btnDraw.IsEnabled = true;
                grdWinner.Children.Clear();
                Utilities.DeleteAllNumber();

                Utilities.ResetWinners();
                Initiate();
            }
        }

        private void AddPreviousCell()
        {
            foreach (BingoNumberInfo number in DataHolder.LstBingoNumber)
            {
                BingoNumber numberCell = new BingoNumber(number);
                Grid.SetRow(numberCell, currentRow);
                Grid.SetColumn(numberCell, currentColumn);
                grdBingoNumbers.Children.Add(numberCell);
                if (currentColumn == columnCount - 1)
                {
                    currentRow = currentRow + 1;
                    currentColumn = 0;
                }
                else
                {
                    currentColumn = currentColumn + 1;
                }
            }


        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            btnDraw.IsEnabled = true;
            btnFirstRow.Style = App.Current.Resources["AppBarButtonStyle"] as Style;
            btnSecondRow.Style = App.Current.Resources["AppBarButtonStyle"] as Style;
            btnThirdRow.Style = App.Current.Resources["AppBarButtonStyle"] as Style;
            btnFourCorner.Style = App.Current.Resources["AppBarButtonStyle"] as Style;
            btnFullHouse.Style = App.Current.Resources["AppBarButtonStyle"] as Style;
            btnUnlucky.Style = App.Current.Resources["AppBarButtonStyle"] as Style;

            currentColumn = 0;
            currentRow = 0;
            Utilities.ResetWinners();
            grdWinner.Children.Clear();
            //DataHolder.LstOriginalNumber = new List<NumberInfo>();
            //DataHolder.LstBingoNumber = new List<NumberInfo>();
            Utilities.DeleteAllNumber();

            Initiate();
        }

        private void Initiate()
        {
            SetUpButton();
            grdBingoNumbers.RowDefinitions.Clear();
            grdBingoNumbers.ColumnDefinitions.Clear();
            grdBingoNumbers.Children.Clear();
            RowandColumnDefinition();
            InitiateNumbers();
            UpdateDrawStat();
        }

        private void SetUpButton()
        {
            if (DataHolder.FirstRow) btnFirstRow.Visibility = Visibility.Visible; else btnFirstRow.Visibility = Visibility.Collapsed;
            if (DataHolder.SecondRow) btnSecondRow.Visibility = Visibility.Visible; else btnSecondRow.Visibility = Visibility.Collapsed;
            if (DataHolder.ThirdRow) btnThirdRow.Visibility = Visibility.Visible; else btnThirdRow.Visibility = Visibility.Collapsed;
            if (DataHolder.FourCorners) btnFourCorner.Visibility = Visibility.Visible; else btnFourCorner.Visibility = Visibility.Collapsed;
            if (DataHolder.FullHouse) btnFullHouse.Visibility = Visibility.Visible; else btnFullHouse.Visibility = Visibility.Collapsed;
            if (DataHolder.Unlucky) btnUnlucky.Visibility = Visibility.Visible; else btnUnlucky.Visibility = Visibility.Collapsed;
        }

        private void RowandColumnDefinition()
        {

            double width = DataHolder.DeviceWidth - 250;
            grdBingoNumbers.Width = width;
            columnCount = (int)(width / DataHolder.ColumnWidth);
            rowCount = 100 % columnCount == 0 ? 100 / columnCount : 100 / columnCount + 1;

            grdBingoNumbers.Width = columnCount * DataHolder.ColumnWidth;

            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition grdRow = new RowDefinition();
                grdRow.Height = new GridLength(DataHolder.ColumnWidth);
                grdBingoNumbers.RowDefinitions.Add(grdRow);
            }

            //defining column for seat arrangement
            for (int i = 0; i < (columnCount); i++)
            {
                ColumnDefinition grdColumn = new ColumnDefinition();
                grdColumn.Width = new GridLength(DataHolder.ColumnWidth);
                grdBingoNumbers.ColumnDefinitions.Add(grdColumn);
            }

        }

        int sec;
        private void InitiateNumbers()
        {

            if (DataHolder.LstOriginalNumber.Count == 0)
            {
                DataHolder.LstOriginalNumber = new List<NumberInfo>();
                List<NumberInfo> lst = new List<NumberInfo>();

                for (int i = 1; i <= DataHolder.TotalNumbers; i++)
                {
                    lst.Add(new NumberInfo() { NumberString = i.ToString(), State = 0 });
                }

                Utilities.SaveAllNumber(lst);

                //DataHolder.LstOriginalNumber = lst;
            }
        }
        private async void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstOriginalNumber.Count > 0)
            {

                grdRoot.IsHitTestVisible = false;
                grdRoot.Opacity = 0.4;
                LoadingPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLoading.Height) / 2;
                LoadingPopUp.HorizontalOffset = (DataHolder.DeviceWidth - brdrMainLoading.Width) / 2;
                if (!LoadingPopUp.IsOpen) LoadingPopUp.IsOpen = true;


                Random rnd = new Random();
                int second = rnd.Next(1, 4);
                txtNumber.Text = string.Empty;
                sec = second;
                await System.Threading.Tasks.Task.Delay(second * 1000);

                if (LoadingPopUp.IsOpen) LoadingPopUp.IsOpen = false;

                ProcessingPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainProcessing.Height) / 2;
                ProcessingPopUp.HorizontalOffset = (DataHolder.DeviceWidth - brdrMainProcessing.Width) / 2;
                if (!ProcessingPopUp.IsOpen) ProcessingPopUp.IsOpen = true;

                List<NumberInfo> lstOriginal = DataHolder.LstOriginalNumber;
                List<BingoNumberInfo> lstBingo = DataHolder.LstBingoNumber;
                IEnumerable<NumberInfo> numbers = lstOriginal.OrderBy(x => rnd.Next()).Take(1);
                NumberInfo number = numbers.First();
                txtNumber.Text = number.NumberString;
                txtTest.Text = string.Format("The Lucky Number Is: ");
                //processRing.IsIndeterminate = false;
            }
            else
            {
                txtMessage.Text = StaticMessage.NoNumberLeft;
                grdRoot.IsHitTestVisible = false;
                grdRoot.Opacity = 0.6;

                MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
                if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;
            }

        }

        private void btnCloseProcessing_Click(object sender, RoutedEventArgs e)
        {
            grdRoot.Opacity = 1;
            grdRoot.IsHitTestVisible = true;
            if (ProcessingPopUp.IsOpen) ProcessingPopUp.IsOpen = false;
            BindNumber(txtNumber.Text);
            UpdateDrawStat();
        }

        void UpdateDrawStat()
        {
            int originalCount = DataHolder.LstOriginalNumber.Count;
            int bingoCount = DataHolder.LstBingoNumber.Count;

            string stat = string.Format("Total Draws: {0} ( {1} Remaining )", bingoCount, originalCount);
            txtDrawStat.Text = stat;
        }

        private void BindNumber(string numberString)
        {
            List<NumberInfo> lstOriginal = DataHolder.LstOriginalNumber;
            List<BingoNumberInfo> lstBingo = DataHolder.LstBingoNumber;

            Utilities.DeleteNumber(numberString);

            NumberInfo number = lstOriginal.FirstOrDefault(x => x.NumberString == numberString);

            BingoNumberInfo bingoNumber = new BingoNumberInfo(number);

            Utilities.SaveBingoNumber(bingoNumber);

            BingoNumber numberCell = new BingoNumber(bingoNumber);

            Grid.SetRow(numberCell, currentRow);
            Grid.SetColumn(numberCell, currentColumn);
            grdBingoNumbers.Children.Add(numberCell);



            //lstOriginal.Remove(number);
            //lstBingo.Add(number);

            //DataHolder.LstOriginalNumber = lstOriginal;
            //DataHolder.LstBingoNumber = lstBingo;


            if (currentColumn == columnCount - 1)
            {
                currentRow = currentRow + 1;
                currentColumn = 0;
            }
            else
            {
                currentColumn = currentColumn + 1;
            }

            if (DataHolder.Unlucky)
            {

                if (DataHolder.LstBingoNumber.Count == DataHolder.UnluckyNumber)
                {
                    txtMessage.Text = StaticMessage.UnLuckyWarning;
                    grdRoot.IsHitTestVisible = false;
                    grdRoot.Opacity = 0.6;

                    MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
                    if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;
                    btnUnlucky.IsEnabled = true;
                }

                else
                {
                    btnUnlucky.IsEnabled = false;
                }
            }


        }

        private void btnSearchReset_Click(object sender, RoutedEventArgs e)
        {
            grdBingoNumbers.Children.Clear();
            currentColumn = 0;
            currentRow = 0;
            AddPreviousCell();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            grdRoot.IsHitTestVisible = true;
            grdRoot.Opacity = 1;
            if (MessagePopUp.IsOpen) MessagePopUp.IsOpen = false;
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                SearchBingoNumber();
            }
        }

        private void SearchBingoNumber()
        {
            List<BingoNumberInfo> lstBingo = DataHolder.LstBingoNumber;
            string searchText = txtBingoNumber.Text.Trim();
            if (Utilities.NumericValidation(searchText))
            {
                BingoNumberInfo num = lstBingo.FirstOrDefault(x => x.NumberString == searchText);
                if (num == null)
                {
                    txtMessage.Text = StaticMessage.NotFound;
                    grdRoot.IsHitTestVisible = false;
                    grdRoot.Opacity = 0.6;
                    MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
                    if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;
                }
                else
                {
                    int index = lstBingo.IndexOf(num);
                    num.State = 2;

                    var children = grdBingoNumbers.Children[index];

                    BingoNumberInfo number = (children as BingoNumber).DataContext as BingoNumberInfo;
                    number.BackColor = "#ff512f";

                    //(children as BingoNumber).DataContext = number;

                    //var children1 = grdBingoNumbers.Children[index];

                    //NumberInfo number1 = (children as BingoNumber).DataContext as NumberInfo;

                    grdBingoNumbers.UpdateLayout();
                }
            }
            else
            {
                txtMessage.Text = StaticMessage.ValidNumber;
                grdRoot.IsHitTestVisible = false;
                grdRoot.Opacity = 0.6;
                MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
                if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;
                //txtBingoNumber.Focus(FocusState.Unfocused);
            }

            txtBingoNumber.Text = string.Empty;

        }

        #region Top Bar Action




        private void btnFirstRow_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {

                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.FirstRowWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.IsReadOnly = true;
                    txtWinner.Text = DataHolder.FirstRowWinner;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 1;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }

        }

        private void btnSecondRow_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {
                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.SecondRowWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.Text = DataHolder.SecondRowWinner;
                    txtWinner.IsReadOnly = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 2;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }
        }

        private void btnThirdRow_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {
                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.ThirdRowWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.Text = DataHolder.ThirdRowWinner;
                    txtWinner.IsReadOnly = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 3;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }
        }

        private void btnFourCorner_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {
                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.FourCornerWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.Text = DataHolder.FourCornerWinner;
                    txtWinner.IsReadOnly = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 4;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }
        }

        private void btnFullHouse_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {
                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.FullHouseWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.Text = DataHolder.FullHouseWinner;
                    txtWinner.IsReadOnly = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 5;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }
        }

        private void btnUnlucky_Click(object sender, RoutedEventArgs e)
        {
            if (DataHolder.LstBingoNumber.Count > 0)
            {
                txtWinner.Text = string.Empty;
                LuckyWinnerPopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMainLuckyWinner.Height) / 2;
                if (!LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = true;

                if (DataHolder.UnluckyWinner.Length > 0)
                {
                    //view mode
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerHeader;
                    txtWinner.Text = DataHolder.UnluckyWinner;
                    txtWinner.IsReadOnly = true;
                    btnSave.IsEnabled = false;
                }
                else
                {
                    btnSave.Tag = 6;
                    btnSave.IsEnabled = true;
                    txtWinner.IsReadOnly = false;
                    txtLuckyWinnerHeader.Text = StaticMessage.LuckyWinnerInputHeader;
                    //save mode
                }
            }
        }

        private void btnSaveWinner_Click(object sender, RoutedEventArgs e)
        {

            string winnerName = txtWinner.Text.Trim();
            if (winnerName == "")
            {
                txtSaveMessage.Text = StaticMessage.RequiredString;
            }
            else
            {
                string saveTag = btnSave.Tag.ToString();
                switch (saveTag)
                {
                    case "1":
                        btnFirstRow.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        DataHolder.FirstRowWinner = winnerName;
                        AppendWinner("First Row", winnerName);
                        break;
                    case "2":
                        btnSecondRow.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        DataHolder.SecondRowWinner = winnerName;
                        AppendWinner("Second Row", winnerName);
                        break;
                    case "3":

                        btnThirdRow.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        DataHolder.ThirdRowWinner = winnerName;
                        AppendWinner("Third Row", winnerName);
                        break;
                    case "4":
                        btnFourCorner.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        AppendWinner("Four Corners", winnerName);
                        DataHolder.FourCornerWinner = winnerName;
                        break;
                    case "5":
                        btnFullHouse.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        AppendWinner("Full House", winnerName);
                        DataHolder.FullHouseWinner = winnerName;

                        FinishBingo();

                        break;
                    case "6":
                        btnUnlucky.Style = App.Current.Resources["AppBarButtonStyleDeactivated"] as Style;
                        AppendWinner("Unlucky", winnerName);
                        DataHolder.UnluckyWinner = winnerName;
                        break;
                    default:
                        break;


                }
                if (LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = false;

            }




        }

        private void FinishBingo()
        {
            txtMessage.Text = StaticMessage.BingoCompleted;
            grdRoot.IsHitTestVisible = false;
            btnDraw.IsEnabled = false;
            grdRoot.Opacity = 0.6;
            MessagePopUp.VerticalOffset = (DataHolder.DeviceHeight - brdrMain.Height) / 2;
            if (!MessagePopUp.IsOpen) MessagePopUp.IsOpen = true;
        }

        private void AppendWinner(string type, string winner)
        {

            Border brdr = new Border()
            {
                VerticalAlignment = VerticalAlignment.Top,
                BorderThickness = new Thickness(0, 0, 0, 1),
                HorizontalAlignment = HorizontalAlignment.Left,
                BorderBrush = Utilities.GetSolidColorBrush("FFFFFF")
            };

            TextBlock txtType = new TextBlock()
            {
                TextAlignment = TextAlignment.Left,
                Margin = new Thickness(5, 5, 0, 0),
                Foreground = Utilities.GetSolidColorBrush("ff512f")

            };
            txtType.FontWeight = FontWeights.SemiBold;
            txtType.FontSize = 22;
            TextBlock txtWinner = new TextBlock() { TextAlignment = TextAlignment.Left, Margin = new Thickness(5, 2, 0, 0), TextWrapping = TextWrapping.Wrap };
            txtWinner.FontSize = 20;

            txtType.Text = string.Format("{0}: ", type);
            txtWinner.Text = string.Format("{0} ", winner);

            brdr.Child = txtType;
            grdWinner.Children.Add(brdr);
            grdWinner.Children.Add(txtWinner);

        }


        private void btnCancelWinner_Click(object sender, RoutedEventArgs e)
        {
            if (LuckyWinnerPopUp.IsOpen) LuckyWinnerPopUp.IsOpen = false;
        }


        #endregion


    }
}

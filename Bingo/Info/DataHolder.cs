using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace Bingo
{
    public class DataHolder
    {
        public static string FirstRowWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FirstRowWinner"))
                    return store.Restore<string>("FirstRowWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FirstRowWinner", value);
            }
        }

        public static string SecondRowWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("SecondRowWinner"))
                    return store.Restore<string>("SecondRowWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("SecondRowWinner", value);
            }
        }

        public static string ThirdRowWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("ThirdRowWinner"))
                    return store.Restore<string>("ThirdRowWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("ThirdRowWinner", value);
            }
        }

        public static string FourCornerWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FourCornerWinner"))
                    return store.Restore<string>("FourCornerWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FourCornerWinner", value);
            }
        }

        public static string FullHouseWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FullHouseWinner"))
                    return store.Restore<string>("FullHouseWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FullHouseWinner", value);
            }
        }

        public static string UnluckyWinner
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("UnluckyWinner"))
                    return store.Restore<string>("UnluckyWinner");
                else
                    return string.Empty;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("UnluckyWinner", value);
            }
        }

        public static bool IsAppInstalled
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("IsAppInstalled"))
                    return store.Restore<bool>("IsAppInstalled");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("IsAppInstalled", value);
            }
        }

        public static List<NumberInfo> LstOriginalNumber
        {
            get
            {
                return Utilities.GetNumbers(0);
            }
            set
            {
                //DataStorage store = new DataStorage();
                //store.Save("LstOriginalNumber", value);
            }
        }

        public static List<BingoNumberInfo> LstBingoNumber
        {
            get
            {
                return Utilities.GetBingoNumbers();
            }
            set
            {
                //DataStorage store = new DataStorage();
                //store.Save("LstBingoNumber", value);
            }
        }

        public static double DeviceWidth = Window.Current.Bounds.Width;
        public static double DeviceHeight = Window.Current.Bounds.Height;
        public static double ColumnWidth = 80;
        public static int TotalNumbers = 90;
        public static int UnluckyNumber = 25;
        public static bool FirstRow
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FirstRow"))
                    return store.Restore<bool>("FirstRow");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FirstRow", value);
            }
        }

        public static bool SecondRow
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("SecondRow"))
                    return store.Restore<bool>("SecondRow");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("SecondRow", value);
            }
        }

        public static bool ThirdRow
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("ThirdRow"))
                    return store.Restore<bool>("ThirdRow");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("ThirdRow", value);
            }
        }

        public static bool FourCorners
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FourCorners"))
                    return store.Restore<bool>("FourCorners");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FourCorners", value);
            }
        }

        public static bool FullHouse
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("FullHouse"))
                    return store.Restore<bool>("FullHouse");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("FullHouse", value);
            }
        }

        public static bool Unlucky
        {
            get
            {
                var store = new DataStorage();
                if (store.ContainsKey("Unlucky"))
                    return store.Restore<bool>("Unlucky");
                else
                    return false;
            }
            set
            {
                DataStorage store = new DataStorage();
                store.Save("Unlucky", value);
            }
        }




    }
}

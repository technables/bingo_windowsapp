using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Bingo
{
    public class Utilities
    {

        private static string _NumericConstraints;
        public static string NumberConstraints
        {
            get
            {
                _NumericConstraints = @"^[0-9]+$";
                return _NumericConstraints;
            }
        }
        public static bool NumericValidation(string number)
        {
            bool validation = Regex.IsMatch(number, NumberConstraints);
            return validation;
        }

        public static void ResetWinners()
        {
            
            DataHolder.FirstRowWinner = string.Empty;
            DataHolder.SecondRowWinner = string.Empty;
            DataHolder.ThirdRowWinner = string.Empty;
            DataHolder.FourCornerWinner = string.Empty;
            DataHolder.FullHouseWinner = string.Empty;
            DataHolder.UnluckyWinner = string.Empty;
        }

        public static void SaveNumber(NumberInfo info)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                string change = string.Empty;
                try
                {



                    int success = db.Insert(info);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void SaveBingoNumber(BingoNumberInfo info)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                string change = string.Empty;
                try
                {

                    int success = db.Insert(info);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void SaveAllNumber(List<NumberInfo> lst)
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                string change = string.Empty;
                try
                {



                    int success = db.InsertAll(lst);

                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void DeleteAllNumber()
        {
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                db.DeleteAll<NumberInfo>();
                db.DeleteAll<BingoNumberInfo>();
            }
        }

        public static void DeleteNumber(string numberString)
        {

            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var numbers = db.Table<NumberInfo>().Where(n => n.NumberString == numberString);

                foreach (NumberInfo num in numbers)
                {
                    num.State = 1;
                    db.Update(num);
                }


            }
        }

        public static List<NumberInfo> GetNumbers(int state)
        {
            List<NumberInfo> lst = new List<NumberInfo>();
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var numbers = db.Table<NumberInfo>().Where(n => n.State == state);

                foreach (NumberInfo num in numbers)
                {
                    lst.Add(num);
                }
            }

            return lst;
        }

        public static List<BingoNumberInfo> GetBingoNumbers()
        {
            List<BingoNumberInfo> lst = new List<BingoNumberInfo>();
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var numbers = db.Table<BingoNumberInfo>();

                foreach (BingoNumberInfo num in numbers)
                {
                    lst.Add(num);
                }
            }

            return lst;
        }

        public static SolidColorBrush GetSolidColorBrush(string colorCode)
        {
            char[] bytes = colorCode.ToCharArray();
            string r = string.Empty;
            string g = string.Empty;
            string b = string.Empty;
            r = colorCode.Substring(0, 2);
            g = colorCode.Substring(2, 2);
            b = colorCode.Substring(4, 2);
            return new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(r, 16), Convert.ToByte(g, 16), Convert.ToByte(b, 16)));
        }
    }
}

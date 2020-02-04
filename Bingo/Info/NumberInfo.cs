using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bingo
{
    public class NumberInfo : INotifyPropertyChanged
    {
       
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string NumberString { get; set; }

        /// <summary>
        /// 0 for normal
        /// 1 for selected
        /// 2 for searched
        /// </summary>
        private int _State;
        public int State
        {
            get
            {
                switch (_State)
                {
                    case 1:
                        BackColor = "#FFF";
                        break;
                    case 2:
                        BackColor = "#b3eaff";
                        break;
                    case 3:
                        BackColor = "#ff512f";
                        break;
                    default:
                        BackColor = "#FFF";
                        break;
                }
                return _State;
            }

            set
            {
                _State = value;
                NotifyPropertyChanged("State");
            }
        }

        private string _BackColor;

        public string BackColor
        {
            get { return _BackColor ?? "#FFF"; }
            set { _BackColor = value; NotifyPropertyChanged("BackColor"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class BingoNumberInfo : INotifyPropertyChanged
    {
        public BingoNumberInfo() { }
        public BingoNumberInfo(NumberInfo info)
        {
            NumberString = info.NumberString;
            State = info.State;
            BackColor = info.BackColor;
        }

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string NumberString { get; set; }

        /// <summary>
        /// 0 for normal
        /// 1 for selected
        /// 2 for searched
        /// </summary>
        private int _State;
        public int State
        {
            get
            {
                switch (_State)
                {
                    case 1:
                        BackColor = "#FFF";
                        break;
                    case 2:
                        BackColor = "#b3eaff";
                        break;
                    case 3:
                        BackColor = "#ff512f";
                        break;
                    default:
                        BackColor = "#FFF";
                        break;
                }
                return _State;
            }

            set
            {
                _State = value;
                NotifyPropertyChanged("State");
            }
        }

        private string _BackColor;

        public string BackColor
        {
            get { return _BackColor ?? "#FFF"; }
            set { _BackColor = value; NotifyPropertyChanged("BackColor"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

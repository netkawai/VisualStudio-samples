using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp3
{
    public class INPCBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Model : INPCBase
    {
        public Model(int n)
        {
            Data = DataList.Create(n);
        }

        DataList _data;
        public DataList Data
        {
            get { return _data; }
            private set { _data = value; OnPropertyChanged(nameof(Data)); }
        }
    }

    public class DataItem : INPCBase
    {
        string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public class DataList : ObservableCollection<DataItem>
    {
        public static DataList Create(int n)
        {
            DataList list = new DataList();
            for (int i=0; i<n; ++i)
            {
                list.Add(new DataItem { Title = "Item " + i });
            }
            return list;
        }
    }
}

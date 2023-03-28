using System;
using System.Collections.ObjectModel;

namespace Globomantics.TeRex
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadWorkWeeks();
            SetWorkWeek();
        }

        private string windowIconKey = "Icon_TeRex";
        public string WindowIconKey
        {
            get => windowIconKey;
            set
            {
                windowIconKey = value;
                OnPropertyChanged(nameof(WindowIconKey));
            }
        }

        private ObservableCollection<DateTime> workWeeks = new ObservableCollection<DateTime>();
        public ObservableCollection<DateTime> WorkWeeks
        {
            get => workWeeks;
            set
            {
                workWeeks = value;
                OnPropertyChanged(nameof(WorkWeeks));
            }
        }

        private DateTime selectedWorkWeek;
        public DateTime SelectedWorkWeek
        {
            get => selectedWorkWeek;
            set
            {
                if (selectedWorkWeek != value)
                {
                    selectedWorkWeek = value;
                    OnPropertyChanged(nameof(SelectedWorkWeek));
                }
            }
        }

        private void LoadWorkWeeks()
        {
            WorkWeeks = new ObservableCollection<DateTime>
            {
                new DateTime(2019, 1, 4),
                new DateTime(2019, 1, 11),
                new DateTime(2019, 1, 18),
                new DateTime(2019, 1, 25),
                new DateTime(2019, 2, 1),
                new DateTime(2019, 2, 8),
                new DateTime(2019, 2, 15),
                new DateTime(2019, 2, 22),
                new DateTime(2019, 3, 1),
                new DateTime(2019, 3, 8),
                new DateTime(2019, 3, 15),
                new DateTime(2019, 3, 22),
                new DateTime(2019, 3, 29),
                new DateTime(2019, 4, 5),
                new DateTime(2019, 4, 12),
                new DateTime(2019, 4, 19),
                new DateTime(2019, 4, 26),
                new DateTime(2019, 5, 3),
                new DateTime(2019, 5, 10),
                new DateTime(2019, 5, 17),
                new DateTime(2019, 5, 24),
                new DateTime(2019, 5, 31),
                new DateTime(2019, 6, 7),
                new DateTime(2019, 6, 14),
                new DateTime(2019, 6, 21),
                new DateTime(2019, 6, 28),
                new DateTime(2019, 7, 5),
                new DateTime(2019, 7, 12),
                new DateTime(2019, 7, 19),
                new DateTime(2019, 7, 26),
                new DateTime(2019, 8, 2),
                new DateTime(2019, 8, 9),
                new DateTime(2019, 8, 16),
                new DateTime(2019, 8, 23),
                new DateTime(2019, 8, 30),
                new DateTime(2019, 9, 6),
                new DateTime(2019, 9, 13),
                new DateTime(2019, 9, 20),
                new DateTime(2019, 9, 27),
                new DateTime(2019, 10, 4),
                new DateTime(2019, 10, 11),
                new DateTime(2019, 10, 18),
                new DateTime(2019, 10, 25),
                new DateTime(2019, 11, 1),
                new DateTime(2019, 11, 8),
                new DateTime(2019, 11, 15),
                new DateTime(2019, 11, 22),
                new DateTime(2019, 11, 29),
                new DateTime(2019, 12, 6),
                new DateTime(2019, 12, 13),
                new DateTime(2019, 12, 20),
                new DateTime(2019, 12, 27),
            };
        }

        private void SetWorkWeek()
        {
            var today = DateTime.Today;
            var daysUntil = ((int)DayOfWeek.Friday - (int)today.DayOfWeek + 7) % 7;
            SelectedWorkWeek = today.AddDays(daysUntil);
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Globomantics.TeRex.Models;
using System.Windows.Input;
using TeRex;

namespace Globomantics.TeRex.Views
{
    public class TeRexViewModel : ViewModelBase
    {
        public ICommand AddTimeCardItemCommand { get; }
        public ICommand DeleteTimeCardItemCommand { get; }
        public ICommand AddExpenseItemCommand { get; }
        public ICommand DeleteExpenseItemCommand { get; }

        public TeRexViewModel()
        {
            AddTimeCardItemCommand = new RelayCommand(HandleAddTimeCardItemCommand);
            DeleteTimeCardItemCommand = new RelayCommand<TimeCardItemViewModel>(HandleDeleteTimeCardItemCommand);

            AddExpenseItemCommand = new RelayCommand(HandleAddExpenseItemCommand);
            DeleteExpenseItemCommand = new RelayCommand<ExpenseItemViewModel>(HandleDeleteExpenseItemCommand);

            LoadProjects();
            TimeCardItems.CollectionChanged += OnTimeCardItemsCollectionChanged;
            AddTimeCardItem();

            LoadExpenseTypes();
            ExpenseItems.CollectionChanged += OnExpenseItemsCollectionChanged;
            AddExpenseItem();
        }

        private void OnTimeCardItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (TimeCardItemViewModel item in e.OldItems)
                    item.PropertyChanged -= OnTimeCardItemChanged;
            }
            if (e.NewItems != null)
            {
                foreach (TimeCardItemViewModel item in e.NewItems)
                    item.PropertyChanged += OnTimeCardItemChanged;
            }

            UpdateTimeCardTotals();
        }

        private void OnTimeCardItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTimeCardTotals();
        }

        private void OnExpenseItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (ExpenseItemViewModel item in e.OldItems)
                    item.PropertyChanged -= OnExpenseItemChanged;
            }
            if (e.NewItems != null)
            {
                foreach (ExpenseItemViewModel item in e.NewItems)
                    item.PropertyChanged += OnExpenseItemChanged;
            }

            UpdateExpenseTotals();
        }

        private void OnExpenseItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateExpenseTotals();
        }

        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        public ObservableCollection<Project> Projects
        {
            get => projects;
            set
            {
                projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        private string employeeId { get; set; }
        public string EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        private DateTime workWeek;
        public DateTime WorkWeek
        {
            get => workWeek;
            set
            {
                workWeek = value;
                OnPropertyChanged(nameof(WorkWeek));
            }
        }

        private ObservableCollection<TimeCardItemViewModel> timeCardItems = new ObservableCollection<TimeCardItemViewModel>();
        public ObservableCollection<TimeCardItemViewModel> TimeCardItems
        {
            get => timeCardItems;
            set
            {
                timeCardItems = value;
                OnPropertyChanged(nameof(TimeCardItems));
            }
        }

        private double day1Total;
        public double Day1Total
        {
            get => day1Total;
            set
            {
                day1Total = value;
                OnPropertyChanged(nameof(Day1Total));
            }
        }

        private double day2Total;
        public double Day2Total
        {
            get => day2Total;
            set
            {
                day2Total = value;
                OnPropertyChanged(nameof(Day2Total));
            }
        }

        private double day3Total;
        public double Day3Total
        {
            get => day3Total;
            set
            {
                day3Total = value;
                OnPropertyChanged(nameof(Day3Total));
            }
        }

        private double day4Total;
        public double Day4Total
        {
            get => day4Total;
            set
            {
                day4Total = value;
                OnPropertyChanged(nameof(Day4Total));
            }
        }

        private double day5Total;
        public double Day5Total
        {
            get => day5Total;
            set
            {
                day5Total = value;
                OnPropertyChanged(nameof(Day5Total));
            }
        }

        private double day6Total;
        public double Day6Total
        {
            get => day6Total;
            set
            {
                day6Total = value;
                OnPropertyChanged(nameof(Day6Total));
            }
        }

        private double day7Total;
        public double Day7Total
        {
            get => day7Total;
            set
            {
                day7Total = value;
                OnPropertyChanged(nameof(Day7Total));
            }
        }

        private double grandTotal;
        public double GrandTotal
        {
            get => grandTotal;
            set
            {
                grandTotal = value;
                OnPropertyChanged(nameof(GrandTotal));
            }
        }

        private bool isSigned;
        public bool IsSigned
        {
            get => isSigned;
            set
            {
                isSigned = value;
                OnPropertyChanged(nameof(IsSigned));
                Signature = isSigned ? "Jessica Vantile" : null;
            }
        }

        private string signature;
        public string Signature
        {
            get => signature;
            set
            {
                signature = value;
                OnPropertyChanged(nameof(Signature));
            }
        }

        private ObservableCollection<ExpenseType> expenseTypes = new ObservableCollection<ExpenseType>();
        public ObservableCollection<ExpenseType> ExpenseTypes
        {
            get => expenseTypes;
            set
            {
                expenseTypes = value;
                OnPropertyChanged(nameof(ExpenseTypes));
            }
        }

        private ObservableCollection<ExpenseItemViewModel> expenseItems = new ObservableCollection<ExpenseItemViewModel>();
        public ObservableCollection<ExpenseItemViewModel> ExpenseItems
        {
            get => expenseItems;
            set
            {
                expenseItems = value;
                OnPropertyChanged(nameof(ExpenseItems));
            }
        }

        private double grandExpenseTotal;
        public double GrandExpenseTotal
        {
            get => grandExpenseTotal;
            set
            {
                grandExpenseTotal = value;
                OnPropertyChanged(nameof(GrandExpenseTotal));
            }
        }

        private void UpdateTimeCardTotals()
        {
            Day1Total = 0;
            Day2Total = 0;
            Day3Total = 0;
            Day4Total = 0;
            Day5Total = 0;
            Day6Total = 0;
            Day7Total = 0;

            foreach(TimeCardItemViewModel item in TimeCardItems)
            {
                Day1Total += item.Day1Hours;
                Day2Total += item.Day2Hours;
                Day3Total += item.Day3Hours;
                Day4Total += item.Day4Hours;
                Day5Total += item.Day5Hours;
                Day6Total += item.Day6Hours;
                Day7Total += item.Day7Hours;
            }

            GrandTotal = Day1Total + Day2Total + Day3Total + Day4Total + Day5Total + Day6Total + Day7Total;
        }

        private void UpdateExpenseTotals()
        {
            GrandExpenseTotal = 0;

            foreach (ExpenseItemViewModel item in ExpenseItems)
            {
                GrandExpenseTotal += item.Amount;
            }
        }

        private void LoadProjects()
        {
            Projects = new ObservableCollection<Project>
            {
                new Project { Id = null, DisplayName = "Select a Project" },
                new Project { Id = "GEDU076", DisplayName = "Educational Marketing" },
                new Project { Id = "GENT002", DisplayName = "Film Industry Marketing" },
                new Project { Id = "GMAR001", DisplayName = "Globomantics Marketing" },
                new Project { Id = "GMFG001", DisplayName = "Manufacturing Marketing" },
                new Project { Id = "GMED011", DisplayName = "Medical Equipment Marketing" },
                new Project { Id = "GENT003", DisplayName = "Music Industry Marketing" },
                new Project { Id = "GPTO001", DisplayName = "Personal Time Off" },
                new Project { Id = "GPHL035", DisplayName = "Pharmaceutical Marketing" },
                new Project { Id = "GENT001", DisplayName = "Television Industry Marketing" },
            };
        }

        private void LoadExpenseTypes()
        {
            ExpenseTypes = new ObservableCollection<ExpenseType>
            {
                new ExpenseType { Id = null, DisplayName = "Select a Type" },
                new ExpenseType { Id = "EXT001", DisplayName = "Air Travel" },
                new ExpenseType { Id = "EXT005", DisplayName = "Entertainment" },
                new ExpenseType { Id = "EXT003", DisplayName = "Food" },
                new ExpenseType { Id = "EXT004", DisplayName = "Hotel" },
                new ExpenseType { Id = "EXT002", DisplayName = "Miscellaneous" },
                new ExpenseType { Id = "EXT006", DisplayName = "Security" },
                new ExpenseType { Id = "EXT002", DisplayName = "Transportation" },
            };
        }

        private void AddTimeCardItem()
        {
            TimeCardItems.Add(new TimeCardItemViewModel { ProjectId = Projects[0].Id });
        }

        private void HandleAddTimeCardItemCommand()
        {
            AddTimeCardItem();
        }

        private void HandleDeleteTimeCardItemCommand(TimeCardItemViewModel item)
        {
            TimeCardItems.Remove(item);
        }

        private void AddExpenseItem()
        {
            ExpenseItems.Add(new ExpenseItemViewModel { ProjectId = Projects[0].Id, ExpenseTypeId = ExpenseTypes[0].Id });
        }

        private void HandleAddExpenseItemCommand()
        {
            AddExpenseItem();
        }

        private void HandleDeleteExpenseItemCommand(ExpenseItemViewModel item)
        {
            ExpenseItems.Remove(item);
        }
    }
}

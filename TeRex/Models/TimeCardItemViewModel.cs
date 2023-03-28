namespace Globomantics.TeRex.Models
{
    public class TimeCardItemViewModel : ViewModelBase
    {
        private string projectId;
        public string ProjectId
        {
            get => projectId;
            set
            {
                projectId = value;
                OnPropertyChanged(nameof(ProjectId));
            }
        }

        private double day1Hours;
        public double Day1Hours
        {
            get => day1Hours;
            set
            {
                day1Hours = value;
                OnPropertyChanged(nameof(Day1Hours));
                UpdateTotals();
            }
        }

        private double day2Hours;
        public double Day2Hours
        {
            get => day2Hours;
            set
            {
                day2Hours = value;
                OnPropertyChanged(nameof(Day2Hours));
                UpdateTotals();
            }
        }

        private double day3Hours;
        public double Day3Hours
        {
            get => day3Hours;
            set
            {
                day3Hours = value;
                OnPropertyChanged(nameof(Day3Hours));
                UpdateTotals();
            }
        }

        private double day4Hours;
        public double Day4Hours
        {
            get => day4Hours;
            set
            {
                day4Hours = value;
                OnPropertyChanged(nameof(Day4Hours));
                UpdateTotals();
            }
        }

        private double day5Hours;
        public double Day5Hours
        {
            get => day5Hours;
            set
            {
                day5Hours = value;
                OnPropertyChanged(nameof(Day5Hours));
                UpdateTotals();
            }
        }

        private double day6Hours;
        public double Day6Hours
        {
            get => day6Hours;
            set
            {
                day6Hours = value;
                OnPropertyChanged(nameof(Day6Hours));
                UpdateTotals();
            }
        }

        private double day7Hours;
        public double Day7Hours
        {
            get => day7Hours;
            set
            {
                day7Hours = value;
                OnPropertyChanged(nameof(Day7Hours));
                UpdateTotals();
            }
        }

        private double totalHours;
        public double TotalHours
        {
            get => totalHours;
            set
            {
                totalHours = value;
                OnPropertyChanged(nameof(TotalHours));
            }
        }

        private void UpdateTotals()
        {
            TotalHours = Day1Hours + Day2Hours + Day3Hours + Day4Hours + Day5Hours + Day6Hours + Day7Hours;
        }
    }
}

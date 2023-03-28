namespace Globomantics.TeRex.Models
{
    public class ExpenseItemViewModel : ViewModelBase
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

        private string expenseTypeId;
        public string ExpenseTypeId
        {
            get => expenseTypeId;
            set
            {
                expenseTypeId = value;
                OnPropertyChanged(nameof(ExpenseTypeId));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private double amount;
        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
    }
}

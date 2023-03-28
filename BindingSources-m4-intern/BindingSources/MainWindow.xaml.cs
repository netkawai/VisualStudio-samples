using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Zza.Data;

namespace BindingSources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DeleteCustomerCommand = new RelayCommand<Customer>(OnDeleteCustomer);
        
            using (ZzaDbContext context = new ZzaDbContext())
            {
                var sampleData = new List<Customer>();
                sampleData.Add(new Customer() { Id=new Guid("7462c7c8-e24c-484a-8f93-013f1c479615"), FirstName= "Derek", LastName= "Puckett", Phone= "(954) 594-9355", Email= "derek.puckett@vulputate.net", Zip= "55154", State= "OH", City= "Quam", Street= "P.O. Box 914, 9990 Dapibus St." });
                sampleData.Add(new Customer() { Id = new Guid("8f64b183-a881-42f5-9c1d-013f1c479616"), FirstName = "Bernard", LastName = "Russell", Phone = "(203) 652-0465", Email = "jordan.jimenez@variusorciin.co.uk", Zip = "88091", State = "OR", City = "Lorem", Street = "Ap #370-9242 Sed, Ave" });
                Customers = new ObservableCollection<Customer>(sampleData);
#if false
                Customers = new ObservableCollection<Customer>(context.Customers.Include("Orders").Include("Orders.OrderItems").Include("Orders.OrderItems.Product"));
#endif
            }
        }

        public RelayCommand<Customer> DeleteCustomerCommand
        {
            get { return (RelayCommand<Customer>)GetValue(DeleteCustomerCommandProperty); }
            set { SetValue(DeleteCustomerCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteCustomerCommandProperty =
            DependencyProperty.Register("DeleteCustomerCommand",
            typeof(RelayCommand<Customer>),
            typeof(MainWindow),
            new PropertyMetadata(null));



        public ObservableCollection<Customer> Customers
        {
            get { return (ObservableCollection<Customer>)GetValue(CustomersProperty); }
            set { SetValue(CustomersProperty, value); }
        }

        public static readonly DependencyProperty CustomersProperty =
            DependencyProperty.Register("Customers",
            typeof(ObservableCollection<Customer>),
            typeof(MainWindow),
            new PropertyMetadata(null));

        private void OnDeleteCustomer(Customer customer)
        {
            Customers.Remove(customer);
        }
        private bool CanDeleteCustomer(Customer customer)
        {
            return customerDataGrid.SelectedItem != null;
        }

    }
}

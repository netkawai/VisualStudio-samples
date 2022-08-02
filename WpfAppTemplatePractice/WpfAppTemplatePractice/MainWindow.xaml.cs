using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppTemplatePractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Students { get; }
        public ObservableCollection<Student> ClassroomStudents { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Students = new ObservableCollection<string>();

            Students.Add("John");
            Students.Add("Paul");
            Students.Add("Ringo");
            Students.Add("George");

            ClassroomStudents = new ObservableCollection<Student>();
            ClassroomStudents.Add(new Student { ID = 1, Name = "John" }); //View binds it ok at startup (one way)
            ClassroomStudents.Add(new Student { ID = 2, Name = "Paul" }); //View binds ut ok at startup (one way)
        }
        public class Student : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public int ID { get; set; }
            public string Name { get; set; }
        }

    }
}

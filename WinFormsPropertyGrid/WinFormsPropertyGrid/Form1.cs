namespace WinFormsPropertyGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Stuff that = new Stuff();

            that.MultiLineProperty = "person1\n person2 \n person3";
            this.propertyGrid1.SelectedObject = that;
        }
    }
}
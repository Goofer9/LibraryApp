using System.Windows;



namespace LibraryApp
{
    public partial class MatchDialog : Window
    {
        public string UserAnswer { get; private set; }

        public MatchDialog(System.Collections.Generic.List<string> options1, System.Collections.Generic.List<string> options2)
        {
            InitializeComponent();

            foreach (var option in options1)
            {
                listBox1.Items.Add(option);
            }

            foreach (var option in options2)
            {
                listBox2.Items.Add(option);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null)
            {
                UserAnswer = $"{listBox1.SelectedItem}. {listBox2.SelectedItem}";
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select both items before submitting.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssemblyScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StateLable.Content = "Searching for supported types...";

            //Scann assemblys
            await Task.Run((Scanner.ScanAssemblys));

            //Display all types names
            AddTypesToListView();

            StateLable.Content = "Select type:";

        }

        private void AddTypesToListView()
        {
            //Add types ordered by fullName 
            foreach (var typeName in Scanner.SupportedTypes.Keys.OrderBy((t) => t.FullName))
                TypesListView.Items.Add(typeName.FullName);
        }

        private void CreateObjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get ctor from dictionary
                Type objType = Scanner.SupportedTypes.First((x) => (x.Key.FullName == TypesListView.SelectedItem.ToString())).Key;

                //Create object 
                dynamic obj = objType.Create();

                MessageBox.Show("Instanse is created: \n" + obj.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type not found!\n" + ex.Message);
            }
        }
    }
}

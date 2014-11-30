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
            await Task.Run((Scanner.Initialize));
        }

        private void CreateObjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Type objType = Scanner.SupportedTypes.First((x) => (x.Key.ToString() == TypeNameTextBox.Text)).Key;
                dynamic obj = objType.Create();

                MessageBox.Show("Type created: \n" + obj.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type not found!\n" + ex.Message);
            }
        }
    }
}

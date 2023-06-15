using ConferenceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ConferenceSystem.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowUsersPage.xaml
    /// </summary>
    public partial class ShowUsersPage : Page
    {
        private bool _onlyJuri;
        public ShowUsersPage(bool onlyJuri)
        {
            InitializeComponent();
            _onlyJuri = onlyJuri;
            UpdateItems();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new ModOrJuryAddEdit((sender as Button).DataContext as Users));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateItems();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.DatabaseContext.Users.Remove((sender as Button).DataContext as Users);
            Manager.Instance.DatabaseContext.SaveChanges();
            UpdateItems();
        }
        public void UpdateItems()
        {
            var allusers = Manager.Instance.DatabaseContext.Users.ToList();
            if (_onlyJuri)
                allusers = allusers.Where(u => u.RoleId == 3).ToList();
            UsersLB.ItemsSource = allusers;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new ModOrJuryAddEdit());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.GoBack();
        }
    }
}

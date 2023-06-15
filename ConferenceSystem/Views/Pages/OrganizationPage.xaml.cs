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
    /// Логика взаимодействия для OrganizationPage.xaml
    /// </summary>
    public partial class OrganizationPage : Page
    {
        public OrganizationPage()
        {
            InitializeComponent();
            GreetingsTextBlock.Text = Manager.Instance.GetGreeting(Manager.Instance.LoggedUser, DateTime.Now);
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Manager.Instance.MainWindow.PageNameTextBlock.Text = "Окно организатора";
        }

        private void EventsBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new EventsPage());
        }

        private void ParticipantsBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new ShowUsersPage(false));
        }

        private void JuriBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new ShowUsersPage(true));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.Navigate(new ModOrJuryAddEdit(Manager.Instance.LoggedUser));
        }
    }
}

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
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();
            EventsIC.ItemsSource = Manager.Instance.DatabaseContext.Events.ToList();
            BackButt.Visibility = Manager.Instance.MainWindow.MainFrame.CanGoBack? Visibility.Visible: Visibility.Hidden;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Manager.Instance.MainWindow.PageNameTextBlock.Text = "Окно мероприятий";
        }

        private void BackButt_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.GoBack();
        }
    }
}

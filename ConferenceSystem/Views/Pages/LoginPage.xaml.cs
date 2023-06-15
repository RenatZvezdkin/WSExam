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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Manager.Instance.MainWindow.PageNameTextBlock.Text = "Окно авторизации";
            if (e.NewValue is bool && (bool)e.NewValue==true || Manager.Instance.LoggedUser!=null)
                Manager.Instance.MainWindow.LoginButton.Visibility = Visibility.Hidden;
            else
                Manager.Instance.MainWindow.LoginButton.Visibility = Visibility.Visible;
        }
        public string GetPassword()
        {
            if (PasswordPb.Visibility == Visibility.Visible)
                return PasswordPb.Password;
            else
                return PasswordTb.Text;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(IdTb.Text, out int id))
            {
                if (Manager.Instance.AttemptLogin(id, GetPassword()))
                {
                    if (Manager.Instance.LoggedUser.RoleId == 1)
                        Manager.Instance.MainWindow.MainFrame.Navigate(new OrganizationPage());
                    else
                        Manager.Instance.MainWindow.MainFrame.Navigate(new EventsPage());

                }
                else
                    MessageBox.Show("Некорректные айди или пароль");
            }
            else
                MessageBox.Show("Вы ввели некорректный айди");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTb.Visibility = Visibility.Visible;
            PasswordPb.Visibility = Visibility.Hidden;
            PasswordTb.Text = PasswordPb.Password;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordTb.Visibility = Visibility.Hidden;
            PasswordPb.Visibility = Visibility.Visible;
            PasswordPb.Password = PasswordTb.Text;
        }
    }
}

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
    /// Логика взаимодействия для ModOrJuryAddEdit.xaml
    /// </summary>
    public partial class ModOrJuryAddEdit : Page
    {
        private Users _redactedUser;
        public ModOrJuryAddEdit(Users redactedUser = null)
        {
            InitializeComponent();
            var zeroEv = new Events { Name = "Без соревнования" };
            var zeroSpec = new Specialities { Name = "Без специальности" };
            EventsCB.ItemsSource = WithZero(Manager.Instance.DatabaseContext.Events.ToList(),zeroEv);
            GendersCB.ItemsSource = Manager.Instance.DatabaseContext.Genders.ToList();
            RolesCB.ItemsSource = Manager.Instance.DatabaseContext.Roles.ToList();
            SpecialitiesCB.ItemsSource = WithZero(Manager.Instance.DatabaseContext.Specialities.ToList(), zeroSpec);
            if (redactedUser != null)
            {
                _redactedUser = redactedUser;
                IdTb.Text = _redactedUser.Id.ToString();
                if (redactedUser.Events == null)
                    EventsCB.SelectedItem = zeroEv;
                if (redactedUser.Specialities == null)
                    SpecialitiesCB.SelectedItem = zeroSpec;
            }
            DataContext = _redactedUser;
        }
        public List<T> WithZero<T>(List<T> mainCollection, T firstElement)
        {
            var list = new List<T>();
            list.Add(firstElement);
            foreach (var i in mainCollection)
                list.Add(i);
            return list;
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Manager.Instance.MainWindow.PageNameTextBlock.Text = "Регистрация жюри/модератора";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (GetPassword() != PasswordRepeatPb.Password)
            {
                MessageBox.Show("Пароли должны совпадать");
                return;
            }
            else if (GetPassword().Length<6)
            {
                MessageBox.Show("Пароль слишком короткий");
                return;
            }
            else if (!(
                GetPassword().Any(c => char.IsDigit(c)) && GetPassword().Any(c => char.IsLetter(c))) && GetPassword().Any(c => char.IsSymbol(c) 
                && GetPassword().ToLower() != GetPassword() && GetPassword().ToUpper() != GetPassword()) )
            {
                MessageBox.Show("Пароль должен содержать буквы разных регистров, цифры, и знаки");
            }
            try
            {
                _redactedUser.Password = GetPassword();
                if (EventsCB.SelectedIndex == 0)
                    _redactedUser.Events = null;
                if (SpecialitiesCB.SelectedIndex == 0)
                    _redactedUser.Specialities = null;
                if (_redactedUser.Id == 0)
                {
                    IdTb.Text = _redactedUser.Id.ToString();
                    Manager.Instance.DatabaseContext.Users.Add(_redactedUser);
                }
                Manager.Instance.DatabaseContext.SaveChanges();
                Manager.Instance.MainWindow.MainFrame.GoBack();
            }
            catch
            {
                MessageBox.Show("Во время сохранения данных произошла ошибка, проверьте правильность заполнения данных");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.MainWindow.MainFrame.GoBack();
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
        public string GetPassword()
        {
            if (PasswordPb.Visibility == Visibility.Visible)
                return PasswordPb.Password;
            else
                return PasswordTb.Text;
        }
    }
}

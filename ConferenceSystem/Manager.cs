using ConferenceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceSystem
{
    public class Manager
    {
        private Manager()
        {
            DatabaseContext = new Database1Entities();
            LoggedUser = null;
            //LoggedUser = DatabaseContext.Users.Find(21); org
        }
        public bool AttemptLogin(int id, string password)
        {
            LoggedUser = DatabaseContext.Users.FirstOrDefault(u => u.Id == id && u.Password==password);
            return LoggedUser != null;
        }
        public Users LoggedUser { get; private set; }
        public Database1Entities DatabaseContext { get; }
        public MainWindow MainWindow { get; private set; }
        public void SetMainWindow(MainWindow window)
        {
            MainWindow = window;
        }
        private static Manager _instance;
        public static Manager Instance { get => _instance == null ? _instance = new Manager() : _instance; }
        public string GetGreeting(Users user, DateTime dateTime)
        {
            if (user == null || dateTime.Hour<9)
                return string.Empty;
            string pronoun = user.GenderId == 1 ? "мистер": "миссис";
            string res;
            if (dateTime.Hour < 11)
                res = "Доброе утро";
            else if (dateTime.Hour < 18)
                res = "Добрый день";
            else
                res = "Добрый вечер";
            return res += ", " + pronoun + " " + user.Patronymic + " " + user.Name;
        }
    }
}

using System.Collections.Generic;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using DoyleyRSVP2.View;
using Xamarin.Forms;

namespace DoyleyRSVP2
{
    public partial class App : Application
    {
        public static UserModel CurrentUser { get; set; }
        public static UserRepository UserRepo { get; private set; }
        public static EventRepository EventRepo { get; private set; }

        public App(string dbPath)
        {
            InitializeComponent();
            UserRepo = new UserRepository(dbPath);
            EventRepo = new EventRepository(dbPath);
            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace DoyleyRSVP2.View
{
    public partial class HomeView : Xamarin.Forms.TabbedPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Clear the current user and navigate to the login page
            App.CurrentUser = null;
            App.Current.MainPage = new NavigationPage(new LoginView());
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DoyleyRSVP2.Droid
{
	[Activity (Label = "SplashActivity", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize)]
	
	public class SplashActivity : Activity
	{
		protected override async void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Drawable.SplashScreen);

            // Delay for a few seconds on the splash screen
            await Task.Delay(3000);  // Delay for 3 seconds

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}


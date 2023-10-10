using System;
using System.IO;
using System.Linq;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using DoyleyRSVP2.Views;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{	
	public partial class LoginView : ContentPage
	{	
		public LoginView ()
		{
			InitializeComponent();
		}

		private async void Login_Clicked(object sender, EventArgs e)
		{
            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayAlert("Error", "All fields must be filled in", "OK");
                return;
            }

            // Create an instance of UserRepository
            string dbPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                "User.db3");
            UserRepository repository = new UserRepository(dbPath);

            // Use the instance to call GetUserByEmail
            UserModel matchingUser = repository.GetUserByEmail(txtEmail.Text);

            if (matchingUser != null && matchingUser.Password == txtPassword.Text)
            {
                // Initialize and set CurrentUser
                App.CurrentUser = new UserModel
                {
                    ID = matchingUser.ID,
                    Name = matchingUser.Name,
                    Email = matchingUser.Email,
                    Password = matchingUser.Password,
                    MobileNumber = matchingUser.MobileNumber
                };

                await Navigation.PushAsync(new HomeView());
            }
            else
            {
                lblErrorMessage.Text = "Your email address or password is incorrect!";
            }
        }

		private async void SignUp_Clicked(object sender, EventArgs e)
		{
            await Navigation.PushAsync(new SignUpView());
        }

        private async void ForgotPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordView());
        }

    }
}


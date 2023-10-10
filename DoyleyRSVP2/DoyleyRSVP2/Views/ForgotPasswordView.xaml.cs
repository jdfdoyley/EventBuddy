using System;
using System.Collections.Generic;
using System.IO;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using Xamarin.Forms;
namespace DoyleyRSVP2.Views
{	
	public partial class ForgotPasswordView : ContentPage
	{	
		public ForgotPasswordView ()
		{
			InitializeComponent ();
		}

        private void Send_Clicked(object sender, EventArgs e)
        {
            // Create an instance of UserRepository
            string dbPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                "User.db3");
            UserRepository repository = new UserRepository(dbPath);

            // Check if email exists in the database
            UserModel matchingUser = repository.GetUserByEmail(txtEmail.Text);

            if (matchingUser != null)
            {
                // If the email exists, display a message saying that a reset link has been sent.
                lblMessage.Text = "A reset link has been sent to your email address.";
                lblMessage.TextColor = Color.Green;
                // Note: Actual email sending will be implemented later.
            }
            else
            {
                // If the email doesn't exist, display a message to the user.
                lblMessage.Text = "No account exists for the provided email address.";
                lblMessage.TextColor = Color.Red;
            }
        }
    }
}


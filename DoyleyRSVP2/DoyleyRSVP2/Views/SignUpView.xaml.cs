using System;
using System.Collections.Generic;
using System.IO;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{	
	public partial class SignUpView : ContentPage
	{
        private UserRepository userRepo;

		public SignUpView ()
		{
			InitializeComponent ();

            // Initialize database
            string dbPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                "User.db3");
            userRepo = new UserRepository(dbPath);
		}

        // Method to handle SignUp button click
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Check if all fields are filled in
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text) ||
                string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                await DisplayAlert("Error", "All fields must be filled in", "OK");
                return;
            }

            // Validate password confirmation
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            // Check if user is already registered
            if (userRepo.IsUserRegistered(txtEmail.Text))
            {
                await DisplayAlert("Info", "User is already registered. Please login.", "OK");
                return;
            }

            // Create new user
            UserModel newUser = new UserModel
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                MobileNumber = txtMobileNumber.Text
            };

            // Add the user to the SQLite database
            userRepo.AddUser(newUser);

            // Clear the form
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;

            // Show success message
            await DisplayAlert("Success", "User created successfully!", "OK");
            await Navigation.PopAsync();  // Redirect to the Sign In page
        }

        // Method to navigate to Sign In page
        private async void OnSigninClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();  // Redirect to the Sign In page
        }
    }
}


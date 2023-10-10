using System;
using System.Collections.Generic;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{
    public partial class AddEventView : ContentPage
    {
        private UserModel _currentUser;

        public AddEventView(UserModel currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate fields here
            if (string.IsNullOrWhiteSpace(HostNameEntry.Text) ||
                string.IsNullOrWhiteSpace(EventNameEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(MaxAttendeesEntry.Text))
            {
                await DisplayAlert("Validation Error", "All fields must be filled out", "OK");
                return;
            }

            // Create a new event and set the host as the current user
            EventModel newEvent = new EventModel
            {
                HostID = _currentUser.ID,
                HostName = HostNameEntry.Text,
                EventName = EventNameEntry.Text,
                EventAddress = AddressEntry.Text,
                MaxAttendees = int.Parse(MaxAttendeesEntry.Text),
                EventDateTime = EventDatePicker.Date + EventTimePicker.Time,
                RsvpDeadline = RsvpDatePicker.Date,
            };

            // Save the new event to the database
            App.EventRepo.AddEvent(newEvent);

            // Also, add an entry to UserEventModel to signify that the user is the host of this event
            UserEventModel userEvent = new UserEventModel
            {
                UserId = _currentUser.ID,
                EventId = newEvent.ID,
                IsReserved = true,
                IsHost = true
            };
                        
            App.EventRepo.RsvpEvent(_currentUser.ID, newEvent.ID); 

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

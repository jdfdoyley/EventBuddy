using System;
using System.Collections.Generic;
using DoyleyRSVP2.Models;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{
    public partial class EventDetailsView : ContentPage
    {
        private EventModel _selectedEvent;

        public EventDetailsView(EventModel selectedEvent)
        {
            InitializeComponent();
            _selectedEvent = selectedEvent;
            BindingContext = _selectedEvent;

            // Initialize button state
            var userId = App.CurrentUser.ID;
            var button = this.FindByName<Button>("btnRsvp");

            if (_selectedEvent.RsvpUserIds.Contains(userId))
            {
                button.Text = "Decline RSVP";
                button.BackgroundColor = Color.Red;
            }
            else
            {
                button.Text = "RSVP";
                button.BackgroundColor = Color.Green;
            }
        }

        private void OnRsvpButtonClicked(object sender, EventArgs e)
        {
            var userId = App.CurrentUser.ID;
            var eventId = _selectedEvent.ID;
            var button = sender as Button;

            // Re-fetch the event to ensure we have the latest data
            _selectedEvent = App.EventRepo.GetEventById(eventId);

            if (!_selectedEvent.RsvpUserIds.Contains(userId))
            {
                App.EventRepo.RsvpEvent(userId, eventId);
                button.Text = "Decline RSVP";
                button.BackgroundColor = Color.Red;
            }
            else
            {
                App.EventRepo.DeclineRsvp(userId, eventId);
                button.Text = "RSVP";
                button.BackgroundColor = Color.Green;
            }

            // Reload the data after making changes
            _selectedEvent = App.EventRepo.GetEventById(eventId);
            BindingContext = _selectedEvent;

            MessagingCenter.Send(this, "DataUpdated");
            //await Navigation.PushAsync(new AttendingEventsView());
        }
    }
}
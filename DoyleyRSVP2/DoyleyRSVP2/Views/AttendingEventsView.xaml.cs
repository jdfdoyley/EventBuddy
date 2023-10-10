using System;
using System.Collections.Generic;
using System.Linq;
using DoyleyRSVP2.Models;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{
    public partial class AttendingEventsView : ContentPage
    {
        public AttendingEventsView()
        {
            InitializeComponent();
            RefreshData();

            // Subscribe to the "DataUpdated" message to refresh the list of attending events
            MessagingCenter.Subscribe<EventDetailsView>(this, "DataUpdated", (sender) => {
                RefreshData();
            });

            // Wire up the ItemSelected event
            AttendingEventsList.ItemSelected += AttendingEventsList_ItemSelected;
        }

        // Ensure the data is refreshed when the view appears
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private void RefreshData()
        {
            // Fetch the EventModel records where the current user is attending
            var attendingEvents = App.EventRepo.GetAttendingEvents(App.CurrentUser.ID);

            // Update the ListView ItemSource with the fetched events
            AttendingEventsList.ItemsSource = attendingEvents;
        }



        private async void AttendingEventsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;  // Deselect the item and return if it's null
            }

            // Navigate to the EventDetailsView for the selected event
            var selectedEvent = e.SelectedItem as EventModel;
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));

            // Deselect the item
            AttendingEventsList.SelectedItem = null;
        }
    }
}

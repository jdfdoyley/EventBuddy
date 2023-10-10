using System;
using DoyleyRSVP2.Models;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{
    public partial class HostingEventsView : ContentPage
    {
        public HostingEventsView()
        {
            InitializeComponent();
            // Subscribe to the "DataUpdated" message to refresh the list of hosting events
            MessagingCenter.Subscribe<EventDetailsView>(this, "DataUpdated", (sender) => {
                RefreshData();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        private void RefreshData()
        {
            // Fetch all events hosted by the logged-in user
            var events = App.EventRepo.GetEventsByUser(App.CurrentUser);
            HostingEventsList.ItemsSource = events;
        }

        private async void HostingEventsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; // Deselect the item and return if it's null
            }

            var selectedEvent = e.SelectedItem as EventModel;
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));

            // Deselect the item
            HostingEventsList.SelectedItem = null;
        }
    }
}

using System.Collections.ObjectModel;
using System.Windows;

namespace order_processing
{
    public partial class NotificationsWindow : Window
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        public NotificationsWindow(ObservableCollection<Notification> notifications)
        {
            InitializeComponent();
            Notifications = notifications;
            DataContext = this;
        }
    }
}

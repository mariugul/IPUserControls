using IPUserControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Prototyping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IpStatus.ConnectionStatus != ConnectionStatus.Connected)
                Connect();
            else
                Disconnect();
        }

        private void Disconnect()
        {
            // "try disconnect"

            // Disconnected!!! Change status
            IpStatus.ConnectionStatus = ConnectionStatus.Disconnected;

            IpPort.IsEnabled = true;
            IpField.IsEnabled = true;
            ConnectionButton.Content = "Connect";
        }

        private void Connect()
        {
            // "try connect"

            // Connected!!! Change status
            IpStatus.ConnectionStatus = ConnectionStatus.Connected;

            IpPort.IsEnabled = false;
            IpField.IsEnabled = false;
            ConnectionButton.Content = "Disconnect";
        }

        #region Property Notifications

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion Property Notifications
    }
}
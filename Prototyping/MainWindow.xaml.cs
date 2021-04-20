using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using IPUserControls;

namespace Prototyping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private bool _enabled;

        public bool Enabled
        {
            get => _enabled; 
            set => SetProperty(ref _enabled, value);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_connectionStatus != ConnectionStatus.Connected)
                Connect();
            else
                Disconnect();
        }

        private void Disconnect()
        {
            // "try disconnect"

            // Disconnected!!! Change status
            _connectionStatus = ConnectionStatus.Disconnected; // 0 means connected

            IpPort.InputIsEnabled = true;
            IpField.IpInputEnabled = true;
            ConnectionButton.Content = "Connect";

        }

        private void Connect()
        {
            // "try connect"

            // Connected!!! Change status
            _connectionStatus = ConnectionStatus.Connected; // 1 means connected

            IpPort.InputIsEnabled = false;
            IpField.IpInputEnabled = false;
            ConnectionButton.Content = "Disconnect";
        }

        private ConnectionStatus _connectionStatus;

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
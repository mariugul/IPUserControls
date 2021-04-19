using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IPUserControls
{
    /// <summary>
    /// Interaction logic for IpStatus.xaml
    /// </summary>
    public partial class IpStatus : UserControl, INotifyPropertyChanged
    {
        public IpStatus()
        {
            InitializeComponent();
        }

        // Status Image Sources
        private const string ImageConnected =    "/IPUserControls;component/Images/ip_connected.png";
        private const string ImageDisconnected = "/IPUserControls;component/Images/ip_disconnected.png";
        private const string ImageConnecting =   "/IPUserControls;component/Images/ip_connecting.png";
        private const string ImageError =        "/IPUserControls;component/Images/ip_error.png";


        #region Exposed Properties

        #endregion

        #region Properties
        private bool _inputEnabled = true;
        
        public bool InputEnabled
        {
            get => _inputEnabled;
            set => SetProperty(ref _inputEnabled, value);
        }

        private ConnectionStatus _connectionStatus = ConnectionStatus.Disconnected;

        /// <summary>
        /// This can have the following statuses:
        /// Connected, Disconnected, Connecting, Error.
        /// </summary>
        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                SetProperty(ref _connectionStatus, value);
                UpdateConnectionImage();
                InputEnabled = ConnectionStatus == ConnectionStatus.Disconnected;
                //ShowConnectionStatusPopup();
            }
        }

        private string _connectionImageSource = ImageDisconnected;

        /// <summary>
        /// Contains the image sources to use for the different connection statuses.
        /// </summary>
        public string ConnectionImageSource
        {
            get => _connectionImageSource;
            set => SetProperty(ref _connectionImageSource, value);
        }
        #endregion

        #region Methods
        private void UpdateConnectionImage()
        {
            switch (ConnectionStatus)
            {
                case ConnectionStatus.Connected:
                    ConnectionImageSource = ImageConnected;
                    break;
                case ConnectionStatus.Disconnected:
                    ConnectionImageSource = ImageDisconnected;
                    break;
                case ConnectionStatus.Connecting:
                    ConnectionImageSource = ImageConnecting;
                    break;
                case ConnectionStatus.Error:
                    ConnectionImageSource = ImageError;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Events
        private void StatusIcon_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupStatusInfo.PlacementTarget = StatusImage;
            PopupStatusInfo.Placement = PlacementMode.Top;
            PopupStatusInfo.IsOpen = true;
            ConnectionInfo.PopupText.Text = ConnectionStatus.ToString();
        }

        private void StatusIcon_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupStatusInfo.Visibility = Visibility.Collapsed;
            PopupStatusInfo.IsOpen = false;
        }
        #endregion

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
        #endregion
    }

    public enum ConnectionStatus
    {
        Connected,
        Disconnected,
        Connecting,
        Error
    }
}

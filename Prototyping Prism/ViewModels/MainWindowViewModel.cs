using IPUserControls;
using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;
using System.Threading;

namespace Prototyping_Prism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            ButtonClickCommand = new DelegateCommand(ButtonClick);
            ButtonIpChangeCommand = new DelegateCommand(IpChangeButtonClick);
            IpAddress = "192.168.0.175";
        }

        private string _title = "Prism Application";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _ipAddress = "0.0.0.0";

        public string IpAddress
        {
            get
            {
                Debug.WriteLine($"Get _ipAddress in VM: {_ipAddress}");
                return _ipAddress;
            }
            set
            {
                SetProperty(ref _ipAddress, value);
                Debug.WriteLine($"Set IpAddress in VM to: {_ipAddress}");
            }
        }

        private ConnectionStatus _connectionStatus = ConnectionStatus.Disconnected;

        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            set => SetProperty(ref _connectionStatus, value);
        }

        public DelegateCommand ButtonIpChangeCommand { get; private set; }

        private void IpChangeButtonClick()
        {
            IpAddress = "100.100.100.100";
        }

        public DelegateCommand ButtonClickCommand { get; private set; }

        private void ButtonClick()
        {
            if (ConnectionStatus == ConnectionStatus.Disconnected)
            {
                ButtonContent = "Connecting";
                ConnectionStatus = ConnectionStatus.Connecting;
            }
            else if (ConnectionStatus == ConnectionStatus.Connecting)
            {
                ConnectionStatus = ConnectionStatus.Connected;
                ButtonContent = "Disconnect";
            }
            else
            {
                ConnectionStatus = ConnectionStatus.Disconnected;
                ButtonContent = "Connect";
            }
        }

        private string _buttonContent = "Connect";
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }
    }

    public enum SomeOtherConnectionStatus
    {
        Connected,
        Disconnected,
        Connecting,
        Error
    }
}
using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;
using System.Security.Permissions;
using IPUserControls;

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
                ConnectionStatus = ConnectionStatus.Connected;
            else
                ConnectionStatus = ConnectionStatus.Disconnected;

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
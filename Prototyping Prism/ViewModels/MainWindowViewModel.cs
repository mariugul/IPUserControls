using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

namespace Prototyping_Prism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            ButtonClickCommand = new DelegateCommand(ButtonClick);
        }
        
        private string _title = "Prism Application";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _ipAddress;

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

        public DelegateCommand ButtonClickCommand { get; private set; }

        private void ButtonClick()
        {
            IpAddress = "123.123.123.123";
        }
    }
}
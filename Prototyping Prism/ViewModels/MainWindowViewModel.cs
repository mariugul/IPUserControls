using System.Diagnostics;
using Prism.Mvvm;

namespace Prototyping_Prism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
        }

        private string _title = "Prism Application";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _ipAddress = "0.0.0.100";

        public string IpAddress
        {
            get
            {
                Debug.WriteLine($"_ipAddress in VM: {_ipAddress}");
                return _ipAddress;
            } 
            set
            {
                SetProperty(ref _ipAddress, value);
                Debug.WriteLine($"Set IpAddress in VM to: {_ipAddress}");
            }
        }
    }
}
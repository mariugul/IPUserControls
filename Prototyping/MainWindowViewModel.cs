using System;
using System.Diagnostics;

namespace Prototyping
{
    public class MainWindowViewModel
    {
        private string _ipAddress;

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                Debug.WriteLine($"Set IpAddress to: {_ipAddress}");
            }

        }
    }
}
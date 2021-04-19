using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace IPUserControls
{
    /// <summary>
    /// Interaction logic for IpField
    /// </summary>
    public partial class IpField : UserControl, INotifyPropertyChanged
    {
        public IpField()
        {
            InitializeComponent();
        }

        #region Exposed Properties

        /// <summary>
        /// Gets the IP Address as a string.
        /// </summary>
        public string IpAddress
        {
            get => (string)GetValue(IpAddressProperty); 
            private set 
            {
                SetValue(IpAddressProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IpAddressProperty =
            DependencyProperty.Register("IpAddress", typeof(string), typeof(IpField), new PropertyMetadata("0.0.0.0"));

        
        /// <summary>
        /// Returns the IP address as a byte array.
        /// </summary>
        public byte[] IpAddressBytes
        {
            get => (byte[])GetValue(IpAddressBytesProperty); 
            private set 
            {
                SetValue(IpAddressBytesProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IpAddressBytesProperty =
            DependencyProperty.Register("IpAddressBytes", typeof(byte[]), typeof(IpField), new PropertyMetadata(new byte[ushort.MaxValue]));

        public bool IpInputEnabled
        {
            get => (bool)GetValue(IpInputEnabledProperty);
            set
            {
                SetValue(IpInputEnabledProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IpInputEnabledProperty =
            DependencyProperty.Register("IpInputEnabled", typeof(bool), typeof(IpPort), new PropertyMetadata(true));

        /// <summary>
        /// Sets the default IP Address that is displayed on start up.
        /// </summary>
        public string DefaultIpAddress
        {
            set
            {
                if (IsValidIpAddress(value))
                {
                    var ipBytes = value.Split('.');
                    IpFirstByte = ipBytes[0];
                    IpSecondByte = ipBytes[1];
                    IpThirdByte = ipBytes[2];
                    IpFourthByte = ipBytes[3];
                }
            }
        }

        #endregion


        #region Properties
        private string _ipFirstByte = "0";

        /// <summary>
        /// The first number in the IP Address.
        /// </summary>
        public string IpFirstByte
        {
            get => _ipFirstByte;
            set
            {
                UpdateIpByte(ref _ipFirstByte, value, nameof(IpFirstByte));
                UpdateIpAddressBytes();
                UpdateIpAddress();
            }
        }

        private string _ipSecondByte = "0";

        /// <summary>
        /// The second number in the IP Address.
        /// </summary>
        public string IpSecondByte
        {
            get => _ipSecondByte;
            set
            {
                UpdateIpByte(ref _ipSecondByte, value, nameof(IpSecondByte));
                UpdateIpAddressBytes();
                UpdateIpAddress();
            }
        }

        private string _ipThirdByte = "0";

        /// <summary>
        /// The third number in the IP Address.
        /// </summary>
        public string IpThirdByte
        {
            get => _ipThirdByte;
            set
            {
                UpdateIpByte(ref _ipThirdByte, value, nameof(IpThirdByte));
                UpdateIpAddressBytes();
                UpdateIpAddress();
            }
        }

        private string _ipFourthByte = "0";

        /// <summary>
        /// The fourth number in the IP Address.
        /// </summary>
        public string IpFourthByte
        {
            get => _ipFourthByte;
            set
            {
                UpdateIpByte(ref _ipFourthByte, value, nameof(IpFourthByte));
                UpdateIpAddressBytes();
                UpdateIpAddress();
            }
        }

        #endregion

        #region Methods
        private void UpdateIpAddress()
        {
            IpAddress =
                IpAddressBytes[0] + "." +
                IpAddressBytes[1] + "." +
                IpAddressBytes[2] + "." +
                IpAddressBytes[3];
            Console.WriteLine($"IP Address: {IpAddress}");
        }

        private void UpdateIpAddressBytes()
        {
            IpAddressBytes[0] = StringToByte(IpFirstByte);
            IpAddressBytes[1] = StringToByte(IpSecondByte);
            IpAddressBytes[2] = StringToByte(IpThirdByte);
            IpAddressBytes[3] = StringToByte(IpFourthByte);
            Console.WriteLine($"IP Address Bytes: {IpAddressBytes[0]} {IpAddressBytes[1]} {IpAddressBytes[2]} {IpAddressBytes[3]}");
        }

        private void UpdateIpByte(ref string backingField, string value, string property)
        {
            ParseIpInput(ref value);

            if (value == backingField) return;

            if (IsByte(value))
                backingField = value;
            else if (IsNumber(value))
                return;

            if (value == "")
            {
                if (backingField == "0")
                    return;
                else
                    backingField = "0";
            }

            OnPropertyChanged(property);
        }

        private void ParseIpInput(ref string input)
        {
            if (input.Contains(" ")) input = input.Replace(" ", "");
            if (input.Contains("-")) input = input.Replace("-", "");
            if (input.Contains("+")) input = input.Replace("+", "");
            if (input.StartsWith("0") && input.Length > 1) input = input.Remove(0, 1);
        }

        private bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        private bool IsByte(string input)
        {
            return byte.TryParse(input, out _);
        }

        private byte StringToByte(string input)
        {
            byte result;
            if (byte.TryParse(input, out result)) return result;
            return 0;
        }

        private bool IsValidIpAddress(string value)
        {
            Regex ipAddressCheck = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            return ipAddressCheck.IsMatch(value);
        }
        #endregion

        #region Events


        // Select All Text On Keyboard Focus
        // ---------------------------------
        private void FirstByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => FirstByteTextBox.SelectAll();
        private void SecondByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => SecondByteTextBox.SelectAll();
        private void ThirdByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => ThirdByteTextBox.SelectAll();
        private void FourthByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => FourthByteTextBox.SelectAll();
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

        #endregion Property Notifications
    }
}

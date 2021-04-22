using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace IPUserControls
{
    /// <summary>
    /// Interaction logic for IpField
    /// </summary>
    public partial class IpField : INotifyPropertyChanged
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
            set
            {
                SetValue(IpAddressProperty, value);
                OnPropertyChanged();
                //Debug.WriteLine($"IpAddress in UC set to: {IpAddress}");
            }
        }

        public static readonly DependencyProperty IpAddressProperty =
            DependencyProperty.Register("IpAddress", typeof(string), typeof(IpField), new FrameworkPropertyMetadata("0.0.0.0"){BindsTwoWayByDefault = true});

        /// <summary>
        /// Returns the IP address as a byte array.
        /// </summary>
        public byte[] IpAddressBytes
        {
            get => (byte[])GetValue(IpAddressBytesProperty);
            set
            {
                SetValue(IpAddressBytesProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IpAddressBytesProperty =
            DependencyProperty.Register("IpAddressBytes", typeof(byte[]), typeof(IpField), new FrameworkPropertyMetadata(new byte[ushort.MaxValue]){BindsTwoWayByDefault = true});

        #endregion Exposed Properties

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
                if (value == _ipFirstByte) return;

                ParseIpByteInput(ref value);

                if (IsByte(value))
                    _ipFirstByte = value;
                else if (IsNumber(value))
                    return;

                if (value == "")
                {
                    if (_ipFirstByte == "0") return;
                    _ipFirstByte = "0";
                }
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

        #endregion Properties

        #region Methods

        private void UpdateIpAddress()
        {
            IpAddress =
                IpAddressBytes[1] + "." +
                StringToByte(IpFirstByte)  + "." +
                StringToByte(IpSecondByte) + "." +
                StringToByte(IpThirdByte)  + "." +
                StringToByte(IpFourthByte) + ".";
        }

        private void UpdateIpAddressBytes()
        {
            var ipBytes = new[]
            {
                StringToByte(IpFirstByte),
                StringToByte(IpSecondByte),
                StringToByte(IpThirdByte),
                StringToByte(IpFourthByte)
            };

            IpAddressBytes = ipBytes;
        }

        private void UpdateIpByte(ref string backingField, string value, string property)
        {
            ParseIpByteInput(ref value);

            if (value == backingField) return;

            if (IsByte(value))
                backingField = value;
            else if (IsNumber(value))
                return;

            if (value == "")
            {
                if (backingField == "0") return;
                backingField = "0";
            }

            OnPropertyChanged(property);
        }

        /// <summary>
        /// Removes whitespace, leading 0's and '+', '-' signs.
        /// These are characters that would be legal to pass in
        /// otherwise, and be evaluated as correct numbers.
        /// <example>
        /// // All of the strings below will equal to a valid number.
        /// string nrStr1 = "002";
        /// string nrStr2 = "-001";
        /// string nrStr3 = "+20";
        /// string nrStr4 = " 123";
        /// isNumber(nrStr1); // Same for nrStrX...
        /// </example>
        /// </summary>
        /// <param name="input"></param>
        private static void ParseIpByteInput(ref string input)
        {
            while (input.Contains(" ")) input = input.Replace(" ", "");
            while (input.Contains("-")) input = input.Replace("-", "");
            while (input.Contains("+")) input = input.Replace("+", "");
            while (input.StartsWith("0") && input.Length > 1) input = input.Remove(0, 1);
        }
        
        private bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        private bool IsByte(string input)
        {
            return byte.TryParse(input, out _);
        }

        /// <summary>
        /// Returns a byte from a string representation. If the string is
        /// an invalid number or bigger than a byte, the result returned is 0.
        /// </summary>
        private static byte StringToByte(string input)
        {
            return byte.TryParse(input, out var result) ? result : (byte)0;
        }

        /// <summary>
        /// The purpose of this is to validate the potential incoming IP address
        /// from an app consuming this User Control.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if the string is a valid IP address.</returns>
        private bool IsValidIpAddress(string value)
        {
            var ipAddressCheck = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"); // Does not validate leading 0's
            return ipAddressCheck.IsMatch(value);
        }

        #endregion Methods

        #region Events

        // Select All Text On Keyboard Focus
        // ---------------------------------
        private void FirstByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => FirstByteTextBox.SelectAll();

        private void SecondByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => SecondByteTextBox.SelectAll();

        private void ThirdByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => ThirdByteTextBox.SelectAll();

        private void FourthByteTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => FourthByteTextBox.SelectAll();

        #endregion Events

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace IPUserControls
{
    /// <summary>
    /// Interaction logic for IpPort.xaml
    /// </summary>
    public partial class IpPort : UserControl, INotifyPropertyChanged
    {
        public IpPort()
        {
            DefaultPortNumber = 3092;
            InitializeComponent();
        }

        // Exposed Properties 
        // --------------------------------------



        // Methods
        // --------------------------------------
        private static bool IsUShort(string input)
        {
            if (input.Length > 5) return false;
            if (!IsNumber(input)) return false;
            return uint.Parse(input) <= 65535;
        }

        private static bool IsNumber(string input)
        {
            return new Regex("^[0-9]+$").IsMatch(input);
        }

        // Properties
        // --------------------------------------
        private ushort _portNumber;

        /// <summary>
        /// Holds the input Port number.
        /// </summary>
        public ushort PortNumber
        {
            get => _portNumber;
            set => SetProperty(ref _portNumber, value);
        }

        /// <summary>
        /// Sets the Port number to display at start up.
        /// </summary>
        public ushort DefaultPortNumber
        {
            set => PortNumber = value;
        }

        private bool _inputEnabled = true;

        /// <summary>
        /// Enables the IP Address and Port text box
        /// </summary>
        public bool InputEnabled
        {
            get => _inputEnabled;
            set => SetProperty(ref _inputEnabled, value);
        }



        // Event Handlers
        // --------------------------------------
        private void PortNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PortNrTextBox.Text == PortNumber.ToString()) return;
            if (PortNrTextBox.Text == "")
            {
                PortNumber = 0;
                PortNrTextBox.Text = 0.ToString();
                PortNrTextBox.SelectAll();
                return;
            }

            if (!IsUShort(PortNrTextBox.Text))
                PortNrTextBox.Text = PortNumber.ToString();
            else
            {
                PortNumber = ushort.Parse(PortNrTextBox.Text);
                PortNrTextBox.Text = PortNumber.ToString();
            }
        }

        private void PortNrTextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e) => PortNrTextBox.SelectAll();


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
}
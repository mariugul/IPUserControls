using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace IPUserControls
{
    /// <summary>
    /// Interaction logic for IpPort.xaml
    /// </summary>
    public partial class IpPort : INotifyPropertyChanged
    {
        public IpPort()
        {
            InitializeComponent();
        }

        // Exposed Properties
        // --------------------------------------
        public ushort PortNumber
        {
            get => (ushort)GetValue(PortNumberProperty);
            private set
            {
                SetValue(PortNumberProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty PortNumberProperty =
            DependencyProperty.Register("PortNumber", typeof(ushort), typeof(IpPort), new PropertyMetadata(ushort.MinValue));

        /// <summary>
        /// Sets the Port number to display at start up.
        /// </summary>
        public ushort DefaultPortNumber { set => PortNumber = value; } // Doesn't work for some reason

        public static readonly DependencyProperty DefaultPortNumberProperty =
            DependencyProperty.Register("DefaultPortNumber", typeof(ushort), typeof(IpPort), new PropertyMetadata(ushort.MinValue));

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

        #endregion Property Notifications
    }
}
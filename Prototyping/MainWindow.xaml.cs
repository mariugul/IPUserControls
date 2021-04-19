using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Prototyping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private bool _enabled;

        public bool Enabled
        {
            get => _enabled; 
            set => SetProperty(ref _enabled, value);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IpPort.InputIsEnabled = !IpPort.InputIsEnabled;
            IpField.IpInputEnabled = !IpField.IpInputEnabled;
        }

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
# IPUserControls.WPF
 With these controls you get an IP Address Text Box, IP Port Text Box and IP Connection Status Icon. Combine them together for a complete IP connection control.
 
## Controls Included
| **Controls**  | **Description**      | Bindable Property   |
| ------------- |----------------------| ------------------- | 
| IpField       | IP Address TextBox   | IP string or byte[] |
| IpPort        | IP Port TextBox      | IP port number      |
| IpStatus      | IP Connection Status | Connection Status

 
## Usage
1. Install the Nuget package
2. Add this to your _SomeView.xaml_ namespace
    ```xaml
    xmlns:ip="clr-namespace:IPUserControls;assembly=IPUserControls.Wpf"
    ```
3. Access the IP controls by typing either or all of the commands
    ```xaml
    <ip:IpField />
    ```
    insert image
    
    ```xaml
    <ip:IpPort />
    ```
    insert image
 
    ```xaml
    <ip:IpStatus />
    ```
    insert image
    
4. Minimal working example of the UI is shown below
    ```xaml
    <Window x:Class="NugetPackageTest.MainWindow"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:ip="clr-namespace:IPUserControls;assembly=IPUserControls.Wpf"
            mc:Ignorable="d"
            Title="MainWindow"
            Height="150"
            Width="400">

        <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
            <Label Content="IP Address" Margin="0,0,10,0" />
            <ip:IpField  />
            <ip:IpPort   />
            <ip:IpStatus />
        </StackPanel>
    </Window>
    ```
    
    This should compile to
    insert image

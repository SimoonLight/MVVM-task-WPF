using Command.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Command;


public partial class MainWindow : Window, INotifyPropertyChanged
{
    private string someText;
    private MessageCommand messageCommand;

    public MessageCommand MessageCommand { get => messageCommand; set { messageCommand = value; OnPropertyChanged(); } }
    public string SomeText
    {
        get => someText; 
        set{
            someText = value;
            OnPropertyChanged();
            MessageCommand = new MessageCommand(SomeText);
        }
    }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        SomeText = "Muhammed";
        MessageCommand = new MessageCommand(SomeText);
    }

    // -------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
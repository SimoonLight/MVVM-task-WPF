using Command.Commands;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Command;

public partial class Example_2 : Window, INotifyPropertyChanged
{
    private string someText;

    public ICommand MessageCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public string? SomeText { get => someText; set { someText = value; OnPropertyChanged(); } }
    public Example_2()
    {
        InitializeComponent();
        DataContext = this;
        MessageCommand = new RelayCommand(ShowMessage, isTextLenOkay);
    }



    public bool isTextLenOkay(object? p)
    {
        return SomeText?.Length > 20;
    }

    public void ShowMessage(object? p)
    {
        //MessageBox.Show(p.ToString());
        StackPanel? st = p as StackPanel;
        st.Background = System.Windows.Media.Brushes.Red;
    }

    // ----------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // -------------------------------------------------------
}

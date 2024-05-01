using MvvmLesson.ViewModels;
using System.Windows;

namespace MvvmLesson.Views;

public partial class EnterView : Window
{
    public EnterView()
    {
        InitializeComponent();
        DataContext = new EnterViewModel();
    }
}

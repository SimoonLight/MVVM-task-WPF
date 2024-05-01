using MvvmLesson.DataBases;
using MvvmLesson.Models;
using MvvmLesson.ViewModels;
using MvvmLesson.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace MvvmLesson;

public partial class App : Application
{
    public void App_OnStartup(object sender, StartupEventArgs e)
    {
        var fileName = "Database/Cars.json";


        if (File.Exists(fileName))
        {
            var text = File.ReadAllText(fileName);
            CarDataBase.CarsDB = JsonSerializer.Deserialize<ObservableCollection<Car>>(text);
        }
        EnterView enterView = new EnterView();
        enterView.ShowDialog();
    }
}

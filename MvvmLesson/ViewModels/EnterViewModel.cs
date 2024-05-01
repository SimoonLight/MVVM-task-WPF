using MvvmLesson.Commands;
using MvvmLesson.DataBases;
using MvvmLesson.Models;
using MvvmLesson.Views;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace MvvmLesson.ViewModels;

public class EnterViewModel : INotifyPropertyChanged
{
    private Car? newCar;
    private ObservableCollection<Person> sellers;

    public Car? NewCar { get => newCar; set { newCar = value;  OnPropertyChanged(); } }

    public ObservableCollection<Person> Sellers { get => sellers; set { sellers = value; OnPropertyChanged(); } }

    public ICommand AddCommand { get; set; }
    public ICommand GetAllCarCommand { get; set; }
    public ICommand SaveCarsCommand { get; set; }

    public EnterViewModel()
    {
        Sellers = CarDataBase.SellersDB!;
        NewCar = new Car();
        AddCommand = new RelayCommand(AddCar, IsAddCarEnabled);
        GetAllCarCommand = new RelayCommand(GetAllCars, IsGetAllCarEnabled);
        SaveCarsCommand = new RelayCommand(SaveCars, IsSaveCarsEnabled);
    }

    #region GetAllCar
    private bool IsGetAllCarEnabled(object? obj)
    {
        return CarDataBase.CarsDB is not null && CarDataBase.CarsDB?.Count > 0 ;
    }
    private void GetAllCars(object? obj)
    {
        GetAllCarView view = new GetAllCarView();
        view.DataContext = new GetAllCarViewModel();

        view.ShowDialog();
    }
    #endregion
    #region AddCar
    public bool IsAddCarEnabled(object? parameter)
    {
        return NewCar?.Model?.Length >= 3 && NewCar?.Make?.Length >= 3 && NewCar?.Year != null && NewCar?.Passengers != null;
    }
    public void AddCar(object? parameter)
    {
        CarDataBase.CarsDB?.Add(NewCar!);
        //MessageBox.Show(CarDataBase.CarsDB.Count.ToString());
        NewCar = new Car();
    }
    #endregion
    #region SaveCars
    private bool IsSaveCarsEnabled(object? obj)
    {
        return CarDataBase.CarsDB is not null;
    }

    private void SaveCars(object? obj)
    {
        var fileName = "Database/Cars.json";
        if (!Directory.Exists("Database"))
            Directory.CreateDirectory("Database");

        if(!File.Exists(fileName))
            File.Create(fileName).Close();

        JsonSerializerOptions op= new();
        op.WriteIndented = true;
        string json = JsonSerializer.Serialize(CarDataBase.CarsDB, op);

        File.WriteAllText(fileName, json);
    }

    #endregion



    // -----------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string? propertyName=null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // -----------------------------------------------------------
}

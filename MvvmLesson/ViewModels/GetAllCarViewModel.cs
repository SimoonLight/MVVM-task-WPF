using MvvmLesson.Commands;
using MvvmLesson.DataBases;
using MvvmLesson.Models;
using MvvmLesson.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MvvmLesson.ViewModels;

public class GetAllCarViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Car>? cars;
    public ObservableCollection<Car>? Cars { get => cars; set { cars = value; OnPropertyChanged(); } }

    public ICommand RemoveCarCommand { get; set; }
    public ICommand EditCarCommand { get; set; }
    public GetAllCarViewModel()
    {
        Cars = CarDataBase.CarsDB;
        RemoveCarCommand = new RelayCommand(RemoveCar, IsRemoveCarEnabled);
        EditCarCommand = new RelayCommand(EditCar, IsRemoveCarEnabled);
    }

    #region RemoveCar
    private bool IsRemoveCarEnabled(object? obj)
    {

        int? selectedIndex = obj as int?;
        if (Cars?.Count > 0 && selectedIndex != -1)
            return true;
        return false;
    }

    private void RemoveCar(object? obj)
    {
        int? selectedIndex = obj as int?;
        if (selectedIndex != -1)
        {
            CarDataBase.CarsDB?.RemoveAt(selectedIndex!.Value);
        }
    }
    #endregion

    #region EditCar
    private void EditCar(object? obj)
    {
        int? selectedIndex = obj as int?;
        Car? editCar = CarDataBase.CarsDB?[selectedIndex!.Value];
        if(editCar is not null)
        {
            EditCarView editCarView = new();
            editCarView.DataContext = new EditCarViewModel(editCar);
            editCarView.ShowDialog();
        }
    }
    #endregion


    // ------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // ------------------------------------------------------
}

using MvvmLesson.Commands;
using MvvmLesson.DataBases;
using MvvmLesson.Models;
using MvvmLesson.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MvvmLesson.ViewModels;

class EditCarViewModel : INotifyPropertyChanged
{
    private Car? editCar;

    public Car? EditCar { get => editCar; set { editCar = value; OnPropertyChanged(); } }
    public Car? CopyEditCar { get; set; }

    private ObservableCollection<Person> sellers;
    public ObservableCollection<Person> Sellers { get => sellers; set { sellers = value; OnPropertyChanged(); } }

    public ICommand SaveEditCarCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    public EditCarViewModel(Car? editCar)
    {
        Sellers = CarDataBase.SellersDB!;
        EditCar = editCar;
        CopyEditCar = new(editCar);
        SaveEditCarCommand = new RelayCommand(SaveEditCar, IsSaveEditEnabled);
        CancelCommand = new RelayCommand(CancelSave);
    }

    #region CancelSave
    public void CancelSave(object? obj)
    {
        EditCarView? editCarView = obj as EditCarView;
        editCarView?.Close();
    }
    #endregion
    #region SaveEdit
    private bool IsSaveEditEnabled(object? obj)
    {
        if (CopyEditCar == EditCar)
            return false;
        return true;
    }

    private void SaveEditCar(object? obj)
    {
        EditCar?.SetCar(CopyEditCar);
    }

    #endregion


    // -----------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // -----------------------------------------------------------
}

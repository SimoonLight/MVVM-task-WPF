using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmLesson.Models;

public class Person:INotifyPropertyChanged
{
    private string? name;
    private string? surname;
    private int age;

    public string? Name { get => name; set { name = value;  OnPropertyChanged(); } }
    public string? Surname { get => surname; set { surname = value; OnPropertyChanged(); } }
    public int Age { get => age; set { age = value; OnPropertyChanged(); } }


    public override string ToString()
    {
        return $"{Name} {Surname}";
    }

    // == Operator overloading
    public static bool operator ==(Person? person1, Person? person2)
    {
        if (person1 is null && person2 is null)
            return true;
        if (person1 is null || person2 is null)
            return false;
        return person1.Name == person2.Name && person1.Surname == person2.Surname && person1.Age == person2.Age;
    }

    // != operator overloading
    public static bool operator !=(Person? person1, Person? person2)
    {
        return !(person1 == person2);
    }

    // -----------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    // -----------------------------------------------------------
}

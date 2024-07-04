using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoExamTraining2.ViewModels;

namespace DemoExamTraining2.Views;

public partial class AddOrEditWindow : Window
{
    public AddOrEditWindow()
    {
        InitializeComponent();
        DataContext = new AddOrEditViewModel();
    }
    
    public AddOrEditWindow(object selected)
    {
        InitializeComponent();
        DataContext = new AddOrEditViewModel(selected);
    }

    public void CloseThisWindow(object sender, RoutedEventArgs args)
    {
        this.Close();
    }
}
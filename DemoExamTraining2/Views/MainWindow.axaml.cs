using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DemoExamTraining2.ViewModels;

namespace DemoExamTraining2.Views;

public partial class MainWindow : Window
{
    private List<Window> Windows = new List<Window>() {};
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    public void OpenAddWindow(object sender, RoutedEventArgs args)
    {
        Window AddOrEditWindow = new AddOrEditWindow();
        CloseAllWindowsExcept();
        Windows.Add(AddOrEditWindow);
        AddOrEditWindow.Show();
    }
    
    public void OpenEditWindow(object sender, TappedEventArgs args)
    {
        Window AddOrEditWindow = new AddOrEditWindow(AgentList.SelectedItem);
        CloseAllWindowsExcept();
        Windows.Add(AddOrEditWindow);
        AddOrEditWindow.Show();
    }

    private void CloseAllWindowsExcept()
    {
        foreach (Window w in Windows)
        {
            if (w != this)
            {
                w.Close();
            }
        }
    }
}
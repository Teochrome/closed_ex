using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data.Converters;
using DemoExamTraining2.Models;

namespace DemoExamTraining2.Helpers;

public class VisibilityConvertor: IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<Agent> agents)
        {
            if (agents.Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Two-way binding is not supported by this converter.");
    }
}
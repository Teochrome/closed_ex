using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media.Imaging;
using DemoExamTraining2.Helpers;
using DemoExamTraining2.Models;

namespace DemoExamTraining2.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    private static Bitmap _placeholder = ImageHelper.LoadFromResource(new Uri("avares://DemoExamTraining2/Assets/Images/placeholder.jpg"));
    private string _keywords;
    private bool _sortDescending = false;
    private int _filterIdx = -1;
    private int _sortingIdx = -1;
    private int _currentPage = 1;
    private static ObservableCollection<Agent> _selected = new ();
    private ObservableCollection<Agent> _filteredAgents = new ();
    private static List<int> _pages = new List<int>() { 1 };
    private List<string> _agentTypes = new List<string>()
    {
        "Все типы",
        "Lv1_Crook",
        "Lv2_Agent",
        "Lv3_Agent",
        "Lv4_Agent"
    };
    private static List<AgentType> _agentTypesForRendering = new List<AgentType>(){new AgentType("All types")}.Concat(Context.AgentTypes.Select(el => el)).ToList();
    private static ObservableCollection<Agent> _agents = new()
    {
        new Agent("Михалыч", "89111234567", "bebrik@bk.ru", "сам себе начальник", "111222333444", "ул. Кузи Лакомкина 1", 4, 5, Placeholder, Context.AgentTypes[0]),
        new Agent("Мамадзиё Нагзибеков", "89914234567", "bebrik@bk.ru", "Анатолий", "3235791850392", "ул. Кузи Лакомкина 2", 3, 10, Placeholder, Context.AgentTypes[1]),
        new Agent("Агент", "89914333333", "agentk@gmail.com", "Анатолий", "3235791834662", "ул. Кузи Лакомкина 3/1", 2, 4, Placeholder, Context.AgentTypes[2]),
        new Agent("Агент", "89914333333", "agent@gmail.com", "Анатолий", "0005791834662", "ул. Кузи Лакомкина 3/2", 5, 1, Placeholder, Context.AgentTypes[3]),
        new Agent("asdfsa", "8911234567", "rik@b.ru", "сам", "111222343444", "ул. Кузи Лакомина 1", 4, 5, Placeholder, Context.AgentTypes[2]),
        new Agent("Agent", "83211234567", "d@mail.ru", "сам начальник", "111222333444", "ул. Кузи Лакомкина 12", 4, 5, Placeholder, Context.AgentTypes[3]),
        new Agent("Федорыч", "89111234567", ";klj@bk.ru", "сам", "111222333444", "ул. Кузи Лакомкина 1", 4, 5, Placeholder, Context.AgentTypes[1]),
        new Agent("genius", "89111234567", "ka;l@bk.ru", "сам начальник", "111222333444", "ул. Кузи Лакомкина 1", 4, 5, Placeholder, Context.AgentTypes[0]),
        new Agent("aooa", "89111234567", "sd@bk.ru", "сам", "111222333444", "ул. Кузи Лакомкина 1", 4, 5, Placeholder, Context.AgentTypes[2]),
        new Agent("WITH SALES", "89111234567", "sd@bk.ru", "сам", "111222333444", "ул. Кузи Лакомкина 1", 4, 5, Placeholder, Context.AgentTypes[2], new ObservableCollection<Sale>() {new Sale("jdskfl", 999999999)}),
    };
    
    private Func<Agent, object> _sortFunc = agent => agent.Id;
    
    public MainWindowViewModel()
    {
        FilteredAgents = _agents;
    }

    public static List<int> Pages
    {
        get => _pages;
        set
        {
            _pages = value;
        }
    }
    
    public ObservableCollection<Agent> FilteredAgents
    {
        get => _filteredAgents;
        set
        {
            _filteredAgents = value;
            OnPropChanged(nameof(FilteredAgents));
        }
    }

    public ObservableCollection<Agent> SelectedAgents
    {
        get => _selected;
        set
        {
            _selected = value;
            OnPropChanged(nameof(SelectedAgents));
        }
    }

    public static ObservableCollection<Agent> Agents
    {
        get => _agents;
        set
        {
            _agents = value;
            UpdatePages(_agents.Count);
        }
    }

    public static Bitmap Placeholder
    {
        get => _placeholder;
    }

    public static List<AgentType> AgentTypesForRender
    {
        get => _agentTypesForRendering;
    }

    public int FilterIdx
    {
        get => _filterIdx;
        set
        {
            _filterIdx = value;
            ApplyFilterSearchSort();
        }
    }
    
    public int SortingIdx
    {
        get => _sortingIdx;
        set => _sortingIdx = value;
    }

    public List<string> AgentTypes
    {
        get => _agentTypes;
    }
    public string Keywords
    {
        get => _keywords;
        set
        {
            _keywords = value;
            ApplyFilterSearchSort();
        }
    }
    
    public void GoToNextPage()
    {
        _currentPage++;
        LoadPage();
    }
    
    public void GoToPreviousPage()
    {
        _currentPage--;
        LoadPage();
    }

    public void GoToPage(int page)
    {
        _currentPage = page;
        LoadPage();
    }
    
    public void SetSort(Func<Agent, object> sortFunc, bool descending)
    {
        _sortFunc = sortFunc;
        _sortDescending = descending;
        ApplyFilterSearchSort();
    }
    
    private void ApplyFilterSearchSort()
    {
        IEnumerable<Agent> result = _agents;

        if (_filterIdx > 0)
        {
            result = result.Where(agent => agent.Type.Name == _agentTypes[_filterIdx]);
        }

        if (!string.IsNullOrWhiteSpace(_keywords))
        {
            result = result.Where(
                    agent => (agent.Name.ToLower().Contains(_keywords.ToLower()) 
                              || agent.Email.ToLower().Contains(_keywords.ToLower()) 
                              || agent.PhoneNum.Contains(_keywords)));
        }

        result = _sortDescending 
            ? result.OrderByDescending(_sortFunc) 
            : result.OrderBy(_sortFunc);

        FilteredAgents = new ObservableCollection<Agent>(result);
    }

    public void ClearFilters()
    {
        SetSort(agent => agent.Id, false);
    }

    public void SortAgentsByNameAsc()
    {
        SetSort(agent => agent.Name, false);
    }

    public void SortAgentsByNameDesc()
    {
        SetSort(agent => agent.Name, true);
    }

    public void SortAgentsByDiscountAsc()
    {
        SetSort(agent => agent.Discount, false);
    }

    public void SortAgentsByDiscountDesc()
    {
        SetSort(agent => agent.Discount, true);
    }

    public void SortAgentsByPriorityAsc()
    {
        SetSort(agent => agent.Priority, false);
    }

    public void SortAgentsByPriorityDesc()
    {
        SetSort(agent => agent.Priority, true);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    
    private void LoadPage()
    {
        SelectedAgents = new ObservableCollection<Agent>(Agents.Skip(_currentPage * 10).Take(10));
        ApplyFilterSearchSort();
    }

    private static void UpdatePages(int AgentsAmount)
    {
        int pageAmount = AgentsAmount / 10;
        List<int> newPages = new List<int>();
        for (int i = 1; i <= pageAmount; i++)
        {
            newPages.Add(i);
        }

        Pages = newPages;
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using DemoExamTraining2.Helpers;
using DemoExamTraining2.Models;
using DemoExamTraining2.Views;

namespace DemoExamTraining2.ViewModels;

public class AddOrEditViewModel: INotifyPropertyChanged
{
    private string _name;
    private string _phoneNumber;
    private string _email;
    private string _principal;
    private string _itn;
    private string _address;
    private int _id;
    private int _checkpoint;
    private uint _priority;
    private int _typeIdx;
    private Bitmap _image = ImageHelper.LoadFromResource(new Uri("avares://DemoExamTraining2/Assets/Images/placeholder.jpg"));
    private ObservableCollection<Sale> _sales = new ObservableCollection<Sale>();
    public bool IsOnEdit { get; private set; }
    public bool IsItHaveAnySales { get; private set; }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public static List<AgentType> AgentTypes
    {
        get => MainWindowViewModel.AgentTypesForRender[1..];
    }

    public string PhoneNumber
    {
        get => _phoneNumber; 
        set => _phoneNumber = value;
    }

    public string Email
    {
        get => _email; 
        set => _email = value;
    }

    public string Principal
    {
        get => _principal;
        set => _principal = value;
    }

    public string ITN
    {
        get => _itn; 
        set => _itn = value;
    }

    public string Address
    {
        get => _address;
        set => _address = value;
    }

    public int TypeIdx
    {
        get => _typeIdx;
        set => _typeIdx = value;
    }

    public int Checkpoint
    {
        get => _checkpoint;
        set => _checkpoint = value;
    }

    public uint Priority
    {
        get => _priority;
        set => _priority = value;
    }

    public Bitmap Image
    {
        get => _image;
        set => _image = value;
    }

    public ObservableCollection<Sale> Sales
    {
        get => _sales;
        set
        {
            _sales = value;
            OnPropChanged(nameof(Sales));
        }
    }

    public AddOrEditViewModel()
    {
        IsOnEdit = false; 
    }

    public AddOrEditViewModel(object selected)
    {
        Agent agent = (Agent)selected;
        _name = agent.Name;
        _phoneNumber = agent.PhoneNum;
        _email = agent.Email;
        _principal = agent.Principal;
        _itn = agent.ITN;
        _address = agent.Address;
        _priority = agent.Priority;
        _checkpoint = agent.Checkpoint;
        _typeIdx = Context.AgentTypes.FindIndex(el => el.Name == agent.Type.Name);
        _image = agent.Logo;
        _id = agent.Id;
        _sales = agent.Sales;
        IsOnEdit = true;
        IsItHaveAnySales = _sales.Count > 0 ? true : false;
    }

    public void AddAgent()
    {
        MainWindowViewModel.Agents.Add(new Agent(_name, _phoneNumber, _email, _principal, _itn, _address, _priority, _checkpoint, _image, Context.AgentTypes[_typeIdx]));
    }
    
    public void SaveChanges()
    {
        MainWindowViewModel.Agents[_id].Name = _name;
        MainWindowViewModel.Agents[_id].PhoneNum = _phoneNumber;
        MainWindowViewModel.Agents[_id].Email = _email;
        MainWindowViewModel.Agents[_id].Principal = _principal;
        MainWindowViewModel.Agents[_id].ITN = _itn;
        MainWindowViewModel.Agents[_id].Address = _address;
        MainWindowViewModel.Agents[_id].Priority = _priority;
        MainWindowViewModel.Agents[_id].Checkpoint = _checkpoint;
        MainWindowViewModel.Agents[_id].Logo= _image;
        MainWindowViewModel.Agents[_id].Type = Context.AgentTypes[_typeIdx];
    }

    public void DeleteAgent()
    {
        Agent agentToDelete = new ObservableCollection<Agent>(MainWindowViewModel.Agents.Where(agent => agent.Id == _id))[0];
        MainWindowViewModel.Agents.Remove(agentToDelete);
    }
    
    public async void SetImage()
    {
        OpenFileDialog explorer = new OpenFileDialog();
        explorer.AllowMultiple = false;
        FileDialogFilter filter = new FileDialogFilter();
        filter.Name = "онли пнгшки";
        filter.Extensions.AddRange(new List<string>() {"jpg", "png", "jpeg"});
        explorer.Filters.Add(filter);
    
        string[] result = await explorer.ShowAsync(new AddOrEditWindow());
        
        if (result.Length > 0)
        {
            string imagePath = result[0];
            Bitmap imageSource = new Bitmap(imagePath);
            Image = imageSource;
            OnPropChanged(nameof(Image));
        }
    }


    public AddOrEditViewModel(bool isOnEdit)
    {
        IsOnEdit = isOnEdit;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
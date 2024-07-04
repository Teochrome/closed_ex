using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media.Imaging;

namespace DemoExamTraining2.Models;

public class Agent: INotifyPropertyChanged
{
    private string _name;
    private string _phoneNum;
    private string _email;
    private string _principal;
    private string _itn;
    private string _address;
    private static int _agentCount;
    private int _id;
    private uint _priority;
    private int _discount;
    private int _checkpoint;
    private AgentType _type;
    private Bitmap _logo;
    private ObservableCollection<Sale> _sales;

    public Agent(string name, string phoneNum, string email, string principal, string itn, string address, uint priority, int checkpoint, Bitmap logo, AgentType type)
    {
        _id = _agentCount;
        _name = name;
        _phoneNum = phoneNum;
        _email = email;
        _principal = principal;
        _itn = itn;
        _address = address;
        _priority = priority;
        _logo = logo;
        _type = type;
        Sales = new ObservableCollection<Sale>();
        _discount = 0;
        _checkpoint = checkpoint;
        _agentCount++;
    }
    
    public Agent(string name, string phoneNum, string email, string principal, string itn, string address, uint priority, int checkpoint, Bitmap logo, AgentType type, ObservableCollection<Sale> sales)
    {
        _name = name;
        _phoneNum = phoneNum;
        _email = email;
        _principal = principal;
        _itn = itn;
        _address = address;
        _priority = priority;
        _logo = logo;
        _type = type;
        _checkpoint = checkpoint;
        Sales = sales;
        _agentCount++;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropChanged(nameof(Name));
        }
    }
    
    public string PhoneNum
    {
        get => _phoneNum;
        set
        {
            _phoneNum = value;
            OnPropChanged(nameof(PhoneNum));
        }
    }
    
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropChanged(nameof(Email));
        }
    }
    
    public string Principal
    {
        get => _principal;
        set
        {
            _principal = value;
            OnPropChanged(nameof(Principal));
        }
    }
    
    public string ITN
    {
        get => _itn;
        set
        {
            _itn = value;
            OnPropChanged(nameof(ITN));
        }
    }
    
    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropChanged(nameof(Address));
        }
    }

    public int Id
    {
        get => _id;
    }

    public int Checkpoint
    {
        get => _checkpoint;
        set => _checkpoint = value;
    }

    public uint Priority
    {
        get => _priority;
        set
        {
            _priority = value;
            OnPropChanged(nameof(Priority));
        }
    }

    public int Discount
    {
        get => _discount;
        private set => _discount = value;
    }

    public AgentType Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropChanged(nameof(Type));
        }
    }

    public Bitmap Logo
    {
        get => _logo;
        set
        {
            _logo = value;
            OnPropChanged(nameof(Logo));
        }
    }

    public ObservableCollection<Sale> Sales
    {
        get => _sales;
        set
        {
            _sales = value;
            int _sum = _sales.Sum(order => order.OrderSum);
            _discount = 
                _sum > 10000 && _sum <= 50000 ? 5
                : _sum > 50000 && _sum <= 150000 ? 10
                : _sum > 150000 && _sum <= 500000 ? 20
                : _sum > 500000 ? 25 : 0;
            Discount = _discount;
            OnPropChanged(nameof(Sales));
            OnPropChanged(nameof(Discount));
        }
    }
}
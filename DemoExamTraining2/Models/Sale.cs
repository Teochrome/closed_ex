using System;

namespace DemoExamTraining2.Models;

public class Sale
{
    public DateTime SaleDate { get; set; }
    public string ProuctName { get; set; }

    public int OrderSum { get; set; }

    public Sale(string name, int orderSum)
    {
        ProuctName = name;
        SaleDate = DateTime.Today;
        OrderSum = orderSum;
    }
}
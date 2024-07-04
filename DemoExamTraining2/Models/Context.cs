using System.Collections.Generic;

namespace DemoExamTraining2.Models;

public class Context
{
    private static List<AgentType> _agentTypes = new()
    {
        new AgentType("Lv1_Crook"),
        new AgentType("Lv2_Agent"),
        new AgentType("Lv3_Agent"),
        new AgentType("Lv4_Agent")
    };
    
    public static List<AgentType> AgentTypes
    {
        get => _agentTypes;
    }
}
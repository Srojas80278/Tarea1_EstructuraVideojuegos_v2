using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    string _name;
    string _prize;

    public string getName()
    {
        return _name;
    }

    public void setName(string newName)
    {
        _name = newName;
    }

    public string getPrize()
    {
        return _prize;
    }
    public void setPrize(string newPrize)
    {
        _prize = newPrize;
    }
}

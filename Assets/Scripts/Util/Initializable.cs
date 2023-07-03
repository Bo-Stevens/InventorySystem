using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Initializable
{
    public static List<Initializable> ItemsToInitialze = new List<Initializable>();
    public abstract void Initialize();
    public Initializable()
    {
        ItemsToInitialze.Add(this);
    }
}

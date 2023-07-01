using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : Singleton
{
    public Util_AnimState AnimationStates;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

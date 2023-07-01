using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState {Idle, Run, Jump, Land, Fall}
public class Util_AnimState: ScriptableObject
{
    public string Idle, Walk, Jump, Land, Fall, Attack;
}

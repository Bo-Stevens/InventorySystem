using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState {Idle, Run, Jump, Land, Fall}
[CreateAssetMenu(fileName ="AnimationStates", menuName ="Animation/AnimationStates")]
public class Util_AnimState: ScriptableObject
{
    public string Idle, Walk, Jump, Land, Fall, Attack;
}

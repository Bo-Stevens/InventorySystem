using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsMovementManager : MonoBehaviour
{
    [Range(0,10f)] public float Gravity;
    public static ActorPhysicsMovementManager Instance;
    private void Awake()
    {
        Instance = this;
    }
}

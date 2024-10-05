using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    private ChaseBehavior chaseBehavior;

    void Start()
    {
        chaseBehavior = GetComponent<ChaseBehavior>();
        chaseBehavior.chaseSpeed = 1.5f; // Уменьшаем скорость для медленного врага
    }
}

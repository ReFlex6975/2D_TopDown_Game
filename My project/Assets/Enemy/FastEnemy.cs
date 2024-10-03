using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : MonoBehaviour
{
    private ChaseBehavior chaseBehavior;

    void Start()
    {
        chaseBehavior = GetComponent<ChaseBehavior>();
        chaseBehavior.chaseSpeed = 2.0f; // Увеличиваем скорость для быстрого врага
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ChaseBehavior chaseBehavior;

    void Start()
    {
        // Получаем компонент ChaseBehavior, чтобы задать цель преследования
        chaseBehavior = GetComponent<ChaseBehavior>();

        // Ищем игрока по тегу "Player" и назначаем его как цель
        chaseBehavior.target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}

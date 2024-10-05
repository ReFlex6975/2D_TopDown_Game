using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    public Transform target; // Цель (игрок)
    public float chaseSpeed = 3.0f; // Скорость преследования
    public float stopDistance = 10f; // Расстояние, на котором враг останавливается

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем SpriteRenderer
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        // Рассчитываем расстояние между врагом и целью
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Если враг дальше, чем stopDistance, то он будет двигаться к игроку
        if (distanceToTarget > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized; // Направление к цели

            // Двигаем врага в сторону игрока
            transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

            // Если враг движется влево (по оси X), поворачиваем спрайт влево
            if (direction.x < 0)
            {
                spriteRenderer.flipX = false; // Поворачиваем спрайт влево
            }
            // Если враг движется вправо (по оси X), поворачиваем спрайт вправо
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = true; // Поворачиваем спрайт вправо
            }
        }
    }
}

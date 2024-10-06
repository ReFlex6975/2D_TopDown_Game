using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("���� �����");
            Die(); // �������� ����� ������
        }
    }

    private void Die()
    {
        Destroy(gameObject); 
    }
    

    public Transform target; // ���� (�����)
    public float chaseSpeed = 3.0f; // �������� �������������
    public float stopDistance = 3f; // ����������, �� ������� ���� ���������������

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("����� � ����� 'Player' �� ������.");
        }
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        // ������������ ���������� ����� ������ � �����
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // ���� ���� ������, ��� stopDistance, �� �� ����� ��������� � ������
        if (distanceToTarget > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized; // ����������� � ����

            // ������� ����� � ������� ������
            transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

            // ���� ���� �������� ����� (�� ��� X), ������������ ������ �����
            if (direction.x < 0)
            {
                spriteRenderer.flipX = false; // ������������ ������ �����
            }
            // ���� ���� �������� ������ (�� ��� X), ������������ ������ ������
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = true; // ������������ ������ ������
            }
        }
    }
}

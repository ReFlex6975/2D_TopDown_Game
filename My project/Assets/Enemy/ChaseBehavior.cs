using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    public Transform target; // ���� (�����)
    public float chaseSpeed = 3.0f; // �������� �������������
    public float stopDistance = 2f; // ����������, �� ������� ���� ���������������

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
            transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
    }
}

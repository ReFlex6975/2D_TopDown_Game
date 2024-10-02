using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ChaseBehavior chaseBehavior;

    void Start()
    {
        // �������� ��������� ChaseBehavior, ����� ������ ���� �������������
        chaseBehavior = GetComponent<ChaseBehavior>();

        // ���� ������ �� ���� "Player" � ��������� ��� ��� ����
        chaseBehavior.target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Slider staminaSlider;
    public float runSpeed = 10;
    public float standartSpeed = 5;
    public float staminaValue = 5;
    public float currentSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ����������� ������������ �������� ��������
        staminaSlider.maxValue = 5;
        staminaSlider.value = staminaValue; // ��������� ��������
    }

    void FixedUpdate()
    {
        staminaSlider.value = staminaValue; // ��������� �������� ��������

        currentSpeed = standartSpeed;
        Stamina();

        // �������� ���� ������������ ��� ��������
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(currentSpeed * x, currentSpeed * y);
    }
    
    private void Stamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaValue > 0) // ������ ������� ��� ����
        {
            staminaValue -= Time.deltaTime;
            currentSpeed = runSpeed;
        }
        if (!(Input.GetKey(KeyCode.LeftShift) && staminaValue > 0)) // ����������� �������, ���� �� ������ ������� ����
        {
            staminaValue += Time.deltaTime;
            currentSpeed = standartSpeed;
        }
        // ����������� ������� �� 0 �� 5
        if (staminaValue >= 5) staminaValue = 5; 
        if (staminaValue <= 0) staminaValue = 0;
    }
}

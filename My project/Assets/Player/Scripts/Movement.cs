using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Slider staminaSlider;
    public float runSpeed = 10f;
    public float standartSpeed = 5f;
    public float staminaValue = 5f;
    public float currentSpeed;
    private Rigidbody2D rb;
    private Animator animator;

    public float minMovingSpeed = 0.1f;
    private bool isRun = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Настройка слайдера стамины
        staminaSlider.maxValue = 5f;
        staminaSlider.value = staminaValue;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        staminaSlider.value = staminaValue; // обновление значения слайдера

        // Определяем скорость
        currentSpeed = standartSpeed;
        Stamina();

        // Получаем ввод пользователя для движения
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(x, y).normalized;
        rb.velocity = new Vector2(currentSpeed * inputVector.x, currentSpeed * inputVector.y);

        // Проверяем, движется ли игрок и бегает ли он
        if (inputVector.magnitude > minMovingSpeed)
        {
            isRun = Input.GetKey(KeyCode.LeftShift) && staminaValue > 0;
        }
        else
        {
            isRun = false;
        }

        // Передаем параметр анимации
        animator.SetBool("IsRunning", isRun);
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

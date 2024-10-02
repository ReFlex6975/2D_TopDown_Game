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

        // Ќастраиваем максимальное значение ползунка
        staminaSlider.maxValue = 5;
        staminaSlider.value = staminaValue; // начальное значение
    }

    void FixedUpdate()
    {
        staminaSlider.value = staminaValue; // обновл€ем значение ползунка

        currentSpeed = standartSpeed;
        Stamina();

        // ѕолучаем ввод пользовател€ дл€ движени€
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(currentSpeed * x, currentSpeed * y);
    }
    
    private void Stamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaValue > 0) // расход стамины при беге
        {
            staminaValue -= Time.deltaTime;
            currentSpeed = runSpeed;
        }
        if (!(Input.GetKey(KeyCode.LeftShift) && staminaValue > 0)) // восполнение стамины, если не нажата клавиша бега
        {
            staminaValue += Time.deltaTime;
            currentSpeed = standartSpeed;
        }
        // ќграничение стамины от 0 до 5
        if (staminaValue >= 5) staminaValue = 5; 
        if (staminaValue <= 0) staminaValue = 0;
    }
}

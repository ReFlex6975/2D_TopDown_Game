using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Slider staminaSlider;
    public float runSpeed = 7f;
    public float standartSpeed = 4f;
    public float staminaValue = 5f;
    public float currentSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float minMovingSpeed = 0.1f;
    private bool isRun = false;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Настройка слайдера стамины
        staminaSlider.maxValue = 5f;
        staminaSlider.value = staminaValue;
    }

    void Update()
    {
        HandleAttack(); // Проверяем атаку независимо от состояния

        if (!isAttacking) // Только если не идёт атака, можно двигаться
        {
            HandleMovement();
        }
    }

    private void HandleAttack()
    {
        if (!isAttacking) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Атака начата!");
                isAttacking = true;
                animator.SetBool("IsAttacking", true);
                // Запускаем корутину для завершения атаки
                StartCoroutine(ResetAttack());
            }
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.85f); // Убедитесь, что время совпадает с длительностью анимации атаки
        Debug.Log("Атака завершена!");
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    private void FixedUpdate()
    {
        // Перемещение управляется в Update, но если вы хотите, вы можете переместить это сюда
        if (isAttacking == false) // Если атака не выполняется, разрешаем движение
        {
            HandleMovement();
        }
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

        if (x > 0) // Движение вправо
        {
            spriteRenderer.flipX = false;
        }
        else if (x < 0) // Движение влево
        {
            spriteRenderer.flipX = true;
        }

        // Проверяем, движется ли игрок и бегает ли он
        if (inputVector.magnitude > minMovingSpeed)
        {
            // isRun = Input.GetKey(KeyCode.LeftShift) && staminaValue > 0;
            isRun = true;
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

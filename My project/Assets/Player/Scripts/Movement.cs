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

    public GameObject fireballPrefab; // Префаб огненного шара
    public float fireballSpeed = 10f; // Скорость огненного шара

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

   
        staminaSlider.maxValue = 5f;
        staminaSlider.value = staminaValue;
    }

    void Update()
    {
        HandleAttack();
        HandleMagicAttack();
    }

    private void HandleMagicAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            Debug.Log("Атака магией начата!");
            isAttacking = true;
            animator.SetBool("IsCharge", true);

            // Выпускаем огненный шар
            

            StartCoroutine(ResetMagicAttack());
        }
    }

    public float fireballSpawnOffset = 1f; // Смещение от игрока для появления огненного шара

    private void ShootFireball()
    {
        // Получаем позицию курсора в мире
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Обнуляем ось Z, чтобы работать в 2D

        // Вычисляем направление от игрока к курсору
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Определяем позицию появления огненного шара с небольшим смещением от игрока
        Vector3 fireballSpawnPosition = transform.position + direction * fireballSpawnOffset;

        // Создаем огненный шар на этой позиции
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPosition, Quaternion.identity);

        // Задаем движение огненного шара в направлении курсора
        fireball.GetComponent<Rigidbody2D>().isKinematic = true; // Отключаем физику
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;

        Destroy(fireball, 30f);
    }

    private IEnumerator ResetMagicAttack()
    {

        yield return new WaitForSeconds(1f);
        ShootFireball();
        Debug.Log("Атака магией завершена!");
        isAttacking = false;

        animator.SetBool("IsCharge", false);

    }



    private void HandleAttack()
    {
       
        
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                Debug.Log("Атака начата!");
                isAttacking = true;
                animator.SetBool("IsAttacking", true);

            

                StartCoroutine(ResetAttack());
            }
       
    }

    private IEnumerator ResetAttack()
    {
        
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Атака завершена!");
        isAttacking = false;
        
        animator.SetBool("IsAttacking", false);
        
    }

    private void FixedUpdate()
    {
        
        HandleMovement();
    }

    private void HandleMovement()
    {
        staminaSlider.value = staminaValue;

        currentSpeed = standartSpeed;
        Stamina();

      
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

     
        if (inputVector.magnitude > minMovingSpeed)
        {
            // isRun = Input.GetKey(KeyCode.LeftShift) && staminaValue > 0;
            isRun = true;
        }
        else
        {
            isRun = false;
        }

      
        animator.SetBool("IsRunning", isRun);
    }

    private void Stamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaValue > 0) 
        {
            staminaValue -= Time.deltaTime;
            currentSpeed = runSpeed;
        }
        if (!(Input.GetKey(KeyCode.LeftShift) && staminaValue > 0)) 
        {
            staminaValue += Time.deltaTime;
            currentSpeed = standartSpeed;
        }
       
        if (staminaValue >= 5) staminaValue = 5; 
        if (staminaValue <= 0) staminaValue = 0;
    }
}

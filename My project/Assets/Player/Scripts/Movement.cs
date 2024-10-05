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
    private bool isDeath = false;
    private bool isTakedamage = false;
    private bool isInvincible = false;

    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;

    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;

    public float fireballSpawnOffset = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        staminaSlider.maxValue = 5f;
        staminaSlider.value = staminaValue;

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Персонаж погиб");
        isDeath = true;
        animator.SetBool("IsDeath", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(20f);
            Debug.Log("Персонаж получил урон");
            isTakedamage = true;
            animator.SetBool("IsTakedamage", true);

            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isTakedamage = false;
        animator.SetBool("IsTakedamage", false);
        isInvincible = false;
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

            StartCoroutine(ResetMagicAttack());
        }
    }

    private void ShootFireball()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        Vector3 fireballSpawnPosition = transform.position + direction * fireballSpawnOffset;

        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPosition, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().isKinematic = true;
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

        spriteRenderer.flipX = x < 0;

        isRun = inputVector.magnitude > minMovingSpeed;
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


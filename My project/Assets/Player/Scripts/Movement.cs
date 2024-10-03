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

   
        staminaSlider.maxValue = 5f;
        staminaSlider.value = staminaValue;
    }

    void Update()
    {
        HandleAttack();

    }

    public void MoveCharRight()
    {
        transform.position += new Vector3(2f, 0, 0);
        Debug.Log("1!");
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

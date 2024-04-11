using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cam;

    public float speed = 6f;
    public float turnSmooth = 0.1f;
    public float jumpHeight = 3f; // Jump height
    public float gravity = -9.81f; // Gravity
    public float dashDistance = 5f; // Dash distance
    public float dashDuration = 0.2f; // Dash duration
    public float dashCooldown = 1f; // Dash cooldown

    private float turnVelocity;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isDashing;
    private bool canDash = true;

     Animator animator;
    private void Start()
    {
        animator=GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("PosX", horizontal);
        animator.SetFloat("PosY", vertical);




        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
         
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        // Jump
        if (Input.GetKeyDown("space") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Dash
        if (Input.GetKeyDown("e") && !isDashing && canDash)
        {
            StartCoroutine(Dash());
        }

        // Apply gravity
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            characterController.Move(transform.forward * dashDistance * Time.deltaTime / dashDuration);
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        isDashing = false;
    }
}
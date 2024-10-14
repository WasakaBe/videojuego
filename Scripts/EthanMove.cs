using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanMove : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float speed = 5f; // Velocidad del personaje
    public float airSpeedFactor = 0.5f; // Factor de velocidad en el aire
    public float jumpForce = 5f; // Fuerza de salto

    // Variables para definir los límites del movimiento en X
    public float minX = -8.4f; // Límite izquierdo
    public float maxX = 8.4f;  // Límite derecho

    private bool isGrounded; // Para verificar si el personaje está en el suelo
    private bool jumpRequested = false; // Para saber si se ha presionado la tecla de salto
    public Transform groundCheck; // Punto para verificar el suelo
    public float groundCheckDistance = 1.8f; // Distancia del raycast hacia el suelo
    public LayerMask groundLayer; // Capa del suelo

    private Animator animator; // Referencia al componente Animator
    private bool facingRight = true; // Para controlar la dirección en la que mira el personaje

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Asignamos el Animator
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Verificamos si el personaje está en el suelo usando Raycast
        CheckIfGrounded();

        // Controlamos la animación en función del movimiento horizontal
        HandleAnimation();

        // Controlamos la rotación del personaje
        FlipCharacter();

        // Detectamos si se presionó la tecla SPACE para saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true; // Marcamos que se ha solicitado un salto
        }
    }

    private void FixedUpdate()
    {
        // Aplicamos una velocidad horizontal reducida si está en el aire
        float currentSpeed = isGrounded ? speed : speed * airSpeedFactor;

        // Aplicamos la velocidad horizontal
        Rigidbody2D.velocity = new Vector2(Horizontal * currentSpeed, Rigidbody2D.velocity.y);

        // Ejecutamos el salto si ha sido solicitado
        if (jumpRequested && isGrounded)
        {
            Jump();
            jumpRequested = false; // Reseteamos el salto una vez ejecutado
        }

        // Clampeamos la posición del personaje dentro de los límites definidos
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    // Método para realizar el salto
    private void Jump()
    {
        // Añadimos una fuerza vertical para el salto solo si está tocando el suelo
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);

        // Activamos la animación de salto solo cuando se presiona SPACE
        animator.SetBool("brincar_derecho", true);
        animator.SetBool("running_derecho", false); // Desactivamos correr al saltar
    }

    // Método para verificar si el personaje está en el suelo usando Raycast
    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null)
        {
            isGrounded = true;
            animator.SetBool("brincar_derecho", false); // Desactivar animación de salto cuando toca el suelo
        }
        else
        {
            isGrounded = false;
        }
    }

    // Método para controlar las animaciones según el movimiento
    private void HandleAnimation()
    {
        // Si se está moviendo a la derecha, activamos la animación de correr
        if (Horizontal != 0 && isGrounded)
        {
            animator.SetBool("running_derecho", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("running_derecho", false);
        }
    }

    // Método para voltear el personaje al cambiar de dirección
    private void FlipCharacter()
    {
        if (Horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (Horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    // Método para invertir el personaje en el eje X
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // Invertimos la escala en el eje X
        transform.localScale = scaler;
    }

    // Método para visualizar el Raycast en la vista de escena
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}

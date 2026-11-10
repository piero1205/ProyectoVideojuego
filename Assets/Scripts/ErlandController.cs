using UnityEngine;

public class ErlandController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 12f;

    public Animator animator;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool enSuelo = false;
    private float movimiento = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        movimiento = 0f;

        // Movimiento horizontal
        if (Input.GetKey(KeyCode.A))
        {
            movimiento = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movimiento = 1f;
        }

        // Orientacion del personaje
        if (movimiento < 0)
        {
            sprite.flipX = true;
        }

        if (movimiento > 0)
        {
            sprite.flipX = false;
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.W) && enSuelo)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                fuerzaSalto
            );

            enSuelo = false;

            animator.SetBool("IsJumping", true);
        }

        // Actualizar animaciones
        animator.SetBool("IsWalking", movimiento != 0 && enSuelo);
        animator.SetBool("IsJumping", !enSuelo);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimiento * velocidad,
            rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            enSuelo = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            enSuelo = false;
        }
    }
}



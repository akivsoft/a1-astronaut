using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float flySpeed = 3f;
    [SerializeField] private float downSpeed = 5f;

    [Header("Lives")]
    [SerializeField] private int maxLives = 3;

    private Rigidbody2D rb;

    private int currentLives;
    private bool isDead;

    public int CurrentLives => currentLives;
    public bool IsDead => isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // The player does not use gravity.
        rb.gravityScale = 0f;

        // Keep the player at a fixed horizontal position.
        rb.constraints =
            RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezeRotation;

        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;
    }

    private void Start()
    {
        currentLives = GameModeSettings.HasLives ? maxLives : 0;
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        // TAP/HOLD = DOWN
        // NO INPUT = UP
        float verticalSpeed;

        if (IsInputHeld())
        {
            verticalSpeed = -downSpeed;
        }
        else
        {
            verticalSpeed = flySpeed;
        }

        Vector2 newPosition = rb.position;

        newPosition.y += verticalSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }

    private bool IsInputHeld()
    {
        // Mouse / PC
        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed)
        {
            return true;
        }

        // Touch / Mobile
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            return true;
        }

        return false;
    }

    public void TakeDamage(int damage = 1)
    {
        if (isDead)
            return;

        if (!GameModeSettings.HasLives)
        {
            Die();
            return;
        }

        currentLives -= damage;
        currentLives = Mathf.Max(currentLives, 0);

        Debug.Log(
            "Player lost a life. Remaining lives: " +
            currentLives
        );

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;

        Debug.Log("GAME OVER");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
            return;

        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }
}
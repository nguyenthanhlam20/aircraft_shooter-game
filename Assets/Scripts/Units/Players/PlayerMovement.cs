using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    private Rigidbody2D rb;
    private Player player;
    [SerializeField] private Vector2 moveInput;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input) => moveInput = input.Get<Vector2>();
   

    void Update()
    {
        if (!player.IsDead)
        {
            Vector2 newPosition;
            if (moveInput != Vector2.zero)
            {
                newPosition = rb.position + moveInput.normalized * player.Speed * Time.fixedDeltaTime;
            }
            else
            {
                newPosition = rb.position + new Vector2(joystick.Horizontal * player.Speed * Time.fixedDeltaTime, joystick.Vertical * player.Speed * Time.fixedDeltaTime);
            }


            // Define the boundaries of the screen in world coordinates
            float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float screenHeight = Camera.main.orthographicSize;

            // Calculate the clamped position
            newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth, screenWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight, screenHeight);

            rb.MovePosition(newPosition);
        }
    }

}

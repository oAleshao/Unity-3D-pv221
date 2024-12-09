using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{

    private InputAction moveAction;
    private CharacterController characterController;
    private float speedFactor;
    private bool isMoving = false;
    private Animator animator;
    [SerializeField]
    private ParticleSystem effects;

    private float burstPeriod = 5f;
    private float burstLeft;
    public float burstLevel => burstLeft / burstPeriod;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GameState.AddListener(nameof(GameState.isBurst), OnBurstChanged);
        effects.Stop();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
        }
        if (isMoving)
        {

            bool pressedShift = checkShift();
            if (pressedShift)
            {
                speedFactor = 15f;
            }
            else
            {
                speedFactor = 5f;
            }

            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector3 move = Camera.main.transform.forward; // Напрям погляду камери
            move.y = 0.0f; // Проєктуємо на горизонтальну площину
            if (move == Vector3.zero) // Вектор був вертикальним (погляд вниз) вісь Y
            {
                move = Camera.main.transform.up;
            }
            // У даному місці - напрям постійного руху (польоту)
            // Додаємо до нього управління, яке теж орієнтовано по камері
            move.Normalize(); // Видовжуємо вектор після проєктування

            Vector3 moveForward = move;
            move += moveValue.x * Camera.main.transform.right;
            move.y = moveValue.y;
            move.y -= 10f * Time.deltaTime;
            Vector3 countedMove = speedFactor * Time.deltaTime * move;

            if (this.transform.position.y -
               Terrain.activeTerrain.SampleHeight(this.transform.position) > 1.5f)
            {
                animator.SetInteger("MoveState", 2);
                if (!pressedShift)
                {
                    speedFactor = 10f;
                }
            }
            else
            {
                animator.SetInteger("MoveState", 1);
            }

            if (burstLeft != 0)
            {
                speedFactor = 50f;
            }

            characterController.Move(speedFactor * Time.deltaTime * move);
            this.transform.forward = moveForward; // Повертаємо персонажа у напряму руху

        }
        else
        {
            animator.SetInteger("MoveState", 0);
        }

    }

    private void LateUpdate()
    {
        if (burstLeft > 0f)
        {
            burstLeft -= Time.deltaTime;
            if (burstLeft <= 0f)
            {
                burstLeft = 0f;
                GameState.isBurst = false;
                effects.Stop();
            }

        }
    }


    bool checkShift()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            return true;
        return false;
    }

    private void OnBurstChanged(string ignored)
    {
        if (GameState.isBurst)
        {
            burstLeft = burstPeriod;
            Debug.Log("START");
            effects.Play();
        }
    }

    private void OnDestroy()
    {
        GameState.RemoveListener(nameof(GameState.isBurst), OnBurstChanged);
    }


}

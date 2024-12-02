using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    public bool gotPepper;
    private float pepperTime;

    private InputAction moveAction;
    private CharacterController characterController;
    private float speedFactor;
    private bool isMoving = false;
    private Animator animator;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        pepperTime = 5f;
        gotPepper = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
        }
        if (isMoving)
        {
            if (pepperTime <= 0)
            {
                pepperTime = 5f;
                gotPepper = false;
            }

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
            Vector3 move = Camera.main.transform.forward; // ������ ������� ������
            move.y = 0.0f; // �������� �� ������������� �������
            if (move == Vector3.zero) // ������ ��� ������������ (������ ����) ��� Y
            {
                move = Camera.main.transform.up;
            }
            // � ������ ���� - ������ ��������� ���� (�������)
            // ������ �� ����� ���������, ��� ��� ��������� �� �����
            move.Normalize(); // ��������� ������ ���� �����������

            Vector3 moveForward = move;
            move += moveValue.x * Camera.main.transform.right;
            move.y = moveValue.y;
            move.y -= 50f * Time.deltaTime;
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

            if (gotPepper)
            {
                speedFactor = 50f;
                pepperTime -= Time.deltaTime;
            }

            characterController.Move(speedFactor * Time.deltaTime * move);
            this.transform.forward = moveForward; // ��������� ��������� � ������� ����

        }
        else
        {
            animator.SetInteger("MoveState", 0);
        }

    }


    bool checkShift()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            return true;
        return false;
    }


}

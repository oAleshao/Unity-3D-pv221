using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private GameObject character;
    private Vector3 distanceToCharacter;
    private InputAction look;
    private float angleH, angleH0; // накопичений кут повороту камери по горизонталі
    private float angleV, angleV0; // -- .. -- по вертикалі
   // private float sensitivity;

    void Start()
    {
        //sensitivity = 0.05f;
        look = InputSystem.actions.FindAction("Look");
        character = GameObject.Find("Character");
        distanceToCharacter = this.transform.position - character.transform.position;
        angleH = angleH0 = this.transform.eulerAngles.y;
        angleV = angleV0 = this.transform.eulerAngles.x;
    }

    void Update()
    {
        Vector2 lookValue = look.ReadValue<Vector2>();

        var tmpValue = 3f * Time.deltaTime;

        angleH += lookValue.x * tmpValue;
        if (0 < angleV - lookValue.y * tmpValue && angleV - lookValue.y * tmpValue < 90)
        {
            angleV -= lookValue.y * tmpValue;
        }
        this.transform.eulerAngles = new Vector3(angleV, angleH, 0f);
        this.transform.position = character.transform.position + Quaternion.Euler(angleV - angleV0, angleH - angleH0, 0f) * distanceToCharacter;
    }
}


using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    private float horizontalInput;
    private float verticalInput;
    private float currentHorizontal;
    private float currentVertical;
    private float unusedCurrentVelocity1;
    private float unusedCurrentVelocity2;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !GameManager.Instance.IsPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        currentHorizontal = horizontalInput;
        currentVertical = verticalInput;

        if(!GameManager.Instance.IsPaused)
        {
            playerMovement.SetNextCameraRotation(mouseX, mouseY);
            playerMovement.SetNextPlayerPosition(currentHorizontal, currentVertical);
        }
    }
}
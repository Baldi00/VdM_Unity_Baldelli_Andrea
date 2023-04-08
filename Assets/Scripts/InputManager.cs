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
    private bool isPaused;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !isPaused)
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

        if(!isPaused)
        {
            playerMovement.SetNextCameraRotation(mouseX, mouseY);
            playerMovement.SetNextPlayerPosition(currentHorizontal, currentVertical);
        }
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
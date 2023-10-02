using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour {

    [DllImport("__Internal")]
    public static extern void SyncCameraTransform(float x, float y, float z, float rw, float rx, float ry, float rz);

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start() {
        this.characterController = this.GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
#if UNITY_EDITOR 
        this.ProcessInput();
#endif
    }

    private void ProcessInput() {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = this.transform.TransformDirection(Vector3.forward);
        Vector3 right = this.transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = this.canMove ? (isRunning ? this.runningSpeed : this.walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = this.canMove ? (isRunning ? this.runningSpeed : this.walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = this.moveDirection.y;
        this.moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && this.canMove && this.characterController.isGrounded) {
            this.moveDirection.y = this.jumpSpeed;
        }
        else {
            this.moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (! this.characterController.isGrounded) {
            this.moveDirection.y -= this.gravity * Time.deltaTime;
        }

        // Move the controller
        this.characterController.Move(this.moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (this.canMove) {
            this.rotationX += -Input.GetAxis("Mouse Y") * this.lookSpeed;
            this.rotationX = Mathf.Clamp(this.rotationX, -this.lookXLimit, this.lookXLimit);
            this.playerCamera.transform.localRotation = Quaternion.Euler(this.rotationX, 0, 0);
            this.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * this.lookSpeed, 0);
        }

        SC_FPSController.SyncCameraTransform(this.playerCamera.transform.position.x, this.playerCamera.transform.position.y, this.playerCamera.transform.position.z,
            this.playerCamera.transform.rotation.w, this.playerCamera.transform.rotation.x, this.playerCamera.transform.rotation.y, this.playerCamera.transform.rotation.z);
    }
}

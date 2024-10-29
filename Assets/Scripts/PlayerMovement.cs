using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movespeed;
    [Space]
    [SerializeField] private float lookAngleLimit;
    [SerializeField] private float mouseSensX;
    [SerializeField] private float mouseSensY;    
    private float _currentRotationX = 0;

    [Header("References")]
    [SerializeField] private Transform playerCamera;
    private Rigidbody _rb;

    // Internal
    private const string VERTICAL_AXIS = "Vertical";
    private const string HORIZONTAL_AXIS = "Horizontal";    
    private const string MOUSE_X = "Mouse X";   
    private const string MOUSE_Y = "Mouse Y";   

    private void Awake(){
        _rb = GetComponent<Rigidbody>();
    }

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        HandleMovement();
        HandleRotation();
    }

    private Vector2 GetInputNormalized(){
        float vertical = Input.GetAxisRaw(VERTICAL_AXIS);
        float horizontal = Input.GetAxisRaw(HORIZONTAL_AXIS);
        Vector2 input = new Vector2(horizontal, vertical);
        return input.normalized;
    }

    private void HandleMovement(){
        Vector2 input = GetInputNormalized();
        Vector3 newVelocity = new Vector3(input.x * movespeed, _rb.velocity.y, input.y * movespeed);
        _rb.velocity = transform.TransformDirection(newVelocity);
    }

    private void HandleRotation(){
        float mouseX = Input.GetAxis(MOUSE_X);
        float mouseY = Input.GetAxis(MOUSE_Y);

        // Horizontal rotation
        transform.Rotate(transform.up, mouseX * mouseSensX * Time.deltaTime);

        // Vertical rotation
        _currentRotationX -= mouseY * mouseSensY * Time.deltaTime;
        _currentRotationX = Mathf.Clamp(_currentRotationX, -lookAngleLimit, lookAngleLimit);
        playerCamera.localRotation = Quaternion.Euler(_currentRotationX, 0, 0);
    }
}

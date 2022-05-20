using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] float moveSpeed, gravityModifier, jumpPower, runSpeed = 12;
    public CharacterController charController;

    private Vector3 moveInput;

    public Transform cameraTransform;
    public float mouseSensitivity;
    public bool invertX, invertY;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; 

        //Store Y velocity
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize(); // Normalize cause otherwise Diagonal movement will be faster slightly
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        moveInput = moveInput * moveSpeed;

        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

        if (canJump)
        {
            canDoubleJump = false;
        }

        //Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        } else if(canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        charController.Move(moveInput * Time.deltaTime);

        //Control Camera Rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));


        //Handle Shooting
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 500f))
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward *500f, Color.red);
                if(Vector3.Distance(cameraTransform.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                    
                }
                 
            }
            else
            {
                firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 300f));
                
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        //Animator Control
        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("onGround", canJump);
    }
}

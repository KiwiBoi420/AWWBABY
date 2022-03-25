using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Transform debugHitPointTransform;
    [SerializeField] private Transform hookshotTransform;

    public float NORMAL_FOV = 70f; //HERE IS THE STARTING POINT
    private  float HOOKSHOT_FOV = 120f;
    private  float SPRINT_FOV = 80f;
    private  float DASH_FOV = 90f;
    
    public CharacterController controller;

    public float speed = 15f;
    public float gravity = -29.43f;
    public float jumpHeight = 4f;
    public float sprintSpeed = 30f;
    public float hookshotDistanceReach = 100f;
    public float hookshotDistanceMax = 100f;

    public float dashdistance = 100f;
    public float cooldownTime = 1.5f;
    private float nextFireTime = 0;

  

    private Vector3 hookshotPosition;
    public Vector3 characterVelocityMomentum;
    private float hookshotSize;
    private CameraFov cameraFov;

    private State state;
    public GameObject playerScript; //HERE

    private enum State
    {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer,
        

    }

    public Camera playerCamera;
    public Transform groundCheck;
    
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    

    public Vector3 velocity;
    public bool isGrounded;
    public bool canDoubleJump = false;


    public void AdjustFOV(float newFOV)
    {
        NORMAL_FOV = newFOV;
        cameraFov.SetCameraFov(NORMAL_FOV);
        HOOKSHOT_FOV = NORMAL_FOV + 40f;
     SPRINT_FOV = NORMAL_FOV + 20f;
    DASH_FOV = NORMAL_FOV + 30f;
    
    }
        private void Awake()
    {
        
        state = State.Normal;
        cameraFov = playerCamera.GetComponent<CameraFov>();
        hookshotTransform.gameObject.SetActive(false);

    }

   

    // Update is called once per frame
    void Update()
    {
        
       

        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                break;
            case State.HookshotThrown:
                HandleHookshotThrow();

                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                
                break;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {

                gravity = 0f;
                cameraFov.SetCameraFov(DASH_FOV);
                characterVelocityMomentum += Camera.main.transform.forward * dashdistance;
                nextFireTime = Time.time + cooldownTime;

                gravity = -29.43f;
                
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            cameraFov.SetCameraFov(NORMAL_FOV);

        }
        if (state == State.HookshotFlyingPlayer)
        {
            float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
            float hookshotSpeedMultiplier = 5f;
            hookshotSize -= (hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime) * 3;

        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {

            velocity.y = -8f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
           

                speed = sprintSpeed;
            cameraFov.SetCameraFov(SPRINT_FOV);


        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
           
            speed = 15f;
            cameraFov.SetCameraFov(NORMAL_FOV);


        }



       


        

        if (isGrounded)
        {
            
            canDoubleJump = true;
            if (TestInputJump())
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                
                
            }
          
               

            
        }
        if (TestInputJump() && canDoubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight*2 * -2f * gravity);
            canDoubleJump = false;
           
        }

        //Apply Momentum
        velocity += characterVelocityMomentum;
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Dampen Momentum
        if(characterVelocityMomentum.magnitude >= 0f)
        {

            float momentumDrag = 2.5f;
            velocity -= characterVelocityMomentum;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if(characterVelocityMomentum.magnitude < .0f)
            {

                characterVelocityMomentum = Vector3.zero;
                
            }
        }




    }



    private void ResetGravityEffect()
    {
        velocity.y = -5f;


    }

    private void HandleHookshotStart()
    {

        


        if (TestInputDownHookShot())
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit)){
                //Hit something

                if (raycastHit.distance <= hookshotDistanceReach)
                {
                    debugHitPointTransform.position = raycastHit.point;
                    hookshotPosition = raycastHit.point;
                    hookshotSize = 0f;
                    hookshotTransform.gameObject.SetActive(true);
                    hookshotTransform.localScale = Vector3.zero;
                    state = State.HookshotThrown;
                }
                
            }

        }

    }

    private void HandleHookshotThrow()
    {
        hookshotTransform.LookAt(hookshotPosition);


        float hookshotThrowSpeed = 90f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        if (hookshotSize >= Vector3.Distance(transform.position, hookshotPosition))
        {
            state = State.HookshotFlyingPlayer;
            cameraFov.SetCameraFov(HOOKSHOT_FOV);
        }
    }

    private void HandleHookshotMovement()
    {
        hookshotTransform.LookAt(hookshotPosition);
        gravity = -0.1f;
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;
        
        //float hookshotSpeedMin = 20f;
        //float hookshotSpeedMax = 70f;
        float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        float hookshotSpeedMultiplier = 5f;
        //Mathf.Clamp(Vector3.Distance(transform.position,hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);

        //Move Character Controller
        controller.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);
        
        

        float reachedHookshotPositionDistance = 5f;
        if(Vector3.Distance(transform.position, hookshotPosition) <= reachedHookshotPositionDistance){
            //reached Hookshot Position
            hookshotTransform.gameObject.SetActive(false);
            state = State.Normal;
            ResetGravityEffect();
            gravity = -29.43f;
            cameraFov.SetCameraFov(NORMAL_FOV);
        }

        if (TestInputDownHookShot())
        {

            StopHookshot();
            cameraFov.SetCameraFov(NORMAL_FOV);
        }

        if (TestInputJump())
        {
            //Cancelled With Jump
            float momentumExtraSpeed = 4f;
            characterVelocityMomentum -= velocity - (hookshotDir * hookshotSpeed * momentumExtraSpeed) / 4;
            float jumpSpeed = 30f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;
            characterVelocityMomentum += transform.forward * jumpSpeed * 3;
            state = State.Normal;
            hookshotTransform.gameObject.SetActive(false);
            cameraFov.SetCameraFov(NORMAL_FOV);
            ResetGravityEffect();
            gravity = -29.43f;

        }

    }

private void StopHookshot()
    {

        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;
        float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
        //Cancel Hookshot
        float momentumExtraSpeed = 4f;
        hookshotTransform.gameObject.SetActive(false);
        state = State.Normal;
        //float jumpSpeed = 30f;
        characterVelocityMomentum -= velocity - (hookshotDir * hookshotSpeed * momentumExtraSpeed) / 4;
        //characterVelocityMomentum += transform.forward * jumpSpeed * 3;
        //characterVelocityMomentum += (Vector3.up * jumpSpeed)/5;
        ResetGravityEffect();
        gravity = -29.43f;
        cameraFov.SetCameraFov(NORMAL_FOV);
       

    }

    private bool TestInputDownHookShot()
    {
        return Input.GetKeyDown(KeyCode.E);
        
    }

    private bool TestInputJump()
 {
        return Input.GetKeyDown(KeyCode.Space);

    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SOMETIMES RAYCASTS HITTING WALLS EVEN AFTER YOU RUN OFF THE WALL, ALLOWING YOU TO HAVE 0 Y VELOCITY AND JUST RUN MIDAIR
//SOMETIMES RAYCASTS HITTING WALLS EVEN AFTER YOU RUN OFF THE WALL, ALLOWING YOU TO HAVE 0 Y VELOCITY AND JUST RUN MIDAIR
//SOMETIMES RAYCASTS HITTING WALLS EVEN AFTER YOU RUN OFF THE WALL, ALLOWING YOU TO HAVE 0 Y VELOCITY AND JUST RUN MIDAIR
//SOMETIMES RAYCASTS HITTING WALLS EVEN AFTER YOU RUN OFF THE WALL, ALLOWING YOU TO HAVE 0 Y VELOCITY AND JUST RUN MIDAIR
//SOMETIMES RAYCASTS HITTING WALLS EVEN AFTER YOU RUN OFF THE WALL, ALLOWING YOU TO HAVE 0 Y VELOCITY AND JUST RUN MIDAIR

public class WallRun : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Wall Running")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;
    [SerializeField] private float wallRunJumpForce;

    public GameObject player;

    private bool wallLeft = false;
    private bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;


    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {

                StartWallRun();
                Debug.Log("wall running on the left");
            }
            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on the right");
            }
            else
            {
                StopWallRun();

            }
        }
        else
        {
            StopWallRun();

        }
    }

    void StartWallRun()
    {
        PlayerMovement playerMoveScript = player.GetComponent<PlayerMovement>();

        playerMoveScript.velocity.y = 0f;
        playerMoveScript.gravity = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (wallLeft)
            {

                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
             
                playerMoveScript.characterVelocityMomentum += (wallRunJumpDirection * wallRunJumpForce * 25);
               
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                
                playerMoveScript.characterVelocityMomentum += (wallRunJumpDirection * wallRunJumpForce * 25);
               
            }

        }

    }

    void StopWallRun()
    {
        PlayerMovement playerMoveScript = player.GetComponent<PlayerMovement>();
        playerMoveScript.gravity = -29.43f;
        

    } 

}
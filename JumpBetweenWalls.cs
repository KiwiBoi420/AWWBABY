using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
//IGNORE THIS I JUST USED THIS SCRIPT FILE TO SAVE SOME CODE JUST IN CASE SOMETHING WENT WRONG WITH WALLRUN SCRIPT 
public class JumpBetweenWalls : MonoBehaviour
{

    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float walldistance = 0.5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    public bool wallRight = false;
    public bool wallLeft = false;
    public GameObject playerScript;
    public float jumpDistance = 20f;
    bool CanWallRun()
    {

        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);

    }

    void CheckWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, walldistance);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, walldistance);
    }

    private void Update()
    {
        PlayerMovement playerMoveScript = playerScript.GetComponent<PlayerMovement>();

        CheckWall();
        if (CanWallRun())
        {

            if (wallRight)
            {

                playerMoveScript.gravity = -4f;
                if (Input.GetKeyDown(KeyCode.Space))
                {


                    playerMoveScript.characterVelocityMomentum -= Camera.main.transform.right * jumpDistance;

                    print(playerMoveScript.characterVelocityMomentum + "jumped from right wall");
                    playerMoveScript.gravity = -29.43f;


                }





            }
            else if (wallLeft)
            {
                playerMoveScript.gravity = -4f;
                if (Input.GetKeyDown(KeyCode.Space))
                {



                    playerMoveScript.characterVelocityMomentum += Camera.main.transform.right * jumpDistance;

                    print(playerMoveScript.characterVelocityMomentum + "jumped from left wall");

                }

            }
            else
            {
                playerMoveScript.gravity = -29.43f;

            }

            if (!wallRight && !wallLeft)
            {
                playerMoveScript.gravity = -29.43f;


            }

        }

    }
}

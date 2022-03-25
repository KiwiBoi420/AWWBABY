using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SWORDSLASH : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            animator.SetBool("isclick", true);
        
        }else if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            animator.SetBool("isclick", false);

        }
    }
}

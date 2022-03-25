using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivityDisplay : MonoBehaviour
{
   
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI ValueTextY;
    public TextMeshProUGUI ValueFOV;
    public GameObject thePlayer;
    public GameObject thePlayeractually;

    void Start()
    {
        MouseLook playerScript = thePlayer.GetComponent<MouseLook>();
        PlayerMovement playerMoveScript = thePlayeractually.GetComponent<PlayerMovement>();

        ValueText.text = playerScript.mouseSensX.ToString();
        ValueTextY.text = playerScript.mouseSensY.ToString();
        ValueFOV.text = playerMoveScript.NORMAL_FOV.ToString();
    }

    void Update()
    {
        
        MouseLook playerScript = thePlayer.GetComponent<MouseLook>();
        PlayerMovement playerMoveScript = thePlayeractually.GetComponent<PlayerMovement>();

        ValueText.text = playerScript.mouseSensX.ToString();
        ValueTextY.text = playerScript.mouseSensY.ToString();
        ValueFOV.text = playerMoveScript.NORMAL_FOV.ToString();

    }
}



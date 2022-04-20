using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class controllerBehaviour : MonoBehaviour
{
    private InputDevice leftController, rightController;
    public GameObject tablette;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller,devices);
        rightController = devices[0];
        leftController = devices[1];

        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //faire apparaître la tablette devant le joueur
        if ( rightController.TryGetFeatureValue(CommonUsages.primaryButton,out bool xButtonPressed) && xButtonPressed)
        {
            tablette.transform.position = player.transform.position + player.transform.forward - 0.2f * player.transform.right - 0.2f * player.transform.up;
            tablette.transform.rotation = player.transform.rotation;
        }
            
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SwitchMovementMode : MonoBehaviour
{
    public List<MonoBehaviour> ScriptsTP = new List<MonoBehaviour>();
    public List<MonoBehaviour> ScriptsContinuousMovement = new List<MonoBehaviour>();

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    private int selected = 1; //-1 = Mouvement Continu || 1 = Teleportation
    private bool hasBeenPressed;

    private GameObject VRcam;

    
    

	private void Start()
	{
        //Obtenir la main souhaitée 
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);
        targetDevice = devices[0];

        TeleportMovementActivate();
        hasBeenPressed = false;

        VRcam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

	// Update is called once per frame
	void Update()
    {
        if( targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton,out bool buttonPressed) && buttonPressed)
        {       
       
            if (!hasBeenPressed )
            {
                hasBeenPressed = true;
                Debug.Log("Button pressed");
                selected *= -1;
                if ( selected == 1 )
                    TeleportMovementActivate();
                else
                    ContinuousMovementActivate();
            }
            
        }
        if ( targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton,out bool buttonPressed2) && !buttonPressed2 )
        {
            hasBeenPressed = false;
        }


    }

    public void ContinuousMovementActivate()
    {
        GetComponent<LineRenderer>().enabled = false;
        foreach ( var sc in ScriptsTP )
        {
            sc.enabled = false;
        }

        foreach(var sc in ScriptsContinuousMovement)
        {
            sc.enabled = true;
        }
    }

    void TeleportMovementActivate()
    {
        foreach ( var sc in ScriptsContinuousMovement )
        {
            sc.enabled = false;
        }

        GetComponent<LineRenderer>().enabled = true;
        foreach ( var sc in ScriptsTP )
        {
            sc.enabled = true;
        }
        
    }
       
}

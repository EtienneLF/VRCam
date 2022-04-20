using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraScrolling : MonoBehaviour
{
    
    float scrollSpeed = 0.2f;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice rightController;
    
    GameObject fiche;
    bool canScroll;
	// Start is called before the first frame update

	private void Start()
	{
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);
        rightController = devices[0];
    }
	// Update is called once per frame
	void Update()
    {
        if ( canScroll )
        {
            rightController.TryGetFeatureValue(CommonUsages.primary2DAxis,out Vector2 scrollValue);
            gameObject.GetComponent<Transform>().position += new Vector3(0,(scrollSpeed * scrollValue[1]),0);
        }
    }

    public void enableScrolling()
    {
        canScroll = true;
    }

    public void disableScrolling()
    {
        canScroll = false;
    }
}

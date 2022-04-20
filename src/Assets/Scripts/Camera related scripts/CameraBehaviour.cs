using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
using TMPro;

public class CameraBehaviour : MonoBehaviour
{
    GameObject selectedCamera;
    bool aHasBeenReleased;
    bool inCam = false;

    public InputDeviceCharacteristics controllerCharacteristics;
    public CameraManager cameraManager;
    public RenderTexture textureGardien;
     
    
    int maxCamRotationSpeed = 3;
    float maxCamTranslationSpeed = 0.1f;

    private InputDevice rightController;
    private GameObject mainCam;
    private GameObject activeCamera;
    private GameObject textGameObject;


    void Start()
    {
        cameraManager = CameraManager.instance;
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);
        rightController = devices[0];

        mainCam = GameObject.FindGameObjectsWithTag("MainCamera")[0];

        textGameObject = GameObject.FindGameObjectsWithTag("textCamDisplay")[0];
    }

    // Update is called once per frame
    void Update()
    {
        //detection bouton A quand on est dans une cam pour en sortir
        if ( rightController.TryGetFeatureValue(CommonUsages.primaryButton,out bool AbuttonPressed) && AbuttonPressed )
        {
            if ( aHasBeenReleased ) //Pour éviter le switch à chaque ms d'appui sur A
            {
                if ( inCam)
                {
                    Debug.Log("Appui while in cam");
                    switchToMainCam();

                }
                else if ( !inCam && selectedCamera != null )
                {
                    Debug.Log("Appui NOT IN CAM");
                    switchToSelectedCam();
                }

                aHasBeenReleased = false; //Bouton A appuyé
            }
            
        }

        //Detection bouton A relaché 
        else
        {
            aHasBeenReleased = true;
        }
        //detection bouton B
        if ( rightController.TryGetFeatureValue(CommonUsages.secondaryButton,out bool BbuttonPressed) && BbuttonPressed )
        {
            Debug.Log(getCurrentSelectedCamera());

            if ( getCurrentSelectedCamera() != null )
            {
                cameraManager.delCam(getCurrentSelectedCamera());
                selectedCamera = null;
            }
        }

        if ( inCam ) //Si on est dans une caméra, on doit être capable de lui faire faire des rotations et variations de hauteur.
        {
            MoveSelectedCam();
        }
    }

    /**
     * Cette fonction est appelée lorsque qu'on survole (HOVER ENTER) une caméra
     **/
    public void setCurrentSelectedCamera( Transform camTransform = null)
    {
        selectedCamera = camTransform.gameObject; //On a récupéré le gameObject

        //Spawn Text infos sur les coordonnées de la caméra
        string textToDisplay = "Hauteur : " + System.Math.Round(selectedCamera.transform.position.y,2).ToString() + " m";
        textGameObject.transform.position = selectedCamera.transform.position + selectedCamera.transform.up * -0.5f;
        textGameObject.transform.LookAt(2* textGameObject.transform.position - mainCam.transform.position);

        TMP_Text tmp_text = textGameObject.GetComponent<TMP_Text>();
        tmp_text.text = textToDisplay;

    }

    /**
     * Cette fonction est appelée lorsque qu'on DESELECTIONNE (HOVER EXIT) une caméra
     **/
    public void delCurrentSelectedCamera() 
    {
        selectedCamera = null;

        TMP_Text tmp_text = textGameObject.GetComponent<TMP_Text>();
        tmp_text.text = "";

    } 

    GameObject getCurrentSelectedCamera()
    {
        return selectedCamera;
    }


    public void switchToMainCam()
    {
        Debug.Log("CAM PRINCIPALE");

        //On rallume la cam principale        
        mainCam.GetComponent<Camera>().enabled = true;

       
        //On eteint la cam qui était selectionnée
        activeCamera.transform.GetChild(0).GetComponentInChildren<Camera>().enabled = false;

        cameraManager.refreshCam();
        inCam = false;
    }

    public void switchToSelectedCam()
    {

        activeCamera = getCurrentSelectedCamera();
        Debug.Log("CAM SELECTIONNNEE");
        //On allume la cam selectionnée        
        getCurrentSelectedCamera().transform.GetChild(0).GetComponentInChildren<Camera>().enabled = true;


        //eteint la maincam
        mainCam.GetComponent<Camera>().enabled = false;

        //on eteint la maincam et On incarne la caméra selectionnée
        cameraManager.refreshCam();

        inCam = true;
    }

    //Fonction appelée lorsqu'on est dans une cam
    void MoveSelectedCam()
    {
        //Right CONTROLLER, Joystick pour bouger la caméra, triggers pour faire monter ou descendre
        Vector3 toOffset= new Vector3(0,0,0);

        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis,out Vector2 lJoystickMovement); //Renvoie un Vector2 : [X,Y] avec -1 < x|y < 1

        //Horizontal
        if ( lJoystickMovement[0] < 0 )      toOffset.y = -1 * Mathf.Lerp(0,maxCamRotationSpeed, -lJoystickMovement[0]);
        else if ( lJoystickMovement[0] > 0 ) toOffset.y = Mathf.Lerp(0,maxCamRotationSpeed, lJoystickMovement[0]);

        //Vertical
        if ( lJoystickMovement[1] < 0 )      toOffset.x = Mathf.Lerp(0,maxCamRotationSpeed, -lJoystickMovement[1]);
        else if ( lJoystickMovement[1] > 0 ) toOffset.x = -1 * Mathf.Lerp(0,maxCamRotationSpeed, lJoystickMovement[1]);

        //Rotation
        activeCamera.transform.Rotate(toOffset);

        //Pour garder la caméra toujours droite
        Vector3 eulerRotation = activeCamera.transform.rotation.eulerAngles;
        activeCamera.transform.rotation = Quaternion.Euler(eulerRotation.x,eulerRotation.y,0);

        //Trigger pour monter, grip pour descendre        
        rightController.TryGetFeatureValue(CommonUsages.trigger,out float triggerValue); 
        rightController.TryGetFeatureValue(CommonUsages.grip,out float gripValue);

        float altitudeChange = (triggerValue * maxCamTranslationSpeed) - (gripValue * maxCamTranslationSpeed);
        activeCamera.transform.position += new Vector3(0,altitudeChange,0);


    }


}

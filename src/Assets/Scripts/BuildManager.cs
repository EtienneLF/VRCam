using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;

    public float waitingTime = 0f;

    //public Button yourButton;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }


    #endregion

    public GameObject VB1ACameraPrefab;
    public GameObject M73CameraPrefab;
    public GameObject D71CameraPrefab;
    public GameObject Q71CameraPrefab;
    public CameraManager cameraManager;

    private GameObject cameraToBuild;
    private GameObject player;

    

    void Start()
    {
        cameraManager = CameraManager.instance;
        player = GameObject.FindGameObjectWithTag("MainCamera");
        waitingTime = 0f;

        //Le bouton est mis sous écoute d'évenements (appui). Si evenement alors appel de BuildCameraOn
        //Button btn = yourButton.GetComponent<Button>();
        //btn.onClick.AddListener(BuildCameraOn);
    }
    
    void Update()
    {
        /*
        if (Input.GetKeyDown("p"))
        {
            if (cameraToBuild != null)
            {
                BuildCameraOn();     
            }
        }
        */
    }

    public GameObject GetCameraToBuild()
    {
        return cameraToBuild;
    }
    public void SetCameraToBuild(GameObject cam)
    {
        cameraToBuild = cam;
    }


    public Vector3 GetBuildPosition()
    {
        Vector3 positionOffset = player.transform.forward * 2;
        return player.transform.position + positionOffset;
    }


    public void BuildCameraOn()
    {
        GameObject cam = Instantiate(cameraToBuild, GetBuildPosition(), player.transform.rotation);
        AddCameraToList(cam);
    }
    

    public void AddCameraToList(GameObject cam)
    {
        if(cameraManager!= null)
        {
            cam.SetActive(true);
            cam.GetComponent<AudioListener>().enabled = false;
            cameraManager.addCam(cam);
        }
    }
}

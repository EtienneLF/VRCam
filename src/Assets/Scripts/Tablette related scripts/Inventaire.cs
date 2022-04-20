using UnityEngine;
public class Inventaire : MonoBehaviour
{
    private BuildManager buildManager;


    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectVB1ACamera()
    {
        Debug.Log("Caméra VB1A sélectionée");
        buildManager.SetCameraToBuild(buildManager.VB1ACameraPrefab);
    }
    public void SelectM73Camera()
    {
        Debug.Log("Caméra M73 sélectionée");
        buildManager.SetCameraToBuild(buildManager.M73CameraPrefab);
    }
    public void SelectD71Camera()
    {
        Debug.Log("Caméra D71 sélectionée");
        buildManager.SetCameraToBuild(buildManager.D71CameraPrefab);
    }
    public void SelectQ71Camera()
    {
        Debug.Log("Caméra Q71 sélectionée");
        buildManager.SetCameraToBuild(buildManager.Q71CameraPrefab);
    }
}

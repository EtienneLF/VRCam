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
        Debug.Log("Cam�ra VB1A s�lection�e");
        buildManager.SetCameraToBuild(buildManager.VB1ACameraPrefab);
    }
    public void SelectM73Camera()
    {
        Debug.Log("Cam�ra M73 s�lection�e");
        buildManager.SetCameraToBuild(buildManager.M73CameraPrefab);
    }
    public void SelectD71Camera()
    {
        Debug.Log("Cam�ra D71 s�lection�e");
        buildManager.SetCameraToBuild(buildManager.D71CameraPrefab);
    }
    public void SelectQ71Camera()
    {
        Debug.Log("Cam�ra Q71 s�lection�e");
        buildManager.SetCameraToBuild(buildManager.Q71CameraPrefab);
    }
}

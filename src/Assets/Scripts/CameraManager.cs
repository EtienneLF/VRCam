using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script allow player to equip, the number i of Keyboard give the ith weapon of the list stuffPrefabs

public class CameraManager : MonoBehaviour
{
    
    #region Singleton

    public static CameraManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
   

    #endregion

    

    private List<GameObject> list_cam;

    public RenderTexture textureGardien;

    public GameObject playerCamera;

    private GameObject activeCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        activeCamera = playerCamera;
        list_cam = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    /*
        //Switch camera sequentiel
        if(Input.GetKeyDown((KeyCode)(49))){ //Touche 1 du clavier
            if(activeCamera != playerCamera){
                
                activeCamera.GetComponent<Camera>().targetTexture = textureGardien;
                playerCamera.GetComponent<Camera>().enabled = true;
                playerCamera.GetComponent<AudioListener>().enabled = true;

                activeCamera = playerCamera;
                refreshCam();
            }
        }

        for (int i = 0; i < list_cam.Count; i++)
        {   
            if (Input.GetKeyDown((KeyCode)(49 + i+1)))
            { //KeyCode 48 corresponds to Alpha0 which is the 0 of keyboard
                
                if(activeCamera == playerCamera){
                    activeCamera.GetComponent<Camera>().enabled = false;
                    activeCamera.GetComponent<AudioListener>().enabled = false;
                }
                else{
                    activeCamera.GetComponent<Camera>().targetTexture = textureGardien;
                }
                
                list_cam[i].GetComponent<Camera>().enabled = true;
                list_cam[i].GetComponent<AudioListener>().enabled = true;

                list_cam[i].GetComponent<Camera>().targetTexture = null;

                resetCam(list_cam[i].GetComponent<Camera>());

                activeCamera = list_cam[i];
            }
        
        }*/
    }

    /*
    void nextCam()
    {
        for (int i = 0; i < list_cam.Count; i++)
        {
            if (list_cam[i].activeSelf)
            {
                list_cam[i].SetActive(false);
                if (i + 1 == list_cam.Count)
                {
                    list_cam[0].SetActive(true);
                }
                else
                {
                    list_cam[i + 1].SetActive(true);
                }
                break;
            }
        }
    }*/


    public void addCam(GameObject cam)
    {   
        //changeTexture(cam);
        cam.GetComponent<Camera>().enabled = true;
        list_cam.Add(cam);
        refreshCam();
    }

    public void delCam( GameObject cam )
    {
        cam.GetComponent<Camera>().enabled = false;
        cam.GetComponent<AudioListener>().enabled = false;
        list_cam.Remove(cam);

        if ( activeCamera == cam )
        {
            list_cam[0].GetComponent<Camera>().enabled = true;
            list_cam[0].GetComponent<AudioListener>().enabled = true;
            activeCamera = list_cam[0];

        }
        Destroy(cam);
    }
   
   

    public void randFovCam(GameObject cam)
    {
        Camera c = cam.GetComponent<Camera>();
        c.fieldOfView = Random.Range(0, 90);
    }

    public void changeTexture(GameObject cam)
    {
        Camera c = cam.GetComponent<Camera>();
        c.targetTexture = textureGardien; 
    }

    public void refreshCam()
    {
        //Reset la texture
        textureGardien.Release();
        //Nombre de Cam
        int n = list_cam.Count;
        //Nombre y et x de Cam
        int ny = (int)Mathf.Round(Mathf.Sqrt(n));
        int nx;        
        if(Mathf.Sqrt(n) <= ny){
            nx = ny;
        }
        else{
            nx = ny+1;
        }
        //Taille de chaque Caméra
        float sizex = 1.0f/(nx);   
        float sizey = 1.0f/(ny);
        //Indice modulo x
        int y = -1;

        for(int i = 0; i<list_cam.Count; i++){  //Pour chaque Cam
            int m = i%ny; //Modulo y
            if(m==0){ //Si départ
                if(++y == nx){ //Incrémentation y, si fin alors remettre à 0
                    y=0;
                }
            }
            //Position de la caméra
            float posx = (float)y/nx;
            float posy = (float)m/ny;
            //Initialisation + attribution du rectangle
            Rect r = new Rect(posx,posy,sizex,sizey);
            Camera c = list_cam[i].GetComponent<Camera>();
            c.rect = r;
        }
    }

    public void resetCam(Camera cam){
        Rect r = new Rect(0,0,1,1);
        cam.rect = r;
    }
}

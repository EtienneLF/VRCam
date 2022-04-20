using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instantiateOnClick : MonoBehaviour
{
    public Button yourButton;
    public GameObject prefab;

    private GameObject player;

    public CameraManager cameraManager;

    private GameObject cameraInHand;

    private bool isSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        
        btn.onClick.AddListener(toSpawn);

        player = GameObject.FindGameObjectsWithTag("Player")[0];

        cameraManager = CameraManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter( Collider other )
	{
        if ( other.CompareTag("InfoSheet") )
        {
            Destroy(other);
        }
	}
	void toSpawn()
    {
        Debug.Log("tospanw");
        if ( !isSpawned )
        {
            //Vector3 offset = new Vector3(0,1,1);
            Vector3 offset = player.transform.forward*2;
            Vector3 coords = player.transform.position + offset;            
            cameraInHand = (GameObject)Instantiate(prefab, coords,player.transform.rotation);
            cameraInHand.GetComponent<Camera>().enabled = false;
            cameraInHand.GetComponent<AudioListener>().enabled = false;
            
            cameraManager.addCam(cameraInHand);
            //cameraInHand.transform.position =  //maingauche transform;
            isSpawned = true;
        }

        else
        {
            Destroy(cameraInHand);
            isSpawned = false;
        }     
        
       
    }
}

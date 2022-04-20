using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletteManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private string currentRoom = "main";
    public GameObject player;
    public Transform[] rooms;
    
    private Transform guardianTeleportAnchor;
    private int roomSelected = 1; //Pour l'instant on a 2 pièces : 1 = mainroom, -1 = gardien
    private GameObject buttonTeleportText;

    void Start()
    {
        buttonTeleportText = GameObject.Find("TextButtonKeeper");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToRoom()
    {
        switchRooms();
        //plus tard, teleportation vers la piece passée en argument, pour l'instant TP au mode gardien
        player.transform.position = guardianTeleportAnchor.position;
        Debug.Log("teleport");
    }

    //Si implémentation d'autres rooms, on pourra se tp en passant un nom de room en argument
    void switchRooms()
    {
        roomSelected *= -1;
        if ( roomSelected == 1 ) {
            buttonTeleportText.GetComponent<Text>().text = "Keeper room";
            guardianTeleportAnchor = rooms[0];
        }
        if ( roomSelected == -1 )
        {
            buttonTeleportText.GetComponent<Text>().text = "Main room";
            guardianTeleportAnchor = rooms[1];
        }
    }


}

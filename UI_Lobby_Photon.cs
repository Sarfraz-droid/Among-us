using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class UI_Lobby_Photon : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI curr_text;

    // Start is called before the first frame update
    void Start()
    {
        curr_text = this.GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        curr_text.text = "Player Found : " + (PhotonNetwork.CountOfPlayersInRooms+1) + "/6";
    }
}

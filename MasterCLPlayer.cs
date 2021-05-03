using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MasterCLPlayer : MonoBehaviourPunCallbacks
{
    int maxplayers;
    int imposter;
    // Start is called before the first frame update
    void Awake()
    {
        maxplayers = PhotonNetwork.CountOfPlayersInRooms;

        imposter = (int)(Random.Range(0, (float)(maxplayers + 1)));
        if (PhotonNetwork.IsMasterClient == false)
            return;
        int i = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            if (i == imposter)
            {
                Debug.Log(player.CustomProperties["Name"] + " is imposter");
                hash.Add("Imposter", true);
            }
            else
            {
                Debug.Log(player.CustomProperties["Name"] + "is not imposter");
                hash.Add("Imposter", false);
            }
            i++;

            player.SetCustomProperties(hash);

        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    
}

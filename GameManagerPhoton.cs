using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class GameManagerPhoton : MonoBehaviourPunCallbacks
{


    public Dictionary<string,bool> allPlayers = new Dictionary<string, bool>();
    public List<GameObject> allcharacters;
    public static GameManagerPhoton Instance;

    public GameObject spawnpt;

    public PhotonView Pv;

    string thisobj;
    int thiscount;

    [Header("MasterClient")]

    public GameObject start_button;

    void Awake()
    {
        Pv = GetComponent<PhotonView>();

        foreach(GameObject gb in allcharacters)
        {
            allPlayers.Add(gb.name, false);
        }

        foreach (var player in PhotonNetwork.PlayerList)
        {
            bool state = false;

            if(player.CustomProperties["GameObject"] != null)
            {
                state = (bool)player.CustomProperties["GameObject-State"];
                String gb = (string)player.CustomProperties["GameObject"];
                allPlayers[gb] = state;
            }
        }
    }

    private void Start()
    {
        Instance = this;

        if(PhotonNetwork.IsMasterClient)
        {
            start_button.SetActive(true);
        }

        if (allPlayers == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            if (PlayerMovement.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);


                //                thiscount = PhotonNetwork.CountOfPlayers - 1;
                //thisobj = allcharacters[PhotonNetwork.CountOfPlayers - 1];

                //                PhotonNetwork.Instantiate(allcharacters[(PhotonNetwork.CountOfPlayersInRooms)%allcharacters.Count].name, spawnpt.transform.position, Quaternion.identity, 0);
                PlayerIns();
                PhotonNetwork.Instantiate(thisobj, spawnpt.transform.position, Quaternion.identity, 0);

            }
        }

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }



    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void PlayerIns()
    {
        foreach(var player in allPlayers)
        {
            if(player.Value == false)
            {
                thisobj = player.Key;
                
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                hash.Add("GameObject", thisobj);
                hash.Add("GameObject-State", true);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

                allPlayers[player.Key] = true;
                break;
            }
        }
    }

    public void Start_Button()
    {
        PhotonNetwork.LoadLevel(2);
    }
}


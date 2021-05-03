using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;

    string Gameversion = "1";

    [SerializeField]
    private byte maxPlayersPerRoom = 10;



    bool isConnecting;
    //CallBacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected() was called by PUN" + cause);
        
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandom() Failed, Creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});


    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRandom() was successfull");

        PhotonNetwork.LoadLevel(1);
    }

    //CallBacks end

    void Start()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = Gameversion;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

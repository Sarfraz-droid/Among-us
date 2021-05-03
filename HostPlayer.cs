using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HostPlayer : MonoBehaviourPunCallbacks
{

    public GameObject Player_start_Button;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Player_start_Button.SetActive(true);
        }
    }

    public void Nextlevel()
    {
        PhotonNetwork.LoadLevel(2);
    }
}

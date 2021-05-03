using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonPlayerName : MonoBehaviour
{
    public TextMeshPro Name;

    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if(!pv.IsMine)
        {
            foreach(var player in PhotonNetwork.PlayerList)
            {
                Debug.Log((string)player.CustomProperties["obj_Name"]);
                if ((string)player.CustomProperties["obj_Name"] == this.name)
                {
                    Name.text = (string)player.CustomProperties["Name"];
                    break;
                }
            }
            return;
        }
        Name.text = (string)PhotonNetwork.LocalPlayer.CustomProperties["Name"];
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["Name"]);
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("obj_Name", this.name);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

    }

    // Update is called once per frame
    void Update()
    {


        Name.transform.LookAt(Camera.main.gameObject.transform, Vector3.up);
    }
}

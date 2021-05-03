using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerInputField : MonoBehaviour
{
    // Start is called before the first frame update'

    const string playerprefkey = "PlayerName";

    void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerprefkey))
            {
                defaultName = PlayerPrefs.GetString(playerprefkey);
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;

    }
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("Name", value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["Name"]);
        PlayerPrefs.SetString(playerprefkey, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

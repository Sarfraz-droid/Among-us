using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TaskInfo : MonoBehaviour
{

    public TextMeshProUGUI text;
    public GameObject gameManager;

    public GameObject UI;


    public bool isComplete = false;
    public bool isEnabled = false;

    public GameObject player;

    public GameObject Progress_bar;

    // Start is called before the first frame update

    private void Awake()
    {
        gameManager.GetComponent<GameManager>().allobjs.Add(text, this.gameObject);
        Progress_bar = GameObject.FindGameObjectWithTag("Progress");
    //    Debug.Log(gameManager.GetComponent<GameManager>().allobjs[text]);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            return;
        GameObject[] gb = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject obj in gb)
        {
            if(obj.GetComponent<PhotonView>().IsMine == true)
            {
                player = obj;
                break;
            }
        }
    }

    public void Task_Complete()
    {
        float value = Progress_bar.GetComponent<Image>().fillAmount + gameManager.GetComponent<GameManager>().fk;
        GetComponent<PhotonView>().RPC("Bar_Inc", RpcTarget.All,value);
    }

    [PunRPC]
    void Bar_Inc(float val)
    {
        Progress_bar.GetComponent<Image>().fillAmount = val;
        Debug.Log(Progress_bar.GetComponent<Image>().fillAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{


    public Dictionary<TextMeshProUGUI,GameObject> allobjs =  new Dictionary<TextMeshProUGUI, GameObject>();

    public Dictionary<TextMeshProUGUI, GameObject> taskobj = new Dictionary<TextMeshProUGUI, GameObject>();

    public GameObject task_parent;

    public TextMeshProUGUI[] all_texts;

    public int nooftask = 1;
    int alltask;

    public int noofplayers = 0;

    public ArrayList arrlist = new ArrayList();
    int gb_count = 0;
    public float fk;
    // Start is called before the first frame update
    void Start()
    {
        alltask = nooftask;
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
        foreach (TextMeshProUGUI text in all_texts)
        {
            text.color = Color.red;
             if (nooftask <= 0)
                 break;
            nooftask -= 1;
            obj_ins(text);
        }
    }

    void obj_ins(TextMeshProUGUI instext)
    {
        Debug.Log(allobjs[instext]);

        GameObject gb = Instantiate(instext.gameObject);
        gb.transform.SetParent(task_parent.transform);
        taskobj.Add(gb.GetComponent<TextMeshProUGUI>(), allobjs[instext]);
        allobjs[instext].GetComponent<TaskInfo>().isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        noofplayers = PhotonNetwork.PlayerList.Length-1;
        fk = 1 / (float)(alltask * noofplayers);

        foreach (var obj in taskobj)
        {
            if (obj.Value.GetComponent<TaskInfo>().isComplete == true)
                obj.Key.color = Color.green;
        }
    }
}

using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class Player_Actions : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 0.5f;

    [Header("Particle System : ")]
    public ParticleSystem blood;

    PhotonView photonView;

    [Header("Imposter : ")]
    public bool imposter = false;
    public GameObject imp_show;
    bool found = false;

    [Header("UI : ")]
    public GameObject Use;
    public GameObject Report;
    public GameObject KillButton;
    public GameObject KillCooldown;

    void Start()
    {
        photonView = this.GetComponent<PhotonView>();

        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["Imposter"]);

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
        if (PhotonNetwork.LocalPlayer.CustomProperties["Imposter"] != null && found == false)
        {
            found = true;
            imposter = (bool)PhotonNetwork.LocalPlayer.CustomProperties["Imposter"];
            if (imposter)
            {
                imp_show.SetActive(true);
                Debug.Log("I am Imposter");
            }
            else
                Debug.Log("I am not Imposter");
        }

        bool hasTask = false;
        bool nearKill = false;
        Collider[] hit = Physics.OverlapSphere(transform.position,radius);
        foreach (Collider raycast in hit)
        {
            string tag = raycast.tag;

            if (tag == "Gate")
            {
                Debug.Log("Near a Gate");
                GateInput(raycast.gameObject);
            }

            if (tag == "Player" && raycast.gameObject != this.gameObject)
            {
                Debug.Log("I am imposter");
                nearKill = true;
                PlayerKill(raycast.gameObject);
            }

            if (tag == "Task" && !imposter)
            {
                hasTask = true;
                Debug.Log("Near Files");
                TaskProcess(raycast.gameObject);
            }

        }
        if(!hasTask)
        {
            Debug.Log("No Task");
            Use.GetComponent<Image>().color = Color.grey;
        }else
        {
            Use.GetComponent<Image>().color = Color.white;
            Use.SetActive(true);
        }
        if (imposter)
        {
            if (!nearKill)
                KillButton.GetComponent<Image>().color = Color.grey;
            else
            {
                KillButton.SetActive(true);
                if (KillCooldown.activeInHierarchy == false)
                    KillButton.GetComponent<Image>().color = Color.white;
            }
        }else
        {
            KillButton.SetActive(false);
        }
    }

    void GateInput(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            obj.GetComponent<Gate_Toggle>().GateToggle();
        }
    }
    void PlayerKill(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.Q) && KillCooldown.activeInHierarchy == false)
        {
            KillCooldown.SetActive(true);
            //            KillProcess(obj);
            photonView.RPC("KillProcess", RpcTarget.All,obj.GetComponent<PhotonView>().ViewID);
        }
    }

    void TaskProcess(GameObject obj)
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && obj.GetComponent<TaskInfo>().isComplete == false && obj.GetComponent<TaskInfo>().isEnabled == true)
            obj.GetComponent<TaskInfo>().UI.SetActive(true);
    }

    [PunRPC]
    void KillProcess(int id)
    {
        GameObject[] gb = GameObject.FindGameObjectsWithTag("Player");
        GameObject obj = null;
        foreach(GameObject g_obj in gb)
        {
            if(g_obj.GetComponent<PhotonView>().ViewID == id)
            {
                obj = g_obj;
                break;
            }
        }
        if (obj != null)
        {
            Instantiate(obj.GetComponent<PlayerMovement>().deadmesh, obj.transform.position, obj.transform.rotation);
            GameObject mesh = obj.GetComponent<PlayerMovement>().mesh;
            Debug.Log(obj.GetComponent<PlayerMovement>().photonView.IsMine);
            if (obj.GetComponent<PlayerMovement>().photonView.IsMine)
                obj.GetComponent<PlayerMovement>().deadPlayer.SetActive(true);
            else
            {
                obj.GetComponent<PlayerMovement>().deadPlayer.SetActive(false);
                obj.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            }
            obj.GetComponent<CapsuleCollider>().enabled = false;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            mesh.SetActive(false);
            blood.transform.position = obj.transform.position;
            blood.Play();
            Debug.Log("PlayerKilled");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

}

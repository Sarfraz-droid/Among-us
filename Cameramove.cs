using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Cameramove : MonoBehaviour
{
    public Vector3 offset;

    public float smoothVel = 2f;

    public GameObject Cam;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.gameObject;

        photonView = this.GetComponent<PhotonView>();

    }



    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
            return;

        if(Cam == null)
            Cam = Camera.main.gameObject;


        Vector3 d_pos = transform.position + offset;
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, d_pos, smoothVel*Time.deltaTime);
  
    }
}

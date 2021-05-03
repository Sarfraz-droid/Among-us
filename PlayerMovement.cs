using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    Rigidbody rb;
    public GameObject mesh;
    public GameObject deadPlayer;
    public GameObject deadmesh;
    public static GameObject LocalPlayerInstance;



    [Header("Movement_Info")]
    public bool isMoving;
    public float move_speed = 1f;
    public float v_movement;
    public float h_movement;

    public float smoothTime = 0.1f;
    public float turnsmoothVel;

    public bool doingtask;

    Animator player_Anim;

    [Header("Photon : ")]

    PhotonView photonView;

    public GameObject gamemanager;

    public GameObject curr_obj;

    private void Awake()
    {
        photonView = this.GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            PlayerMovement.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);


    }

    // Start is called before the first frame update
    void Start()
    {

        curr_obj = this.gameObject;
        rb = GetComponent<Rigidbody>();
        player_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        if (doingtask)
        {
            player_Anim.SetBool("isWalking", false);
            rb.velocity = Vector3.zero;
            return;
        }

        getinput();

        Vector3 direction = new Vector3(h_movement, 0, v_movement);

        if (direction.magnitude >= 0.1f)
        {
            player_Anim.SetBool("isMoving", true);
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothVel, smoothTime);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

            rb.velocity = direction * move_speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            isMoving = false;
            player_Anim.SetBool("isMoving", false);
        }
    }

    void getinput()
    {
        h_movement = Input.GetAxis("Vertical") * move_speed*-1;
        v_movement = Input.GetAxis("Horizontal") * move_speed;
    }
}

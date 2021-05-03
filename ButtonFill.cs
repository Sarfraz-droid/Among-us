using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFill : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnMouseDrag()
    {
        Debug.Log("Mouse Down");
        GetComponentInParent<FillWater>().buttonclick();
    }

    private void OnEnable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponent<PlayerMovement>().doingtask = true;
    }
    private void OnDisable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponentInParent<PlayerMovement>().doingtask = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureCheck : MonoBehaviour
{
    [Header("Final Text : ")]

    public TextMeshProUGUI f_text;
    public int f_int = 0;

    [Header("Changable Text : ")]

    public TextMeshProUGUI c_text;
    public int c_int;

    [Header("Win Text : ")]
    public GameObject win_text;

    private void Awake()
    {
        f_int = (int)Random.Range(-20f, 20f);
    }

    private void OnEnable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponent<PlayerMovement>().doingtask = true;
    }
    private void OnDisable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponentInParent<PlayerMovement>().doingtask = false;
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    public void Up_Button()
    {
        if(c_int != f_int)
            c_int++;
    }

    public void Down_Button()
    {
        if(c_int != f_int)
            c_int--;
    }



    // Update is called once per frame
    void Update()
    {
        f_text.text = f_int + "";
        c_text.text = c_int + "";

        if (c_int == f_int)
        {
            StartCoroutine("TaskComp");
        }
    }

    IEnumerator TaskComp()
    {
        win_text.SetActive(true);
        GetComponentInParent<TaskInfo>().isComplete = true;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
        GetComponentInParent<TaskInfo>().Task_Complete();
    }
}

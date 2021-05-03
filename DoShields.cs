using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class DoShields : MonoBehaviour
{
    public Sprite img_fill;

    public GameObject[] buttons;

    public int left = 0;

    public GameObject task_complete;

    private void Awake()
    {
        foreach(GameObject btn in buttons)
        {
            int r = (int)Random.Range(0f, 2f);
            if(r != 0)
            {
                btn.GetComponent<Image>().sprite = img_fill;
            }else
            {
                left++;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(left == 0)
        {
            StartCoroutine("Finish");
        }
        
    }
    public void click(GameObject obj)
    {
        if(obj.GetComponent<Image>().sprite.name == "3")
        {
            left--;
            obj.GetComponent<Image>().sprite = img_fill;
        }
    }
    IEnumerator Finish()
    {
        task_complete.SetActive(true);
        GetComponentInParent<TaskInfo>().isComplete = true;
        yield return new WaitForSeconds(0.5f);
        GetComponentInParent<TaskInfo>().Task_Complete();
        this.gameObject.SetActive(false);
    }
}

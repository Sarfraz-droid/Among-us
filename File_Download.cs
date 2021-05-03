using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class File_Download : MonoBehaviour
{
    public TextMeshProUGUI text;

    float curr_time = 0;

    public Image slider;

    public TextMeshProUGUI files_top;

    public float count = 3f;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("Action", true);
    }

    private void OnEnable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponent<PlayerMovement>().doingtask = true;
    }
    private void OnDisable()
    {
        GetComponentInParent<TaskInfo>().player.GetComponentInParent<PlayerMovement>().doingtask = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (curr_time > count)
        {
            StartCoroutine("done");
            return;
        }
        curr_time += Time.deltaTime;
        text.text = (int)(curr_time * 100/3) + "%";
        slider.fillAmount = curr_time/count;

    }

    IEnumerator done()
    {
        files_top.text = "Successfully downloaded";
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Action", false);
        GetComponentInParent<TaskInfo>().Task_Complete();
        GetComponentInParent<TaskInfo>().isComplete = true;
        this.enabled = false;
        anim.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}

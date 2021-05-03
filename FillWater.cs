using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillWater : MonoBehaviour
{
    // Start is called before the first frame update

    public float Waterbar = 3f;
    float process = 0f;
    public Image task_UI;

    GameObject task_mesh;

    public GameObject text;

    float multiplier;

    public bool pressed = false;

    void Start()
    {
        multiplier = Waterbar;
        task_mesh = GetComponentInParent<TaskInfo>().gameObject;

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
        task_UI.fillAmount = process/multiplier;
        if (pressed)
        {
            if (process >= Waterbar)
            {
                StartCoroutine("TaskComplete");
                pressed = false;
            }
            process += Time.deltaTime;
        }
    }
    public void buttonclick()
    {
        pressed = true;
    }
    public void buttonUp()
    {
        pressed = false;
    }
    IEnumerator TaskComplete()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        task_mesh.GetComponent<TaskInfo>().Task_Complete();
        task_mesh.GetComponent<TaskInfo>().isComplete = true;
        task_mesh.GetComponent<TaskInfo>().UI.SetActive(false);
    }
}

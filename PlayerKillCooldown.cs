using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerKillCooldown : MonoBehaviour
{
    public float Cooldown = 20f;
    TextMeshProUGUI text;
    public GameObject killButton;
    // Start is called before the first frame update
    void Start()
    {
        Cooldown = 20f;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;
        text.text = (int)Cooldown + "";
        killButton.GetComponent<Image>().color = Color.grey;
        if((int)Cooldown == 0)
        {
            killButton.GetComponent<Image>().color = Color.white;
            Cooldown = 20f;
            this.gameObject.SetActive(false);
        }
        
    }
}

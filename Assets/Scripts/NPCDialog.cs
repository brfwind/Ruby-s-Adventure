using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public float displayTime = 4.0f;
    private float timerDisplay;
    private bool hasPlayed;

    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1;
    }

    void Update()
    {
        if(timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;

            if(timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        UIHealthBar.instance.hasTask = true;
        
        if(UIHealthBar.instance.fixedNum >= 5)
        {
            dialogText.text = "伟大的Ruby，谢谢你，你太棒了！";
            if(!hasPlayed)
            {
                AudioManager.PlayAudio("Quest Complete",true);
                hasPlayed = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelect : MonoBehaviour
{
    public void EnterMiniGameA()
    {
        SceneManager.LoadScene("Game");
    }

    public void EnterMiniGameB()
    {
        SceneManager.LoadScene("Menu");
    }
}

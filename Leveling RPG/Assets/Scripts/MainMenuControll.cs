using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControll : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Village");
    }
}

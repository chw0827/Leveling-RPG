using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    public Animator anim;
    public AudioSource sceneChangeSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneChangeEnd;
    }

    public void SceneChangeStart()
    {
        anim.SetTrigger("sceneEnd");
        sceneChangeSound.Play();
    }

    void SceneChangeEnd(Scene scene, LoadSceneMode mode)
    {
        anim.SetTrigger("sceneStart");
    }
}

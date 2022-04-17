using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool canvasIsOn;

    [SerializeField] private int sceneIndex; 

    [SerializeField] private GameObject pauseMenu;

    private bool[] key;

    [SerializeField] private Image[] keyImage; 

    public bool KeyIstVollständig; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        key = new bool[3];
        pauseMenu.SetActive(false);
        foreach (var img in keyImage)
        {
            img.enabled = false; 
        }
    }


    private void Update()
    {
        if (sceneIndex != 0)
        {
             CheckMenuCall();
        }
    }

    private void CheckMenuCall()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvasIsOn)
        {
            PauseMenuOn();
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape) && canvasIsOn)
        {
            PauseMenuOff();
        }
    }

    public void PauseMenuOn()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        canvasIsOn = true;
    }

    public void PauseMenuOff()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        canvasIsOn = false;
    }
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void Die()
    {
        LevelLoader.Instance.StartFade();
        StartCoroutine(LoadBeforeDie(1f));
    }
    IEnumerator LoadBeforeDie(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("nextScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadBeforeNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("nextScene");
        SceneManager.LoadScene(sceneIndex);
    }
    public void AddKey(int keyWert)
    {
        key[keyWert] = true;
        keyImage[keyWert].enabled = true; 
        if (key[0]&&key[1]&&key[2])
        {
            KeyIstVollständig = true; 
            Debug.Log("AllKeys");
        }
    }
    public void LoadNextScene(int newIndex)
    {
        sceneIndex=newIndex;
        StartCoroutine(LoadBeforeNextScene(1f)); 
    }
}
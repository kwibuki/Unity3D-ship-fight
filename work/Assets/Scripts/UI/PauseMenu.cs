using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject UIPauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public Transform SoundManager;
    public GameController gamecontroller;
    Component[] audioSources;
    private bool paused = false;
    

    void Start()
    {
        audioSources = SoundManager.GetComponents(typeof(AudioSource));
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
                pause();
            else
                resume();
        }
    }

    public void pause()
    {
        UIPauseMenu.SetActive(true);
        Time.timeScale = 0;
        foreach (AudioSource audio in audioSources) //Mute sounds
            audio.Pause();

        paused = true;
    }

    public void resume()
    {
        foreach (AudioSource audio in audioSources) //Unmute sounds
            audio.Play();

        Time.timeScale = 1;
        UIPauseMenu.SetActive(false);
        paused = false;
    }


    //Buttons
    public void quit()
    {
        Application.Quit();
    }

    public void toMenu()
    {
        Time.timeScale = 1;
        UIPauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);


        //要么public 出去在外面对mainMenu进行赋值
        //mainMenu.GetComponent<PanelScript>().turnOn();
        //要么直接获取
        GameObject.Find("Menus/MainMenu").GetComponent<PanelScript>().turnOn();

        //退出游戏停止倒计时
        TimeDown.inst.OnExit();


    }

    public void newGame()
    {
        GameObject.Find("Menus/MainMenu").GetComponent<PanelScript>().turnOff();
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        gamecontroller.Destroy();
        gamecontroller.waitAndSet();
    }
}
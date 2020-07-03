using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour{

    public GameController controller;
    public GameObject UI;

    public void playGame()
    {
        controller.waitAndSet();
        UI.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
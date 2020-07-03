using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public int hp = 100;
    public Transform HittedPS;
    public Slider slider;

    public GameController controller;

    // Use this for initialization
    void Start()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        hp -= 30;
        if (hp < 30)
        {
            //Setting particles ON
            HittedPS.gameObject.SetActive(true);
        }
        if (hp < 0)
        {
            Destroy(gameObject);
            controller.EndGame();
        }
        slider.value = hp;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipHealth : MonoBehaviour
{

    public int hp = 100;
    public Transform HittedPS;
    public Slider slider;
    public GameObject ex;
    public static AudioClip sd;
    private static AudioSource audioSource;

    public GameController controller;


    // Use this for initialization
    void Start()
    {
        sd = this.gameObject.GetComponent<AudioSource>().clip;
        audioSource = this.gameObject.GetComponent<AudioSource>();

        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<GameController>();
        if (controller == null)
        {
            Debug.LogError("Unable to find the GameController script");
        }
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
        if (hp < 0 )
        {
            controller.AddScore(10);
            GameObject go = Instantiate(ex, transform.position, transform.rotation);
            audioSource.PlayOneShot(sd);
            Destroy(this.gameObject);
            
        }
        slider.value = hp;

    }

}
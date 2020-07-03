using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform player;
    public Transform playership;
    public Transform enemy;
    public Transform ai;
    public Transform ai1;
    public Transform ai2;
    public Transform ai3;
    public Transform ai4;
    public Transform ai5;
    public Transform ai6;
    public Transform ai7;
    public Transform winUI;
    public Transform loseUI;
    public Text text;
    public Text time;
    public Vector3 playerPos;
    public Vector3 aiPos;
    public Vector3 ai1Pos;
    public Vector3 ai2Pos;
    public Vector3 ai3Pos;
    public Vector3 ai4Pos;
    public Vector3 ai5Pos;
    public Vector3 ai6Pos;
    public Vector3 ai7Pos;
    public Vector3 cameraPos;
    public int Score = 0;
    public bool gameEnded = false;

    // Use this for initialization
    public void Start () {
        player.transform.position = playerPos;
        ai.transform.position = aiPos;
        ai1.transform.position = ai1Pos;
        ai2.transform.position = ai2Pos;
        ai3.transform.position = ai3Pos;
        ai4.transform.position = ai4Pos;
        ai5.transform.position = ai5Pos;
        ai6.transform.position = ai6Pos;
        ai7.transform.position = ai7Pos;
        text.text = "Score :0";
        
    }
	


    public void AddScore(int points)
    {
        Score += points;
        text.text = "Score : " + Score;
    }

    public void waitAndSet()
    {
        gameEnded = false;
		text.text = "Score :0";
        Score = 0;
        //重置倒计时
        TimeDown.inst.OnReset();

        GameObject.Find("Camera").transform.position = cameraPos;

        player.transform.position = playerPos;
        playership = Instantiate(player, playerPos, Quaternion.identity);

        ai.transform.position = aiPos;
        ai = Instantiate(enemy, aiPos, Quaternion.identity);

        ai1.transform.position = ai1Pos;
        ai1 = Instantiate(enemy, ai1Pos, Quaternion.identity);

        ai2.transform.position = ai2Pos;
        ai2 = Instantiate(enemy, ai2Pos, Quaternion.identity);

        ai3.transform.position = ai3Pos;
        ai3 = Instantiate(enemy, ai3Pos, Quaternion.identity);

        ai4.transform.position = ai4Pos;
        ai4 = Instantiate(enemy, ai4Pos, Quaternion.identity);

        ai5.transform.position = ai5Pos;
        ai5 = Instantiate(enemy, ai5Pos, Quaternion.identity);

        ai6.transform.position = ai6Pos;
        ai6 = Instantiate(enemy, ai6Pos, Quaternion.identity);

        ai7.transform.position = ai7Pos;
        ai7 = Instantiate(enemy, ai7Pos, Quaternion.identity);
     
}

	void Update (){

        if ( Score == 80) { onWin();}
            
        if (gameEnded) { onLose();}
            
    }

    public void EndGame()
    {
        gameEnded = true;
    }

    void onWin()
    {
            winUI.gameObject.SetActive(true);
        
    }

    void onLose()
    {
        loseUI.gameObject.SetActive(true);
    }

    public void Destroy()
    {
        GameObject pl = GameObject.Find("player(Clone)");
        Destroy(pl);
        GameObject[] ais = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject enemy in ais)
            Destroy(enemy);
    }
}

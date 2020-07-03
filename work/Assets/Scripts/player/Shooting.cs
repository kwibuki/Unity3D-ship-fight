using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    public Transform Bullet; //Bullet prefab
    public Transform BulletSpawnPos;
    public Transform ShootPS;
    public float reloadTime = 1.0f;
    bool reloaded = true;


    public bool Shoot()
    {
        if (reloaded) 
        {
            //Shooting
            reloaded = false;
            Invoke("reload", reloadTime);
            Transform newBullet = Instantiate(Bullet, BulletSpawnPos.transform.position, BulletSpawnPos.rotation);
            newBullet.transform.name = "Bullet" + transform.name;

            //Setting particles ON, then OFF
            ShootPS.gameObject.SetActive(true);
            Invoke("setLightPSOff", 0.2f);
            Invoke("setShootingPSOff", 0.6f);
            return true;  
        }
            

        return false;
    }
    void Update()
    {
        //按鼠标左键
        if (Input.GetMouseButton(0))
        {
            if (reloaded)
            {
                //Shooting
                reloaded = false;
                Invoke("reload", reloadTime);
                Transform newBullet = Instantiate(Bullet, BulletSpawnPos.transform.position, BulletSpawnPos.rotation);
                newBullet.transform.name = "Bullet" + transform.name;

                //Setting particles ON, then OFF
                ShootPS.gameObject.SetActive(true);
                Invoke("setLightPSOff", 0.2f);
                Invoke("setShootingPSOff", 0.6f);
            }
        }
    }
    void reload()
    {
        reloaded = true;
    }

    void setLightPSOff()
    {
        ShootPS.Find("Point Light").gameObject.SetActive(false);
    }

    void setShootingPSOff() 
    {
        ShootPS.Find("Point Light").gameObject.SetActive(true);
        ShootPS.gameObject.SetActive(false);
    }
}

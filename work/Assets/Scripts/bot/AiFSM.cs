using UnityEngine;
using System.Collections;

public class AiFSM : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
    }
    //Current state that the Ship is in
    public FSMState curState;
    //Speed of the Ship
    private float curSpeed;
    //Ship Rotation Speed
    private float curRotSpeed;
    public Transform Bullet; //Bullet prefab
    public Transform BulletSpawnPos;
    public Transform ShootPS;
    public float reloadTime = 1.0f;
    bool reloaded = true;

    //Initialize the Finite state machine for the Ship
    protected override void Initialize()
    {
        curState = FSMState.Patrol;
        curSpeed = 5.0f;
        curRotSpeed = 1.5f;

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        //Set Random destination point first
        FindNextPoint();

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one " + "with Tag named 'Player'");

    }

    //Update each frame
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol:
                UpdatePatrolState();
                break;
            case FSMState.Chase:
                UpdateChaseState();
                break;
        }
    }

    protected void UpdatePatrolState()
    {
        //Find another random patrol point if the current
        //point is reached
        if (Vector3.Distance(transform.position, destPos) <= 3.0f)
        {
            //print("Reached to the destination point\n" + "calculating the next point");
            FindNextPoint();
        }

        //Check the distance with player Ship
        //When the distance is near, transition to chase state
        else if (Vector3.Distance(transform.position, playerTransform.position) <= 100.0f)
        {
            //print("Switch to Chase Position");
            curState = FSMState.Chase;
        }

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void FindNextPoint()
    {
        int rndIndex = Random.Range(0, pointList.Length);
        float rndRadius = 5.0f;
        Vector3 rndPosition = Vector3.zero;
        destPos = pointList[rndIndex].transform.position + rndPosition;

        //Check Range to decide the random point
        //as the same as before
        if (IsInCurrentRange(destPos))
        {
            rndPosition = new Vector3(Random.Range(-rndRadius, rndRadius), 0.0f, Random.Range(-rndRadius, rndRadius));
            destPos = pointList[rndIndex].transform.position + rndPosition;
        }
    }

    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);
        if (xPos <= 8 && zPos <= 8)
            return true;
        return false;
    }

    protected void UpdateChaseState()
    {
        //Set the target position as the player position
        destPos = playerTransform.position;
        //Check the distance with player When
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        //Go back to patrol as player is now too far
        if (dist >= 100.0f)
        {
            curState = FSMState.Patrol;
            FindNextPoint();
        }
        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
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
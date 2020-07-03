using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10;
    public float angularSpeed = 5;
    public Camera myCamera;
    private Rigidbody myRigibody;
    private float rotateSpeed = 2; //mouse control camera rotate speed
    private Vector3 offset;
    private float cameraRotate;

    // Use this for initialization
    void Start()
    {
        myRigibody = this.GetComponent<Rigidbody>();
        offset = this.transform.position;
        GameObject tmp = GameObject.FindGameObjectWithTag("MainCamera");
        myCamera = tmp.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //获得玩家的键盘输入
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Sounds>().startEngineSound();
            //transform.Find("Water1").setActive(true) ;
        }
        else
        {
            this.GetComponent<Sounds>().stopEngineSound();
            //transform.Find("Water1").Stop();
        }

        //前后移动
        GetComponent<Rigidbody>().velocity = transform.forward * v * speed;
        GetComponent<Rigidbody>().angularVelocity = transform.up * h * angularSpeed;

    }
    void LateUpdate()
    {
        myCamera.transform.position += this.transform.position - offset;
        offset = this.transform.position;
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        myCamera.transform.RotateAround(this.transform.position, Vector3.up, mouseX);
        cameraRotate = myCamera.transform.eulerAngles.y / 180 * Mathf.PI;
    }
}
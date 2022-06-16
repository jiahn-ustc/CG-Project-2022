using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//摄像机的旋转
//玩家左右旋转控制实现左右旋转
//摄像机上下旋转实现上下旋转
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;//视线灵敏度
    public Transform playerBody;//玩家位置
    public float xRotation = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //得到Player的transform
        GameObject Player = GameObject.Find("Player");
        playerBody = Player.transform;
        /*
        //设置相机位置
        Vector3 pos = transform.position;
        
        pos.y = playerBody.position.y+1.8f;
        transform.position = pos;*/

        //隐藏光标
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity*Time.deltaTime;
        xRotation -= mouseY;//将上下旋转的轴值进行累减
        xRotation=Mathf.Clamp(xRotation, -80f, 80f);//限制竖直方向的范围为正负80度

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);//玩家水平方向旋转 
        

    }
}

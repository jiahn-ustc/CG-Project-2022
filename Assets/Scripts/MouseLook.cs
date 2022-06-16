using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������ת
//���������ת����ʵ��������ת
//�����������תʵ��������ת
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;//����������
    public Transform playerBody;//���λ��
    public float xRotation = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //�õ�Player��transform
        GameObject Player = GameObject.Find("Player");
        playerBody = Player.transform;
        /*
        //�������λ��
        Vector3 pos = transform.position;
        
        pos.y = playerBody.position.y+1.8f;
        transform.position = pos;*/

        //���ع��
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity*Time.deltaTime;
        xRotation -= mouseY;//��������ת����ֵ�����ۼ�
        xRotation=Mathf.Clamp(xRotation, -80f, 80f);//������ֱ����ķ�ΧΪ����80��

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);//���ˮƽ������ת 
        

    }
}

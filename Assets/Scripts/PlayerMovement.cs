using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*��������ƶ�*/
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10f;//�ƶ��ٶ�
    public Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDirection = (transform.right * h + transform.forward * v).normalized;
        characterController.Move(moveDirection*speed*Time.deltaTime);
    }
}

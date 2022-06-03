using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*��������ƶ�*/
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10f;//�ƶ��ٶ�
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public bool isRun;
    public Vector3 moveDirection;//�ƶ�����

    public bool isJump;
    public float jumpForce = 1f;//��Ծ����
    public Vector3 velocity;//�������Y���1�������仯������
    public float gravity = -20f;//��������

    public bool isGround;

    [Header("��������")]
    [SerializeField][Tooltip("���ܰ���")]private KeyCode runInputName;
    [SerializeField] [Tooltip("��Ծ����")] private string jumpInputName;

    [Header("��������")]
    [SerializeField] private AudioSource audioSource;//����Դ
    public AudioClip walkingSound;
    public AudioClip runningSound;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        runInputName = KeyCode.LeftShift;//���ñ��ܰ���ΪLeftShift
        jumpInputName = "Jump";//������Ծ����
        walkSpeed = 5f;
        runSpeed = 20f;
        jumpForce = 1f;
        gravity = -20f;

        audioSource = GetComponent<AudioSource>();//�õ�����Դ���
    }
    private void Update()
    {
        CheckGround();
        Move();
    }

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        isRun = Input.GetKey(runInputName);
        speed = isRun ? runSpeed : walkSpeed;

        moveDirection = (transform.right * h + transform.forward * v).normalized;
        characterController.Move(moveDirection*speed*Time.deltaTime);
        
        if(isGround==false)
        {
            velocity.y += gravity * Time.deltaTime;//���ڵ��棬velocity.y�𽥼�С
        }
        characterController.Move(velocity * Time.deltaTime);
        Jump();
        PlayMoveSound();
        //Debug.Log("�Ƿ����ڲ�������" + audioSource.isPlaying);
    }

    //���Ž�ɫ�ƶ���Ч
    public void PlayMoveSound()
    {
        //��ɫ�ڵ����ϲ������ƶ��ٶ�
        if(isGround && moveDirection.sqrMagnitude>0.9f)
        {
            audioSource.clip = isRun ? runningSound : walkingSound;//ָ�����ŵ���Ч
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
    }

    public void Jump()
    {
        isJump = Input.GetButtonDown(jumpInputName);
        if(isJump && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);//����˲�䣬velocity.yΪһ��ֵ
        }
    }
    //�жϽ�ɫ�Ƿ��ڵ�����
    public void CheckGround()
    {
        isGround = characterController.isGrounded;
    }
}

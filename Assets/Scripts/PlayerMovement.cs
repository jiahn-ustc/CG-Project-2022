using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*控制玩家移动*/
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10f;//移动速度
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public bool isRun;
    public Vector3 moveDirection;//移动方向

    public bool isJump;
    public float jumpForce = 1f;//跳跃力度
    public Vector3 velocity;//设置玩家Y轴的1个冲量变化（力）
    public float gravity = -20f;//设置重力

    public bool isGround;

    [Header("按键设置")]
    [SerializeField][Tooltip("奔跑按键")]private KeyCode runInputName;
    [SerializeField] [Tooltip("跳跃按键")] private string jumpInputName;

    [Header("声音设置")]
    [SerializeField] private AudioSource audioSource;//声音源
    public AudioClip walkingSound;
    public AudioClip runningSound;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        runInputName = KeyCode.LeftShift;//设置奔跑按键为LeftShift
        jumpInputName = "Jump";//设置跳跃按键
        walkSpeed = 5f;
        runSpeed = 20f;
        jumpForce = 1f;
        gravity = -20f;

        audioSource = GetComponent<AudioSource>();//得到声音源组件
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
            velocity.y += gravity * Time.deltaTime;//不在地面，velocity.y逐渐减小
        }
        characterController.Move(velocity * Time.deltaTime);
        Jump();
        PlayMoveSound();
        //Debug.Log("是否正在播放音乐" + audioSource.isPlaying);
    }

    //播放角色移动音效
    public void PlayMoveSound()
    {
        //角色在地面上并且有移动速度
        if(isGround && moveDirection.sqrMagnitude>0.9f)
        {
            audioSource.clip = isRun ? runningSound : walkingSound;//指定播放的音效
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
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);//跳的瞬间，velocity.y为一正值
        }
    }
    //判断角色是否在地面上
    public void CheckGround()
    {
        isGround = characterController.isGrounded;
    }
}

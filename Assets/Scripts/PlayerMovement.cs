using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*��������ƶ�*/
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10f;//�ƶ��ٶ�
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public bool isRun;
    public bool isWalk;
    public bool isAttack;//��ɫ�Ƿ��ܵ�����
    public Vector3 moveDirection;//�ƶ�����
    public float maxHealth = 200f;

    public bool isJump;
    public float jumpForce = 1f;//��Ծ����
    public Vector3 velocity;//�������Y���1�������仯������
    public float gravity = -20f;//��������

    public bool isGround;
    public Slider BloodUI;

    [Header("��������")]
    [SerializeField][Tooltip("���ܰ���")]private KeyCode runInputName;
    [SerializeField] [Tooltip("��Ծ����")] private string jumpInputName;

    [Header("��������")]
    [SerializeField] private AudioSource audioSource;//����Դ
    public AudioClip walkingSound;
    public AudioClip runningSound;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        runInputName = KeyCode.LeftShift;//���ñ��ܰ���ΪLeftShift
        jumpInputName = "Jump";//������Ծ����
        isAttack = false;

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

        isRun = Input.GetKey(runInputName) && (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0);
        isWalk = (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) ? true : false;
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

    private void OnTriggerEnter(Collider other)
    {
        OnHitPlayer(other);
    }

    private void OnHitPlayer(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            _currentHealth -= enemyBullet.damage;
            StartCoroutine(routine: OnDamage());

            if(other.GetComponent<Rigidbody>())
            {
                Destroy(other.gameObject);
            }
        }
        if(other.CompareTag("MeleeArea"))
        {
            print(message: "player hit");
            _currentHealth -= 10;
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        if(_currentHealth < 0)
        {
            OnDie();
        }

        yield return new WaitForSeconds(0.2f);
    }

    private void OnDie()
    {
        SceneManager.LoadScene("Map_v2");
    }

    //�����ܵ�����
    public void attacked(float attackValue)
    {
        if (BloodUI.value > attackValue)
            BloodUI.value -= attackValue;
        else
            BloodUI.value = 0;
        Debug.Log("Ѫ��Ϊ" + BloodUI.value);

    }
}

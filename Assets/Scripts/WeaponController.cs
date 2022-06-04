using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public PlayerMovement PM;
    public Transform shooterPoint;//���λ��
    public int bulletsMag = 31;//һ�������ӵ�������
    public int range = 100;//���
    public int bulletLeft = 50;//����
    public int currentBullets;//��ǰ�ӵ�����

    public float fireRate = 0.1f;//���٣�ԽС����ٶ�Խ��
    private float fireTimer=0f;//��ʱ��

    private bool GunShootInput;//�Ƿ���������

    public ParticleSystem muzzleFlash;//ǹ�ڻ�����Ч
    public Light muzzleFlashLight;//ǹ�ڻ���ƹ�
    public GameObject hitParticle;//�ӵ�������Ч
    public GameObject bullectHole;//����

    private AudioSource audioSource;
    public AudioClip AK47SoundClip;//ǹ�����Ч
    public AudioClip reloadAmmoClip;//������Ч

    public bool isReload;//�Ƿ����ڻ���
    public bool isInspect;//�Ƿ����ڼ���

    private Animator anim;

    [Header("��λ����")]
    [SerializeField][Tooltip("��װ�ӵ�����")]private KeyCode reloadInputName;//����
    [SerializeField] [Tooltip("������������")] private KeyCode inspectInputName;//����

    [Header("UI����")]
    public Image CrossHairUI;
    public Text AmmoTextUI;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        bulletLeft = 70;
        shooterPoint = GameObject.Find("ShootPoint").transform;
        currentBullets = bulletsMag;
        reloadInputName = KeyCode.R;
        inspectInputName = KeyCode.F;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        GunShootInput = Input.GetMouseButton(0);
        if(GunShootInput && currentBullets>0)
        {
            GunFire(); 
        }
        else
        {
            muzzleFlashLight.enabled = false;
        }
        //��ȡ��ǰ���Ŷ���
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("reload"))
        {
            isReload = true;
        }
        else
            isReload = false;
        if(Input.GetKeyDown(reloadInputName)&&currentBullets<bulletsMag && bulletLeft>0)
        {
            Reload();
        }
        if (info.IsName("inspect_weapon"))
        {
            isInspect = true;
        }
        else
            isInspect = false;
        if(Input.GetKeyDown(inspectInputName))
        {
            //������������
            anim.SetTrigger("Inspect");
        }
        anim.SetBool("Run", PM.isRun);
        anim.SetBool("Walk", PM.isWalk);

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
    }

    public void GunFire()
    {
        if (fireTimer < fireRate || currentBullets<=0 || isReload||PM.isRun||isInspect) return;
        Vector3 shootDirection = shooterPoint.forward;//�������
        RaycastHit hit;
        if(Physics.Raycast(shooterPoint.position,shootDirection,out hit,range))
        {
            //   Debug.Log(hit.transform.name + "����"); 
            GameObject hitParticleEffect= Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//ʵ�����ӵ����е���Ч
            GameObject bullectHoleEffect= Instantiate(bullectHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) ;//ʵ�������׵���Ч

            Destroy(hitParticleEffect, 1f);
            Destroy(bullectHoleEffect, 3f);
        }
        PlayShootSound();//���������Ч
        muzzleFlash.Play();//���������
        muzzleFlashLight.enabled = true;//����ǹ�ĵƹ�

        currentBullets--;
        UpdateUI();
        fireTimer = 0f;
    }

    public void UpdateUI()
    {
        AmmoTextUI.text = currentBullets + " / " + bulletLeft;
    }

    public void Reload()
    {
        int willToLoad = bulletsMag - currentBullets;
        PlayReloadAnimation();
        if(willToLoad<=bulletLeft)
        {
            bulletLeft -= willToLoad;
            currentBullets += willToLoad;
        }
        else
        {
            currentBullets += bulletLeft;
            bulletLeft = 0;
        }
        UpdateUI();
    }
    //����װ������
    public void PlayReloadAnimation()
    {
        anim.Play("reload", 0, 0);
        audioSource.clip = reloadAmmoClip;
        audioSource.Play();
    }
    //���������Ч
    public void PlayShootSound()
    {
        audioSource.clip = AK47SoundClip;
        audioSource.Play();
    }
}

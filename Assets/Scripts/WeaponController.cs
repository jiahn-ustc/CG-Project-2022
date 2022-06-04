using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public PlayerMovement PM;
    public Transform shooterPoint;//射击位置
    public int bulletsMag = 31;//一个弹夹子弹的数量
    public int range = 100;//射程
    public int bulletLeft = 50;//备弹
    public int currentBullets;//当前子弹数量

    public float fireRate = 0.1f;//射速，越小射击速度越快
    private float fireTimer=0f;//计时器

    private bool GunShootInput;//是否按下鼠标左键

    public ParticleSystem muzzleFlash;//枪口火焰特效
    public Light muzzleFlashLight;//枪口火焰灯光
    public GameObject hitParticle;//子弹击中特效
    public GameObject bullectHole;//弹孔

    private AudioSource audioSource;
    public AudioClip AK47SoundClip;//枪射击音效
    public AudioClip reloadAmmoClip;//换弹音效

    public bool isReload;//是否正在换弹
    public bool isInspect;//是否正在检视

    private Animator anim;

    [Header("键位设置")]
    [SerializeField][Tooltip("填装子弹按键")]private KeyCode reloadInputName;//换弹
    [SerializeField] [Tooltip("检视武器按键")] private KeyCode inspectInputName;//换弹

    [Header("UI设置")]
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
        //获取当前播放动画
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
            //检视武器动画
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
        Vector3 shootDirection = shooterPoint.forward;//射击方向
        RaycastHit hit;
        if(Physics.Raycast(shooterPoint.position,shootDirection,out hit,range))
        {
            //   Debug.Log(hit.transform.name + "打到了"); 
            GameObject hitParticleEffect= Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//实例出子弹击中的特效
            GameObject bullectHoleEffect= Instantiate(bullectHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) ;//实例出弹孔的特效

            Destroy(hitParticleEffect, 1f);
            Destroy(bullectHoleEffect, 3f);
        }
        PlayShootSound();//播放射击音效
        muzzleFlash.Play();//播放射击火花
        muzzleFlashLight.enabled = true;//播放枪的灯光

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
    //播放装弹动画
    public void PlayReloadAnimation()
    {
        anim.Play("reload", 0, 0);
        audioSource.clip = reloadAmmoClip;
        audioSource.Play();
    }
    //播放射击音效
    public void PlayShootSound()
    {
        audioSource.clip = AK47SoundClip;
        audioSource.Play();
    }
}

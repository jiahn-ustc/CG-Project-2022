using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
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
    //public GameObject hitParticle;//�ӵ�������Ч
    public GameObject bullectHole;//����

    private AudioSource audioSource;
    public AudioClip AK47SoundClip;//ǹ�����Ч

    [Header("��λ����")]
    [SerializeField][Tooltip("��װ�ӵ�����")]private KeyCode reloadInputName;//����

    [Header("UI����")]
    public Image CrossHairUI;
    public Text AmmoTextUI;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bulletLeft = 50;
        shooterPoint = GameObject.Find("ShootPoint").transform;
        currentBullets = bulletsMag;
        reloadInputName = KeyCode.R;
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
        if(Input.GetKeyDown(reloadInputName)&&currentBullets<bulletsMag && bulletLeft>0)
        {
            Reload();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
    }

    public void GunFire()
    {
        if (fireTimer < fireRate || currentBullets<=0) return;
        Vector3 shootDirection = shooterPoint.forward;//�������
        RaycastHit hit;
        if(Physics.Raycast(shooterPoint.position,shootDirection,out hit,range))
        {
            //   Debug.Log(hit.transform.name + "����"); 
            //GameObject hitParticleEffect= Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));//ʵ�����ӵ����е���Ч
            GameObject bullectHoleEffect= Instantiate(bullectHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) ;//ʵ�������׵���Ч

            //Destroy(hitParticleEffect, 1f);
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
    //���������Ч
    public void PlayShootSound()
    {
        audioSource.clip = AK47SoundClip;
        audioSource.Play();
    }
}

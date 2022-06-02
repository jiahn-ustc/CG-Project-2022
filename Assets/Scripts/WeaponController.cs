using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Transform shooterPoint;//射击位置
    public int bulletsMag = 31;//一个弹夹子弹的数量
    public int range = 100;//射程
    public int bulletLeft = 50;//备弹
    public int currentBullets;//当前子弹数量

    public float fireRate = 0.1f;//射速，越小射击速度越快
    private float fireTimer=0f;//计时器

    private bool GunShootInput;//是否按下鼠标左键

    [Header("键位设置")]
    [SerializeField][Tooltip("填装子弹按键")]private KeyCode reloadInputName;//换弹

    [Header("UI设置")]
    public Image CrossHairUI;
    public Text AmmoTextUI;

    // Start is called before the first frame update
    void Start()
    {
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
        if(GunShootInput)
        {
            GunFire(); 
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
        Vector3 shootDirection = shooterPoint.forward;//射击方向
        RaycastHit hit;
        if(Physics.Raycast(shooterPoint.position,shootDirection,out hit,range))
        {
            Debug.Log(hit.transform.name + "打到了"); 
        }
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
}

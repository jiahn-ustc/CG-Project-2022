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

    [Header("��λ����")]
    [SerializeField][Tooltip("��װ�ӵ�����")]private KeyCode reloadInputName;//����

    [Header("UI����")]
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
        Vector3 shootDirection = shooterPoint.forward;//�������
        RaycastHit hit;
        if(Physics.Raycast(shooterPoint.position,shootDirection,out hit,range))
        {
            Debug.Log(hit.transform.name + "����"); 
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

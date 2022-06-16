using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornPot : MonoBehaviour
{
    public GameObject targetEnemy;
    //���ɹ����������
    public int enemyTotalNum = 10;
    //���ɹ����ʱ����
    public float intervalTime = 3;
    //���ɹ���ļ�����
    private int enemyCounter;
    //���
    private GameObject targetPlayer;
    //���ɹ���ļ�����
    private Health _health;

    // Use this for initialization
    void Start()
    {
        //���
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        //��ʼʱ���������Ϊ0��
        enemyCounter = 0;
        //�ظ����ɹ���
        InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //���������ɹ���
    private void CreatEnemy()
    {
        //�����Ҵ��
        if (targetPlayer.GetComponent<PlayerMovement>()._currentHealth > 0)
        {
            //����һֻ����
            Instantiate(targetEnemy, this.transform.position, Quaternion.identity);
            enemyCounter++;
            //��������ﵽ���ֵ
            if (enemyCounter == enemyTotalNum)
            {
                //ֹͣˢ��
                CancelInvoke();
            }
        }
        //�������
        else
        {
            //ֹͣˢ��
            CancelInvoke();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʵ�������İڶ�
public class WeaponSway : MonoBehaviour
{
    public float amount=0.03f;//ҡ�ڷ���
    public float smoothAmount=6f;//ҡ��ƽ��ֵ
    public float maxAmount=0.06f;//������ҡ��
    [SerializeField] public Vector3 startPosition;//��������Գ�ʼλ��
    // Start is called before the first frame update
    void Start()
    {
        amount = 0.03f;
        smoothAmount = 6f;
        maxAmount = 0.06f;
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = -Input.GetAxis("Mouse X") * amount;
        float mouseY = -Input.GetAxis("Mouse Y") * amount;

        //����ҡ�ڷ���
        mouseX = Mathf.Clamp(mouseX, -maxAmount, maxAmount);
        mouseY = Mathf.Clamp(mouseY, -maxAmount, maxAmount);

        //ʵ��ҡ��
        Vector3 tempPosition = new Vector3(mouseX, mouseY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, tempPosition + startPosition, Time.deltaTime * smoothAmount);
    }
}

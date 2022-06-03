using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实现武器的摆动
public class WeaponSway : MonoBehaviour
{
    public float amount=0.03f;//摇摆幅度
    public float smoothAmount=6f;//摇摆平滑值
    public float maxAmount=0.06f;//最大幅度摇摆
    [SerializeField] public Vector3 startPosition;//武器的相对初始位置
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

        //限制摇摆幅度
        mouseX = Mathf.Clamp(mouseX, -maxAmount, maxAmount);
        mouseY = Mathf.Clamp(mouseY, -maxAmount, maxAmount);

        //实现摇摆
        Vector3 tempPosition = new Vector3(mouseX, mouseY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, tempPosition + startPosition, Time.deltaTime * smoothAmount);
    }
}

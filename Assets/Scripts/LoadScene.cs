using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Text loadingText;
    public Image progressBar;
    private int curProgressValue = 0;

    void FixedUpdate()
    {
        int progressValue = 100;

        if (curProgressValue < progressValue)
        {
            curProgressValue++;
        }

        loadingText.text = "Ⱥţ������Ŭ������Ŷ..." + curProgressValue + "%";//ʵʱ���½��Ȱٷֱȵ��ı���ʾ  

        progressBar.fillAmount = curProgressValue / 100f;//ʵʱ���»�������ͼƬ��fillAmountֵ  

        if (curProgressValue == 100)
        {
            loadingText.text = "OK";//�ı���ʾ���OK
            
        }
    }
}

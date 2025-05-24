using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    int point;
    public TextMeshProUGUI timeText;
    float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void SetPanel()
    {
        point = GameManager.Instance.point;
        time = GameManager.Instance.time;
        pointText.text = "Point : "+point.ToString();
        timeText.text = "Time : " + (time / 60).ToString("00") + " : " + (time % 60).ToString("00");
    }
}

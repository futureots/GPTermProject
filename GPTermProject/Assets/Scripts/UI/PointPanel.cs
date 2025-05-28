using TMPro;
using UnityEngine;

public class PointPanel : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pointText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var point = GameManager.Instance.point;
        pointText.text = $"Point : {point}";

        int time = (int)GameManager.Instance.time;
        timeText.text = (time / 60).ToString("00") + " : " + (time % 60).ToString("00");
    }
}

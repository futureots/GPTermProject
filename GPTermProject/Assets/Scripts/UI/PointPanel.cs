using TMPro;
using UnityEngine;

public class PointPanel : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var point = GameManager.Instance.point;
        text.text = $"Point : {point}";
    }
}

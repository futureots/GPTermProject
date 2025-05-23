using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpPanel : MonoBehaviour
{
    Image bar;
    TextMeshProUGUI text;
    Cake target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bar = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        target = GameManager.Instance.cake;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = target.curHp/(float)target.maxHp;
        bar.fillAmount = percent;
        text.text = $"{target.curHp} / {target.maxHp}";
    }
}

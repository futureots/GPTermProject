using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingUI : MonoBehaviour
{
    public PlayerSetting setting;
    public TextMeshProUGUI value;
    public Slider slider;
    public int maxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = setting.RotateSpeed / maxSpeed;
        slider.onValueChanged.AddListener(x => setting.RotateSpeed = x * maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        value.text = "Speed : " + setting.RotateSpeed;
    }


}

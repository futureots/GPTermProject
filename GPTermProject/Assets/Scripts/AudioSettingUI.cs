using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingUI : MonoBehaviour
{
    public PlayerSetting setting;
    public AudioSource audioSource;
    public TextMeshProUGUI value;
    public Slider slider;

    private void Awake()
    {
        audioSource.volume = setting.BgmSound;
    }
    private void Start()
    {
        slider.onValueChanged.AddListener(x => setting.BgmSound = (int)(x * 100) / 100f);
    }
    private void Update()
    {
        value.text = "À½·® : " + setting.BgmSound;
        audioSource.volume = setting.BgmSound;
    }
    private void OnEnable()
    {
        slider.value = setting.BgmSound;
    }
}

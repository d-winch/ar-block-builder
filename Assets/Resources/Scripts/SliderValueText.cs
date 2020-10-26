using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string.  {0} is replaced with the actual value")]
    private string formatText = "{0}°";

    private TextMeshProUGUI tmproText;

    private void Start()
    {
        tmproText = GetComponent<TextMeshProUGUI>();

        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        tmproText.text = string.Format(formatText, value);
    }
}
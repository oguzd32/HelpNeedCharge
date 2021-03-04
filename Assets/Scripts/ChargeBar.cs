using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public void SetMaxCharge(int charge)
    {
        slider.maxValue = charge;
        slider.value = charge;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetCharge(int charge)
    {
        slider.value = charge;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

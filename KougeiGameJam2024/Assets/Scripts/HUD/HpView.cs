using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] private Slider lPlayerHpSlider;
    [SerializeField] private Slider rPlayerHpSlider;

    private void SetLPlayerHp(float normalizedHp)
    {
        lPlayerHpSlider.value = normalizedHp;
    }
    
    private void SetRPlayerHp(float normalizedHp)
    {
        rPlayerHpSlider.value = normalizedHp;
    }
}

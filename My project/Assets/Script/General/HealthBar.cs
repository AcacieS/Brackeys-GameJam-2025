using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] private HealthBar healthBar;
    private void Start()
    {
        UpdateHealthUI();
        //healthBar.SetMaxHealth(GameManager.Instance.GetMaxArmHealth());
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    private void OnEnable()
    {
        GameManager.OnHealthChanged += UpdateHealthUI;
        UpdateHealthUI(); // also update immediately in case coins changed before scene loaded
    }

    private void OnDisable()
    {
        GameManager.OnHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        SetHealth(GameManager.Instance.GetArmHealth());
    }
}



using UnityEngine.UI;
using UnityEngine;

public class HealthBar
{
    private Image curHealthBar;
    private Image maxHealthBar;

    public HealthBar(GameObject healthBarPanel) 
    {
        curHealthBar = healthBarPanel.transform.Find("CurrentHealth").gameObject.GetComponent<Image>();
        maxHealthBar = healthBarPanel.transform.Find("MaxHealth").gameObject.GetComponent<Image>();
    }

    public void SetCurHealth(int health)
    {
        curHealthBar.fillAmount = 1f / 9f * health;
    }

    public void SetMaxHealth(int health)
    {
        maxHealthBar.fillAmount = 1f / 9f * health;
    }
}

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject healthBarPanel;

    public HealthBar healthBar;

    private UIManager() {}

    private static UIManager _instance;

    public static UIManager Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<UIManager>();
        }
        return _instance;
    }

    private void Awake()
    {
        healthBar = new HealthBar(healthBarPanel);
    }
}

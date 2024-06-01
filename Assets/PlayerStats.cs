using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Image p1Bar;
    public Image p2Bar;
    public TMP_Text p1Text;
    public TMP_Text p2Text;
    public float p1Health = 100f;
    public float currentHealthp1;
    public float p2Health = 100f;
    public float currentHealthp2;

    void Start()
    {
        currentHealthp1 = p1Health;
        currentHealthp2 = p2Health;

        // Hata ayýklama için baþlangýçta referanslarý kontrol edin
        if (p1Bar == null)
        {
            Debug.LogError("HealthBar referansý atanmadý!");
        }
        if (p2Bar == null)
        {
            Debug.LogError("ManaBar referansý atanmadý!");
        }
        if (p1Text == null)
        {
            Debug.LogError("HealthText referansý atanmadý!");
        }
        if (p2Text == null)
        {
            Debug.LogError("ManaText referansý atanmadý!");
        }
    }

    void Update()
    {
        // Saðlýk ve mana barlarýný sürekli güncelle
        if (p1Bar != null)
        {
            p1Bar.fillAmount = currentHealthp1 / p1Health;
        }
        else
        {
            Debug.LogError("p1 referansý boþ!");
        }

        if (p2Bar != null)
        {
            p2Bar.fillAmount = currentHealthp2 / p2Health;
        }
        else
        {
            Debug.LogError("p2 referansý boþ!");
        }

        // TextMeshPro metinlerini güncelle
        if (p1Text != null)
        {
            p1Text.text = "p1: " + currentHealthp1;
        }
        if (p2Text != null)
        {
            p2Text.text = "p2: " + currentHealthp2;
        }

        // Bilgileri konsola yazdýrma
        Debug.Log("Current p1: " + currentHealthp1);
        Debug.Log("Current p2: " + currentHealthp2);
    }

    public void TakeDamage(float amount)
    {
        currentHealthp1 -= amount;
        if (currentHealthp1 < 0)
            currentHealthp1 = 0;

        Debug.Log("Took Damage: " + amount);
    }

    public void UseMana(float amount)
    {
        currentHealthp2 -= amount;
        if (currentHealthp2 < 0)
            currentHealthp2 = 0;

        Debug.Log("Used Mana: " + amount);
    }
}

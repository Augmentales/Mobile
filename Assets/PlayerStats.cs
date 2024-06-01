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

        // Hata ay�klama i�in ba�lang��ta referanslar� kontrol edin
        if (p1Bar == null)
        {
            Debug.LogError("HealthBar referans� atanmad�!");
        }
        if (p2Bar == null)
        {
            Debug.LogError("ManaBar referans� atanmad�!");
        }
        if (p1Text == null)
        {
            Debug.LogError("HealthText referans� atanmad�!");
        }
        if (p2Text == null)
        {
            Debug.LogError("ManaText referans� atanmad�!");
        }
    }

    void Update()
    {
        // Sa�l�k ve mana barlar�n� s�rekli g�ncelle
        if (p1Bar != null)
        {
            p1Bar.fillAmount = currentHealthp1 / p1Health;
        }
        else
        {
            Debug.LogError("p1 referans� bo�!");
        }

        if (p2Bar != null)
        {
            p2Bar.fillAmount = currentHealthp2 / p2Health;
        }
        else
        {
            Debug.LogError("p2 referans� bo�!");
        }

        // TextMeshPro metinlerini g�ncelle
        if (p1Text != null)
        {
            p1Text.text = "p1: " + currentHealthp1;
        }
        if (p2Text != null)
        {
            p2Text.text = "p2: " + currentHealthp2;
        }

        // Bilgileri konsola yazd�rma
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

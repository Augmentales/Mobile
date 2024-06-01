using UnityEngine;
using TMPro;

public class Example : MonoBehaviour
{
    public PlayerStats playerStats;
    public TMP_Text turnText;

    private bool isPlayer1Turn = true;

    private void Start()
    {
        turnText.text = string.Empty;
        turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayer1Turn = !isPlayer1Turn;
            turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";
            playerStats.TakeDamage(10);
            Debug.Log("Space tuþuna basýldý: 10 hasar alýndý.");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            isPlayer1Turn = !isPlayer1Turn;
            turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";
            playerStats.UseMana(10);
            Debug.Log("M tuþuna basýldý: 10 mana kullanýldý.");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isPlayer1Turn = !isPlayer1Turn;
            turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";
            playerStats.TakeDamage(10);
            Debug.Log("Yukarý ok tuþuna basýldý: 10 hasar alýndý.");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isPlayer1Turn = !isPlayer1Turn;
            turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";
            playerStats.UseMana(10);
            Debug.Log("Aþaðý ok tuþuna basýldý: 10 mana kullanýldý.");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isPlayer1Turn = !isPlayer1Turn;
            turnText.text = isPlayer1Turn ? "Turn P1" : "Turn P2";
            Debug.Log(turnText.text);
        }
    }
}

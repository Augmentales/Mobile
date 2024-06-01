using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    private bool dead;
    private bool attack;
    public Animator animator;
    
    public void setData(bool isDead, bool isAttacking)
    {
        dead = isDead;
        attack = isAttacking;
        gameObject.SetActive(!isDead);
        if (attack)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Karakter Atak Basladi");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //animator.start();
        //animator.setTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool isDead()
    {
        return dead;
    }
}

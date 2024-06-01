using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animator animator;

    [DllImport("SharedObject")]
    public static extern int getRandomNumber();

    [SerializeField]
    private float attackInterval; // Time in seconds between attacks

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(attackRoutine());
    }

    IEnumerator attackRoutine()
    {
        while (true)
        {
            attackInterval = getRandomNumber();
            yield return new WaitForSeconds(attackInterval);
            string logMessage = $"Attack Interval: {attackInterval}, Animator State: {animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")}";
            Debug.Log(logMessage);
            animator.SetTrigger("Attack");
        }
    }
}

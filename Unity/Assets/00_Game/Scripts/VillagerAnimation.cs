using UnityEngine;
using System.Collections;

public class VillagerAnimation : MonoBehaviour
{
    new Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	void Update ()
    {

    }

    public void Dance()
    {
        animator.SetTrigger("dance");
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
    }
}

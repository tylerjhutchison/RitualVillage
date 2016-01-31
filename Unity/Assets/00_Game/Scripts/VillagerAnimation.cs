using UnityEngine;
using System.Collections;

public class VillagerAnimation : MonoBehaviour
{
    new Animator animator;
	//Depending on the letter we select 

	void Start ()
    {
        animator = GetComponent<Animator>();
		//animator.SetLayerWeight ();
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

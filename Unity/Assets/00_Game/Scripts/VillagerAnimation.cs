using UnityEngine;
using System.Collections;

public class VillagerAnimation : MonoBehaviour
{
	public Animator anim;


	void Start ()
    {
		
		//animator.SetLayerWeight ();
    }
	
	void Update ()
    {

    }

	public void Init(string letter) {
		anim = GetComponent<Animator>();
		//this is getting called before start for
		char asciiLetter = letter.ToCharArray ()[0];
		int stateLayerIndex = System.Convert.ToInt32 (asciiLetter) - 97;
		anim.SetLayerWeight(0,1f);
		anim.SetLayerWeight(stateLayerIndex +1,1f);


	}

    public void Dance()
    {
		
		anim.SetTrigger("dance");
    }

    public void Idle()
    {
		anim.SetTrigger("idle");
    }
}

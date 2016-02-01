using UnityEngine;
using System.Collections;

public class VillagerAnimation : MonoBehaviour
{
	public Animator anim;
    public Villager villager;

	void Start ()
    {
        villager = GetComponent<Villager>();
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

    public void MusicReact ()
    {
        if (villager.currentState == Villager.State.Dancing)
        {
            iTween.PunchScale(gameObject,
                iTween.Hash
                (
                    "x", 2f,
                    "time", .6f

                )
            );
        }
    }

}

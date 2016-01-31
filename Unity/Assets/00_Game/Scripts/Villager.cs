using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour
{
    public enum State { Idle, Dancing}
    State currentState;

    public string letter;
    Material skinMaterial;
    Color origColor;
    TextMesh textMesh;

    new VillagerAnimation animation;

    public void Init(string s)
    {
        animation = GetComponent<VillagerAnimation>();

        skinMaterial = GetComponentInChildren<MeshRenderer>().material;
        origColor = skinMaterial.color;

        textMesh = GetComponentInChildren<TextMesh>();

        letter = s;
        textMesh.text = s;
    }

    void Update ()
    {
	    if (Input.GetKeyDown(letter))
        {
            Debug.Log(letter);
			if (currentState == State.Idle) {
				Dance ();
			} 
			else {
				StopDancing ();
			}
        }
	}

	void Dance (){
		currentState = State.Dancing;
		skinMaterial.color = Color.red;
        animation.Dance();
		StartCoroutine(StopDanceHelper(Random.Range (3.0f,10.0f)));
	}
		
	IEnumerator StopDanceHelper (float sleepTime)
    {
        yield return new WaitForSeconds(sleepTime);
		StopDancing ();
    }

	void StopDancing(){
		currentState = State.Idle;
		skinMaterial.color = origColor;
        animation.Idle();
	}


}

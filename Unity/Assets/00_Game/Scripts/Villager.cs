using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour
{
    public enum State { Idle, Dancing}
    public State currentState;

    public string letter;
    Material skinMaterial;
    Color origColor;
    TextMesh textMesh;
	public float timeSinceLastDanced;

    public void Init(string s)
    {
        skinMaterial = GetComponentInChildren<MeshRenderer>().material;
        origColor = skinMaterial.color;

        textMesh = GetComponentInChildren<TextMesh>();

        letter = s;
        textMesh.text = s;

		currentState = State.Idle;
		timeSinceLastDanced = Time.time;
    }

    void Update ()
    {
	    if (Input.GetKeyDown(letter))
        {
            //Debug.Log(letter);
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
		timeSinceLastDanced = Time.time;
	}

	public float TimeSpentIdle() {
		if (currentState == State.Idle) {
			return  Time.time - timeSinceLastDanced;
		} else {
			return 0;
		}
	}

}

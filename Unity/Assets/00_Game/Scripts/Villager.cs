using UnityEngine;
using System.Collections;
using DerelictComputer;

public class Villager : MonoBehaviour
{
    public enum State { Idle, Dancing, Watching}
    public State currentState;
    public Vector2 gridCoord; // the keyboard keys grid coordinates I am standing on

    // music stuff
    private ToggleSuspend musicToggle;
    private Pattern musicPattern;
    private Sampler musicSample;
    public bool usePitch;

    public string letter;
    Material skinMaterial;
    Color origColor;
    TextMesh textMesh;
	public float timeSinceLastDanced;

	Vector3 keyboardPosition;

    new VillagerAnimation animation;

    public void Init(string s)
    {
        animation = GetComponent<VillagerAnimation>();
		animation.Init ("b");

        skinMaterial = GetComponentInChildren<MeshRenderer>().material;
        origColor = skinMaterial.color;

        // music stuff
        musicToggle = GetComponent<ToggleSuspend>();
        musicPattern = GetComponent<Pattern>();
        SetMusicPattern();
        musicSample = GetComponent<Sampler>();
        SetMusicSample();

		keyboardPosition = transform.localPosition;

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
			else if (currentState == State.Dancing) {
				//OH FUCK! THIS SHOULD PISS HIM OFF.
				//SEND OUT A SIGNAL SHAKE THE CAMERA ETC.
				//Maybe you get a penalty of -2 seconds to start with.
				StopDancing ();
			}
			else {
				//Else do nothing. They are watching.

			}
        }
	}

	void Dance (){
        StartDancing();
		StartCoroutine(StopDanceHelper(Random.Range (3.0f,10.0f)));
	}
		
	IEnumerator StopDanceHelper (float sleepTime)
    {
        yield return new WaitForSeconds(sleepTime);
		StopDancing ();
    }

    void StartDancing()
    {
        musicToggle.Toggle();
        currentState = State.Dancing;
        skinMaterial.color = Color.red;
        animation.Dance();
    }

	void StopDancing(){
        musicToggle.Toggle();
        currentState = State.Idle;
		skinMaterial.color = origColor;
		timeSinceLastDanced = Time.time;
        animation.Idle();
    }

	public float TimeSpentIdle() {
		if (currentState == State.Idle) {
			return  Time.time - timeSinceLastDanced;
		} else {
			return 0;
		}
	}

	public void StartWatching (){
		currentState = State.Watching;
		//Let's go to a place on a curve between things. starting at 0 and going until blah.
		float xPos = Random.Range(0f,10f);
		float zPos = (-Mathf.Pow(xPos, 2)*.1f + xPos + 3);
		transform.localPosition = new Vector3(xPos ,0, zPos);

	}

	public void JoinDance (){
		currentState = State.Idle;
		timeSinceLastDanced = Time.time;
		transform.localPosition = keyboardPosition;

	}

    void SetMusicPattern ()
    {
        musicPattern.Steps[(int)gridCoord.x].Active = true;
    }

    void SetMusicSample ()
    {
        if (usePitch)
            musicSample.Samples[0].Pitch = gridCoord.x;
    }

}

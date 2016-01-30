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

    public void Init(string s)
    {
        skinMaterial = GetComponentInChildren<MeshRenderer>().material;
        origColor = skinMaterial.color;

        textMesh = GetComponentInChildren<TextMesh>();

        letter = s;
        textMesh.text = s;
    }

    void Update ()
    {
	    if (Input.GetKey(letter))
        {
            StartCoroutine(Dance());
        }
	}

    IEnumerator Dance ()
    {
        currentState = State.Dancing;
        skinMaterial.color = Color.red;
        yield return new WaitForSeconds(1);
        skinMaterial.color = origColor;
        currentState = State.Idle;
    }


}

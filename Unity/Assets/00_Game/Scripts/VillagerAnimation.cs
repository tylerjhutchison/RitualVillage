using UnityEngine;
using System.Collections;

public class VillagerAnimation : MonoBehaviour
{
    public Transform headJoint;

	void Start ()
    {

    }
	
	void Update ()
    {
        //headJoint.right = -(headJoint.position - Camera.main.transform.position);
        //headJoint.Rotate(Vector3.right, 90);
        //headJoint.Rotate(Vector3.forward, 180);

        headJoint.LookAt(Camera.main.transform);
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Villager) )]
public class VillagerController : MonoBehaviour
{
    public Villager villagerPrefab;

    public Villager[] villagers;

    public Vector2 gridSpaceSize;
    string[] qwerty = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
	
	void Start ()
    {
        //create all the villagersy
        //create a char of chars

        int column = 0;
        for (int i=0; i< qwerty.Length; i++)
        {
            print(i);

            float posX = 0;
            float posZ = 0;

            if (i > 18)
            {
                // front row
                posX = (i % 19) * gridSpaceSize.x;
                posZ = 0;
            } 
            else if (i > 9)
            {
                // second row
                posX = (i % 10) * gridSpaceSize.x;
                posZ = 1 * gridSpaceSize.y;
            }
            else
            {
                // back row
                posX = (i %10) * gridSpaceSize.x;
                posZ = 2 * gridSpaceSize.y;
            }
            Vector3 pos = new Vector3(posX, 0, posZ);
            Villager myVillager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity) as Villager;
            myVillager.transform.parent = this.gameObject.transform;
            myVillager.transform.localPosition = pos;

            myVillager.Init(qwerty[i]);
        }
        //for each letter of the alphabet, create a villager

    }
	
	void Update ()
    {
	
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Villager) )]
public class VillagerController : MonoBehaviour
{
    public Villager villagerPrefab;

    public Villager[] villagers;

    public Vector2 gridSpaceSize;
	
	void Start ()
    {
        //create all the villagersy
        //create a char of chars
        for (char c = 'A'; c <= 'Z'; c++)
        {
            int i = System.Convert.ToInt32(c) - 65;

            float posX = (i%10) * gridSpaceSize.x;
            float posZ = 0;

            if (i > 19)
            {
                // front row
                posZ = 0;
            } 
            else if (i > 10)
            {
                // second row

                posZ = 1 * gridSpaceSize.y;
            }
            else
            {
                // back row
                posZ = 2 * gridSpaceSize.y;
            }
            Vector3 pos = new Vector3(posX, 0, posZ);
            Villager myVillager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity) as Villager;
            myVillager.transform.parent = this.gameObject.transform;
            myVillager.transform.localPosition = pos;

            myVillager.Init(c);
        }
        //for each letter of the alphabet, create a villager

    }
	
	void Update ()
    {
	
	}
}

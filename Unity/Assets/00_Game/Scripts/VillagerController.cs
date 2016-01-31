using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Villager) )]
public class VillagerController : MonoBehaviour
{
    public Villager villagerPrefab;
    public GameObject groundTilePrefab; // HACK

    public Villager[] villagers;

    public Vector2 gridSpaceSize;
    string[] qwerty = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
    public float frontRowShift;
    public float middleRowShift;

	void Start ()
    {
        //create all the villagersy
        //create a char of chars

        // HACK for ground tile / keyboard
        GameObject keyboard = new GameObject("Keyboard");
        keyboard.transform.position = this.transform.position;

        for (int i=0; i< qwerty.Length; i++)
        {
            //print(i);

            float posX = 0;
            float posZ = 0;

            if (i > 18)
            {
                // front row
                posX = (i % 19) * gridSpaceSize.x + frontRowShift;
                posZ = 0;
            } 
            else if (i > 9)
            {
                // second row
                posX = (i % 10) * gridSpaceSize.x + middleRowShift;
                posZ = 1 * gridSpaceSize.y;
            }
            else
            {
                // back row
                posX = (i %10) * gridSpaceSize.x;
                posZ = 2 * gridSpaceSize.y;
            }
            // villager spawn
            Vector3 pos = new Vector3(posX, 0, posZ);
            Villager myVillager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity) as Villager;
            myVillager.transform.parent = this.gameObject.transform;
            myVillager.transform.localPosition = pos;

            // HACK ground tile spawn
            GameObject groundTile = Instantiate(groundTilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            groundTile.transform.parent = this.gameObject.transform;
            groundTile.transform.localPosition = pos;
            groundTile.transform.parent = keyboard.transform;

            myVillager.Init(qwerty[i]);
        }
        //for each letter of the alphabet, create a villager

    }
	
	void Update ()
    {
	
	}
}

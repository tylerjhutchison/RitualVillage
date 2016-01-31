using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent( typeof(Villager) )]
public class VillagerController : MonoBehaviour
{
    public Villager villagerPrefab;
    public GameObject groundTilePrefab; // HACK
	GameObject keyboard;

    //public Villager[] villagers;
	List<Villager> villagers;
	//Create a Row for all the villagers
	List<Villager> frontRow;
	List<Villager> middleRow;
	List<Villager> backRow;


    public Vector2 gridSpaceSize;
    string[] qwerty = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
    public float frontRowShift;
    public float middleRowShift;
	public float timeToNextFaithCheck;

	void Start ()
    {
        //create all the villagersy
        //create a char of chars
		villagers = new List<Villager> ();

		frontRow = new List<Villager> ();
		middleRow = new List<Villager> ();
		backRow = new List<Villager> ();

        // HACK for ground tile / keyboard
        keyboard = new GameObject("Keyboard");
        keyboard.transform.position = this.transform.position;

        for (int i=0; i< qwerty.Length; i++)
        {
            //print(i);
            if (i > 18)
            {
                // front row
				Vector3 pos = new Vector3((i % 19) * gridSpaceSize.x + frontRowShift, 0, 0);
				SpawnVillager (pos, qwerty[i], frontRow);
            } 
            else if (i > 9)
            {
                // second row
				Vector3 pos = new Vector3((i % 10) * gridSpaceSize.x + middleRowShift, 0,  1 * gridSpaceSize.y);
				SpawnVillager (pos, qwerty[i], middleRow);
            }
            else
            {
                // back row
				Vector3 pos = new Vector3((i %10) * gridSpaceSize.x, 0,  2 * gridSpaceSize.y);
				SpawnVillager (pos, qwerty[i], backRow);
            }
            
        }
        //for each letter of the alphabet, create a villager
		StartCoroutine (UpdateFaith ());


    }

	void SpawnVillager( Vector3 pos, string letter, List<Villager> row) {
		// villager spawn
		Villager myVillager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity) as Villager;
		myVillager.transform.parent = this.gameObject.transform;
		myVillager.transform.localPosition = pos;
		villagers.Add (myVillager);
		row.Add (myVillager);

		// HACK ground tile spawn
		GameObject groundTile = Instantiate(groundTilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		groundTile.transform.parent = this.gameObject.transform;
		groundTile.transform.localPosition = pos;
		groundTile.transform.parent = keyboard.transform;

		myVillager.Init(letter);
	}

	void Update ()
    {
		
	}

	/**
	 * Continually poll villagers faith.
	 */
	IEnumerator UpdateFaith() {
		float refreshRate = .5f;
		float totalLackOfFaith = 0;

		while (totalLackOfFaith < 60f) {

			totalLackOfFaith = 0;
			foreach (Villager currentVillager in villagers)
			{
				totalLackOfFaith += currentVillager.TimeSpentIdle ();
			}
			print ("Total Faith: " + totalLackOfFaith);
			
			yield return new WaitForSeconds (refreshRate);
		}

		print ("YOU LOSE!");
	}
}

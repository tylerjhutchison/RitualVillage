using UnityEngine;
//using UnityEditor;
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
	Queue<Villager> frontRow;
	Queue<Villager> middleRow;
	Queue<Villager> backRow;

	public event System.Action OnStartGame;
	public event System.Action OnGameOver;



    public Vector2 gridSpaceSize;
    string[] qwerty = new string[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
    public float frontRowShift;
    public float middleRowShift;
	public float timeToNextFaithCheck;
	public float totalLackOfFaith { get; protected set;}

	void Start ()
    {
        //create all the villagersy
        //create a char of chars
		villagers = new List<Villager> ();

		frontRow = new Queue<Villager> ();
		middleRow = new Queue<Villager> ();
		backRow = new Queue<Villager> ();

        // HACK for ground tile / keyboard
        keyboard = new GameObject("Keyboard");
        keyboard.transform.position = this.transform.position;

        for (int i=0; i< qwerty.Length; i++)
        {
			SpawnVillager (qwerty[i]);
        }
		//Send the the back and front row to the edge of the map.
		MakeRowWatch(backRow);
		MakeRowWatch(frontRow);

        //for each letter of the alphabet, create a villager
		StartGame();
    }


	void StartGame(){

		OnStartGame ();
		StartCoroutine (UpdateFaith ());
		StartCoroutine (IncreaseDifficulty ());
	}


	void SpawnVillager( string letter) {
		// villager spawn
		Vector3 pos = TranslateAlphabetToCoord(letter);
		Villager myVillager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity) as Villager;
        myVillager.gridCoord = TranslateAlphabetToGridCoord(letter);
        myVillager.transform.parent = this.gameObject.transform;
		myVillager.transform.localPosition = pos;
		villagers.Add (myVillager);
		FindMyRow (letter).Enqueue (myVillager);
		//FindMyRow (letter).Add (myVillager);

		// HACK ground tile spawn
		GameObject groundTile = Instantiate(groundTilePrefab, new Vector3(0,-.2f,0), Quaternion.identity) as GameObject;
		groundTile.transform.parent = this.gameObject.transform;
		groundTile.transform.localPosition = pos;
		groundTile.transform.parent = keyboard.transform;

		myVillager.Init(letter);
	}

	void Update ()
    {
		
	}

    int IndexOf(string[] strings, string s)
    {
        int myIndex = -1;
        for (int i = 0; i < strings.Length; i++)
        {
            if (s == strings[i])
            {
                myIndex = i;
            }
        }
        return myIndex;
    }

	Vector3 TranslateAlphabetToCoord (string letter)
	{
		int i = IndexOf (qwerty, letter);
		Vector3 pos;

		if (i > 18)
		{
			// front row
			pos = new Vector3((i % 19) * gridSpaceSize.x + frontRowShift, 0, 0);
		} 
		else if (i > 9)
		{
			// second row
			pos = new Vector3((i % 10) * gridSpaceSize.x + middleRowShift, 0,  1 * gridSpaceSize.y);
		}
		else
		{
			// back row
			pos = new Vector3((i %10) * gridSpaceSize.x, 0,  2 * gridSpaceSize.y);
		}
		return pos;
	}

    Vector2 TranslateAlphabetToGridCoord (string letter)
    {
        int i = IndexOf(qwerty, letter);
        Vector2 gridCoord;

        if (i > 18)
        {
            // front row
            gridCoord = new Vector2((i % 19), 0);
        }
        else if (i > 9)
        {
            // second row
            gridCoord = new Vector2((i % 10), 1);
        }
        else
        {
            gridCoord = new Vector2((i % 10), 2);
        }
        return gridCoord;
    }

	Queue<Villager> FindMyRow (string letter) {
		int i = IndexOf (qwerty, letter);
		Queue<Villager> myRow;

		if (i > 18)
		{
			myRow = frontRow;
		} 
		else if (i > 9)
		{
			myRow = middleRow;
		}
		else
		{
			myRow = backRow;
		}
		return myRow;
	}

	void MakeRowWatch (Queue<Villager> currentRow) {
		foreach (Villager currentVillager in currentRow) {
			currentVillager.StartWatching ();
		}
	}

	void MakeRowJoin (List<Villager> currentRow) {
		foreach (Villager currentVillager in currentRow) {
			currentVillager.JoinDance ();
		}
	}


	/**
	 * Continually poll villagers faith.
	 */
	IEnumerator UpdateFaith() {
		float refreshRate = .5f;
		totalLackOfFaith = 0;

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
		GameOver ();
	}


	/**
	 * Continually poll villagers faith.
	 */
	IEnumerator IncreaseDifficulty() {


		while (backRow.Count > 0 && frontRow.Count > 0) {
			yield return new WaitForSeconds (10);

			Queue<Villager> useRow;
			if (frontRow.Count > 0) {
				useRow = frontRow;
			} 
			else {
				useRow = backRow;
			}
				
			Villager newVillager = useRow.Dequeue ();
			newVillager.JoinDance ();

			//MakeRowJoin (backRow);

		}




	}

	[ContextMenu ("Lose Game")]
	void GameOver(){
		OnGameOver ();
	}
}

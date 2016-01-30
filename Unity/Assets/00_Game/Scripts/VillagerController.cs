using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Villager) )]
public class VillagerController : MonoBehaviour
{
    public Villager villagerPrefab;

    public Villager[] villagers;    
	
	void Start ()
    {

        //create all the villagers
        //create a char of chars
        for (char c = 'A'; c <= 'Z'; c++)
        {
            //do something with letter
            //print(c); 
            Villager myVillager = Instantiate(villagerPrefab,Vector3.zero, Quaternion.identity) as Villager;
        }
        //for each letter of the alphabet, create a villager

    }
	
	void Update ()
    {
	
	}
}

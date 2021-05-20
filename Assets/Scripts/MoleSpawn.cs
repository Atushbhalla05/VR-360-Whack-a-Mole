using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//********************************************************************************//
//********SCRIPT THAT RUNS LOGGIC FOR SPAWNING MOLES FOR THE PLAYER TO HIT********//
//********************************************************************************//

public class MoleSpawn : MonoBehaviour
{
    public GameObject molePrefab;
    public GameObject[] moleSpawns;
    private float spawnInterval = 2f;
    public float currSpawnTime = 0f;
    public float currTime = 0f;
    public float reduceSpawnTimer = 8f;
    private GameObject[] moleInstantiate = new GameObject[12];


    void Update()
    {
      currSpawnTime += Time.deltaTime;
      currTime += Time.deltaTime;

      //Keep track for last spawned and Reset the timer
      if(currSpawnTime >= spawnInterval){
        SpawnMole();
        currSpawnTime = 0f;
      }

      //spawnInterval lower bound to 0.75 seconds
      //We also reduce spawn timer each 8 seconds of GamePlay.
      if(currTime >= reduceSpawnTimer && spawnInterval >= 0.75f){
        currTime = 0;
        //Reducing spawn interval to increase difficulty
        spawnInterval -= 0.1f; // Make a lower bound for spawnInterval so that it's not instantly respawning moles
      }
    }

    //Function to Spawn Moles at certain spawn location
    void SpawnMole(){

      //We have 12 spawn locations out of which we
      //have to keep track of which spawn location is curently not in use
      int randomPos = Random.Range(0,12);
      if(!moleInstantiate[randomPos]){
        Vector3 spawnPos = moleSpawns[randomPos].transform.position;
        //Mole position is a little low so that we can show an animation of it coming up.
        Vector3 molePosition = new Vector3(spawnPos.x,spawnPos.y - 0.5f, spawnPos.z);
        //All moles will be Instantiated in a way that they face the Player.
        moleInstantiate[randomPos] = Instantiate(molePrefab, molePosition, moleSpawns[randomPos].transform.rotation);
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//********************************************************************************//
//**SCRIPT THAT RUNS AT START OF GAME TO SHOW SOME GAMEOBJECTS AND DISABLE OTHER**//
//********************************************************************************//

public class GameStarter : MonoBehaviour
{
		public GameObject[] objectsToEnable;
		public MonoBehaviour[] scriptsToEnable;
		public GameObject[] objectsToDisable;
		public MonoBehaviour[] scriptsToDisable;

    void Update()
    {
			//Press Enter enable some game objects and disable some
			if (Input.GetKeyDown(KeyCode.Return)) {

				//Activating  objects
				for (int i = 0; i < objectsToEnable.Length; i++) {
					objectsToEnable[i].SetActive(true);
				}

				//Activating  scripts
				for (int i = 0; i < scriptsToEnable.Length; i++) {
					scriptsToEnable[i].enabled = true;
				}

				//Deactivating objects
				for (int i = 0; i < objectsToDisable.Length; i++) {
					objectsToDisable[i].SetActive(false);
				}

				//Deactivating scripts
				for (int i = 0; i < scriptsToDisable.Length; i++) {
					scriptsToDisable[i].enabled = false;
				}
			}
    }
}

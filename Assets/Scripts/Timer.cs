using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//********************************************************************************//
//********************* SCRIPT FOR TIMER OF GAME *********************************//
//********************************************************************************//

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    private float currTime = 0f;
		private float currScore = 0f;
		private bool ended;
    public float startTime = 90.0f;
    public Text countDown;
		public Text scoreText;
    public Text GameOverText;
		public GameObject[] objectsToTrigger;
		public MonoBehaviour[] scriptsToTrigger;

    //At start of game, start timer
    void Start()
    {
			ended = false;
      currTime = startTime;
			scoreText.text = "Score: " + currScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        //Run game timer backwards and print string without decimal
        currTime -= 1 * Time.deltaTime;
        countDown.text = "Time: " + currTime.ToString("0");

        // If timer goes less than 0, Game Ends
        if(currTime <= 0)
        {
            currTime = 0;
            //Game Ends Here
						if (!ended) {
              //Show the final score and deactivate some UI elements to show final game screen.
							ended = true;
							string finalScore = scoreText.text;
							countDown.gameObject.SetActive(false);
							scoreText.gameObject.SetActive(false);

              //We have an array that will deactivate at the
              //end of the game and activate back whhent he game starts.
							for (int i = 0; i < objectsToTrigger.Length; i++) {
								objectsToTrigger[i].SetActive(!objectsToTrigger[i].activeSelf);
							}

							for (int i = 0; i < scriptsToTrigger.Length; i++) {
								scriptsToTrigger[i].enabled = !scriptsToTrigger[i].enabled;
							}

              //Showing game over text
							GameOverText.gameObject.SetActive(true);
							GameOverText.text = "GAME OVER\n"+finalScore+"\nPress [R] to restart\nPress [Enter] to Exit Game";
						}

            //Click Enter to quit game
            if(Input.GetKeyDown(KeyCode.Return)){
              Application.Quit();
            }

            //Click R to restart game
            if(Input.GetKeyDown(KeyCode.R)){
              SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }

        //Show red color for countdown as the player is coming close to game end
        if(currTime <= 10)
        {
            countDown.color = Color.red;
        }
        //Show blue color for countdown as the player as ample of time to earn points
				if(currTime > 10) {
					countDown.color = Color.blue;
				}
    }

    //Function to increase or decrease score
		public void AddScore(float points) {
			currScore += points;
			scoreText.text = "Score: " + currScore.ToString("0");
		}

    //Function to increase or decrease timer
		public void AddTime(float time) {
			currTime += time;
			countDown.text = "Time: " + currTime.ToString("0");
		}
}

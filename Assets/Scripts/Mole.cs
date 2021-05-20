using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//********************************************************************************//
//******THIS SCRIPT SHOWS THE BEHAVIOUR OF THE MOLES APPEARING IN THE GAME *******//
//********************************************************************************//

public class Mole : MonoBehaviour
{
    // Start is called before the first frame update
    private float timetoGoUp = 1f;
    private float currLifeTime = 0f;
    private float moleLife = 3f;
    private float timetoGoDown = 1f;
    private IEnumerator coroutineUp;
    private bool rightMole;
		private GameObject scoreboard;
    public AudioSource[] takeDamageSound;
		public AudioSource wrongHitSound;
		public GameObject rightHitParticles;
		public GameObject wrongHitParticles;
		public Material wrongMoleMaterial;
    void Start()
    {
        //At the start we decide if the mole will be the
        //right mole and the wrong mole for the player to hit
        float percent = Random.Range(0.0f, 1.0f);
        if (percent < 0.667f) {
          // 67% chance for right mole
					// Change appearance of mole depending on if it is right or wrong
          rightMole = true;
        } else {
          rightMole = false;
					GetComponentInChildren<Renderer>().material = wrongMoleMaterial;
        }
				scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
        coroutineUp = MoveMoleUp();
        //Using a Coroutine to show an animation of moles moving up
        StartCoroutine(coroutineUp);
    }

    // Update is called once per frame
    void Update()
    {
      currLifeTime += Time.deltaTime;
      //If life of mole more than moleslife, coroutine for mole go down
      if(currLifeTime >= moleLife){
        StartCoroutine(MoveMoleDown());
        //Destroy Mole after 1.2 seconds of life
        Destroy(gameObject, 1.2f);
      }
    }

    //OnTrigger Even if Hammer hits the moles
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Hammer") {
        StopCoroutine(coroutineUp);
        // What should happen when a mole gets hit? (particles, sound, SendMessage to scoreboard)
        currLifeTime = moleLife;

        if (rightMole) {
          //Show Right particle hits and play sound for dying animation.
          Instantiate(rightHitParticles, transform);
					for (int i = 0; i < takeDamageSound.Length; i++) {
						takeDamageSound[i].Play();
					}
          // If the rightMole is hit, then SendMessage to scoreboard for positive points, positive particles/sounds
					scoreboard.SendMessage("AddScore", 1.0f);
					scoreboard.SendMessage("AddTime", 1.0f);

        } else {
          //Show wrong particle hits and play error sound.
					Instantiate(wrongHitParticles, transform);
					wrongHitSound.Play();
          // Not the right mole, SendMessage to scoreboard for negative points, negative particles/sounds
					scoreboard.SendMessage("AddScore", -10.0f);
					scoreboard.SendMessage("AddTime", -10.0f);

        }
      }
    }

    //Coroutine for the Mole to go Up
    IEnumerator MoveMoleUp(){
      float currTime = 0f;
      Vector3 finalPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
    // currentPos should be saved so that transform.position doesn't keep changing when passed to Vector3.Lerp
    // But since it gives a logarithmic movement effect that fits a mole springing up, this is fine
    // Same for MoveMoleDown()
      while(currTime <= timetoGoUp){
        transform.position = Vector3.Lerp(transform.position, finalPos, currTime/timetoGoUp);
        currTime += Time.deltaTime;
        yield return null;
      }
    }

    //Coroutine for the Mole to go Down
    IEnumerator MoveMoleDown(){
			GetComponent<Collider>().enabled = false;
      float currTime = 0f;
      Vector3 finalPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
      while(currTime <= timetoGoDown){
        transform.position = Vector3.Lerp(transform.position, finalPos, currTime/timetoGoUp);
        currTime += Time.deltaTime;
        yield return null;
      }
    }
}

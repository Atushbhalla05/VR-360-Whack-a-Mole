using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    //This script destroys the prefab to which it is attached
    //when the duration of its particle system ends.
		void Start() {
			Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
		}
}

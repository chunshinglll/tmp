﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
		//private Complete.TankHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 50f;            // How long between each spawn.
        //public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
		private GameObject player; 
		public List<GameObject> enemylist;
		public GameObject gameman;
        void Start ()
        {
			//player = GameObject.FindGameObjectWithTag ("Player");
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
			//InvokeRepeating ("Spawning", spawnTime, spawnTime);
        }
		public void StartSpawn(){
			InvokeRepeating ("Spawning", spawnTime, spawnTime);
		}
		public void PauseSpawn(){
			CancelInvoke ();
		}
		public void EndSpawn(){
			CancelInvoke ();
			foreach (GameObject a in enemylist) {
				Destroy (a);
			}

			enemylist.Clear ();

		}
		private IEnumerator Spawn ()
        {
            // If the player has no health left...
			float remainHP = player.GetComponent<Complete.TankHealth>().CurrentHealth;
			if(remainHP<= 0f)
            {
                // ... exit the function.
				yield return spawnTime;
            }

            // Find a random index between zero and one less than the number of spawn points.
            //int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Transform a = player.GetComponent<TankManager> ().m_SpawnPoint;
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			Instantiate (enemy, new Vector3(a.transform.position.x+Random.Range(-30,30),0,a.transform.position.z+Random.Range(-30,30)), a.transform.rotation);
			yield return spawnTime;
        }
		void Spawning ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			// If the player has no health left...
			if (player==null) return ;
			float remainHP = player.GetComponent<Complete.TankHealth>().CurrentHealth;
			if(remainHP<= 0f)
			{
				// ... exit the function.
				 return ;
			}

			// Find a random index between zero and one less than the number of spawn points.
			//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Transform a = player.transform;
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			enemylist.Add(
				Instantiate (enemy, new Vector3(a.transform.position.x+Random.Range(-30,30),0,a.transform.position.z+Random.Range(-30,30)), a.transform.rotation) as GameObject);

		}
    }
}
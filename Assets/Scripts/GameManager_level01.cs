﻿//Timmy Chan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager_level01 : MonoBehaviour, IGameManager {
	public SpawnManager spawnManager;
	public GameHealthManager gameHealthManager;
	public Transform enemyManager;
	public GameObject longBow;
	public GameObject gameOverMessage;

	private float timeToStart = 3f;
	public bool started = false;
	public bool levelEnded = false;
	// Use this for initialization
	void Start () {
		gameHealthManager.gameManager = (IGameManager)this;
	}

	// Update is called once per frame
	void Update() {
		if (!levelEnded) {
			if (!started) { 
				if (timeToStart >= 0) {
					timeToStart -= Time.deltaTime;
				} else if (longBow.GetComponent<Longbow>().IsAttachedToHand()) {
					GameStart ();
				}
			}
			if (started) {
				//if there are no more lives
				/*if (gameHealthManager.RemainingGameHealth () <= 0) {
					GameLost ();
					levelEnded = true;
				}*/
				//if there are no more enemies to be spawned
				if (spawnManager.RemainingWavesCount () == 0) {
					//if there are no more enemies alive
					if (enemyManager.childCount == 0) {
						GameWin ();
						levelEnded = true;
					}
				}
			}
		}
		else {
			//what to do?
		}
	}

	public void GameLost() {
		levelEnded = true;
		print("You have lost the game");
		spawnManager.StopSpawning();
		var message = Instantiate(gameOverMessage, Player.instance.hmdTransform.position, Quaternion.identity).GetComponent<GameUI_ImportantMessage>();
		message.cam = Player.instance.hmdTransform;
		message.transform.position = Player.instance.hmdTransform.position;
		//change scene??
	}

	public void GameStart() {
		started = true;
		print ("Game Start!");
		spawnManager.StartSpawningWaves();
	}

	public void GamePause() {
		throw new System.NotImplementedException();
	}

	public void GameRestart() {
		throw new System.NotImplementedException();
	}

	public void GameWin() {
		levelEnded = true;
		print("You have won the game");
	}
}
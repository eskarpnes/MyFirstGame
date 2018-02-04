﻿//Timmy Chan
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_level01 : MonoBehaviour, IGameManager
{
	public SpawnManager spawnManager;
	public EnemyManager enemyManager;
	public GameHealthManager gameHealthManager;
	public GameUI_ImportantMessage importantMessage;
	public CrowSpawner[] crowSpawners;

	private float timeToStart = 3f;
	public bool started = false;
	public bool levelEnded = false;

	void Start()
	{
		gameHealthManager.gameManager = (IGameManager)this;
	}

	void Update()
	{
		// TEMPORARY DEVELOPER HOTKEYS:

		if (Input.GetKeyDown(KeyCode.Q))
			GameWin();
		if (Input.GetKeyDown(KeyCode.W))
			GameStart();
		if (Input.GetKeyDown(KeyCode.K))
		{
			foreach (Enemy enemy in enemyManager.GetComponentsInChildren<Enemy>())
				enemy.InflictDamage(enemy.startHealth);
		}
		// END


		if (!levelEnded)
		{
			if (!started)
			{
				if (Input.GetKeyUp(KeyCode.A))
					GameStart();
				if (Input.GetKeyUp(KeyCode.S))
					GameLost();
				if (timeToStart >= 0)
				{
					//timeToStart -= Time.deltaTime;
				} else
				{
					//GameStart ();
				}
			}
			if (started)
			{
				//if there are no more lives
				/*if (gameHealthManager.RemainingGameHealth () <= 0) {
					GameLost ();
					levelEnded = true;
				}*/
				//if there are no more enemies to be spawned
				if (spawnManager.RemainingWavesCount() == 0)
				{
					//if there are no more enemies alive
					if (enemyManager.transform.childCount == 0)
					{
						GameWin();
						levelEnded = true;
					}
				}
			}
		} else
		{
			//what to do?
		}
	}

	public void GameLost()
	{
		levelEnded = true;
		print("You have lost the game");
		spawnManager.StopSpawning();
		importantMessage.Show("Game Over");
		SoundManager.PlayLevelLostSound();
		//Invoke ("LoadMenu", 5f);
		//change scene??
		Invoke("GameRestart", 10f);
	}

	public void GameStart()
	{
		if (started)
			return;
		print("Game Start!");
		SoundManager.PlayStinger();
		Invoke("StartSpawn", 2f);
	}

	public void StartSpawn()
	{
		SoundManager.PlayCrowSound();
		foreach (CrowSpawner crowSpawner in crowSpawners)
			crowSpawner.SpawnCrows();

		spawnManager.StartSpawningWaves();
		started = true;
	}

	public void GamePause()
	{
		throw new System.NotImplementedException();
	}

	public void GameRestart()
	{
		SceneManager.LoadScene("menu");
	}

	public void GameWin()
	{
		levelEnded = true;
		print("You have won the game");
		importantMessage.Show("Game Win");
		SoundManager.PlayWinFanfare();
		//Invoke("GameRestart", 5f);
	}

	void LoadMenu()
	{
		SceneManager.LoadScene("menu");
	}
}

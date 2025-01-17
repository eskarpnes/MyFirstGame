﻿//Timmy Chan
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_level01 : MonoBehaviour, IGameManager
{
	private static GameManager_level01 THIS;

	public SpawnManager spawnManager;
	public EnemyManager enemyManager;
	public GameHealthManager gameHealthManager;
	public GameUI_ImportantMessage importantMessage;

	public string sceneLoadedOnRestart = "menu";

	public bool started = false;
	public bool levelEnded = false;

	void Awake()
	{
		if (THIS == null)
		{
			THIS = this;
		} else if (THIS != this)
		{
			Debug.LogWarning("There's more than one GameManager in the scene!");
			Destroy(gameObject);
		}
	}

	void Start()
	{
		gameHealthManager.gameManager = this;
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
		spawnManager.StartSpawningWaves();
		started = true;
	}

	public void GamePause()
	{
		throw new System.NotImplementedException();
	}

	public void GameWin()
	{
		levelEnded = true;
		print("You have won the game");
		importantMessage.Show("Game Win");

		AudioClip sound = SoundManager.PlayWinFanfare();
		Invoke("GameRestart", Mathf.Max(sound.length, 5f));
	}

	public void GameLost()
	{
		levelEnded = true;
		print("You have lost the game");
		spawnManager.StopSpawning();
		importantMessage.Show("Game Over");

		AudioClip sound = SoundManager.PlayLevelLostSound();
		Invoke("GameRestart", Mathf.Max(sound.length, 10f));
	}

	public void GameRestart()
	{
		SceneManager.LoadScene(sceneLoadedOnRestart);
	}
}

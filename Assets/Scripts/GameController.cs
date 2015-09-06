using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	private int score;

	public Text comboText;
	private int combo;

	public Text gameOverText;
	public Text restartText;

	private bool gameOver;
	private bool restart;



	void Start ()
	{
		gameOver = false;
		restart = false;

		gameOverText.text = "";
		restartText.text = "";
		comboText.text = "";

		score = 0;
		combo = 1;

		UpdateScore ();

		StartCoroutine (SpawnWaves ());
	}

	void Update (){
		if (restart) 
		{
			if(Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver){
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{

		score += newScoreValue * combo;
		combo += 1;
		UpdateScore ();

	}

	public void ResetCombo ()
	{
		combo = 1;
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
		comboText.text = "combo x " + combo;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}

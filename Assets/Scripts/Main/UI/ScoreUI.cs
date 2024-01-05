using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Text scoreTextShadow;
	[SerializeField]
	private int _score;

	private void Start()
	{
		_score = 0;
		AddScore(0);
	}

	public void AddScore(int amount)
	{
		_score += amount;
		_score = Mathf.Clamp(_score, 0, 99999);
		scoreText.text = $"SCORE : {_score:D5}";
		scoreTextShadow.text = scoreText.text;
	}
}
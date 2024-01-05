using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField] private GameUI gameUI;
	[SerializeField] private CameraScroll scroll;

	public int enemyCount;
	public int killedEnemyCount;
	private int _playerLayer;

	private void Start()
	{
		_playerLayer = LayerMask.NameToLayer("Player");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer != _playerLayer) return;

		scroll.stop = true;
		gameUI.Success(enemyCount == killedEnemyCount);
	}
}
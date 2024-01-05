using UnityEngine;

public class Beam : MonoBehaviour
{
	[SerializeField] private GameObject enemyBeam;
	[SerializeField] private AudioClip hitSe;
	public float speed;
	public ScoreUI scoreUI;
	private Transform _transform;
	private int _playerLayer;
	private int _enemyLayer;
	private int _goalLayer;
	private int _wallLayer;

	private void Start()
	{
		_transform = transform;
		_playerLayer = LayerMask.NameToLayer("Player");
		_enemyLayer = LayerMask.NameToLayer("Enemy");
		_goalLayer = LayerMask.NameToLayer("Goal");
		_wallLayer = LayerMask.NameToLayer("Wall");
	}

	private void Update()
	{
		var pos = _transform.position;
		pos.x += speed * Time.deltaTime;
		_transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var layer = other.gameObject.layer;
		//敵の場合は弾を消す
		if (layer == _enemyLayer)
		{
			var enemy = other.GetComponent<Enemy>();
			scoreUI.AddScore(enemy.score);
			Destroy(gameObject);
			return;
		}

		//プレイヤーの場合は無視
		if (layer == _playerLayer) return;
		if (layer == _goalLayer) return;
		if (layer == _wallLayer)
		{
			Destroy(gameObject);
			return;
		}

		//その他の場合は跳ね返す
		var pos = _transform.position;
		var beamGo = Instantiate(enemyBeam, pos, Quaternion.identity);
		var beam = beamGo.GetComponent<EnemyBeam>();
		beam.speed = -speed;
		AudioSource.PlayClipAtPoint(hitSe, pos);
		Destroy(gameObject);
	}
}
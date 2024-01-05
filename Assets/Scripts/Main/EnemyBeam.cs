using UnityEngine;

public class EnemyBeam : MonoBehaviour
{
	public float speed;
	private Transform _transform;

	private int _playerLayer;

	private void Start()
	{
		_transform = transform;
		_playerLayer = LayerMask.NameToLayer("Player");
	}

	private void Update()
	{
		var pos = _transform.position;
		pos.x += speed * Time.deltaTime;
		_transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//プレイヤー以外の場合は無視
		if (other.gameObject.layer != _playerLayer) return;

		//プレイヤーにダメージを与える
		var player = other.gameObject.GetComponent<Player>();
		//攻撃が出来たときだけ、弾を消す
		if (player.Damage())
		{
			Destroy(gameObject);
		}
	}
}
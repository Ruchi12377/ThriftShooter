using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject beam;
	[SerializeField] private AudioClip shotSE;
	[SerializeField] private AudioClip damageSE;
	public int score;

	private int _beamSpeed;
	private int _beamLayer;
	private Transform _transform;

	private static Goal _goal;
	private static Player _player;

	private void Start()
	{
		_beamLayer = LayerMask.NameToLayer("Beam");
		_transform = transform;

		if (_player == null)
		{
			_player = FindObjectOfType<Player>();
		}

		if (_goal == null)
		{
			_goal = FindObjectOfType<Goal>();
		}

		_goal.enemyCount++;

		Invoke(nameof(Shoot), Random.Range(1, 2));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer != _beamLayer) return;

		AudioSource.PlayClipAtPoint(damageSE, _transform.position);
		_goal.killedEnemyCount++;
		Destroy(gameObject);
	}

	private void Shoot()
	{
		var nextShootTime = Random.Range(4, 10);
		var pos = _transform.position;
		var pPos = _player.transform.position;

		//まだ画面に映ってないから無視
		if (pos.x - pPos.x > 20)
		{
			Invoke(nameof(Shoot), nextShootTime);
			return;
		}

		var beamPos = pos;
		beamPos.x -= 1;
		var shootBeam = Instantiate(beam, beamPos, Quaternion.identity).GetComponent<EnemyBeam>();
		shootBeam.speed = _player.Mode switch
		{
			GameMode.Normal => _player.normalSpeed,
			GameMode.Hard => _player.hardSpeed,
			GameMode.Hell => _player.hellSpeed,
			_ => throw new ArgumentOutOfRangeException()
		};
		shootBeam.speed *= -1;

		AudioSource.PlayClipAtPoint(shotSE, pos);
		Invoke(nameof(Shoot), nextShootTime);
	}
}
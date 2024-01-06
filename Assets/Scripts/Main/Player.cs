using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private GameObject beam;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float speed;
	[SerializeField] private float starTime;
	[SerializeField] private float shootRestTime;
	[SerializeField] private HPUI hpUI;
	[SerializeField] private GameUI gameUI;
	[SerializeField] private ScoreUI scoreUI;
	[SerializeField] private CameraScroll scroll;
	[SerializeField] private AudioClip shotSE;
	[SerializeField] private AudioClip damageSE;

	public float normalSpeed;
	public float hardSpeed;
	public float hellSpeed;

	private Transform _transform;
	private Vector2 _velocity;
	private float _latestDamagedTime;
	private float _latestShootTime;
	private int _hp = 3;
	private int _enemyLayer;
	private bool _isDead;

	public GameMode Mode { get; private set; } = GameMode.Normal;

	private void Start()
	{
		_transform = transform;
		_enemyLayer = LayerMask.NameToLayer("Enemy");

		var modeObject = FindObjectOfType<ModeObject>();
		if (modeObject != null)
		{
			Mode = modeObject.mode;
			Destroy(modeObject.gameObject);
		}
	}

	private void Update()
	{
		if (scroll.stop) return;

		Move();
		if (Input.GetButtonDown("Fire1"))
		{
			Shot();
		}
	}

	private void Move()
	{
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
		_velocity = new Vector2(x, y);
		_velocity *= speed * Time.deltaTime;

		rb.AddForce(_velocity);

		//範囲内に収める
		var pos = _transform.position;
		pos.x = Mathf.Clamp(pos.x, -8, 100);
		pos.y = Mathf.Clamp(pos.y, -4, 4);

		_transform.position = pos;
	}

	private void Shot()
	{
		if (Time.time - _latestShootTime < shootRestTime) return;

		var pos = _transform.position;
		var beamPos = pos;
		beamPos.x += 1;
		var shootBeam = Instantiate(beam, beamPos, Quaternion.identity).GetComponent<Beam>();
		shootBeam.speed = Mode switch
		{
			GameMode.Normal => normalSpeed,
			GameMode.Hard => hardSpeed,
			GameMode.Hell => hellSpeed,
			_ => throw new ArgumentOutOfRangeException()
		};

		shootBeam.scoreUI = scoreUI;

		AudioSource.PlayClipAtPoint(shotSE, pos);
	}

	public bool Damage()
	{
		if (_isDead) return false;
		//一定時間の無敵状態なのに攻撃判定があっても無視する
		if (Time.time - _latestDamagedTime < starTime) return false;

		_hp--;

		if (_hp <= 0)
		{
			_hp = 0;
			scroll.stop = true;
			_isDead = true;
			gameUI.Fail();
		}

		hpUI.Damage(_hp);
		AudioSource.PlayClipAtPoint(damageSE, _transform.position);
		//最後に攻撃した時間を更新
		_latestDamagedTime = Time.time;
		return true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer != _enemyLayer) return;

		//敵に直接当たったときもダメージ
		Damage();
	}
}
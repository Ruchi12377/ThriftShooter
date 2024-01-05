using UnityEngine;
using UnityEngine.SceneManagement;

public class FailUI : MonoBehaviour
{
	private const float SelectDead = 0.3f;
	[SerializeField] private RectTransform select;

	[SerializeField] private RectTransform retry;
	[SerializeField] private RectTransform quit;
	[SerializeField] private RectTransform title;
	private int _currentSelect;
	private bool _isDownSelect;
	private float _appearedTime;

	private void Start()
	{
		gameObject.SetActive(false);
	}

	private void Update()
	{
		_appearedTime += Time.deltaTime;
		Select();

		//連打防止
		if(_appearedTime < 2) return;
		
		if (Input.GetButton("Fire1"))
		{
			if (_currentSelect == 0)
			{
				SceneManager.LoadScene("Main");
			}
			else if (_currentSelect == 1)
			{
				Ex.Quit();
			}
			else
			{
				SceneManager.LoadScene("Title");
			}
		}
	}

	private void Select()
	{
		var x = Input.GetAxis("Horizontal");
		if (x == 0)
		{
			_isDownSelect = false;
			return;
		}

		if (_isDownSelect) return;

		if (x > SelectDead)
		{
			_currentSelect++;
			_isDownSelect = true;
		}
		else if (x < SelectDead * -1)
		{
			_currentSelect--;
			_isDownSelect = true;
		}

		_currentSelect = Mathf.Clamp(_currentSelect, 0, 2);

		var target = _currentSelect switch
		{
			0 => retry,
			1 => quit,
			2 => title,
			_ => default
		};

		if (target == null) return;

		select.localPosition = target.localPosition + new Vector3(-135, 0, 0);
	}
}
using UnityEngine;

public class Mode : MonoBehaviour
{
	[SerializeField] private RectTransform select;

	[SerializeField] private RectTransform normal;
	[SerializeField] private RectTransform hard;
	[SerializeField] private RectTransform hell;
	[SerializeField] private ModeObject modeObject;
	private const float SelectDead = 0.3f;
	private int _currentSelect;
	private bool _isDownSelect;

	private void Update()
	{
		Select();
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
			0 => normal,
			1 => hard,
			2 => hell,
			_ => default
		};

		if (target == null) return;

		select.localPosition = target.localPosition + new Vector3(-200, 0, 0);
		modeObject.mode = (GameMode) _currentSelect;
	}
}
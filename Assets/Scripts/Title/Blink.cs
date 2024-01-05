using UnityEngine;

public class Blink : MonoBehaviour
{
	[SerializeField] private float onInterval;
	[SerializeField] private float offInterval;

	private void Start()
	{
		Invoke(nameof(Turn), offInterval);
	}

	private void Turn()
	{
		gameObject.SetActive(gameObject.activeSelf == false);
		Invoke(nameof(Turn), gameObject.activeSelf ? onInterval : offInterval);
	}
}
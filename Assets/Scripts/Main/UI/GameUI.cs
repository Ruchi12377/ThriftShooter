using UnityEngine;

public class GameUI : MonoBehaviour
{
	[SerializeField] private SuccessUI successUI;
	[SerializeField] private FailUI failUI;
	[SerializeField] private AudioClip successSE;
	[SerializeField] private AudioClip failSE;
	
	public void Success(bool isPerfect)
	{
		successUI.gameObject.SetActive(true);
		if (isPerfect)
		{
			successUI.Perfect();
		}
		Audio.Play(successSE);
	}

	public void Fail()
	{
		failUI.gameObject.SetActive(true);
		Audio.Play(failSE);
	}
}
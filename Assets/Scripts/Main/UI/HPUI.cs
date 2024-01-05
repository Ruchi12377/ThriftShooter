using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
	[SerializeField] private Sprite heartFull;
	[SerializeField] private Sprite heartEmpty;
	
	private Image[] hpUIs;

	private void Start()
	{
		hpUIs = transform.GetComponentsInChildren<Image>();
		foreach (var hp in hpUIs)
		{
			hp.sprite = heartFull;
		}
	}

	public void Damage(int currentHp)
	{
		for (var i = 0; i < 3; i++)
		{
			if (i <= currentHp - 1)
			{
				hpUIs[i].sprite = heartFull;
			}
			else
			{
				hpUIs[i].sprite = heartEmpty;
			}
		}
	}
}
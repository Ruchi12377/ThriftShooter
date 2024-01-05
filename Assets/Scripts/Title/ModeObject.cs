using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeObject : MonoBehaviour
{
	public GameMode mode = 0;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name != "Title") return;
		
		if (Input.GetButton("Fire1"))
		{
			SceneManager.LoadScene("Main");
		}
	}
}

public enum GameMode
{
	Normal = 0,
	Hard = 1,
	Hell = 2
}
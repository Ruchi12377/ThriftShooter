using UnityEngine;

public static class Audio
{
	private static Transform _camera;

	public static void Play(AudioClip clip)
	{
		if (_camera == null)
		{
			_camera = Camera.current.transform;
		}
		
		AudioSource.PlayClipAtPoint(clip, _camera.position);
	}
}
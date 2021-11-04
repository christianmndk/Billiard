using UnityEngine;
using UnityEngine.SceneManagement;

public class MakePersistant : MonoBehaviour
{

	public int MainMenuBuildIndex = 0;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		SceneManager.LoadScene(MainMenuBuildIndex);
	}
}
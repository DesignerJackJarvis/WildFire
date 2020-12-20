using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAwake : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

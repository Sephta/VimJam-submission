using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLocation : MonoBehaviour
{
    public int loadSceneIndex = 0;
    public int unloadSceneIndex = 0;

    public void ChangeLocal()
    {
        SceneManager.LoadSceneAsync(loadSceneIndex, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(unloadSceneIndex);
    }
}

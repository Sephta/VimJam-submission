using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLocation : MonoBehaviour
{
    public int sceneIndex = 0;

    public void ChangeLocal()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

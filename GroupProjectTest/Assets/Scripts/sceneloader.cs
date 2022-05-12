using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneloader : MonoBehaviour
{
    // Start is called before the first frame update
    public void backtomennu()
    {
      Time.timeScale = 1f;
      SceneManager.LoadScene(0);
    }
    public void replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
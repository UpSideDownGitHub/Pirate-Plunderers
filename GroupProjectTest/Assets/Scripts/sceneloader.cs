using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneloader : MonoBehaviour
{
    // Start is called before the first frame update
    public void backtomennu()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }
    public void resume()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
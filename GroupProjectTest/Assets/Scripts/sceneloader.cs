using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneloader : MonoBehaviour
{
    // Start is called before the first frame update
    public void backtomennu()
    {
      SceneManager.LoadScene(0);
    }
    public void replay()
    {
        SceneManager.LoadScene(1);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public int scene;
    public CanvasGroup canvas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }

    public void LoadScene()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        
        yield return StartCoroutine(Fade(1, 2));

        GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (saveData.progression.firstTime)
            scene = 2;

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(Fade(0, 1));
        loadingScreen.SetActive(false);
    }

    public IEnumerator Fade(float targetValue, float duration)
    {
        float startValue = canvas.alpha;
        float time = 0;
        while (time < duration)
        {
            canvas.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = targetValue;
    }
}

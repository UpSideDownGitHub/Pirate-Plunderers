using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI level;

    public float increaseRate = 1.25f;
    public int maxValue, minValue;
    public int startNum = 100;
    public void Start()
    {
        updateXP();
    }
    public void updateXP()
    {
        GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        int ans = 0;
        if (saveData.progression.xp > 100)
        {
            float num = Mathf.Log10((float)saveData.progression.xp / 100f) / Mathf.Log10(1.25f);
            ans = Mathf.CeilToInt(num);
        }
        if (ans != saveData.progression.level)
        {
            saveData.progression.level = ans;
            saveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        }

        level.text = ans.ToString();

        maxValue = (int)(startNum * Mathf.Pow(increaseRate, ans));
        if (ans == 0)
        {
            minValue = 0;
        }
        else
        {
            minValue = (int)(startNum * Mathf.Pow(increaseRate, ans - 1));
        }
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        slider.value = saveData.progression.xp;
    }
}

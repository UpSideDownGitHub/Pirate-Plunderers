using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnlockSectionManager : MonoBehaviour
{
    public Collider2D collider2d;

    public void Start()
    {
        GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (saveData.progression.firstBossDefeated)
            turnOffCollider();
    }

    public void turnOffCollider()
    {
        collider2d.enabled = false;
    }
}

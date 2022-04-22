using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Customisations : MonoBehaviour
{
    [Header("Cannons")]
    public GameObject[] cannons;
    [Header("Sails")]
    public GameObject[] sails;
    [Header("Ships")]
    public Sprite[] Size;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Vector2[] spawnPositions;
    public GameObject cannonMain;
    public GameObject sailMain;

    // Start is called before the first frame update
    void Start()
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].SetActive(false);
        }
        for (int i = 0; i < sails.Length; i++)
        {
            sails[i].SetActive(false);
        }

        cannons[SaveData.loadout.cannon].SetActive(true);
        sails[SaveData.loadout.sail].SetActive(true);

        spriteRenderer.sprite = Size[SaveData.loadout.size];
        cannonMain.transform.position = spawnPositions[SaveData.loadout.size * 2];
        sailMain.transform.position = spawnPositions[SaveData.loadout.size * 2 + 1];

        Vector2 S = spriteRenderer.sprite.bounds.size;
        boxCollider2D.size = S;
    }
}

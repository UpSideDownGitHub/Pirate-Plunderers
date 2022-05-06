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

    [Header("Colour")]
    public GameObject[] sprites;

    [Header("Positions")]
    public Vector2[] spawnPositions;
    public GameObject cannonMain;
    public GameObject sailMain;
    public GameObject colourMain;
    public GameObject playerMain;

    [Header("Stat Changing")]
    public PlayerMove playerMove; // Speed & Turning Speed; 

    // Start is called before the first frame update
    void Start()
    {
        loadCustomisations();
    }

    public void loadCustomisations()
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
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(false);
        }

        cannons[SaveData.loadout.cannon].SetActive(true);
        sails[SaveData.loadout.sail].SetActive(true);
        sprites[SaveData.loadout.colour].SetActive(true);

        spriteRenderer.sprite = Size[SaveData.loadout.size];
        cannonMain.transform.localPosition = new Vector3(spawnPositions[SaveData.loadout.size * 4].x, spawnPositions[SaveData.loadout.size * 4].y, -0.1f);
        sailMain.transform.localPosition = new Vector3(spawnPositions[SaveData.loadout.size * 4 + 1].x, spawnPositions[SaveData.loadout.size * 4 + 1].y, -0.25f);
        colourMain.transform.localPosition = new Vector3(spawnPositions[SaveData.loadout.size * 4 + 2].x, spawnPositions[SaveData.loadout.size * 4 + 2].y, -0.25f);
        playerMain.transform.localPosition = new Vector3(spawnPositions[SaveData.loadout.size * 4 + 3].x, spawnPositions[SaveData.loadout.size * 4 + 3].y, -0.25f);

        Vector2 S = spriteRenderer.sprite.bounds.size;
        boxCollider2D.size = S;

        // setting variables based upon what is equipped
        playerMove.playerSpeed = SaveData.ShipUpgrade.Sizes[SaveData.loadout.size].speed + SaveData.ShipUpgrade.Sails[SaveData.loadout.sail].speed;
        playerMove.rotationSpeed = SaveData.ShipUpgrade.Sizes[SaveData.loadout.size].turning + SaveData.ShipUpgrade.Sails[SaveData.loadout.sail].turning;
    }
}

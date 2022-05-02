using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject resultsMenu;

    public void turnOffMenu()
    {
        resultsMenu.SetActive(false);
    }
}

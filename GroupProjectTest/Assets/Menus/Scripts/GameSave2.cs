using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine;

[XmlRoot("SaveData")]
public class GenralSaveContainer
{
    // below is links to other classes in GameSave.cs
    [XmlElement("Progression")]
    public Progression progression;

    [XmlElement("Loadout")]
    public Loadout loadout;

    [XmlElement("ShipUpgrades")]
    public ShipUpgrade ShipUpgrade;


    // The ability to save data
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GenralSaveContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
    // The ability to load data
    public static GenralSaveContainer Load(string path)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(GenralSaveContainer));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as GenralSaveContainer;
            }
        }
        catch
        {
            File.WriteAllText(Application.persistentDataPath + "/GameSave.xml", "<?xml version=\"1.0\"?>\n" +
"<SaveData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n" +
  "<Progression>\n" +
    "<level>0</level>\n" +
    "<xp> 0 </xp>\n" +
    "<coins> 0 </coins>\n" +
    "<firstTime> true </firstTime>\n" +
    "<firstBossDefeated > false </firstBossDefeated >\n" +
    "<finalBossDefeated > false </finalBossDefeated >\n" +
  "</Progression >\n" +
  "<Loadout >\n" +
    "<cannon > 0 </cannon >\n" +
    "<sail > 0 </sail >\n" +
    "<colour > 0 </colour >\n" +
    "<size > 0 </size >\n" +
  "</Loadout >\n" +
  "<ShipUpgrades >\n" +
    "<Cannons >\n" +
      "<Cannon ID = \"0\" >\n" +
        "<name > Default </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > default cannon that is a good all rounder </description >\n" +
        "<unlocked > true </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 0 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"1\" >\n" +
        "<name > Small Shot </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > small cannon with high fire rate but lower damage </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 1000 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"2\" >\n" +
        "<name > Large Shot </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > Large cannon with low fire rate but high damage </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 1250 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"3\" >\n" +
        "<name > Bombs </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > mortor that can shoot bombs that deal massive AOE damage </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 0 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"4\" >\n" +
        "<name > Double Shot </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > double shot cannon with same damage as regular cannon but two bullets </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 2000 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"5\" >\n" +
        "<name > Quad Shot </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > quad shot cannon with same damage as regular cannon but 4 bullets </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 4000 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"6\" >\n" +
        "<name > Harpoon </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > the harpoon deals massive damage with decent fire rate </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 5000 </price >\n" +
      "</Cannon >\n" +
      "<Cannon ID = \"7\" >\n" +
        "<name > Rapid Fire </name >\n" +
        "<damage > 100 </damage >\n" +
        "<range > 100 </range >\n" +
        "<description > rapid fire shoots many bullets but will have to reload </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 7500 </price >\n" +
      "</Cannon >\n" +
    "</Cannons >\n" +
    "<Sails >\n" +
      "<Sail ID = \"0\" >\n" +
        "<name > Small </name >\n" +
        "<speed > 1.6 </speed >\n" +
        "<turning > 95 </turning >\n" +
        "<description > small sail that is the default at the start of the game </description >\n" +
        "<unlocked > true </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 0 </price >\n" +
      "</Sail >\n" +
      "<Sail ID = \"1\" >\n" +
        "<name > Medium </name >\n" +
        "<speed > 2.1 </speed >\n" +
        "<turning > 75 </turning >\n" +
        "<description > Medium sized sail that increase speed and decreases turning </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 5000 </price >\n" +
      "</Sail >\n" +
      "<Sail ID = \"2\" >\n" +
        "<name > Large </name >\n" +
        "<speed > 2.6 </speed >\n" +
        "<turning > 55 </turning >\n" +
        "<description > Large sized sail that increase speed to an insane ammount but decreases turning </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 10000 </price >\n" +
      "</Sail >\n" +
    "</Sails >\n" +
    "<Colours >\n" +
      "<Colour ID = \"0\" >\n" +
        "<name > Colour 1 </name >\n" +
        "<description > Colour 1 </description >\n" +
        "<unlocked > true </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 100 </price >\n" +
      "</Colour >\n" +
      "<Colour ID = \"1\" >\n" +
        "<name > Colour 2 </name >\n" +
        "<description > Colour 2 </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 200 </price >\n" +
      "</Colour >\n" +
      "<Colour ID = \"2\" >\n" +
        "<name > Colour 3 </name >\n" +
        "<description > Colour 3 </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 300 </price >\n" +
      "</Colour >\n" +
      "<Colour ID = \"3\" >\n" +
        "<name > Colour 4 </name >\n" +
        "<description > Colour 4 </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 300 </price >\n" +
      "</Colour >\n" +
      "<Colour ID = \"4\" >\n" +
        "<name > Colour 5 </name >\n" +
        "<description > Colour 5 </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 300 </price >\n" +
      "</Colour >\n" +
      "<Colour ID = \"5\" >\n" +
        "<name > Colour 6 </name >\n" +
        "<description > Colour 6 </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 300 </price >\n" +
      "</Colour >\n" +
    "</Colours >\n" +
    "<Sizes >\n" +
      "<Size ID = \"0\" >\n" +
        "<name > Starter </name >\n" +
        "<health > 1000 </health >\n" +
        "<speed > 1.6 </speed >\n" +
        "<turning > 95 </turning >\n" +
        "<description > Starter </description >\n" +
        "<unlocked > true </unlocked >\n" +
        "<buyable > false </buyable >\n" +
        "<price > 0 </price >\n" +
      "</Size >\n" +
      "<Size ID = \"1\" >\n" +
        "<name > Small </name >\n" +
        "<health > 2500 </health >\n" +
        "<speed > 1.3 </speed >\n" +
        "<turning > 75 </turning >\n" +
        "<description > Small </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 500 </price >\n" +
      "</Size >\n" +
      "<Size ID = \"2\" >\n" +
        "<name > Medium </name >\n" +
        "<health > 4000 </health >\n" +
        "<speed > 1 </speed >\n" +
        "<turning > 55 </turning >\n" +
        "<description > Medium </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 5000 </price >\n" +
      "</Size >\n" +
      "<Size ID = \"3\" >\n" +
        "<name > Large </name >\n" +
        "<health > 5500 </health >\n" +
        "<speed > 0.8 </speed >\n" +
        "<turning > 35 </turning >\n" +
        "<description > Large </description >\n" +
        "<unlocked > false </unlocked >\n" +
        "<buyable > true </buyable >\n" +
        "<price > 15000 </price >\n" +
      "</Size >\n" +
    "</Sizes >\n" +
  "</ShipUpgrades >\n" +
"</SaveData > \n");

            var serializer = new XmlSerializer(typeof(GenralSaveContainer));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as GenralSaveContainer;
            }
        }
    }

}
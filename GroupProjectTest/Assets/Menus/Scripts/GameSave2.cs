using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System;

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
        var serializer = new XmlSerializer(typeof(GenralSaveContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as GenralSaveContainer;
        }
    }

}
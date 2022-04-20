using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class Progression
{
    public int level;
    public int xp;
    public int coins;
    public bool firstBossDefeated;
    public bool finalBossDefeated;
}


public class Loadout
{
    public int cannon;
    public int sail;
    public int colour;
    public int size;
}


public class ShipUpgrade
{
    [XmlArray("Cannons")]
    [XmlArrayItem("Cannon")]
    public Cannon[] Cannons;

    [XmlArray("Sails")]
    [XmlArrayItem("Sail")]
    public Sail[] Sails;

    [XmlArray("Colours")]
    [XmlArrayItem("Colour")]
    public Colour[] Colours;

    [XmlArray("Sizes")]
    [XmlArrayItem("Size")]
    public Size[] Sizes;
}

public class Cannon
{
    [XmlAttribute("ID")]
    public int ID;
    public float damage;
    public float range;
    public string description;
    public bool unlocked;
    public bool buyable;
}
public class Sail
{
    [XmlAttribute("ID")]
    public int ID;
    public float speed;
    public float turning;
    public string description;
    public bool unlocked;
    public bool buyable;
}
public class Colour
{
    [XmlAttribute("ID")]
    public int ID;
    public string description;
    public bool unlocked;
    public bool buyable;
}
public class Size
{
    [XmlAttribute("ID")]
    public int ID;
    public float health;
    public float speed;
    public float turning;
    public string description;
    public bool unlocked;
    public bool buyable;
}
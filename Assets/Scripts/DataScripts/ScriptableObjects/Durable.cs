using System.Collections.Generic;
using UnityEngine;

public enum DurableType
{
	Axe,
	Pickaxe,
	Weapon,
	Armor
}

[CreateAssetMenu(fileName = "Durable", menuName = "Scriptable Objects/Durable")]
public class Durable : Item
{
	[field:SerializeField] public DurableType Type { get; private set; }
	[field:SerializeField] public int MaxDurability { get; private set; }
	[field:SerializeField] public List<ItemEntry> RepairCost { get; private set; }
	[field:SerializeField] public int Efficiency { get; private set; }
	
}
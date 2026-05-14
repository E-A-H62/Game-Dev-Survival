using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "Scriptable Objects/Structure")]
public class Structure : Entity
{
	[Header("Structure Fields")]
	[field:SerializeField] public int MaxHealth { get; private set; }
	[field:SerializeField] public DurableType Tool { get; private set; } // Used to break the structure
	[field:SerializeField] public List<ItemEntry> Drops { get; private set; }
}
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
	[field:SerializeField] public List<ItemEntry> Ingredients { get; private set; }
	[field:SerializeField] public Structure Station { get; private set; }

}
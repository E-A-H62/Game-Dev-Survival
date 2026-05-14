using System;
using UnityEngine;

[Serializable]
public class ItemEntry
{
	[field:SerializeReference] public Item EntryItem { get; private set; }
	[field:SerializeField] public int Count { get; set; }
	public ItemEntry(Item item, int count)
	{
		EntryItem = item;
		Count = count;
	}
}
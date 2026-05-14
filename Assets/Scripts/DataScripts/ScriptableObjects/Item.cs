using UnityEngine;

public enum Rarities
{
    Common,
	Uncommon,
	Rare,
	Epic,
	Legendary
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : Entity
{
	[Header("Item Fields")]
	[field:SerializeField] public string Description { get; private set; }
	[field:SerializeField] public Rarities Rarity { get; private set; }
	[field:SerializeField] public int Value { get; private set; }
	[field:SerializeField] public string AssetName2D { get; private set; }

}
using UnityEngine;

[CreateAssetMenu(fileName = "ItemRecipe", menuName = "Scriptable Objects/ItemRecipe")]
public class ItemRecipe : Recipe
{
	[field:SerializeField] public ItemEntry Result { get; private set; }

}
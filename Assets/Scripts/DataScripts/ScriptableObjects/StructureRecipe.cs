using UnityEngine;

[CreateAssetMenu(fileName = "StructureRecipe", menuName = "Scriptable Objects/StructureRecipe")]
public class StructureRecipe : Recipe
{
	[field:SerializeField] public Structure Result { get; private set; }
}
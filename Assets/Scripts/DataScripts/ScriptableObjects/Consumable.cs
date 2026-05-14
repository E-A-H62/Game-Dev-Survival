using UnityEngine;
public enum EffectType
{
	None,
	Regeneration,
	Speed
}
[CreateAssetMenu(fileName = "Consumable", menuName = "Scriptable Objects/Consumable")]
public class Consumable : Item
{
	[field:SerializeField] public int Health { get; private set; }
	[field:SerializeField] public EffectType Effect { get; private set; }
	[field:SerializeField] public int Duration { get; private set; } // In seconds.
}
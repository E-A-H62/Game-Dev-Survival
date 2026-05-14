using UnityEngine;

public class Entity: ScriptableObject
{
    [Header("Entity Fields")]
    // Set up such that the variable can be changed in the editor and retrieved by other scripts, but not set by other scripts.
    [field:SerializeField] public int ID { get; private set; }
    [field:SerializeField] public string Name { get; private set; } 
}
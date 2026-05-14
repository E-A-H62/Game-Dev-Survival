// using UnityEngine;
// using System.Collections.Generic;

// public class TeleportationSpot : MonoBehaviour
// {
//     public Transform target;

//     [SerializeField]
//     public List<GameObject> objectsToTeleport;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player") && objectsToTeleport != null)
//         {
//             TeleportationManager.Instance.player = other.gameObject;
//             int obj = Random.Range(0, objectsToTeleport.Count);
//             target = objectsToTeleport[obj].GetComponent<Transform>();
//             Debug.Log("Teleporting to " + target.position);
//             TeleportationManager.Instance.Teleport(target);
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             TeleportationManager.Instance.player = null;
//         }
//     }
// }
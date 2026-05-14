// using UnityEngine;

// public class TeleportationManager : MonoBehaviour
// {
//     public static TeleportationManager Instance {get;set;}
//     public GameObject player;
//     private void Awake()
//     {
//         if (Instance !=null && Instance != this)
//         {
//             Destroy(gameObject);
//         }
//         else
//         {
//             Instance = this;
//         }
//     }

//     public void Teleport(Transform target)
//     {
//         // var cc = player.GetComponent<CharacterController>();
//         // if (cc != null) cc.enabled = false;
//         // player.transform.root.position = target.position;
//         // if (cc != null) cc.enabled = true;
//         var cc = player.GetComponent<CharacterController>();
//         cc.Move(target.position - player.transform.position);
//     }
// }

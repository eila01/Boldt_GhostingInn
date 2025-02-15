using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class DoorEvent : MonoBehaviour
{
    [SerializeField] public Door door;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "NPC")
        {
            door.Open(collider.transform.position);
        }
    }
}

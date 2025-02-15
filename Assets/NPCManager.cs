using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerTarget;
    public float speed;
    
    public void followPlayer()
    {
        float dist = Vector3.Distance(transform.position, playerTarget.position);
        // transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        if (dist <= 2)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        }
    }
}

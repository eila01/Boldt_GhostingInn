using UnityEngine;

public class FluteController : MonoBehaviour
{
    // Variables
    public float speed;
    public float groundDistance;
    public float followDistance; // follow distance from PC 
    
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    
    // store player location
    public Transform playerTransform;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }
        // move the body
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // move up
        if (Input.GetKey(KeyCode.LeftShift))
        {
          //  rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        }
        // move down
        if (Input.GetKey(KeyCode.LeftControl))
        {
         //   rb.AddForce(Vector3.down * speed, ForceMode.Impulse);
        }
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.linearVelocity = moveDir * speed;

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }

    }
}

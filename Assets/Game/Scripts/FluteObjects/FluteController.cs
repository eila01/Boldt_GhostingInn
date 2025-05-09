using UnityEngine;

public class FluteController : MonoBehaviour
{

    // Variables
    public float speed;
    public float groundDistance;
    public float followDistance = 1; // follow distance from PC 

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    // store player locations
    //public Transform playerTransform;

    private Collider _collider;
    private PlayerSwitch _playerSwitch;
    public bool _isEnable;
    public Collider playerCollider;

    public Transform playerTarget;
    public RoomCameraSwap roomCameraSwap;
    public bool followingPlayer;
    void Start()
    {
        //roomCameraSwap = roomCameraSwap.GetComponent<RoomCameraSwap>();
        
        roomCameraSwap.rightRotate = false;
        _isEnable = false;
        _playerSwitch = GetComponent<PlayerSwitch>();
        rb = gameObject.GetComponent<Rigidbody>();
        
        /*
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), _collider);
        */
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

        if (_isEnable)
        {

            followingPlayer = false;
            // move the body
            
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (roomCameraSwap.rightRotate == true)
            {
                x = Input.GetAxis("Vertical");
                 y = Input.GetAxis("Horizontal");
            }
            else
            {
                x = Input.GetAxis("Horizontal");
                 y = Input.GetAxis("Vertical");
            }
           
            // move up
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            }

            // move down
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(Vector3.down * speed, ForceMode.Impulse);
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
        else
        {
            followPlayer(); // Fix Problem Later: Flute will follow Player, but if player switch to flute and switch back
            // Debug.Log("FollowPlayer");
        }
    }

    public void followPlayer()
    {
        followingPlayer = true;
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

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "GhostObj")
        {
            // enable text

            // if flute enter collision then check if flute is holding down E

            // if flute holds down E then turn down light intensity / turn off particles / get disable text / and spawn Ghost
        }
    }
}
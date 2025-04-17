using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private ObjectFader _fader;
    public CinemachineVirtualCamera[] allVirtualCams;

    public void SwitchToCamera(string cameraName)
    {
        foreach (var vcam in allVirtualCams)
        {
            if (vcam.name == cameraName)
                vcam.Priority = 10; // Make active
            else
                vcam.Priority = 0; // Lower priority
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                {
                    return;
                }

                if (hit.collider.gameObject == player)
                {
                    // nothing in front of the player
                    if (_fader != null)
                    {
                        _fader.DoFade = false;
                    }
                }
                else
                {
                    _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (_fader != null)
                    {
                        _fader.DoFade = true;
                    }
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private bool IsRotatingDoor = true;
    [SerializeField] private float Speed = 1f;
    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float ForwardDirection = 0;
    
    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;
    
    [SerializeField] private UnityEvent doorCloseEvent;
    [SerializeField] private UnityEvent doorOpenEvent;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Forwards is pointing into the door frame, choose a direction 
        Forward = transform.right;
        
    }

    public void OpenSimple()
    {
        doorOpenEvent.Invoke();
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationOpen(1f)); // opening the door based on the player pos
            }
        }
    }
    public void Open(Vector3 UserPosition)
    {
        doorOpenEvent.Invoke();
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot)); // opening the door based on the player pos
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            // negative rotation
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }
        else
        {
            // positive rotation
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
        IsOpen = true;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        doorCloseEvent.Invoke();
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
        
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);
        
        IsOpen = false;
        
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}

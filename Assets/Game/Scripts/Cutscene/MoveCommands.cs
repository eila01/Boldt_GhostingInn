using System.Collections;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class MoveCommands : MonoBehaviour
{
    [YarnCommand("move")]
    public static IEnumerator MoveObject(string objectName, float x, float y, float z, float duration)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj == null) yield break;

        // Lock player movement if necessary
        PlayerController.canMove = false;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Temporarily make Rigidbody kinematic to avoid physics interference
        }

        Vector3 start = obj.transform.position;
        Vector3 target = obj.transform.position + new Vector3(x, y, z);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(start, target, elapsed / duration);

            // Update position with Rigidbody (physics-friendly movement)
            if (rb != null)
            {
                rb.MovePosition(newPos); // Physics-based movement
            }
            else
            {
                obj.transform.position = newPos; // Non-physics-based movement
            }

            yield return null;
        }

        // Final position after the movement is complete
        if (rb != null)
        {
            rb.MovePosition(target); // Ensure the final position is applied
            rb.isKinematic = false; // Restore Rigidbody physics interaction
        }
        else
        {
            obj.transform.position = target; // Non-physics-based final position
        }

        // Unlock player movement
        PlayerController.canMove = true;
    }
}

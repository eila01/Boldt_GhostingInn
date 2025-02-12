using UnityEngine;
[ExecuteInEditMode]

public class MagicLightSource : MonoBehaviour
{
    public Material reveal;

    public Light spotlight;

    public float lightRange;



    RaycastHit hit;

    private void Update()

    {

//        reveal.SetVector("_LightPosition", spotlight.transform.position);

        reveal.SetVector("_LightDirection", -spotlight.transform.forward);

        reveal.SetFloat("_LightAngle", spotlight.spotAngle);



        if(Physics.Raycast(transform.position, transform.forward, out hit, 1000, LayerMask.GetMask("InvisibleInk")))

        {

            float distanceToBlackInk = Vector3.Distance(spotlight.transform.position, hit.collider.transform.position);

            if (distanceToBlackInk < lightRange)

            {

                float unit = lightRange / 100;

                float distanceFromInkToLight = Vector3.Distance(hit.collider.transform.position, spotlight.transform.position);

                float strength = (100 - (distanceFromInkToLight / unit)) / 10;

                reveal.SetFloat("_StrengthScalar", strength);

            }

            else

            {

                reveal.SetFloat("_StrengthScalar", 0);

            }

        }

    }

}

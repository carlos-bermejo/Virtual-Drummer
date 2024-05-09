using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrummerAnimator : MonoBehaviour
{

    public static void HitInstrument(GameObject target, Vector3 instrumentPosition, string rotation="LEFT")
    {
        float movementSpeed = 5f;

        // determines rotation of the target
        Quaternion targetRotation = Quaternion.identity;
        if (rotation == "LEFT")
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
        else if (rotation == "RIGHT")
            targetRotation = Quaternion.Euler(0f, 90f, 0f);

        // determines direction to go and distance to instrument
        Vector3 direction = (instrumentPosition - target.transform.position).normalized;
        float distance = Vector3.Distance(target.transform.position, instrumentPosition);

        // moves the target towards the instrument's position
        target.transform.position += direction * movementSpeed * Time.deltaTime;

        // when distance is close enough, stop movement
        if (distance < 0.1f)
        {
            target.transform.position = instrumentPosition;
        }

        // turn object with the previously established degrees
        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, targetRotation, Time.deltaTime * movementSpeed);
    }

    public static void HitPedal(GameObject footTarget, GameObject kneeTarget)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

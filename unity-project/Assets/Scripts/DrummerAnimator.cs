using UnityEngine;

public class DrummerAnimator : MonoBehaviour
{

    public static void HitInstrument(GameObject target, Vector3 instrumentPosition, string rotationDirection = "LEFT", float movementSpeed = 20f)
    {
        if (target == null)
        {
            Debug.LogError("Target GameObject is null.");
            return;
        }

        // Calculate total distance and time
        float totalDistance = Vector3.Distance(target.transform.position, instrumentPosition);
        float totalTime = totalDistance / movementSpeed;

        // Speed factor calculation
        float speedFactor = Time.deltaTime / totalTime;

        // Determine target rotation
        Quaternion targetRotation = Quaternion.identity;
        if (rotationDirection == "LEFT")
            targetRotation = Quaternion.Euler(0f, -90f, 0f);
        else if (rotationDirection == "RIGHT")
            targetRotation = Quaternion.Euler(0f, 90f, 0f);

        // Turn if necessary
        if (Quaternion.Angle(target.transform.rotation, targetRotation) > 0.1f)
        {
            target.transform.rotation = Quaternion.Lerp(target.transform.rotation, targetRotation, speedFactor);
        }

        // Move towards instrument position
        target.transform.position = Vector3.Lerp(target.transform.position, instrumentPosition, speedFactor);

        // Ensure target reached instrument position
        if (Vector3.Distance(target.transform.position, instrumentPosition) < 0.01f)
        {
            // Reset position
            target.transform.position = instrumentPosition;
        }
    }

    public static void HitPedal(GameObject footTarget, GameObject kneeTarget, float pedalSpeed = 55f)
    {
        if (footTarget == null || kneeTarget == null)
        {
            Debug.LogError("FootTarget or KneeTarget GameObject is null.");
            return;
        }


        Vector3 originalFootPosition = footTarget.transform.position;
        Vector3 originalKneePosition = kneeTarget.transform.position;

        Vector3 pedalKneePosition = originalKneePosition + Vector3.up * 0.1f;
        Vector3 pedalFootPosition = originalFootPosition + Vector3.up * 0.1f;

        footTarget.transform.position = Vector3.Lerp(originalFootPosition, pedalFootPosition, pedalSpeed * Time.deltaTime);
        kneeTarget.transform.position = Vector3.Lerp(originalKneePosition, pedalKneePosition, pedalSpeed * Time.deltaTime);

        if (Vector3.Distance(footTarget.transform.position, pedalFootPosition) < 0.01f)
        {
            Vector3 returnFootPosition = originalFootPosition;
            Vector3 returnKneePosition = originalKneePosition;

            footTarget.transform.position = Vector3.Lerp(pedalFootPosition, returnFootPosition, pedalSpeed * Time.deltaTime);
            kneeTarget.transform.position = Vector3.Lerp(pedalKneePosition, returnKneePosition, pedalSpeed * Time.deltaTime);

            if (Vector3.Distance(footTarget.transform.position, returnFootPosition) < 0.01f)
            {
                footTarget.transform.position = returnFootPosition;
                kneeTarget.transform.position = returnKneePosition;
            }
        }
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

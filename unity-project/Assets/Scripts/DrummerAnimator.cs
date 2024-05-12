using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DrummerAnimator : MonoBehaviour
{

    private static GameObject handTarget;
    private static GameObject footTarget;
    private static GameObject kneeTarget;
    private static GameObject currentInstrument;
    private static GameObject originalHandPosition;
    private static GameObject originalFootPosition;
    private static GameObject originalKneePosition;
    private static float speedFactor = 20f;
    private static bool isResting;
    public GameObject head;


    public static DrummerAnimator Instance;

    void Awake()
    {
        Instance = this;
    }

    public void HitInstrument(GameObject target, GameObject instrument, GameObject originalPosition)
    {
        if (target == null || instrument == null)
        {
            Debug.LogError("Target and/or instrument GameObject are null in HitInstrument call.");
            return;
        }

        isResting = false;

        handTarget = target;
        footTarget = null;
        kneeTarget = null;
        currentInstrument = instrument;

        originalHandPosition = originalPosition;

    }

    public static void HitPedal(GameObject currentFootTarget, GameObject currentKneeTarget, GameObject originalPosition, GameObject originaSecondarylPosition, GameObject currentHandTarget = null, GameObject instrument = null, GameObject originalCurrentHandPosition = null)
    {
        if (currentFootTarget == null || currentKneeTarget == null)
        {
            Debug.LogError("FootTarget and/or KneeTarget GameObject is null in HitPedal.");
            return;
        }
        isResting = false;

        footTarget = currentFootTarget;
        kneeTarget = currentKneeTarget;

        //if hits pedal and also instrument
        if(currentHandTarget != null && instrument != null && originalCurrentHandPosition != null)
        {
            handTarget = currentHandTarget;
            currentInstrument = instrument;
            originalHandPosition = originalCurrentHandPosition;
        } else
        {
            handTarget=null;
        }

        originalFootPosition = originalPosition;
        originalKneePosition = originaSecondarylPosition;
    }

    private void Update()
    {
        if (isResting == false)
        {
            if (handTarget != null && footTarget == null)
            {
                //----INSTRUMENT-----
                MoveHead();
                MoveHand();
            }
            else if (footTarget != null && kneeTarget != null)
            {
                //----PEDAL-----
                if (handTarget != null)
                {
                    MoveHead();
                    MoveHand();
                    MoveFootAndKnee();
                } else
                {
                    MoveFootAndKnee();
                }
            }
        }
    }

    void MoveHand()
    {
        // turn target if necessary
        if (Quaternion.Angle(handTarget.transform.rotation, currentInstrument.transform.rotation) > 0.1f)
        {
            handTarget.transform.rotation = Quaternion.Lerp(handTarget.transform.rotation, currentInstrument.transform.rotation, Mathf.SmoothStep(0, 1, speedFactor));
        }

        handTarget.transform.position = Vector3.MoveTowards(handTarget.transform.position, currentInstrument.transform.position + Vector3.up * 0.2f, Mathf.SmoothStep(0, 1, speedFactor));

        // when its close enough, change direction to the actual instrument position
        if (Vector3.Distance(handTarget.transform.position, currentInstrument.transform.position + Vector3.up * 0.2f) < 0.01f)
        {
            handTarget.transform.position = Vector3.MoveTowards(currentInstrument.transform.position + Vector3.up * 0.2f, currentInstrument.transform.position, Mathf.SmoothStep(0, 1, speedFactor));

            // when its close enough, return to original position
            if (Vector3.Distance(handTarget.transform.position, currentInstrument.transform.position) < 0.01f)
            {
                Invoke("ResetPosition", 0.05f);
            }
        }
    }

    void MoveFootAndKnee()
    {
        //move up
        footTarget.transform.position = Vector3.MoveTowards(footTarget.transform.position, originalFootPosition.transform.position + Vector3.up * 0.2f, speedFactor);
        kneeTarget.transform.position = Vector3.MoveTowards(kneeTarget.transform.position, originalKneePosition.transform.position + Vector3.up * 0.2f, speedFactor);

        //move down again
        if (Vector3.Distance(footTarget.transform.position, originalFootPosition.transform.position + Vector3.up * 0.2f) < 0.01f)
        {
            Invoke("ResetPosition", 0.1f);
        }
    }

    void MoveHead()
    {
        if (Quaternion.Angle(head.transform.rotation, currentInstrument.transform.rotation) > 0.1f)
        {
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, currentInstrument.transform.rotation, Mathf.SmoothStep(0, 1, speedFactor));
        }
    }

    void ResetPosition()
    {
        if(handTarget != null)
            handTarget.transform.position = Vector3.MoveTowards(handTarget.transform.position, originalHandPosition.transform.position, Mathf.SmoothStep(0, 1, speedFactor*10));
        if(footTarget != null && kneeTarget != null)
        {
            footTarget.transform.position = Vector3.MoveTowards(footTarget.transform.position, originalFootPosition.transform.position, Mathf.SmoothStep(0, 1, speedFactor * 10));
            kneeTarget.transform.position = Vector3.MoveTowards(kneeTarget.transform.position, originalKneePosition.transform.position, Mathf.SmoothStep(0, 1, speedFactor * 10));
        }
        isResting = true;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

}

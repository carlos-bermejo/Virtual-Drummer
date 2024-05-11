using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MidiMapper : MonoBehaviour
{
    private static MidiMapper instance;

    //object that works as body-part targets
    public GameObject rightTargetHand;
    public GameObject leftTargetHand;
    public GameObject leftFoot;
    public GameObject leftKnee;
    public GameObject rightFoot;
    public GameObject rightKnee;

    //positions for each of the instuments
    public Vector3 snarePosition;
    public Vector3 lowTomPosition;
    public Vector3 highTomPosition;
    public Vector3 midTomPosition;
    public Vector3 hiHatPosition;
    public Vector3 crashCymbalPosition;
    public Vector3 splashChinaCymbalPosition;
    public Vector3 rideCymbalPosition;

    //getter for using static references to the class attributes
    public static MidiMapper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MidiMapper>();
                if (instance == null)
                {
                    Debug.LogError("No MidiMapper instance found in the scene.");
                }
            }
            return instance;
        }
    }

    public static void MapMidiToAction(int note, float velocity)
    {

        if (Instance == null)
        {
            Debug.LogError("MidiMapper instance is null.");
            return;
        }

        if (note == 35 || note == 36)
            //bass
            DrummerAnimator.HitPedal(Instance.rightFoot, Instance.rightKnee);
        else if (note == 37 || note == 38 || note == 39 || note == 40 || note == 56 || note == 58 || note == 65 || note == 66 || note == 67 || note == 68 || note == 69 || note == 70 || note == 71 || note == 72 || note == 73 || note == 74 || note == 75 || note == 76 || note == 77 || note == 78 || note == 79 || note == 80 || note == 81)
            //snare and miscelaneous
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.snarePosition);
        else if (note == 41 || note == 45 || note == 61 || note == 64)
            //low tom
            DrummerAnimator.HitInstrument(Instance.rightTargetHand, Instance.lowTomPosition);
        else if (note == 43 || note == 50 || note == 60 || note == 62 || note == 63)
            //high tom
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.highTomPosition);
        else if (note == 47 || note == 48)
            //mid tom
            DrummerAnimator.HitInstrument(Instance.rightTargetHand, Instance.midTomPosition);
        else if (note == 42)
            //hi hat
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.hiHatPosition);
        else if (note == 44 || note == 46)
        {
            //hi hat + pedal
            DrummerAnimator.HitInstrument(Instance.rightTargetHand, Instance.midTomPosition);
            DrummerAnimator.HitPedal(Instance.leftFoot, Instance.leftKnee);
        } 
        else if (note == 49 || note == 57)
            //crash cymbal
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.crashCymbalPosition);
        else if (note == 52 || note == 54 || note == 55)
            //splash or china cymbal
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.splashChinaCymbalPosition);
        else if (note == 51 || note == 53 || note == 59)
            //ride cymbal
            DrummerAnimator.HitInstrument(Instance.leftTargetHand, Instance.rideCymbalPosition);
        else
        {
            //default
            Debug.LogError("This MIDI note is not in the key range for drum kit instruments!");
            DrummerAnimator.HitPedal(Instance.rightFoot, Instance.rightKnee);
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

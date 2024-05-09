using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiMapper : MonoBehaviour
{
    //object that works as body-part targets
    public static GameObject rightTargetHand;
    public static GameObject leftTargetHand;
    public static GameObject leftFoot;
    public static GameObject leftKnee;
    public static GameObject rightFoot;
    public static GameObject rightKnee;

    //positions for each of the instuments
    public static Vector3 snarePosition;
    public static Vector3 lowTomPosition;
    public static Vector3 highTomPosition;
    public static Vector3 midTomPosition;
    public static Vector3 hiHatPosition;
    public static Vector3 crashCymbalPosition;
    public static Vector3 splashChinaCymbalPosition;
    public static Vector3 rideCymbalPosition;

    public static void MapMidiToAction(int note, float velocity)
    {

        if (note == 35 || note == 36)
            //bass
            DrummerAnimator.HitPedal(rightFoot, rightKnee);
        else if (note == 37 || note == 38 || note == 39 || note == 40 || note == 56 || note == 58 || note == 65 || note == 66 || note == 67 || note == 68 || note == 69 || note == 70 || note == 71 || note == 72 || note == 73 || note == 74 || note == 75 || note == 76 || note == 77 || note == 78 || note == 79 || note == 80 || note == 81)
            //snare and miscelaneous
            DrummerAnimator.HitInstrument(leftTargetHand, snarePosition);
        else if (note == 41 || note == 45 || note == 61 || note == 64)
            //low tom
            DrummerAnimator.HitInstrument(rightTargetHand, lowTomPosition);
        else if (note == 43 || note == 50 || note == 60 || note == 62 || note == 63)
            //high tom
            DrummerAnimator.HitInstrument(leftTargetHand, highTomPosition);
        else if (note == 47 || note == 48)
            //mid tom
            DrummerAnimator.HitInstrument(rightTargetHand, midTomPosition);
        else if (note == 42)
            //hi hat
            DrummerAnimator.HitInstrument(leftTargetHand, hiHatPosition);
        else if (note == 44 || note == 46)
        {
            //hi hat + pedal
            DrummerAnimator.HitInstrument(rightTargetHand, midTomPosition);
            DrummerAnimator.HitPedal(leftFoot, leftKnee);
        } 
        else if (note == 49 || note == 57)
            //crash cymbal
            DrummerAnimator.HitInstrument(leftTargetHand, crashCymbalPosition);
        else if (note == 52 || note == 54 || note == 55)
            //splash or china cymbal
            DrummerAnimator.HitInstrument(leftTargetHand, splashChinaCymbalPosition);
        else if (note == 51 || note == 53 || note == 59)
            //ride cymbal
            DrummerAnimator.HitInstrument(leftTargetHand, rideCymbalPosition);
        else
        {
            //default
            Debug.LogError("This MIDI note is not in the key range for drum kit instruments!");
            DrummerAnimator.HitPedal(rightFoot, rightKnee);
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

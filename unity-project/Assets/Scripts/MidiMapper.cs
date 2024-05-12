using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MidiMapper : MonoBehaviour
{
    public DrummerAnimator drummerAnimator;

    //object that works as body-part targets
    public GameObject rightTargetHand;
    public GameObject leftTargetHand;
    public GameObject leftFoot;
    public GameObject leftKnee;
    public GameObject rightFoot;
    public GameObject rightKnee;

    //object for each of the instuments
    public GameObject snarePosition;
    public GameObject lowTomPosition;
    public GameObject highTomPosition;
    public GameObject midTomPosition;
    public GameObject hiHatPosition;
    public GameObject crashCymbalPosition;
    public GameObject splashChinaCymbalPosition;
    public GameObject rideCymbalPosition;

    //position for each hand/feet
    public GameObject RightHandOriginalPosition;
    public GameObject LeftHandOriginalPosition;
    public GameObject RightFootOriginalPosition;
    public GameObject LeftFootOriginalPosition;
    public GameObject RightKneeOriginalPosition;
    public GameObject LeftKneeOriginalPosition;

    public static MidiMapper Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //find an instance of DrummerAnimator to execute its methods properly
        drummerAnimator = FindObjectOfType<DrummerAnimator>();
        if (drummerAnimator == null)
        {
            Debug.LogError("No object of type DrummerAnimator found in scene for MidiMapper.");
        }
    }

    public void MapMidiToAction(int note, float velocity)
    {

        if (note == 35 || note == 36)
            //bass
            DrummerAnimator.HitPedal(rightFoot, rightKnee, RightFootOriginalPosition, RightKneeOriginalPosition);
        else if (note == 37 || note == 38 || note == 39 || note == 40 || note == 56 || note == 58 || note == 65 || note == 66 || note == 67 || note == 68 || note == 69 || note == 70 || note == 71 || note == 72 || note == 73 || note == 74 || note == 75 || note == 76 || note == 77 || note == 78 || note == 79 || note == 80 || note == 81)
            //snare and miscelaneous
            drummerAnimator.HitInstrument(leftTargetHand, snarePosition, LeftHandOriginalPosition);
        else if (note == 41 || note == 45 || note == 61 || note == 64)
            //low tom
            drummerAnimator.HitInstrument(rightTargetHand, lowTomPosition, RightHandOriginalPosition);
        else if (note == 43 || note == 50 || note == 60 || note == 62 || note == 63)
            //high tom
            drummerAnimator.HitInstrument(leftTargetHand, highTomPosition, LeftHandOriginalPosition);
        else if (note == 47 || note == 48)
            //mid tom
            drummerAnimator.HitInstrument(rightTargetHand, midTomPosition, RightHandOriginalPosition);
        else if (note == 42)
            //hi hat
            drummerAnimator.HitInstrument(leftTargetHand, hiHatPosition, LeftHandOriginalPosition);
        else if (note == 44 || note == 46)
            //hi hat + pedal
            DrummerAnimator.HitPedal(leftFoot, leftKnee, LeftFootOriginalPosition, LeftKneeOriginalPosition, leftTargetHand, hiHatPosition, LeftHandOriginalPosition);
        else if (note == 49 || note == 57)
            //crash cymbal
            drummerAnimator.HitInstrument(leftTargetHand, crashCymbalPosition, LeftHandOriginalPosition);
        else if (note == 52 || note == 54 || note == 55)
            //splash or china cymbal
            drummerAnimator.HitInstrument(leftTargetHand, splashChinaCymbalPosition, LeftHandOriginalPosition);
        else if (note == 51 || note == 53 || note == 59)
            //ride cymbal
            drummerAnimator.HitInstrument(leftTargetHand, rideCymbalPosition, LeftHandOriginalPosition);
        else
        {
            //default
            Debug.LogError("This MIDI note is not in the key range for drum kit instruments!");
            DrummerAnimator.HitPedal(rightFoot, rightKnee, RightFootOriginalPosition, RightKneeOriginalPosition);
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

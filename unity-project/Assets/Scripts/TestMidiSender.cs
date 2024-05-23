using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TestMidiSender : MonoBehaviour
{

    private MidiMapper midiMapper;
    public float intervalInSeconds;
    public int[] notesToSend = new int[5];

    private void Start()
    {
        midiMapper = FindObjectOfType<MidiMapper>();
        if (midiMapper == null)
        {
            Debug.LogError("No object of type MidiMapper found in scene for TestMidiSender.");
        }
        StartCoroutine(SendNotesRoutine());
    }

    private IEnumerator SendNotesRoutine()
    {
        while (true)
        {
            for (int i = 0; i < notesToSend.Length; i++)
            {
                yield return new WaitForSeconds(intervalInSeconds);
                midiMapper.MapMidiToAction(notesToSend[i], 1f);
            }
            
        }
    }
}

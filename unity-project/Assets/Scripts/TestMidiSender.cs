using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TestMidiSender : MonoBehaviour
{

    public float intervalInSeconds; // Intervalo en segundos entre cada nota
    public int noteToSend; // Nota MIDI a enviar

    private void Start()
    {
        // Comienza la corrutina que envía las notas
        StartCoroutine(SendNotesRoutine());
    }

    private IEnumerator SendNotesRoutine()
    {
        // Repite indefinidamente
        while (true)
        {
            // Espera el intervalo especificado
            yield return new WaitForSeconds(intervalInSeconds);

            // Llama al método MapMidiToAction del MidiMapper para enviar la nota
            MidiMapper.MapMidiToAction(noteToSend, 1f);
        }
    }
}

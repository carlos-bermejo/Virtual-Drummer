using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDataOnScreen : MonoBehaviour
{
    public static GameObject textObject;
    public static TextMeshProUGUI text;

    void Start()
    {
        textObject = GameObject.Find("MidiDataText");
        text = textObject.GetComponent<TextMeshProUGUI>();
    }

    public static void ShowDataOnText(string textToShow)
    {
        if (textObject != null)
        {
            if (text != null)
            {
                text.text = textToShow;
            }
            else
            {
                Debug.LogError("The text object has no attribute text.");
            }
        }
    }
}


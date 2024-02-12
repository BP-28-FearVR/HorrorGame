using UnityEngine;
using TMPro;

public class ParticipantIDHandler : MonoBehaviour
{

    [Tooltip("The text object in the 'InputField'. Displays the not yet submitted Value.")]
    [SerializeField] private TextMeshProUGUI textField;

    [Tooltip("The maxmimum number of characters that can be written to the 'InputField'.")]
    [SerializeField] private int maxCharInText = 16;


    [Tooltip("The text object for Outputing the actual Value. Displays only the submitted Value.")]
    [SerializeField] private TextMeshProUGUI outputText;

    // Stores the ParticipantID after it is Submitted
    private static string _participantID = null;

    // Getter for _participantID 
    public static string ParticipantID { get => _participantID; }

    void Start()
    {
        textField.text = "";
    }

    // Writes the pressed Number to textField.
    public void NumberButtonPress(int number)
    {
        if (textField.text.Length + 1 <= maxCharInText)
        {
            textField.text += number.ToString();
        }
    }

    // Deleteds the last Character from textField.
    public void RemoveButtonPress()
    {
        if (!string.IsNullOrEmpty(textField.text))
        {
            textField.text = textField.text.Substring(0, textField.text.Length - 1);
        }
    }

    // Stores the Value from textField, displayes it and clears the textField.
    public void SubmitButtonPress()
    {
        if (!string.IsNullOrEmpty(textField.text))
        {
            _participantID = textField.text;
            outputText.text = _participantID;

            textField.text = "";
        }
    }

}

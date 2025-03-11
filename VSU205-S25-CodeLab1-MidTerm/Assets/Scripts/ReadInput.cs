using TMPro;
using UnityEngine;

public class ReadInput : MonoBehaviour
{


    public TMP_InputField inputField;  // Reference to the TMP_InputField component
    private string input;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optional: You can add a listener to automatically get the input when the user changes the text
        inputField.onEndEdit.AddListener(OnInputEnd);
    }

    // Update is called once per frame (this is for other game logic, no need to use here)
    void Update()
    {

    }

    // Called when the user finishes typing in the input field
    private void OnInputEnd(string userInput)
    {
        input = userInput;
        Debug.Log("Player Name: " + input);
        // return input;
    }

    // Get the player's input name (to be called when the timer finishes)
    public string GetInputName()
    {
        
        Debug.Log("inputfield: " + inputField.text + "input: " + input);
        return input;
    }
}
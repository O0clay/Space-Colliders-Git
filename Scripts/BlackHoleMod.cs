using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements; // Add this for TextMeshPro support

public class ChangeBlackHoleMAss : MonoBehaviour
{
   public Rigidbody planetRigidbody; // The Rigidbody of the planet
    public TMP_InputField massInputField; // InputField for mass (or UnityEngine.UI.InputField if using legacy InputField)
    public TMP_InputField scaleXInputField; // InputField for scale X
    public TMP_InputField scaleYInputField; // InputField for scale Y
    public TMP_InputField scaleZInputField; // InputField for scale Z

    public TMP_InputField positionXInputField;
    public TMP_InputField positionYInputField;
    public TMP_InputField positionZInputField;


    public Transform planetTransform; // Transform of the planet
    public void UpdateMass()
    {
        if (float.TryParse(massInputField.text, out float newMass))
        {
            newMass = Mathf.Clamp(newMass, 0.1f, 6000000f);
            planetRigidbody.mass = newMass;
            Debug.Log($"Updated mass of {planetRigidbody.name} to {newMass}");
        }
        else
        {
            Debug.LogWarning("Invalid input for mass. Please enter a number between 0.1 and 400.");
        }
    }

    public void UpdateScale()
    {
        // Parse X, Y, Z values from InputFields
        if (float.TryParse(scaleXInputField.text, out float scaleX) &&
            float.TryParse(scaleYInputField.text, out float scaleY) &&
            float.TryParse(scaleZInputField.text, out float scaleZ))
        {
            // Update the planet's scale
            planetTransform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            Debug.Log($"Updated scale of {planetTransform.name} to ({scaleX}, {scaleY}, {scaleZ})");
        }
        else
        {
            Debug.LogWarning("Invalid input for scale. Please enter valid numbers for X, Y, and Z.");
        }
    }
   public void UpdatePosition()
    {
        // Parse X, Y, Z values from InputFields
        if (float.TryParse(positionXInputField.text, out float posX) &&
            float.TryParse(positionYInputField.text, out float posY) &&
            float.TryParse(positionZInputField.text, out float posZ))
        {
            // Update the planet's position
            planetTransform.position = new Vector3(posX, posY, posZ);
            Debug.Log($"Updated position of {planetTransform.name} to ({posX}, {posY}, {posZ})");
        }
        else
        {
            Debug.LogWarning("Invalid input for position. Please enter valid numbers for X, Y, and Z.");
        }
    }
}
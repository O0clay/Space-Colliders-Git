using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetModulator : MonoBehaviour
{
    [System.Serializable]
    public class CelestialBody
    {
        public string name;
        public Rigidbody rigidbody; // Rigidbody of the celestial body
        public Transform transform; // Transform of the celestial body
    }

    public List<CelestialBody> celestialBodies; // List of all celestial bodies in the solar system

    // UI Elements
    public Dropdown planetDropdown; // Dropdown to select a planet
    public InputField massInputField;
    public InputField positionXInputField;
    public InputField positionYInputField;
    public InputField positionZInputField;
    public InputField scaleXInputField;
    public InputField scaleYInputField;
    public InputField scaleZInputField;

    private CelestialBody selectedBody;

    private void Start()
    {
        PopulateDropdown();
        SelectPlanet(0); // Default to the first planet
    }

    private void PopulateDropdown()
    {
        planetDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (CelestialBody body in celestialBodies)
        {
            options.Add(body.name);
        }

        planetDropdown.AddOptions(options);
        planetDropdown.onValueChanged.AddListener(SelectPlanet);
    }

    public void SelectPlanet(int index)
    {
        selectedBody = celestialBodies[index];
        UpdateUIValues();
    }

    public void UpdateUIValues()
    {
        if (selectedBody == null) return;

        // Update UI with current values
        massInputField.text = selectedBody.rigidbody.mass.ToString("F2");
        Vector3 position = selectedBody.transform.position;
        positionXInputField.text = position.x.ToString("F2");
        positionYInputField.text = position.y.ToString("F2");
        positionZInputField.text = position.z.ToString("F2");

        Vector3 scale = selectedBody.transform.localScale;
        scaleXInputField.text = scale.x.ToString("F2");
        scaleYInputField.text = scale.y.ToString("F2");
        scaleZInputField.text = scale.z.ToString("F2");
    }

    public void UpdateMass()
    {
        if (selectedBody == null) return;

        if (float.TryParse(massInputField.text, out float newMass))
        {
            newMass = Mathf.Clamp(newMass, 0.1f, 400f);
            selectedBody.rigidbody.mass = newMass;
            Debug.Log($"Updated mass of {selectedBody.name} to {newMass}");
        }
        else
        {
            Debug.LogWarning("Invalid mass value.");
        }
    }

    public void UpdatePosition()
    {
        if (selectedBody == null) return;

        if (float.TryParse(positionXInputField.text, out float posX) &&
            float.TryParse(positionYInputField.text, out float posY) &&
            float.TryParse(positionZInputField.text, out float posZ))
        {
            selectedBody.transform.position = new Vector3(posX, posY, posZ);
            Debug.Log($"Updated position of {selectedBody.name} to {selectedBody.transform.position}");
        }
        else
        {
            Debug.LogWarning("Invalid position values.");
        }
    }

    public void UpdateScale()
    {
        if (selectedBody == null) return;

        if (float.TryParse(scaleXInputField.text, out float scaleX) &&
            float.TryParse(scaleYInputField.text, out float scaleY) &&
            float.TryParse(scaleZInputField.text, out float scaleZ))
        {
            selectedBody.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            Debug.Log($"Updated scale of {selectedBody.name} to {selectedBody.transform.localScale}");
        }
        else
        {
            Debug.LogWarning("Invalid scale values.");
        }
    }
}
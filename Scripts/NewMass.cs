using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChangePlanetMass : MonoBehaviour
{
    [System.Serializable]
    public class Planet
    {
        public string name; // Display name in the dropdown
        public Rigidbody rigidbody; // Rigidbody of the planet
        public Transform transform; // Transform of the planet
    }

    public TMP_Dropdown planetDropdown; // Dropdown to select planets
    public List<Planet> planets; // List of all planets in the solar system
    public MultiCameraToggle cameraToggle; // Reference to the camera toggle system

    public TMP_InputField massInputField; // InputField for mass
    public TMP_InputField scaleXInputField, scaleYInputField, scaleZInputField; // Scale InputFields
    public TMP_InputField positionXInputField, positionYInputField, positionZInputField; // Position InputFields

    private Planet selectedPlanet; // The currently selected planet
    private Rigidbody planetRigidbody; // Current planet's Rigidbody
    private Transform planetTransform; // Current planet's Transform

    private void Start()
    {
        PopulateDropdown();
        SelectPlanet(0); // Default to the first planet
        planetDropdown.onValueChanged.AddListener(SelectPlanet);
    }

    private void PopulateDropdown()
    {
        planetDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (Planet planet in planets)
        {
            options.Add(planet.name);
        }

        planetDropdown.AddOptions(options);
    }

public void SelectPlanet(int index)
{
    if (index < 0 || index >= planets.Count) return;

    // Update the selected planet
    selectedPlanet = planets[index];
    planetRigidbody = selectedPlanet.rigidbody;
    planetTransform = selectedPlanet.transform;

    UpdateUIValues();

    // Update the camera index and focus
    if (cameraToggle != null && selectedPlanet != null)
    {
        cameraToggle.currentCameraIndex = index; // Update the camera index
        cameraToggle.FocusOnPlanet(selectedPlanet.transform); // Focus on the selected planet
        Debug.Log($"Switched to planet: {selectedPlanet.name}");
    }
}
    public void UpdateUIValues()
    {
        if (selectedPlanet == null) return;

        massInputField.text = planetRigidbody.mass.ToString("F2");

        Vector3 position = planetTransform.position;
        positionXInputField.text = position.x.ToString("F2");
        positionYInputField.text = position.y.ToString("F2");
        positionZInputField.text = position.z.ToString("F2");

        Vector3 scale = planetTransform.localScale;
        scaleXInputField.text = scale.x.ToString("F2");
        scaleYInputField.text = scale.y.ToString("F2");
        scaleZInputField.text = scale.z.ToString("F2");
    }

    public void UpdateMass()
    {
        if (float.TryParse(massInputField.text, out float newMass))
        {
            newMass = Mathf.Clamp(newMass, 0.1f, 500f);
            planetRigidbody.mass = newMass;
        }
    }

    public void UpdateScale()
    {
        if (float.TryParse(scaleXInputField.text, out float scaleX) &&
            float.TryParse(scaleYInputField.text, out float scaleY) &&
            float.TryParse(scaleZInputField.text, out float scaleZ))
        {
            planetTransform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }

    public void UpdatePosition()
    {
        if (float.TryParse(positionXInputField.text, out float posX) &&
            float.TryParse(positionYInputField.text, out float posY) &&
            float.TryParse(positionZInputField.text, out float posZ))
        {
            planetTransform.position = new Vector3(posX, posY, posZ);
        }
    }
    
}
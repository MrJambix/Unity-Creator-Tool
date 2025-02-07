using UnityEngine;
using UnityEngine.UI;

public class LightingController : MonoBehaviour
{
    public Light mainLight; // The main light source
    public Slider lightIntensitySlider; // The slider to control light intensity
    public Toggle lightToggle; // The toggle to turn the light on and off
    public RenderTextureEditor renderTextureEditor; // The render texture editor to select the light render texture
    public Dropdown lightTypeDropdown; // The dropdown menu to select the light type

    private float minIntensity = 0.1f; // The minimum intensity of the light
    private float maxIntensity = 10f; // The maximum intensity of the light

    private void Start()
    {
        // Initialize the light intensity slider
        lightIntensitySlider.minValue = minIntensity;
        lightIntensitySlider.maxValue = maxIntensity;
        lightIntensitySlider.value = mainLight.intensity;

        // Initialize the light toggle
        lightToggle.isOn = mainLight.enabled;

        // Initialize the render texture editor
        renderTextureEditor.onRenderTextureChanged += (renderTexture) =>
        {
            mainLight.renderTexture = renderTexture;
        };

        // Initialize the light type dropdown
        lightTypeDropdown.ClearOptions();
        lightTypeDropdown.AddOptions(new List<Dropdown.OptionData>
        {
            new Dropdown.OptionData("Point Light"),
            new Dropdown.OptionData("Directional Light"),
            new Dropdown.OptionData("Spot Light")
        });
        lightTypeDropdown.value = 0;
        lightTypeDropdown.onValueChanged.AddListener((index) =>
        {
            switch (index)
            {
                case 0:
                    mainLight.type = LightType.Point;
                    break;
                case 1:
                    mainLight.type = LightType.Directional;
                    break;
                case 2:
                    mainLight.type = LightType.Spot;
                    break;
            }
        });
    }

    private void Update()
    {
        // Update the light intensity based on the slider value
        mainLight.intensity = lightIntensitySlider.value;

        // Update the light toggle
        mainLight.enabled = lightToggle.isOn;
    }
}

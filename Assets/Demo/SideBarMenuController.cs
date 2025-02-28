using UnityEngine;
using UnityEngine.UI;

public class SidebarMenuController : MonoBehaviour
{
    public Button[] menuButtons;

    public GameObject[] panels;

    // Currently selected button index
    private int selectedButtonIndex = -1;

    void Start()
    {
        // Initialize button click listeners
        for (int i = 0; i < menuButtons.Length; i++)
        {
            int index = i; // Capture the index for the lambda
            menuButtons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Deactivate all panels initially
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        // If the same button is clicked again, do nothing
        if (selectedButtonIndex == buttonIndex)
            return;

        // Deactivate the previously selected panel
        if (selectedButtonIndex != -1)
        {
            panels[selectedButtonIndex].SetActive(false);
            ResetButtonColor(menuButtons[selectedButtonIndex]);
        }

        // Activate the new panel
        panels[buttonIndex].SetActive(true);

        // Highlight the new button
        HighlightButton(menuButtons[buttonIndex]);

        // Update the selected button index
        selectedButtonIndex = buttonIndex;
    }

    void HighlightButton(Button button)
    {
        // Change the button color to indicate selection
        ColorBlock colors = button.colors;
        colors.normalColor = Color.green; // Change to your desired highlight color
        button.colors = colors;
    }

    void ResetButtonColor(Button button)
    {
        // Reset the button color to its default
        ColorBlock colors = button.colors;
        colors.normalColor = Color.white; // Change to your default color
        button.colors = colors;
    }
}
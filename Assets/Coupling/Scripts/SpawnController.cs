using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnController : MonoBehaviour
{
    [SerializeField] ObjectSpawner spawner;
    [SerializeField] UI_Manager UI;
    private void OnEnable()
    {
        DeactivateSpawner();
        UIController();
    }

    private void DeactivateSpawner()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
        spawner.gameObject.SetActive(false);
    }

    private void UIController()
    {
        UI = FindObjectOfType<UI_Manager>(true);
        if (UI != null)
        {
            UI.gameObject.SetActive(true);
            UI.OnObjectSpawned(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject normalView;

    [Header("Select Type")]
    [SerializeField] Button opeaqueButton;
    [SerializeField] Button transparentButton;

    [Header("Animation Control")]
    [SerializeField] Button connectButton;
    [SerializeField] Button disconnectButton;

    [SerializeField] Slider slider;

    [Tooltip("Other's dependencies")]
    AnimationController animator;
    ObjectActivationController activationController;

    private void Start()
    {
        connectButton.interactable = true;
        disconnectButton.interactable = false;

        connectButton.onClick.AddListener(OnConnectButtonClicked);
        disconnectButton.onClick.AddListener(OnDisconnectButtonClicked);

        opeaqueButton.onClick.AddListener(OnOpeaqueButtonClicked);
        transparentButton.onClick.AddListener(OnTransparentButtonClicked);

        slider.onValueChanged.AddListener(activationController.HandleTranspancy);
    }

    public void OpeaqueView()
    {
        normalView.SetActive(true);
    }

    public void TransparentView()
    {
        OpeaqueView();
        //slider.gameObject.SetActive(true);
    }

    private void OnConnectButtonClicked()
    {
        if (animator != null)
        {
            if(activationController.IsTransparent)
                animator.PlayAnimation("connect_t");
            else
                animator.PlayAnimation("connect");

            disconnectButton.interactable = true;
            connectButton.interactable = false;
        }
    }
    private void OnDisconnectButtonClicked()
    {
        if (animator != null)
        {
            if (activationController.IsTransparent)
                animator.PlayAnimation("disconnect_t");
            else
                animator.PlayAnimation("disconnect");

            connectButton.interactable = true;
            disconnectButton.interactable = false;
        }
    }

    private void OnOpeaqueButtonClicked()
    {
        if(opeaqueButton != null)
        {
            activationController.SelectType(false);
            activationController.HandleObjectActicvation();
        }
    }

    private void OnTransparentButtonClicked()
    {
        if (transparentButton != null)
        {
            activationController.SelectType(true);
            activationController.HandleObjectActicvation();
        }
    }
    public void OnObjectSpawned(GameObject spawnedObject)
    {
        GameObject animatedObject = spawnedObject;
        animator = animatedObject.GetComponent<AnimationController>();
        activationController = animatedObject.GetComponent<ObjectActivationController>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found");
        }
        else
        {
            Debug.Log("Animator assigned successfully");
        }
    }
}

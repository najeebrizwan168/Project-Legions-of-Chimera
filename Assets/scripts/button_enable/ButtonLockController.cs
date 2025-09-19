using UnityEngine;
using UnityEngine.UI;

public class ButtonLockController : MonoBehaviour
{
    public Button targetButton;
    public GameObject timer;
    public Transform timer_Position;
    public RectTransform uiParent;

    private GameObject currentTimer; // holds the spawned timer

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnButtonPressed);
        }
    }

    void OnButtonPressed()
    {
        Debug.Log("Button pressed!");
        DisableButton();
    }

    public void DisableButton()
    {
        if (targetButton != null)
        {
            targetButton.interactable = false;
        }

        if (timer == null)
        {
            Debug.LogError("Timer GameObject is not assigned!");
        }
        else
        {
            Debug.Log("Timer found, activating...");

            // Instantiate and parent under UI
            currentTimer = Instantiate(timer, uiParent);
            currentTimer.transform.position = timer_Position.position;

            // Set the onTimerEnd event to call EnableButton
            Timer timerScript = currentTimer.GetComponent<Timer>();
            if (timerScript != null)
            {
                timerScript.onTimerEnd.AddListener(EnableButton);
            }
        }
    }

    public void EnableButton()
    {
        if (targetButton != null)
        {
            targetButton.interactable = true;
        }

        if (currentTimer != null)
        {
            Destroy(currentTimer); // destroy the instance, not the prefab!
        }
    }
}

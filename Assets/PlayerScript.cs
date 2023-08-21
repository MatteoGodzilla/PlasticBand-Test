using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public GameObject square;
    public TextMeshProUGUI tmp;
    BasicPlayer inputActions;

    void Start()
    {
        inputActions = new BasicPlayer();
        inputActions.Enable();
        inputActions.Map.Click.performed += OnClick;
        tmp.text = inputActions.Map.Click.GetBindingDisplayString();
    }

    void OnClick(InputAction.CallbackContext context)
    {
        square.SetActive(context.ReadValueAsButton());
    }

    public void StartRebinding()
    {
        Debug.Log("Start Rebinding");
        inputActions.Disable();
        inputActions.Map.Click.PerformInteractiveRebinding()
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete((operation) =>
            {
                inputActions.Enable();
                tmp.text = inputActions.Map.Click.GetBindingDisplayString();
            })
            .OnCancel((operation) =>
            {
                Debug.Log("Canceled");
                inputActions.Enable();
            })
            .Start();
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputActions inputActions;

    public UnityEvent onClick;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        inputActions = new InputActions();
    }

    private void OnEnable() => inputActions.Enable();

    private void OnDisable() => inputActions.Disable();

    private void OnClickButtonPressed()
    {
        inputActions.Player.Click.performed += ctx =>
        {
            onClick.Invoke();
        };
    }

    private void Update()
    {
        OnClickButtonPressed();
    }
}

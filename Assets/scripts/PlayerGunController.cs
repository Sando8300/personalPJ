using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunController : MonoBehaviour
{
    public Gun gun;
    InputAction fireAction;
    InputAction reloadAction;

    private void OnEnable()
    {
        var map = InputSystem.actions;
        fireAction = map.FindAction("Attack");
        reloadAction = map.FindAction("Reload");

        fireAction.Enable();
        reloadAction.Enable();

        fireAction.started += OnFireStarted;
        fireAction.canceled += OnFireCanceled;
        fireAction.performed += OnFirePerformed;
        reloadAction.performed += OnReloadPerformed;
    }

    private void OnDisable()
    {
        fireAction.started -= OnFireStarted;
        fireAction.canceled -= OnFireCanceled;
        fireAction.performed -= OnFirePerformed;
        reloadAction.performed -= OnReloadPerformed;

        fireAction?.Disable();
        reloadAction?.Disable();
    }

    void OnFireStarted(InputAction.CallbackContext ctx)
    {
        if (gun != null)
            gun.OnTriggerHeld();
    }
    void OnFireCanceled(InputAction.CallbackContext ctx)
    {
        if (gun != null)
            gun.OffTriggerHeld();
    }

    void OnFirePerformed(InputAction.CallbackContext ctx)
    {
        if (gun != null)
            gun.FireShot();
    }
    void OnReloadPerformed(InputAction.CallbackContext ctx)
    {
        if (gun == null) return;
        if (gun.reload != null) return;
          if (gun.reload == null) gun.reload = StartCoroutine(gun.ReloadDuration());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


}

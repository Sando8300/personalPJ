/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CustomXRGrab : XRGrabInteractable
{
   private Rigidbody _rigidbody;
    private CharacterJoint _parentJoint;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        _parentJoint = GetComponent<CharacterJoint>();
    }

    private void Start()
    {
        Rigidbody _parentRigidbody = GetComponentInParent<Rigidbody>();
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        if (_parentJoint != null)
        {
            // 잡는 순간 부모 Joint의 연결을 끊습니다.
            // 이렇게 하면 이 부위만 독립적으로 움직이게 됩니다.
            _parentJoint.connectedBody = null;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // 놓는 순간 원래의 Joint 연결을 복구합니다.
        // 연결이 다시 활성화되어 래그돌이 통합된 물리 시스템으로 돌아옵니다.
        if (_parentJoint != null)
        {

            _parentJoint.connectedBody.GetComponentInParent<Rigidbody>();
            // 부모 Rigidbody를 찾아 다시 연결해야 합니다. 
            // 이 예시에서는 parent의 Rigidbody를 미리 캐싱해두는 것이 좋습니다.
            // 예를 들어, ParentRigidbody 라는 변수를 만들어서 Start()에서 미리 찾아두세요.
        }
    }
}
*/
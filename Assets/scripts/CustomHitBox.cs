using UnityEngine;



public class CustomHitBox : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] string animName;
    [SerializeField] float duration = 0.5f;
    [SerializeField] Animator animator;
    public bool isAnim = false;


    private void Awake()
    {

        animator = GetComponentInParent<Animator>();

    }


    public float HitandAnim()
    {

        
        if (!isAnim) animator.SetTrigger(animName);
        isAnim = true;
        Invoke("HitAnimDelay", duration);
        return damageMultiplier;

    }

    public void HitAnimDelay()
    {
        isAnim = false;
    }
}




using UnityEngine;


public class bulletDamage : MonoBehaviour
{
    public GameObject Damager;
    float damage = 0;

    public bool isDamaged = false;
    float dmgMul = 1;

    [SerializeField] AudioClip audioClip;
    public GameObject bloodFxPrefabs;


    private void Awake()
    {

        Damager = this.gameObject;

    }

    private void Start()
    {
        //damage = 
    }
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Enemy") && !isDamaged)
        {

            ContactPoint contactPoint = collision.contacts[0];
            Vector3 hitPoint = contactPoint.point;
            Vector3 hitNormal = contactPoint.normal;

            isDamaged = true;
            bool isHitbox = collision.gameObject.TryGetComponent<CustomHitBox>(out CustomHitBox customHitBox);
            if (isHitbox)
            {
                Debug.Log("특정부위 공격성공");
                dmgMul = customHitBox.HitandAnim();
            }
            Quaternion rotation = Quaternion.LookRotation(hitNormal);
            collision.gameObject.GetComponentInParent<EnemySimpleDamaged>().TakeDamage(damage * dmgMul, Damager);
            GameObject bloodfx = Instantiate(bloodFxPrefabs, hitPoint, rotation);
            AudioSource.PlayClipAtPoint(audioClip, hitPoint);
            Debug.Log("데미지입힘.");

            //위에서 불러온 에니메이션 실행 및  위 TakeDamage에 값을 곱함.


        }
    }

/*    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isDamaged)
        {
            isDamaged = !isDamaged;
        }
    }
*/


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}

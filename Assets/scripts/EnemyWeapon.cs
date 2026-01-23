using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float attackDamage = 25f;
    public GameObject enemy;

    // [추가] 쿨타임 설정 (2초)
    public float attackCooldown = 2.0f;
    // [추가] 다음 공격이 가능한 시간(타임스탬프)을 저장할 변수
    private float nextAttackTime = 0f;

    private void Start()
    {
        GetComponent<Collider>().gameObject.SetActive(false); 

        SubjectAI ai = GetComponentInParent<SubjectAI>();
        if (ai != null)
        {
            enemy = ai.gameObject;
        }
    }

    // 공격 콜라이더가 무언가에 닿았을 때 실행
    private void OnTriggerEnter(Collider other)
    {
        // [추가] 1. 쿨타임 체크
        // 현재 시간(Time.time)이 다음 공격 가능 시간보다 작으면 (아직 쿨타임 중이면) 무시
        if (Time.time < nextAttackTime)
        {
            return;
        }

        // 2. 부딪힌 녀석(other)에게서 SimpleDamaged 컴포넌트를 찾습니다.
        SimpleDamaged player = other.GetComponent<SimpleDamaged>();

        // 3. 만약 플레이어가 맞다면 (null이 아니라면)
        if (player != null)
        {
            // [추가] 공격 성공! -> 다음 공격 가능 시간을 '현재시간 + 2초' 뒤로 미룸
            nextAttackTime = Time.time + attackCooldown;

            // 4. 데미지 정보를 포장합니다.
            DamageInfo dmg = new DamageInfo
            {
                amount = attackDamage,
                type = DamageType.Physical,
                attacker = enemy != null ? enemy : this.gameObject // 적 본체를 공격자로 등록 (없으면 무기 자신)
            };

            // 5. 플레이어에게 데미지 상자를 던집니다!
            player.TakeDamage(dmg);

            Debug.Log("플레이어 타격 성공!");
            GetComponent<Collider>().gameObject.SetActive(false);
        }
    }
}
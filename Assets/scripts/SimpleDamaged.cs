


using UnityEngine;
using UnityEngine.Rendering;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

    public class SimpleDamaged : MonoBehaviour
    {

    private void Start()
    {
        GameManagerScript.instance.simpleDamagedPlayer = this;

        /*   if (volume != null && volume.profile.TryGet<Vignette>(out vignette))
           {
               // 성공적으로 가져왔다면 초기 강도 0으로 설정
               vignette.intensity.value = 0f;
           }*/
    }

        public Volume volume;
        private Vignette vignette;

        public void TakeDamage(DamageInfo damageInfo)
        {
        float finalDamage = CalculateDamage(damageInfo);

        GameManagerScript.instance.currentHp -= finalDamage;
        StartCoroutine(GameManagerScript.instance.uiManager.HpRefresh());

        if (GameManagerScript.instance.currentHp <= 0)
            Debug.Log("플레이어 사망");           
        }

    float CalculateDamage(DamageInfo info)
    {
        float damage = info.amount;

        // 방어구 적용
        float armor = GameManagerScript.instance.currentArmor;

        if (info.type == DamageType.Physical)
        {
            damage -= armor;
        }

        return Mathf.Max(damage, 1f); // 최소 데미지
    }

   

        void Update()
        {
          //  CustomUpdateVignette();
        }
    

    void CustomUpdateVignette()
        {

            float targetIntensity = 1.0f - GameManagerScript.instance.hpRadio;   
            Mathf.Clamp(targetIntensity, 0.0f, 1.0f);
            if (volume != null && volume.profile.TryGet<Vignette>(out Vignette vignette))
            {
                // 성공적으로 가져왔다면 초기 강도 0으로 설정
                vignette.intensity.value = targetIntensity;
            }
        }
    }


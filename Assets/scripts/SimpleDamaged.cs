


using UnityEngine;
using UnityEngine.Rendering;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

    public class SimpleDamaged : MonoBehaviour
    {
        
        public Volume volume;
        private Vignette vignette;

        public void TakeDamage(float damage, GameObject damager)
        {
            GameManagerScript.instance.currentHp -= damage;
        StartCoroutine(GameManagerScript.instance.uiManager.HpRefresh());
            Debug.Log("데미지 입음");
            //  tunnelingVignetteController.defaultParameters.apertureSize
           
            if (GameManagerScript.instance.currentHp <= 0)
            { 
                Debug.Log("플레이어 사망"); 
                //사망 카메라 애니메이션 및 사우드과 블러효과 + 리트라이 UI오픈
            }
               

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
        }

        private void Start()
        {
            GameManagerScript.instance.simpleDamagedPlayer = this;

         /*   if (volume != null && volume.profile.TryGet<Vignette>(out vignette))
            {
                // 성공적으로 가져왔다면 초기 강도 0으로 설정
                vignette.intensity.value = 0f;
            }*/
        }
        // Update is called once per frame
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


using System.Collections;
using UnityEngine;

public class capsuleInteracttion : MonoBehaviour
{
    public Material material;
    public float fadeSpeed = 2f;
    float value;
    bool isFade = true;
    float imsi;


    private void Awake()
    {

        material.SetFloat("_AhphaValue", 1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            ToggleFade();
        }
    }
    public void ToggleFade()
    {


        isFade = !isFade;
        StartCoroutine(Blink());


    }
    IEnumerator Blink()
    {
        float targetValue = isFade ? 0f : 1f;
        while (true)
        {

            float currentValue = material.GetFloat("_AhphaValue");
            float nextValue = Mathf.MoveTowards(
                           currentValue,
                           targetValue,
                           Time.deltaTime * fadeSpeed // speed는 항상 양수
                       );
           

            // 4. 계산된 색상 값을 다시 텍스트 컴포넌트에 할당합니다.
            material.SetFloat("_AhphaValue", nextValue);
            if (Mathf.Approximately(nextValue, targetValue))
            {
                material.SetFloat("_AhphaValue", targetValue); // 정확히 목표 값으로 설정
                break; // 코루틴 종료
            }
            // 5. 다음 프레임까지 대기합니다.
            yield return null;
        }
    }
}

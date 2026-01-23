using System.Collections;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    // 효과를 적용받을 대상 (자기 자신)
    private GameManagerScript gameManagerScript;

    private void Awake()
    {
        gameManagerScript = GetComponent<GameManagerScript>();
    }

   public  void    AddDefenseBuff(int amount, float duration, bool forever)
    {
        
    }







    // 외부(아이템 효과 등)에서 이 함수를 부름
    public void AddHealOverTime(int totalAmount, float duration)
    {
        StartCoroutine(CoHeal(totalAmount, duration));
    }

    private IEnumerator CoHeal(int amount, float duration)
    {
        float timeElapsed = 0;
        // 1초당 회복량 계산
        float rate = amount / duration;
        float healAccumulator = 0;
      
        while (timeElapsed < duration)
        {
            healAccumulator += rate * Time.deltaTime;
            if (healAccumulator > 1f)
            {
                float healInt = 0;
                healInt = Mathf.FloorToInt(healAccumulator);
                //힐값이 1이 넘으면 정수부분만 변수에 넣어 힐함수 실행.

                gameManagerScript.HpModify((int)healInt);
                healAccumulator -= healInt;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 남은 자투리 회복 등 마무리 처리
    }
}
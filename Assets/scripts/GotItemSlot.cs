using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GotItemSlot : MonoBehaviour
{
    Image img;
    TextMeshProUGUI textname;
    [SerializeField] float timer, fadeSpeed;


    private void Awake()
    {
        img = GetComponentInChildren<Image>();
        textname = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    public void Getinfo(ItemData item)
    {
        img.sprite = item.itemimg;
        textname.text = item.itemName;
        StartCoroutine(TextBlink());
    }

    public IEnumerator TextBlink()
    {
        timer = Time.time + 3;
        while (true)
        {
            // 1. 시간 기반의 0~1 사이의 변화값 계산 (sin 함수로 주기적인 움직임 생성)
            // (Time.time * fadeSpeed)로 시간의 흐름을 조절
            // Mathf.PingPong은 0과 길이 사이를 왕복하는 값을 반환 (여기에선 0과 1)
            float t = Mathf.PingPong(Time.time * fadeSpeed, 1f);

            // 2. 현재 색상 값을 가져옵니다.
            Color c = textname.color;
            

            // 3. Lerp를 사용하여 알파값을 0과 1 사이에서 변화시킵니다.
            // (Color의 알파값은 0~1 사이의 float 값입니다.)
            c.a = Mathf.Lerp(0f, 1f, t);

            // 4. 계산된 색상 값을 다시 텍스트 컴포넌트에 할당합니다.
            textname.color = c;
            img.color = c;
            yield return null;
            if (timer < Time.time)
            {  
                yield break;

            }


            // 5. 다음 프레임까지 대기합니다.
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;


public class TabletInteract : MonoBehaviour
{

    public GameObject[] screenArray;
    public GameObject[] journalArray;
    [SerializeField] TextMeshProUGUI interactText;
    public float fadeSpeed = 1f;
    public ContainerUnlock[] containerUnlock;
    public Rigidbody labDoorRB;
    public int index = 3; //인증완료창
    int defindex = 2; //메인매뉴창
    

    [SerializeField] SubjectAI[] subjectAIs;
    int maxSubject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (interactText != null)
            StartCoroutine(TextBlink());
        index = 3;
    }

    // Update is called once per frame


    IEnumerator TextBlink()
    {
        while (true)
        {
            // 1. 시간 기반의 0~1 사이의 변화값 계산 (sin 함수로 주기적인 움직임 생성)
            // (Time.time * fadeSpeed)로 시간의 흐름을 조절
            // Mathf.PingPong은 0과 길이 사이를 왕복하는 값을 반환 (여기에선 0과 1)
            float t = Mathf.PingPong(Time.time * fadeSpeed, 1f);

            // 2. 현재 색상 값을 가져옵니다.
            Color c = interactText.color;

            // 3. Lerp를 사용하여 알파값을 0과 1 사이에서 변화시킵니다.
            // (Color의 알파값은 0~1 사이의 float 값입니다.)
            c.a = Mathf.Lerp(0f, 1f, t);

            // 4. 계산된 색상 값을 다시 텍스트 컴포넌트에 할당합니다.
            interactText.color = c;

            // 5. 다음 프레임까지 대기합니다.
            yield return null;
        }
    }

    public void OpenDoor()
    {
        foreach (var con in containerUnlock)
        {
            Rigidbody doorRB = con.gameObject.GetComponent<Rigidbody>();
            doorRB.constraints = RigidbodyConstraints.None;
            Animator anim = con.gameObject.GetComponent<Animator>();
            if (con.gameObject.CompareTag("LeftDoor"))
                anim.Play("AutoOpenConLeft");
            else if (con.gameObject.CompareTag("RightDoor"))
                anim.Play("AutoOpenConRight");
        }

        //subjects.SubjectMove();
        for (int i = 0; i < subjectAIs.Length; i++)
        {
            SubjectAI agent = subjectAIs[i].gameObject.GetComponent<SubjectAI>();
            agent.enemyStart = true;
        }


    }

    bool isScan = false;
    public void ScanKeyCard()
    {
        if (isScan)
        {
            labDoorRB.constraints = RigidbodyConstraints.None;
            for (int i = 0; i < screenArray.Length-1; i++)
            {
                screenArray[i].SetActive(false);
            }
            screenArray[index].SetActive(true);
            Debug.Log("스캔확인되어 문이 열림     ");
            
        }
        else
        {

            index = 1;
            screenArray[index].SetActive(true);
            screenArray[defindex].SetActive(false);
            Debug.Log("스캔필요함    ");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            isScan = true;
            //스캔완료 소리 
            Debug.Log("스캔완료     ");

        }
    }
}




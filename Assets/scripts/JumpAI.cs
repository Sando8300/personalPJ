using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class JumpAI : MonoBehaviour
{
    public enum JumpType //설정한 점프 타입에 맞게 AI가 점프한다.
    {
        General,
        Fast,
        Slow,
        Teleport
    }
    SubjectAI AI;
    NavMeshAgent agent;
    NavMeshLink link;

    public float offset = 1;
    public float height = 2f;
    public float moveSpd = 1f;
    public float duration = 1f;
    public JumpType jumpType;
    Animator animator;
    public GameObject navMeshLink;
    Transform player;
    public bool isJumped = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = Camera.main.GetComponentInParent<Transform>();
        link = GetComponentInChildren<NavMeshLink>();
    }

    Vector3 startPos;
    Vector3 endPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        AI = GetComponent<SubjectAI>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false; // OffMeshLink를 자동으로 밟지 않도록 설정
        navMeshLink.SetActive(false);
        jumpType = JumpType.General; // 점프 타입 설정
        while (true)
        {

            yield return new WaitForSeconds(0.2f);
            
            if (agent.isOnOffMeshLink && !isJumped) // 링크를 밟으면 실행될 수 있도록 조건문 설정
            {
                
                Debug.Log("점프패드발동");
                if (jumpType == JumpType.General)
                {
                    agent.isStopped = true;
                    StartCoroutine(General());
                }

                agent.updateRotation = true;
            }
            yield return null;
        }
    }

    IEnumerator General()
    {

        animator.SetBool("isJump", true);
        float jumpDelay = 0;
        float nomarlizeTime = 0f;
        isJumped = true;

        while (jumpDelay < 0.8f)
        {
            jumpDelay += Time.deltaTime;
            yield return null;
        }


        Debug.Log($"[General] startPos(world) = {startPos}, endPos(world) = {endPos}");
        while (nomarlizeTime < 1f)
        {

            float yOffset = height * (nomarlizeTime - nomarlizeTime * nomarlizeTime);
            transform.position = Vector3.Lerp(agent.currentOffMeshLinkData.startPos, agent.currentOffMeshLinkData.endPos, nomarlizeTime) + height * Vector3.up * yOffset;       
            nomarlizeTime = nomarlizeTime + Time.deltaTime / duration;
            yield return null;

        }
        animator.SetBool("isJump", false);
        navMeshLink.SetActive(false);
        agent.CompleteOffMeshLink();
        agent.ResetPath();
        agent.isStopped = false;

        while (jumpDelay < 1.5f)
        {
            jumpDelay += Time.deltaTime;
            yield return null;
        }


        isJumped = false;



    }
    // Update is called once per frame

    

  
}

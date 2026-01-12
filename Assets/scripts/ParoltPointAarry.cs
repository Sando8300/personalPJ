using UnityEngine;
using UnityEngine.AI;

public class ParoltPointAarry : MonoBehaviour
{
    public Transform[] patrolPointAarry;
    [SerializeField] SubjectAI[] subjects;
    int maxSubject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        maxSubject = 20;      
        /*for (int i = 0; i < subjects.Length; i++)
        {
            
           NavMeshAgent agent = subjects[i].gameObject.GetComponent<NavMeshAgent>();
            agent.speed = 0;
        }*/

        // Update is called once per frame

    }


    public void SubjectMove()
    {
        for (int i = 0; i < subjects.Length; i++)
        {
            NavMeshAgent agent = subjects[i].gameObject.GetComponent<NavMeshAgent>();
            agent.speed = 1.5f;
        }
    }
}

using UnityEngine;

public class JounalTrigger : MonoBehaviour
{
    public TabletInteract tabletInteract;
    public int index=0;

    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {   
            JournalUpdate();
            tabletInteract.journalArray[index].SetActive(true);
        }


    }
    public void JournalUpdate()
    {
        for (int i = 0; i < tabletInteract.journalArray.Length; i++)
        {
            tabletInteract.journalArray[i].SetActive(false);
        }
    }


}

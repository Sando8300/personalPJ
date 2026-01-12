using UnityEngine;

public class FootStepGiver_Sand : MonoBehaviour
{
    public FootStepData footstepData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FPplayer audioChange = other.GetComponent<FPplayer>();
            audioChange.footstepGeneral = footstepData.footstepAudio.walkSand;
            audioChange.runningfootstepGeneral = footstepData.footstepAudio.runSand;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPplayer audioChange = other.GetComponent<FPplayer>();
            audioChange.footstepGeneral = footstepData.footstepAudio.walkGrass;
            audioChange.runningfootstepGeneral = footstepData.footstepAudio.runGrass;
        }
    }
}

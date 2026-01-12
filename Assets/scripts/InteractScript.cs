using UnityEngine;
using UnityEngine.UI;

public class OpenInteractScript : MonoBehaviour
{
    public Image interactUI;
    bool isReady = false;
    public Animator openL;
    public Animator openR;
    bool open = false;

    public AudioClip[] sounds;
    public AudioSource source;
    private void Start()
    {

        interactUI.color = GameManagerScript.instance.uiManager.cOFF;
    }

    private void Update()
    {
        if (isReady && Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactUI.sprite = GetComponent<InteractableObject>().icon;
            interactUI.color = GameManagerScript.instance.uiManager.cON; ;
            
            isReady = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactUI.color = GameManagerScript.instance.uiManager.cOFF;

            isReady = false;
        }
    }



    void Interact()
    {
        if (!open)
        {
            openL.Play("AutoOpenConLeft");
            openR.Play("AutoOpenConRight");
            open = !open;
            source.PlayOneShot(sounds[0]);
        }
        if (open)
            openL.Play("AutoOpenConLeft");
        openR.Play("AutoOpenConRight");
        open = !open;
        source.PlayOneShot(sounds[0]);
    }
}

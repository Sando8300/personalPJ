using Unity.VisualScripting;
using UnityEngine;

public class ItemEquip : MonoBehaviour
{
    public AudioClip imsiaudio;
    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
                Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(imsiaudio, transform.position);
                //지퍼소리출력.
        }
    }

}

using UnityEngine;

public class ObjectUIManger : MonoBehaviour
{
    public Transform playerpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerpoint);
    }
}

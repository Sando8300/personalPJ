using Unity.Cinemachine.Samples;
using UnityEngine;

public class AmmoPoolingMG : MonoBehaviour
{
    public int maxAmmo = 50;
    public GameObject[] Mag;
    public GameObject bulletprefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mag = new GameObject[maxAmmo];
        for (int i = 0; i < maxAmmo; i++)
        {
            Mag[i] = Instantiate(bulletprefab);
            Mag[i].GetComponent<bulletDamage>();
            Mag[i].SetActive(false);
        }
    }

}

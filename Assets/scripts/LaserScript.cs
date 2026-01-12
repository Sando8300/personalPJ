using UnityEngine;
using UnityEngine.VFX;

public class LaserScript : MonoBehaviour
{

    public Transform laserOrigin;
    public Vector3 realpoint;
    public VisualEffect laserVFX;
    public float maxLaserPoint = 100f;
    public float test = 5;
    public VisualEffect colLaserVFX;


    private void Start()
    {
        Debug.Log(laserVFX.transform.position.x);
    }
    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Vector3 endPoint;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.parent.forward, out hit, maxLaserPoint))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = laserOrigin.position + laserOrigin.parent.forward * maxLaserPoint;
        }
        realpoint = transform.InverseTransformPoint(endPoint);
        laserVFX.SetVector3("LaserColPoint",new Vector3(realpoint.x, 0,0));
       // colLaserVFX.Play();
    }
}

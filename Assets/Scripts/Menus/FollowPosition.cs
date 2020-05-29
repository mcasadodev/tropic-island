using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target, child;
    public bool zCentering;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;

        if (zCentering)
        {
            Vector3 xRot = new Vector3(0, 0, target.rotation.eulerAngles.z);
            child.localRotation = Quaternion.Euler(xRot);
        }
    }
}
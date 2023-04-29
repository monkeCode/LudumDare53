using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToDestination : MonoBehaviour
{
    public Transform target;                    ////Target to point at (you could set this to any gameObject dynamically)


    private void Update()
    {
        if (target == null)
            return;
        var vectorToTarget = target.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

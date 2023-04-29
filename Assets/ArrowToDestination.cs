using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToDestination : MonoBehaviour
{
    // public Transform target;                    
    //
    //
    // private void Update()
    // {
    //     if (target == null)
    //         return;
    //     var vectorToTarget = target.transform.position - transform.position;
    //     var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
    //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    // }

    private Camera cam;
    private float borderSize = 100f;

    public Transform target;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var vectorToTarget = target.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var pointerOnScreenPos = cam.WorldToScreenPoint(target.position);
        bool isOffScreen = pointerOnScreenPos.x <= borderSize || pointerOnScreenPos.x >= Screen.width - borderSize ||
                           pointerOnScreenPos.y <= borderSize || pointerOnScreenPos.y >= Screen.height - borderSize;
        if (!isOffScreen) return;
        var cappedScreenPos = pointerOnScreenPos;
        if (cappedScreenPos.x <= borderSize)
            cappedScreenPos.x = borderSize;
        if (cappedScreenPos.x >= Screen.width - borderSize)
            cappedScreenPos.x = Screen.width - borderSize;
        if (cappedScreenPos.y <= borderSize)
            cappedScreenPos.y = borderSize;
        if (cappedScreenPos.y >= Screen.height - borderSize)
            cappedScreenPos.y = Screen.height - borderSize;
        var pointerWorldPosition = cam.ScreenToWorldPoint(cappedScreenPos);
        transform.position = pointerWorldPosition;
        transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}

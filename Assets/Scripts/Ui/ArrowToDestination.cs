using UnityEngine;

public class ArrowToDestination : MonoBehaviour
{
    [SerializeField] private float borderSize = 200f;
    
    private Camera cam;
    private bool isOffScreen;
    private Vector3 vectorUp = new Vector3(0, 2, 0); 
    
    public Vector3 target;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        var toPosition = target;
        var fromPosition = cam.transform.position;
        fromPosition.z = 0f;
        var dir = (toPosition - fromPosition).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.localEulerAngles = new Vector3(0, 0, angle);

        var pointerOnScreenPos = cam.WorldToScreenPoint(target);
        isOffScreen = pointerOnScreenPos.x <= borderSize || pointerOnScreenPos.x >= Screen.width - borderSize ||
                      pointerOnScreenPos.y <= borderSize || pointerOnScreenPos.y >= Screen.height - borderSize;
        if (isOffScreen)
        {
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
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        }
        else
        {
            transform.position = target + vectorUp;
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }

    }
}

using UnityEngine;

namespace Weapons
{
    public class AOEWeaponHitZone: MonoBehaviour
    {
        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform bottomRight;
        public float topLeftX;
        public float topLeftY;
        public float bottomRightX;
        public float bottomRightY;

        private void Start()
        {
            topLeftX = topLeft.transform.position.x;
            topLeftY = topLeft.transform.position.y;
            bottomRightX = bottomRight.transform.position.x;
            bottomRightY = bottomRight.transform.position.y;
        }
    }
}
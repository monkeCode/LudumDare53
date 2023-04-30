using UnityEngine;

namespace Weapons
{
    public class AOEWeaponHitZone: MonoBehaviour
    {
        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform bottomRight;
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip hitEffect;
        public float topLeftX;
        public float topLeftY;
        public float bottomRightX;
        public float bottomRightY;

        private void Start()
        {
            hitEffect.wrapMode = WrapMode.ClampForever;
        }
        
        private void Update()
        {
            var position = topLeft.transform.position;
            topLeftX = position.x;
            topLeftY = position.y;
            var position1 = bottomRight.transform.position;
            bottomRightX = position1.x;
            bottomRightY = position1.y;
        }

        public void ShowHitEffect()
        {
            Debug.Log("zone hit effect");
            animator.Play("AOEHit", -1, 0f);
        }
    }
}
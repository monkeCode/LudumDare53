using UnityEngine;

namespace Entities
{
    public class Boss : Entity
    {
        [SerializeField] private float _toPlayerDis;
        
        protected override void Update()
        {
            base.Update();
            var dir = Player.Player.Instance.transform.position - transform.position;
            if (dir.magnitude > _toPlayerDis)
            {
                transform.position = Player.Player.Instance.transform.position + dir.normalized * _toPlayerDis / 2;
            }
        }
    }
}

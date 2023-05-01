using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int heal;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<Player.Player>())
            return;
        var heals = heal/100 * Player.Player.Instance.MaxHp;
        UiManager.Instance.ShowHealingText(heals, Player.Player.Instance.transform.position);
        Player.Player.Instance.Heal(heals);
        Destroy(gameObject);
    }
}

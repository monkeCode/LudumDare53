using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    [SerializeField] private GameObject uhOhSociety;
    void Start()
    {
        WaveGenerator.TimeEnded += Cringe;
    }

    private void Cringe()
    {
        if (Player.Player.Instance.Money > 1000)
            StartCoroutine(UltraCringe());
        else
            StartCoroutine(UltraCringeBad());

    }

    private IEnumerator UltraCringe()
    {
        UiManager.Instance.FlashBang();
        yield return new WaitForSeconds(1f);
        HeinzDoofenshmirtzInstantinator.TOTALYKIIIIIIIIIIIIIIIILL();
        Instantiate(uhOhSociety, transform.position, Quaternion.identity);
        uhOhSociety.SetActive(true);
    }

    private IEnumerator UltraCringeBad()
    {
        UiManager.Instance.DarkEnd();
        yield return new WaitForSeconds(1f);
    }
}

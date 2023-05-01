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

        StartCoroutine(UltraCringe());

    }

    private IEnumerator UltraCringe()
    {
        UiManager.Instance.FlashBang();
        yield return new WaitForSeconds(1f);
        uhOhSociety.SetActive(true);
    }
}

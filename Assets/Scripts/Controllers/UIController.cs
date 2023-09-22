using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIController : MonoBehaviour
{
    protected IEnumerator ToggleGameObjectWithDelay(GameObject obj, bool state, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(state);
    }

    protected IEnumerator TimeScaleControlDelay(float timeScale, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = timeScale;
    }
}

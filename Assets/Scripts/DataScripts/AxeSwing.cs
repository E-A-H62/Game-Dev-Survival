using System.Collections;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public Transform axe;
    public float swingSpeed = 10f;

    private Vector3 originalRotation;
    private bool swinging;

    void Start()
    {
        originalRotation = axe.localEulerAngles;
    }

    public void Swing()
    {
        if (!swinging)
            StartCoroutine(SwingRoutine());
    }

    IEnumerator SwingRoutine()
    {
        swinging = true;

        axe.localEulerAngles = originalRotation + new Vector3(0f, 0f, -60f);
        yield return new WaitForSeconds(0.15f);

        axe.localEulerAngles = originalRotation;
        yield return new WaitForSeconds(0.25f);

        swinging = false;
    }
}
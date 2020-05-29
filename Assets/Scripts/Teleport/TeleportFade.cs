using System.Collections;
using UnityEngine;

public class TeleportFade : MonoBehaviour
{
    public Animator anim;
    public float fadeTime;

    void Update()
    {
        if (Pause.P.blockControl)
            return;
        if (Pause.P.paused)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Fade(fadeTime));
        }
    }

    IEnumerator Fade(float _fadeTime)
    {
        Pause.P.blockControl = true;
        anim.Play("TeleportFade_FadeIn");
        Debug.Log("cc");
        yield return new WaitForSeconds(_fadeTime);
        Debug.Log("dd");
        anim.Play("TeleportFade_FadeOut");
        yield return new WaitForSeconds(_fadeTime);
        anim.Play("TeleportFade_Blank");
        Pause.P.blockControl = false;
    }
}
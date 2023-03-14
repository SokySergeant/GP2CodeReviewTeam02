using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImgInOut : MonoBehaviour
{
    [SerializeField] private Image _img;
    
    

    public void FadeImg(bool fadeIn = true)
    {
        StartCoroutine(fadeIn ? Fade(1, 0.5f) : Fade(0, 0.5f));
    }
    
    
    
    private IEnumerator Fade(float endValue, float duration)
    {
        Color color = _img.color;
        float time = 0;
        float startValue = color.a;
        while(time < duration)
        {
            color.a = (Mathf.Lerp(startValue, endValue, time / duration));
            _img.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = endValue;
        _img.color = color;
        yield return null;
    }
}

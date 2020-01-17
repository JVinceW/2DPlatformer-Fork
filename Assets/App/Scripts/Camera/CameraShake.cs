using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Shakes(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude, 0));
    }

    public void ShakesVertical(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude, 1));
    }
    public void ShakesHorizontal(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude, 2));
    }
    /// <summary>
    /// Shake camera
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="magnitude"></param>
    /// <param name="type">0: all, 1: Vertical, 2: Horizontal</param>
    /// <returns></returns>
   IEnumerator Shake(float duration, float magnitude, int type)
    {
        Vector3 OriginPosition = transform.localPosition;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            if(type == 0)
                transform.localPosition = new Vector3(x, y, OriginPosition.z);
            if(type == 1)
                transform.localPosition = new Vector3(OriginPosition.x, y, OriginPosition.z);
            if(type == 2)
                transform.localPosition = new Vector3(x, OriginPosition.y, OriginPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = OriginPosition;
    }
}

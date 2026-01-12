using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
    public float time = 0.05f;
    public bool autoFlicker = false; // 자동 깜박임 사용 여부

    private Light _light;
    private Coroutine _flickerCoroutine;
    private Coroutine _flashCoroutine;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    void Start()
    {
        if (autoFlicker)
            StartFlicker();
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            _light.enabled = !_light.enabled;

            float timer = time;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

    // 자동 깜박이 제어
    public void StartFlicker()
    {
        if (_flickerCoroutine == null)
            _flickerCoroutine = StartCoroutine(Flicker());
    }

    public void StopFlicker()
    {
        if (_flickerCoroutine != null)
        {
            StopCoroutine(_flickerCoroutine);
            _flickerCoroutine = null;
        }
    }

    // 한 번만 켜고 duration 후 끄기 (Gun에서 호출)
    public void FlashOnce(float duration = 0.05f)
    {
        // 자동 깜박이가 켜져있다면 끈다(원하면 주석 처리)
        StopFlicker();

        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashCoroutine(duration));
    }

    private IEnumerator FlashCoroutine(float duration)
    {
        _light.enabled = true;
        yield return new WaitForSeconds(duration);
        _light.enabled = false;
        _flashCoroutine = null;
    }
}

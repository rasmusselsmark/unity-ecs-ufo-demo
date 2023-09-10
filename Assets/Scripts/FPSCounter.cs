using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 
public class FPSCounter : MonoBehaviour
{
    public int FramesPerSec { get; protected set; }
 
    [SerializeField] private float frequency = 0.5f;
 
    private Text counter;
 
    private void Start()
    {
        counter = GetComponent<Text>();
        counter.text = "";
        StartCoroutine(FPS());
    }
 
    private IEnumerator FPS()
    {
        for (; ; )
        {
            var lastFrameCount = Time.frameCount;
            var lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
 
            var timeSpan = Time.realtimeSinceStartup - lastTime;
            var frameCount = Time.frameCount - lastFrameCount;
 
            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            counter.text = $"FPS: {FramesPerSec}";
        }
    }
}
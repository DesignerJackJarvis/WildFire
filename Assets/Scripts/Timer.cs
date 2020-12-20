using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;
    private void Update()
    {
        time += Time.deltaTime;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
    public UnityEvent<string> time;
    public UnityEvent<string> treesChange;
    private Timer _timer;

    private void Start()
    {
        treesChange.Invoke("Trees Remaining "+FindObjectOfType<TreeGeneration>()._alreadySpawnedPlaces.Count);
        FindObjectOfType<TreeGeneration>().ONTreeDead += delegate(int i) {treesChange.Invoke("Trees Remaining "+i); };
        _timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        var seconds = (int) (_timer.time % 60);
        var minutes = (int) (_timer.time / 60);
        time.Invoke(minutes != 0 ? $"{minutes}:{seconds:00}" : seconds.ToString());
    }
}

using UnityEngine;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
    public UnityEvent<string> time;
    public UnityEvent<string> treesChange;
    private Timer _timer;

    private void Start()
    {
        FindObjectOfType<TreeGeneration>().ONTreeDead += delegate(int i) {time.Invoke(i.ToString()); };
        _timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        var seconds = (int) (_timer.time % 60);
        var minutes = (int) (_timer.time / 60);
        treesChange.Invoke($"{minutes}:{seconds}");
    }
}

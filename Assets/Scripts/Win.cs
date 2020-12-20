using UnityEngine;
using UnityEngine.Events;

public class Win : MonoBehaviour
{
    public UnityEvent<string> winMessage;
    private string _message;
    private bool _didBeatHighScore;
    public void DisplayHighScore()
    {
        var time = FindObjectOfType<Timer>().time;
        var treesRemaining = FindObjectOfType<TreeGeneration>().TreesRemaining;
        var highScore = FindObjectOfType<HighScore>().Load();
        if (highScore.treesRemaining == 0 && highScore.time == 0)
        {
            var seconds = (int) (time % 60);
            var minutes = (int) (time / 60);
            var newTime = minutes != 0 ? $"{minutes}:{seconds}" : seconds.ToString();
            _message =
                "You Had No Previous High Score \n" +
                $"Your Time Was {newTime} And You Had {treesRemaining} Trees Remaining";
        }
        else if (highScore.treesRemaining > treesRemaining || highScore.time < time)
        {
            var seconds = (int) (time % 60);
            var minutes = (int) (time / 60);
            var newTime = minutes != 0 ? $"{minutes}:{seconds}" : seconds.ToString();
            var oldSeconds = (int) (highScore.time % 60);
            var oldMinutes = (int) (highScore.time / 60);
            var oldTime = minutes != 0 ? $"{oldMinutes}:{oldSeconds}" : oldSeconds.ToString();
            _message =
                $"You Did Not Beat Your High Score Of {oldTime} And {highScore.treesRemaining} Trees Remaining \n" +
                $"Your Time Was {newTime} And You Had {treesRemaining} Trees Remaining";
        }
        else if (highScore.treesRemaining < treesRemaining)
        {
            var seconds = (int) (time % 60);
            var minutes = (int) (time / 60);
            var newTime = minutes != 0 ? $"{minutes}:{seconds}" : seconds.ToString();
            var oldSeconds = (int) (highScore.time % 60);
            var oldMinutes = (int) (highScore.time / 60);
            var oldTime = minutes != 0 ? $"{oldMinutes}:{oldSeconds}" : oldSeconds.ToString();
            _didBeatHighScore = true;
            _message =
                $"You Did Beat Your High Score Of {oldTime} And {highScore.treesRemaining} Trees Remaining \n" +
                $"Your Time Was {newTime} And You Had {treesRemaining} Trees Remaining";
        }
        else if (highScore.time > time)
        {
            var seconds = (int) (time % 60);
            var minutes = (int) (time / 60);
            var newTime = minutes != 0 ? $"{minutes}:{seconds}" : seconds.ToString();
            var oldSeconds = (int) (highScore.time % 60);
            var oldMinutes = (int) (highScore.time / 60);
            var oldTime = minutes != 0 ? $"{oldMinutes}:{oldSeconds}" : oldSeconds.ToString();
            _didBeatHighScore = true;
            _message =
                $"You Did Beat Your High Score Of {oldTime} And {highScore.treesRemaining} Trees Remaining \n" +
                $"Your Time Was {newTime} And You Had {treesRemaining} Trees Remaining";
        }

        if (_didBeatHighScore)
        {
            FindObjectOfType<HighScore>().Save(time, treesRemaining);
        }
        winMessage.Invoke(_message);
    }
}
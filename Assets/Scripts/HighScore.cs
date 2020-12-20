using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int treesRemaining;
    public float highScore;
    public void Save(float seconds, int treesRemaining)
    {
        PlayerPrefs.SetFloat($"{gameObject.name}_HighScore", seconds);
        PlayerPrefs.SetInt($"{gameObject.name}_TreesRemaining", treesRemaining);
    }

    public HighScore Load()
    {
        highScore = PlayerPrefs.GetFloat($"{gameObject.name}_HighScore", 0);
        treesRemaining = PlayerPrefs.GetInt($"{gameObject.name}_TreesRemaining", 0);
        return this;
    }
}
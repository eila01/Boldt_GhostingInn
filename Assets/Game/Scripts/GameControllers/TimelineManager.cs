using UnityEngine;
using UnityEngine.Playables;
public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;

    public PlayableDirector[] timelines;

    void Awake()
    {
        // Singleton setup (optional, for global access)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Plays a timeline by name
    /// </summary>
    public void Play(string timelineName)
    {
        foreach (var timeline in timelines)
        {
            if (timeline.name == timelineName)
            {
                timeline.gameObject.SetActive(true);
                timeline.Play();
                return;
            }
        }

        Debug.LogWarning($"Timeline '{timelineName}' not found.");
    }

    /// <summary>
    /// Stops a timeline by name
    /// </summary>
    public void Stop(string timelineName)
    {
        foreach (var timeline in timelines)
        {
            if (timeline.name == timelineName)
            {
                timeline.Stop();
                return;
            }
        }
    }

    /// <summary>
    /// Check if a timeline is currently playing
    /// </summary>
    public bool IsPlaying(string timelineName)
    {
        foreach (var timeline in timelines)
        {
            if (timeline.name == timelineName)
                return timeline.state == PlayState.Playing;
        }
        return false;
    }
}

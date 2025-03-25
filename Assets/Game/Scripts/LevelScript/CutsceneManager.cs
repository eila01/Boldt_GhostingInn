using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static bool isCutsceneOn;
    public Animator camanimator;
    public string CutsceneName;

    void StartCutscene()
    {
        isCutsceneOn = true;
        camanimator.SetBool(CutsceneName, true);
        Invoke(nameof(StopCutscene), 3f);
    }

    void StopCutscene()
    {
        isCutsceneOn = false;
        camanimator.SetBool(CutsceneName, false);
    }
}

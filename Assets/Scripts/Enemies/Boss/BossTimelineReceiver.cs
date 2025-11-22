using UnityEngine;
public class BossTimelineReceiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] aiScripts;
    private Animator bossAnimator;
    private void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }
    public void DisableAI()
    {
        //bossAnimator.enabled = false;
        foreach (var script in aiScripts)
            script.enabled = false;
    }
    public void EnableAI()
    {
        //bossAnimator.enabled = true;
        foreach (var script in aiScripts)
            script.enabled = true;
        bossAnimator.SetBool("Started", true);
    }
}

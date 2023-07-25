using UnityEngine;

public sealed class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clipClick;
    [SerializeField]
    private AudioClip clipChat;
    [SerializeField]
    private AudioClip clipPhase;
    [SerializeField]
    private AudioClip clipJudge;
    [SerializeField]
    private AudioClip clipCountUp;
    [SerializeField]
    private AudioClip clipResult;

    public void PlayClick() {
        source.PlayOneShot(clipClick);
    }

    public void PlayChat() {
        source.PlayOneShot(clipChat);
    }

    public void PlayPhase() {
        source.PlayOneShot(clipPhase);
    }

    public void PlayJudge() {
        source.PlayOneShot(clipJudge);
    }

    public void PlayCountUp() {
        source.PlayOneShot(clipCountUp);
    }

    public void PlayResult() {
        source.PlayOneShot(clipResult);
    }
}

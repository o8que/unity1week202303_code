using DG.Tweening;
using UnityEngine;

namespace Ore2Lib
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] musicList;

        private AudioSource source;
        private float initialVolume;
        private int currentMusicId;
        private Tween sourceFadeTween;

        private void Start() {
            source = GetComponent<AudioSource>();
            initialVolume = source.volume;
            currentMusicId = 0;
            sourceFadeTween = source
                .DOFade(0f, 2f)
                .SetEase(Ease.OutCirc)
                .OnComplete(() => source.Stop())
                .SetAutoKill(false)
                .SetLink(gameObject);
        }

        public void Play(int musicId = 0) {
            if (musicId == currentMusicId && source.isPlaying) { return; }

            sourceFadeTween.Complete();
            currentMusicId = musicId;
            source.clip = musicList[musicId];
            source.volume = initialVolume;
            source.Play();
        }

        public void Stop(bool immediately = false) {
            if (!source.isPlaying) { return; }

            if (immediately) {
                source.Stop();
            } else {
                sourceFadeTween.Restart();
            }
        }
    }
}

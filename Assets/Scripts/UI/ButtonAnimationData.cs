using DG.Tweening;
using UnityEngine;

namespace Eatable
{
    [System.Serializable]
    public sealed class ButtonAnimationData
    {
        public TypeTransitionButton typeTransitionButton = TypeTransitionButton.None;
        public TypeAnimationButton typeAnimation = TypeAnimationButton.None;
        public Ease ease = Ease.Linear;
        public AudioClip audioClip;
        public float value = 0;
        public float duration = 0.5f;
        public bool isLoop;
        public bool isClearPosition;
    }
}
using UnityEngine;

namespace Eatable
{
    [CreateAssetMenu(fileName = "GameCfg", menuName = "My/GameCfg")]
    public sealed class GameCfg : ScriptableObject
    {
        public float SizeSwipe => _sizeSwipe;
        [SerializeField] private float _sizeSwipe=100f;
        public float SpeedReturnCard => _speedReturnCard;
        [SerializeField] private float _speedReturnCard=1;
        public float SpeedCorrectExitCard => _speedCorrectExitCard;
        [SerializeField] private float _speedCorrectExitCard=0.1f;
        public float ExitLenghth => _exitLenghth;
        [SerializeField] private float _exitLenghth=0.5f;
        public float SpeedIncorrectExitCard => _speedIncorrectExitCard;
        [Header("Shake Params")]
        [SerializeField] private float _speedIncorrectExitCard = 0.5f;
        public int ShakeStrenghth => _shakeStrenghth;
        [SerializeField] private int _shakeStrenghth=250;
        public int ShakeVibrato => _shakeVibrato;
        [SerializeField] private int _shakeVibrato=10;
        public int ShakeRandomLess => _shakeRandomLess;
        [SerializeField] private int _shakeRandomLess=90;

    }
}
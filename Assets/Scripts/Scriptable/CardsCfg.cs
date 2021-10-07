using UnityEngine;

namespace Eatable
{
    [CreateAssetMenu(fileName ="DataCard",menuName ="My/DataCard")]
    public sealed class CardsCfg : ScriptableObject
    {
        public DataCard[] DataCard => _dataCard;
        [SerializeField] private DataCard[] _dataCard;
    }
}
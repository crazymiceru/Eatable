using UnityEngine;

namespace Eatable
{
    [CreateAssetMenu(menuName = "My/ButtonUICfg", fileName = "ButtonUICfg")]
    public sealed class CustomButtonCfg : ScriptableObject
    {
        public ButtonAnimationData[] typeAnimationData;
    }
}
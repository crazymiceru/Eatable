using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Eatable
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var visualElement = new VisualElement();
            var fieldTypeAnimationData = new PropertyField(serializedObject.FindProperty(CustomButton.NameTypeAnimationData));
            visualElement.Add(fieldTypeAnimationData);
            visualElement.Add(new Label(" "));
            visualElement.Add(new IMGUIContainer(OnInspectorGUI));
            return visualElement;
        }
    }
}
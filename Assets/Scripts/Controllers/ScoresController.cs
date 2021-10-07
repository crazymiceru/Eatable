using TMPro;
using UnityEngine;

namespace Eatable
{

    public class ScoresController : ControllerBasic
    {
        private IReadOnlySubscriptionField<int> _scores;
        private TextMeshProUGUI _text;

        public ScoresController(IReadOnlySubscriptionField<int> scores)
        {
            _scores = scores;
            _scores.Subscribe(SetScores);

            var tagScores=Reference.Canvas.GetComponentInChildren<TagScores>();
            if (tagScores!=null && tagScores.TryGetComponent(out TextMeshProUGUI text))
                _text = text;
            else Debug.LogWarning($"Dont find TagScores or TextMeshProUGUI");
        }

        protected override void OnDispose() => _scores.UnSubscribe(SetScores);
        private void SetScores(int scoresValue) => _text.text = scoresValue.ToString();
    }
}
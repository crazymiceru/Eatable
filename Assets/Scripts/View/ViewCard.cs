using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Eatable
{
    public class ViewCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action evtOnBeginDrag;
        public event Action evtOnEndDrag;
        public CardsCfg CardsCfg => _cardsCfg;
        [SerializeField] private CardsCfg _cardsCfg;

        public void OnBeginDrag(PointerEventData eventData) => evtOnBeginDrag?.Invoke();

        public void OnEndDrag(PointerEventData eventData) => evtOnEndDrag?.Invoke();

        public void OnDrag(PointerEventData eventData) { }
    }
}
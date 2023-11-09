using System.Collections.Generic;
using UnityEngine;
 
#if UNITY_EDITOR

using UnityEditor;

#endif

namespace UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class SmoothDynamicHorizontalLayoutGroup : MonoBehaviour
    {
        [SerializeField] private float _targetSpacing = 0f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private RectOffset _padding = new RectOffset();
        [SerializeField] private TextAnchor _childAlignment = TextAnchor.MiddleCenter;

        private float _spacing = 0f;
        private List<RectTransform> _items;
        private float _totalWidth = 0f;
        private RectTransform _rectTransform;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            UpdateLayout();
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                UpdateLayout();
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                UpdateLayout();
            }
        }

        public void UpdateLayout()
        {
            _items = new List<RectTransform>();

            foreach (Transform child in transform)
            {
                RectTransform rect = child as RectTransform;
                
                if (rect != null && rect.gameObject.activeSelf)
                {
                    _items.Add(rect);
                }
            }

            _totalWidth = CalculateTotalWidth();
            
            UpdateSpacing();
            UpdatePositions();
        }

        private float CalculateTotalWidth()
        {
            float width = _padding.left + _padding.right;
            
            foreach (RectTransform item in _items)
            {
                width += item.sizeDelta.x;
            }

            width += _spacing * (_items.Count - 1);
            
            return width;
        }

        private void UpdateSpacing()
        {
            float availableWidth = _rectTransform.rect.width;

            _spacing = Mathf.Min(_targetSpacing,
                (availableWidth - _totalWidth + _spacing * (_items.Count - 1)) / (_items.Count - 1));
        }

        private void UpdatePositions()
        {
            float currentX = GetStartPosition();

            foreach (RectTransform item in _items)
            {
                float itemCenterX = currentX + item.sizeDelta.x / 2f; 
                Vector3 targetPosition = new Vector3(itemCenterX, GetYPosition(item), 0f);

                item.anchorMin = new Vector2(0f, 1f);
                item.anchorMax = new Vector2(0f, 1f);
                
                item.pivot = new Vector2(0.5f, 0.5f);
                
                if (Application.isPlaying)
                {
                    item.anchoredPosition = Vector3.Lerp(item.anchoredPosition, targetPosition, Time.deltaTime * _speed);
                }
                else
                {
                    item.anchoredPosition = targetPosition;
                }

                currentX += item.sizeDelta.x + _spacing;
            }
        }

        private float GetStartPosition()
        {
            float availableWidth = _rectTransform.rect.width - _padding.left - _padding.right;
            float requiredWidth = _totalWidth - _padding.left - _padding.right;
            float start = _padding.left;

            switch (_childAlignment)
            {
                case TextAnchor.UpperCenter:
                case TextAnchor.MiddleCenter:
                case TextAnchor.LowerCenter:
                    start = _padding.left + (availableWidth - requiredWidth) / 2f;
                    break;
                
                case TextAnchor.UpperRight:
                case TextAnchor.MiddleRight:
                case TextAnchor.LowerRight:
                    start = _padding.left + availableWidth - requiredWidth;
                    break;
            }
            
            return start;
        }

        private float GetYPosition(RectTransform item)
        {
            float pos = 0f;
            
            if (_childAlignment == TextAnchor.UpperLeft || _childAlignment == TextAnchor.UpperCenter ||
                _childAlignment == TextAnchor.UpperRight)
            {
                pos = -(item.sizeDelta.y / 2f);
                pos -= _padding.top;
            }
            else if (_childAlignment == TextAnchor.MiddleLeft || _childAlignment == TextAnchor.MiddleCenter ||
                _childAlignment == TextAnchor.MiddleRight)
            {
                pos = -(item.sizeDelta.y / 2f);
                
                pos -= _padding.top;
                pos += _padding.bottom;
                
                pos -= (_rectTransform.rect.height - item.sizeDelta.y) / 2f;
            }
            else if (_childAlignment == TextAnchor.LowerLeft || _childAlignment == TextAnchor.LowerCenter ||
                _childAlignment == TextAnchor.LowerRight)
            {
                pos = -(item.sizeDelta.y / 2f);
                
                pos += _padding.bottom;
                
                pos -= _rectTransform.rect.height - item.sizeDelta.y;
            }

            return pos;
        }
        
#if UNITY_EDITOR
        [CustomEditor(typeof(SmoothDynamicHorizontalLayoutGroup))]
        public class SmoothDynamicHorizontalLayoutGroupEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if ((target as MonoBehaviour).enabled)
                {
                    (target as SmoothDynamicHorizontalLayoutGroup).UpdateLayout();
                }
            }
        }
#endif
    }
} 

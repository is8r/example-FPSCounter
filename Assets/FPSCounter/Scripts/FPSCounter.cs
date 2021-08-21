using UnityEngine;
using UnityEngine.UI;

namespace Is8r.FPSCounter
{
    public enum PositionType
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [RequireComponent(typeof(Text))]
    public class FPSCounter : MonoBehaviour
    {
        public PositionType Position = PositionType.TopLeft;

        PositionType lastPosition;
        double current;
        float elapsedTime;

        Text m_Text;
        RectTransform m_RectTransform;
        CanvasScaler m_CanvasScaler;

        void Start()
        {
            m_CanvasScaler = transform.parent.GetComponent<CanvasScaler>();
            m_RectTransform = GetComponent<RectTransform>();
            m_Text = GetComponent<Text>();
        }

        void Update()
        {
            if (lastPosition != Position)
            {
                lastPosition = Position;
                UpdatePos();
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > 0.25f)
            {
                elapsedTime = 0;
                current = (1f / Time.unscaledDeltaTime);
                m_Text.text = $"{current:F2}fps";
            }
        }

        private void UpdatePos()
        {
            m_RectTransform.anchoredPosition = GetPos();
        }

        private Vector2 GetPos()
        {
            Vector2 size = new Vector2(m_RectTransform.sizeDelta.x, m_RectTransform.sizeDelta.y);
            float ratio = (m_CanvasScaler.referenceResolution.x/ Screen.height);
            if (m_CanvasScaler.matchWidthOrHeight == 0)
            {
                ratio = (m_CanvasScaler.referenceResolution.x / Screen.width);
            }

            switch (Position)
            {
                case PositionType.TopLeft:
                    return Vector2.zero;
                case PositionType.TopRight:
                    return new Vector2(Screen.width - size.x, 0) * ratio;
                case PositionType.BottomLeft:
                    return new Vector2(0, (Screen.height - size.y) * -1) * ratio;
                case PositionType.BottomRight:
                    return new Vector2(Screen.width - size.x, (Screen.height - size.y) * -1) * ratio;
                default:
                    return Vector2.zero;
            }
        }
    }
}

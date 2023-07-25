using UnityEngine;

namespace Ore2Lib
{
    public static class RectTransformExtensions
    {
        public static void SetAnchoredPosition(this RectTransform rectTransform, float x, float y) {
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x) {
            rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y) {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public static void SetSize(this RectTransform rectTransform, float width, float height) {
            rectTransform.sizeDelta = new Vector2(width, height);
        }

        public static void SetWidth(this RectTransform rectTransform, float width) {
            rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
        }

        public static void SetHeight(this RectTransform rectTransform, float height) {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        }
    }
}

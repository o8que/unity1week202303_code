using UnityEngine;

namespace Ore2Lib
{
    public static class TransformExtensions
    {
        public static void SetPosition(this Transform transform, float x, float y, float z) {
            transform.position = new Vector3(x, y, z);
        }

        public static void SetPositionX(this Transform transform, float x) {
            var v = transform.position;
            transform.position = new Vector3(x, v.y, v.z);
        }

        public static void SetPositionY(this Transform transform, float y) {
            var v = transform.position;
            transform.position = new Vector3(v.x, y, v.z);
        }

        public static void SetPositionZ(this Transform transform, float z) {
            var v = transform.position;
            transform.position = new Vector3(v.x, v.y, z);
        }

        public static void SetPosition(this Transform transform, float x, float y) {
            transform.position = new Vector3(x, y, transform.position.z);
        }

        public static void SetPositionXZ(this Transform transform, float x, float z) {
            transform.position = new Vector3(x, transform.position.y, z);
        }

        public static void SetPositionYZ(this Transform transform, float y, float z) {
            transform.position = new Vector3(transform.position.x, y, z);
        }

        public static void SetLocalPosition(this Transform transform, float x, float y, float z) {
            transform.localPosition = new Vector3(x, y, z);
        }

        public static void SetLocalPositionX(this Transform transform, float x) {
            var v = transform.localPosition;
            transform.localPosition = new Vector3(x, v.y, v.z);
        }

        public static void SetLocalPositionY(this Transform transform, float y) {
            var v = transform.localPosition;
            transform.localPosition = new Vector3(v.x, y, v.z);
        }

        public static void SetLocalPositionZ(this Transform transform, float z) {
            var v = transform.localPosition;
            transform.localPosition = new Vector3(v.x, v.y, z);
        }

        public static void SetLocalPosition(this Transform transform, float x, float y) {
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }

        public static void SetLocalPositionXZ(this Transform transform, float x, float z) {
            transform.localPosition = new Vector3(x, transform.localPosition.y, z);
        }

        public static void SetLocalPositionYZ(this Transform transform, float y, float z) {
            transform.localPosition = new Vector3(transform.localPosition.x, y, z);
        }

        public static void SetRotation(this Transform transform, float x, float y, float z) {
            transform.eulerAngles = new Vector3(x, y, z);
        }

        public static void SetRotationX(this Transform transform, float x) {
            var v = transform.eulerAngles;
            transform.eulerAngles = new Vector3(x, v.y, v.z);
        }

        public static void SetRotationY(this Transform transform, float y) {
            var v = transform.eulerAngles;
            transform.eulerAngles = new Vector3(v.x, y, v.z);
        }

        public static void SetRotation(this Transform transform, float z) {
            var v = transform.eulerAngles;
            transform.eulerAngles = new Vector3(v.x, v.y, z);
        }

        public static void SetRotationXY(this Transform transform, float x, float y) {
            transform.eulerAngles = new Vector3(x, y, transform.eulerAngles.z);
        }

        public static void SetRotationXZ(this Transform transform, float x, float z) {
            transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, z);
        }

        public static void SetRotationYZ(this Transform transform, float y, float z) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, z);
        }

        public static void SetLocalRotation(this Transform transform, float x, float y, float z) {
            transform.localEulerAngles = new Vector3(x, y, z);
        }

        public static void SetLocalRotationX(this Transform transform, float x) {
            var v = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(x, v.y, v.z);
        }

        public static void SetLocalRotationY(this Transform transform, float y) {
            var v = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(v.x, y, v.z);
        }

        public static void SetLocalRotation(this Transform transform, float z) {
            var v = transform.localEulerAngles;
            transform.localEulerAngles = new Vector3(v.x, v.y, z);
        }

        public static void SetLocalRotationXY(this Transform transform, float x, float y) {
            transform.localEulerAngles = new Vector3(x, y, transform.localEulerAngles.z);
        }

        public static void SetLocalRotationXZ(this Transform transform, float x, float z) {
            transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, z);
        }

        public static void SetLocalRotationYZ(this Transform transform, float y, float z) {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, z);
        }

        public static void SetLocalScale(this Transform transform, float x, float y, float z) {
            transform.localScale = new Vector3(x, y, z);
        }

        public static void SetLocalScaleX(this Transform transform, float x) {
            var v = transform.localScale;
            transform.localScale = new Vector3(x, v.y, v.z);
        }

        public static void SetLocalScaleY(this Transform transform, float y) {
            var v = transform.localScale;
            transform.localScale = new Vector3(v.x, y, v.z);
        }

        public static void SetLocalScaleZ(this Transform transform, float z) {
            var v = transform.localScale;
            transform.localScale = new Vector3(v.x, v.y, z);
        }

        public static void SetLocalScale(this Transform transform, float x, float y) {
            transform.localScale = new Vector3(x, y, transform.localScale.z);
        }

        public static void SetLocalScaleXZ(this Transform transform, float x, float z) {
            transform.localScale = new Vector3(x, transform.localScale.y, z);
        }

        public static void SetLocalScaleYZ(this Transform transform, float y, float z) {
            transform.localScale = new Vector3(transform.localScale.x, y, z);
        }
    }
}

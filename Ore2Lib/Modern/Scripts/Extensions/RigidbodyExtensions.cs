using UnityEngine;

namespace Ore2Lib
{
    public static class RigidbodyExtensions
    {
        public static void AddForceTowards(this Rigidbody rigidbody, Vector3 terminalVelocity) {
            // see: https://qiita.com/yuji_yasuhara/items/1f438f0f27f5ef854a73
            float mass = rigidbody.mass;
            float drag = rigidbody.drag;
            rigidbody.AddForce(terminalVelocity * (mass * drag / (1f - drag * Time.fixedDeltaTime)));
        }
    }
}

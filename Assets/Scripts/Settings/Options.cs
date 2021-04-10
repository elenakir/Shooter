using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    [CreateAssetMenu(fileName = "New Options", menuName = "Options Zenject")]
    public class Options : ScriptableObjectInstaller
    {
        public float PlayerSpeed;
        public float MouseSensibility;
        public float JumpHeight;
        public int BestResult;
        public Vector3 PlayerPosition;
        public Quaternion PlayerRotation;

        public override void InstallBindings()
        {
        }
    }
}

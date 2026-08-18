using UnityEngine;

namespace Spidy.XRDataShowcase
{
    public class EyeData
    {
        public Vector3? leftPosition;
        public Vector3? rightPosition;

        // Vector3? stores Euler Angles (Pitch, Yaw, Roll)
        public Vector3? leftRotation;
        public Vector3? rightRotation;

        public float? leftPupilDiameter;
        public float? rightPupilDiameter;

        public float leftPupilOpenness;
        public float rightPupilOpenness;

        public bool pupilDataValid;

        public void ResetToNull()
        {
            leftPosition = null;
            rightPosition = null;

            leftRotation = null;
            rightRotation = null;

            leftPupilDiameter = null;
            rightPupilDiameter = null;
        }
    }
}
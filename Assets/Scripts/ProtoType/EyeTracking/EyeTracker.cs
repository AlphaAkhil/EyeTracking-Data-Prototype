using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

namespace Spidy.XRDataShowcase.GazeTracking
{
    public class EyeDataProvider : MonoBehaviour
    {
        [Header("Blink Settings")]
        [Tooltip("Openness values below this threshold will mark the eye as closed (0.0 to 1.0).")]
        [Range(0.0f, 0.5f)]
        public float eyeClosedThreshold = 0.3f;

        private EyeData eyeData;

        private void Awake()
        {
            eyeData = new EyeData();
        }

        // Status Flags
        public bool IsLeftEyeClosed => eyeData != null && eyeData.leftPupilOpenness <= eyeClosedThreshold;
        public bool IsRightEyeClosed => eyeData != null && eyeData.rightPupilOpenness <= eyeClosedThreshold;

        public EyeData UpdateEyeData()
        {
            if (eyeData == null) eyeData = new EyeData();

            GetGeometricData();
            GetGazeData();
            GetPupilData();

            return eyeData;
        }

        private bool GetGeometricData()
        {
            if (!XR_HTC_eye_tracker.Interop.GetEyeGeometricData(out XrSingleEyeGeometricDataHTC[] geometrics) ||
                geometrics == null || geometrics.Length < 2)
            {
                eyeData.leftPupilOpenness = 0f;
                eyeData.rightPupilOpenness = 0f;
                eyeData.ResetToNull();
                return false;
            }

            // Left Eye Openness
            var leftGeometric = geometrics[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
            eyeData.leftPupilOpenness = leftGeometric.isValid ? leftGeometric.eyeOpenness : 0f;

            // Right Eye Openness
            var rightGeometric = geometrics[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
            eyeData.rightPupilOpenness = rightGeometric.isValid ? rightGeometric.eyeOpenness : 0f;

            return true;
        }

        private bool GetGazeData()
        {
            if (!XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] gazes) ||
                gazes == null || gazes.Length < 2)
            {
                eyeData.ResetToNull();
                return false;
            }

            // LEFT EYE ROTATION
            var leftGaze = gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
            if (!IsLeftEyeClosed && leftGaze.isValid)
            {
                eyeData.leftPosition = leftGaze.gazePose.position.ToUnityVector();
                Quaternion rotation = leftGaze.gazePose.orientation.ToUnityQuaternion();  
                eyeData.leftRotation = rotation.eulerAngles; // Corrected: store Euler angles
            }
            else
            {
                eyeData.leftPosition = null;
                eyeData.leftRotation = null;
            }

            // RIGHT EYE ROTATION
            var rightGaze = gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
            if (!IsRightEyeClosed && rightGaze.isValid)
            {
                eyeData.rightPosition = rightGaze.gazePose.position.ToUnityVector();
                Quaternion rotation = rightGaze.gazePose.orientation.ToUnityQuaternion();
                eyeData.rightRotation = rotation.eulerAngles; // Corrected: store Euler angles
            }
            else
            {
                eyeData.rightPosition = null;
                eyeData.rightRotation = null; 
            }

            return true;
        }

        private bool GetPupilData()
        {
            if (!XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] pupils) ||
                pupils == null || pupils.Length < 2)
            {
                eyeData.ResetToNull();
                return false;
            }

            // LEFT PUPIL
            var leftPupil = pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
            eyeData.leftPupilDiameter = (!IsLeftEyeClosed && leftPupil.isDiameterValid) ? leftPupil.pupilDiameter : null;

            // RIGHT PUPIL
            var rightPupil = pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
            eyeData.rightPupilDiameter = (!IsRightEyeClosed && rightPupil.isDiameterValid) ? rightPupil.pupilDiameter : null;

            return true;
        }
    }
}
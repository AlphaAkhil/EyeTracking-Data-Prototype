using UnityEngine;

namespace Spidy.XRDataShowcase.GazeTracking
{
    public class GazeFocus : DataLogger
    {
        [Header("References")]
        [SerializeField] private EyeDataProvider eyeTracker;
        [SerializeField] private VisualObjectTracker aoiTracker;

        [Header("Logging")]
        [SerializeField] private float logInterval = 0.1f;

        private float logTimer;

        protected override void Awake()
        {
            base.Awake();

            CreateCSV(
                "Timestamp," +
                "LeftEyeOpenness," +
                "RightEyeOpenness," +
                "LeftPupilDiameter," +
                "RightPupilDiameter," +
                "LeftEyePitch,LeftEyeYaw,LeftEyeRoll," +
                "RightEyePitch,RightEyeYaw,RightEyeRoll," +
                "ObjectName," +
                "ObjectID," +
                "CurrentAOI," +
                "PreviousAOI," +
                "ObjectDistance"
            );
        }

        private void Update()
        {
            if (eyeTracker == null || aoiTracker == null)
                return;

            logTimer += Time.deltaTime;

            if (logTimer < logInterval) return;

            // Update eye metrics and pass them to AOI tracker
            EyeData eyeData = eyeTracker.UpdateEyeData();
            aoiTracker.TrackGaze(eyeData);

            logTimer %= logInterval;
            SaveData(eyeData);
        }

        private void SaveData(EyeData eyeData)
        {
            string timestamp = Time.time.ToString("F3");

            string row =
                timestamp + "," +
                // Eye openness
                eyeData.leftPupilOpenness + "," +
                eyeData.rightPupilOpenness + "," +

                // Pupil diameter
                (eyeData.leftPupilDiameter.HasValue ? eyeData.leftPupilDiameter.Value.ToString("F2") : "null") + "," +
                (eyeData.rightPupilDiameter.HasValue ? eyeData.rightPupilDiameter.Value.ToString("F2") : "null") + "," +
                
                // Eye Euler Angles (Pitch = X, Yaw = Y, Roll = Z)
                GetRotationCSV(eyeData.leftRotation) + "," +
                GetRotationCSV(eyeData.rightRotation) + "," +

                // Object & AOI
                EscapeCSV(aoiTracker.currentAOI) + "," +
                EscapeCSV(aoiTracker.currentObjectID) + "," +
                EscapeCSV(aoiTracker.currentAOI) + "," +
                EscapeCSV(aoiTracker.previousAOI) + "," +

                // Distance
                aoiTracker.objectDistance;

            AddRow(row);
        }

        private string GetRotationCSV(Vector3? eyeRotation)
        {
            if (eyeRotation.HasValue)
            {
                Vector3 rotation = eyeRotation.Value;
                return string.Format("{0:F2},{1:F2},{2:F2}", rotation.x, rotation.y, rotation.z);
            }

            return "null,null,null";
        }  

        private string EscapeCSV(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\"");
                return "\"" + value + "\"";
            }

            return value;
        }
    }
}
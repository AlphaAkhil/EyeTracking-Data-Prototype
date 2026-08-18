using UnityEngine;

namespace Spidy.XRDataShowcase.GazeTracking
{
    public class VisualObjectTracker : MonoBehaviour
    {
        [Header("Gaze Settings")]
        [SerializeField, Range(0.5f, 100f)]
        private float gazeDistance = 10f;

        [SerializeField]
        private LayerMask trackableLayer;

        [Header("Current AOI")]
        public string currentAOI { get; private set; } = "None";
        public string previousAOI { get; private set; } = "None";
        public string currentObjectID { get; private set; } = "None";

        [Header("Distance")]
        public float objectDistance { get; private set; } = -1f;

        [Header("Debug")]
        public float selectedObjectAngle;
        public bool debug = true;

        public string GetcurrentAOI => currentAOI;
        public string GetPreviousAOI => previousAOI;
        public string GetCurrentObjectID => currentObjectID;
        public float GetObjectDistance => objectDistance;

        public float GetSelectedObjectAngle()
        {
            return selectedObjectAngle;
        }

        // public void TrackGaze(EyeData eyeData)
        // {
        //     if (eyeData == null)
        //     {
        //         ClearAOI();
        //         return;
        //     }

        //     // Check validity of nullable positions and rotations
        //     bool hasLeftGaze = eyeData.leftPosition.HasValue && eyeData.leftRotation.HasValue;
        //     bool hasRightGaze = eyeData.rightPosition.HasValue && eyeData.rightRotation.HasValue;

        //     if (!hasLeftGaze && !hasRightGaze)
        //     {
        //         ClearAOI();
        //         return;
        //     }

        //     // Extract position and calculate direction vectors by converting Euler angles to Quaternion
        //     Vector3 leftPos = hasLeftGaze ? eyeData.leftPosition.Value : Vector3.zero;
        //     Vector3 leftDir = hasLeftGaze ? (Quaternion.Euler(eyeData.leftRotation.Value) * Vector3.forward) : Vector3.forward;

        //     Vector3 rightPos = hasRightGaze ? eyeData.rightPosition.Value : Vector3.zero;
        //     Vector3 rightDir = hasRightGaze ? (Quaternion.Euler(eyeData.rightRotation.Value) * Vector3.forward) : Vector3.forward;

        //     bool leftHit = hasLeftGaze && Physics.Raycast(leftPos, leftDir, out RaycastHit leftHitInfo, gazeDistance, trackableLayer);
        //     bool rightHit = hasRightGaze && Physics.Raycast(rightPos, rightDir, out RaycastHit rightHitInfo, gazeDistance, trackableLayer);

        //     if (!leftHit && !rightHit)
        //     {
        //         ClearAOI();
        //         return;
        //     }

        //     if (leftHit && !rightHit)
        //     {
        //         SetAOI(leftHitInfo.collider.gameObject, leftHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        //         return;
        //     }

        //     if (!leftHit && rightHit)
        //     {
        //         SetAOI(rightHitInfo.collider.gameObject, rightHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        //         return;
        //     }

        //     GameObject leftObject = leftHitInfo.collider.gameObject;
        //     GameObject rightObject = rightHitInfo.collider.gameObject;

        //     if (leftObject == rightObject)
        //     {
        //         SetAOI(leftObject, leftHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        //         return;
        //     }

        //     GameObject selectedObject;
        //     RaycastHit selectedHit;

        //     float leftAngle = GetAngleFromCombinedGaze(leftHitInfo.point, leftDir, rightDir, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        //     float rightAngle = GetAngleFromCombinedGaze(rightHitInfo.point, leftDir, rightDir, leftPos, rightPos, hasLeftGaze, hasRightGaze);

        //     if (leftAngle <= rightAngle)
        //     {
        //         selectedObject = leftObject;
        //         selectedHit = leftHitInfo;
        //         selectedObjectAngle = leftAngle;
        //     }
        //     else
        //     {
        //         selectedObject = rightObject;
        //         selectedHit = rightHitInfo;
        //         selectedObjectAngle = rightAngle;
        //     }

        //     SetAOI(selectedObject, selectedHit, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        // }


        public void TrackGaze(EyeData eyeData)
        {
            if (eyeData == null)
            {
                ClearAOI();
                return;
            }

            // Check validity of nullable positions and rotations
            bool hasLeftGaze = eyeData.leftPosition.HasValue && eyeData.leftRotation.HasValue;
            bool hasRightGaze = eyeData.rightPosition.HasValue && eyeData.rightRotation.HasValue;

            if (!hasLeftGaze && !hasRightGaze)
            {
                ClearAOI();
                return;
            }

            // Extract position and calculate direction vectors by converting Euler angles to Quaternion
            Vector3 leftPos = hasLeftGaze ? eyeData.leftPosition.Value : Vector3.zero;
            Vector3 leftDir = hasLeftGaze ? (Quaternion.Euler(eyeData.leftRotation.Value) * Vector3.forward) : Vector3.forward;

            Vector3 rightPos = hasRightGaze ? eyeData.rightPosition.Value : Vector3.zero;
            Vector3 rightDir = hasRightGaze ? (Quaternion.Euler(eyeData.rightRotation.Value) * Vector3.forward) : Vector3.forward;

            // Initialize with default values so C# knows they are assigned
            RaycastHit leftHitInfo = default;
            RaycastHit rightHitInfo = default;

            bool leftHit = hasLeftGaze && Physics.Raycast(leftPos, leftDir, out leftHitInfo, gazeDistance, trackableLayer);
            bool rightHit = hasRightGaze && Physics.Raycast(rightPos, rightDir, out rightHitInfo, gazeDistance, trackableLayer);

            if (!leftHit && !rightHit)
            {
                ClearAOI();
                return;
            }

            if (leftHit && !rightHit)
            {
                SetAOI(leftHitInfo.collider.gameObject, leftHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
                return;
            }

            if (!leftHit && rightHit)
            {
                SetAOI(rightHitInfo.collider.gameObject, rightHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
                return;
            }

            GameObject leftObject = leftHitInfo.collider.gameObject;
            GameObject rightObject = rightHitInfo.collider.gameObject;

            if (leftObject == rightObject)
            {
                SetAOI(leftObject, leftHitInfo, leftPos, rightPos, hasLeftGaze, hasRightGaze);
                return;
            }

            GameObject selectedObject;
            RaycastHit selectedHit;

            float leftAngle = GetAngleFromCombinedGaze(leftHitInfo.point, leftDir, rightDir, leftPos, rightPos, hasLeftGaze, hasRightGaze);
            float rightAngle = GetAngleFromCombinedGaze(rightHitInfo.point, leftDir, rightDir, leftPos, rightPos, hasLeftGaze, hasRightGaze);

            if (leftAngle <= rightAngle)
            {
                selectedObject = leftObject;
                selectedHit = leftHitInfo;
                selectedObjectAngle = leftAngle;
            }
            else
            {
                selectedObject = rightObject;
                selectedHit = rightHitInfo;
                selectedObjectAngle = rightAngle;
            }

            SetAOI(selectedObject, selectedHit, leftPos, rightPos, hasLeftGaze, hasRightGaze);
        }
        private Vector3 GetCombinedGazeDirection(Vector3 leftDir, Vector3 rightDir, bool hasLeft, bool hasRight)
        {
            if (hasLeft && hasRight)
            {
                Vector3 combined = (leftDir.normalized + rightDir.normalized).normalized;
                return combined.sqrMagnitude < 0.0001f ? leftDir : combined;
            }
            return hasLeft ? leftDir : rightDir;
        }

        private float GetAngleFromCombinedGaze(
            Vector3 targetPoint, 
            Vector3 leftDir, 
            Vector3 rightDir, 
            Vector3 leftPos, 
            Vector3 rightPos, 
            bool hasLeft, 
            bool hasRight)
        {
            Vector3 combinedDirection = GetCombinedGazeDirection(leftDir, rightDir, hasLeft, hasRight);

            Vector3 eyeCenter;
            if (hasLeft && hasRight) eyeCenter = (leftPos + rightPos) * 0.5f;
            else if (hasLeft) eyeCenter = leftPos;
            else eyeCenter = rightPos;

            Vector3 directionToTarget = (targetPoint - eyeCenter).normalized;

            return Vector3.Angle(combinedDirection, directionToTarget);
        }

        private void SetAOI(
            GameObject target, 
            RaycastHit hit, 
            Vector3 leftPos, 
            Vector3 rightPos, 
            bool hasLeft, 
            bool hasRight)
        {
            string newAOI = target.name;

            if (currentAOI != newAOI)
            {
                previousAOI = currentAOI;
                currentAOI = newAOI;
            }

            currentObjectID = GetObjectID(target);

            Vector3 eyeCenter;
            if (hasLeft && hasRight) eyeCenter = (leftPos + rightPos) * 0.5f;
            else if (hasLeft) eyeCenter = leftPos;
            else eyeCenter = rightPos;

            objectDistance = Vector3.Distance(eyeCenter, hit.point);
        }

        private void ClearAOI()
        {
            if (currentAOI != "None")
            {
                previousAOI = currentAOI;
                currentAOI = "None";
            }

            currentObjectID = "None";
            objectDistance = 0f;
            selectedObjectAngle = 0f;
        }

        private string GetObjectID(GameObject target)
        {
            AOIObjectID id = target.GetComponentInParent<AOIObjectID>();

            if (id != null)
            {
                return id.ID;
            }

            return target.GetInstanceID().ToString();
        }
    }
}
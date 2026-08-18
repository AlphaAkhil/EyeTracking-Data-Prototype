using UnityEngine;

public class LocalPositionLooper : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3 newLocation;
    [SerializeField] private float duration = 2f;

    [Header("Movement Curve")]
    [SerializeField]
    private AnimationCurve movementCurve = new AnimationCurve(
        new Keyframe(0f, 0f, 0f, 0f),   // Slow start
        new Keyframe(0.5f, 0.5f, 2f, 2f), // Fast middle
        new Keyframe(1f, 1f, 0f, 0f)    // Slow end
    );

    [Header("Loop")]
    [SerializeField] private bool playOnStart = true;

    private Vector3 initialLocalPosition;
    private Vector3 targetLocalPosition;

    private float timer;
    private bool movingForward = true;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        targetLocalPosition = initialLocalPosition + newLocation;

        if (!playOnStart)
            enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / duration);

        // Apply Animation Curve
        float curveValue = movementCurve.Evaluate(t);

        if (movingForward)
        {
            transform.localPosition = Vector3.LerpUnclamped(
                initialLocalPosition,
                targetLocalPosition,
                curveValue
            );
        }
        else
        {
            transform.localPosition = Vector3.LerpUnclamped(
                targetLocalPosition,
                initialLocalPosition,
                curveValue
            );
        }

        // Reached the end of this movement
        if (timer >= duration)
        {
            timer = 0f;
            movingForward = !movingForward;
        }
    }
}
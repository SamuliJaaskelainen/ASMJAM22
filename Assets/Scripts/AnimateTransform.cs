using UnityEngine;

public class AnimateTransform : MonoBehaviour
{
    [SerializeField] bool autoStart = true;

    [SerializeField] bool isLocal = true;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] Vector3 startRotation;
    [SerializeField] Vector3 endRotation;
    [SerializeField] [Range(0.0f, 1.0f)] float value = 0.0f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] bool useCurve = false;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float curveMultiplier = 1.0f;

    bool towardsEnd = true;

    void Start()
    {
        Play();
    }

    void Update()
    {
        value += (towardsEnd ? speed : -speed) * Time.deltaTime;

        if (value >= 1.0f)
        {
            value = 1.0f;
            towardsEnd = false;
        }
        else if (value <= 0.0f)
        {
            value = 0.0f;
            towardsEnd = true;
        }

        UpdatePosition();
    }

    public void Play(bool reversed = false)
    {
        if (reversed)
        {
            towardsEnd = false;
            value = 1.0f;
        }
        else
        {
            towardsEnd = true;
            value = 0.0f;
        }
    }

    void UpdatePosition()
    {
        if (isLocal)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, useCurve ? curve.Evaluate(value) * curveMultiplier : value);
            transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, useCurve ? curve.Evaluate(value) * curveMultiplier : value);
        }
        else
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, useCurve ? curve.Evaluate(value) * curveMultiplier : value);
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, useCurve ? curve.Evaluate(value) * curveMultiplier : value);
        }
    }
}

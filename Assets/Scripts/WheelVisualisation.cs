using UnityEngine;
using UnityEngine.Events;

public class WheelVisualisation : MonoBehaviour
{
	[Range(0, 359f)] public float unlockRotation;
	[Range(0, 359f)] public float contactArea;
	public Transform pointer;
	public Transform unlockRotationIndicator;
	public WheelVisualisation childWheelVisualisation;
	public UnityEvent awakeFinished;

	public Wheel Wheel { get; private set; }
	private Wheel _childWheel;

	public void OnEnable()
	{
		if (childWheelVisualisation != null)
		{
			_childWheel = childWheelVisualisation.Wheel;
		}

		Wheel = new Wheel(unlockRotation / 360f, _childWheel, contactArea / 360f); // _childWheel is allowed to be null
		unlockRotationIndicator.rotation = Quaternion.AngleAxis(Wheel.UnlockRotation * 360, Vector3.back);
		awakeFinished?.Invoke();
	}


	public void Update()
	{
		pointer.transform.rotation = Quaternion.AngleAxis(Wheel.CurrentRotation, Vector3.back);
	}
}
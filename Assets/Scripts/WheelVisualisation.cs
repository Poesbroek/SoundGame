using UnityEngine;

public class WheelVisualisation : MonoBehaviour
{
	[Range(0, 359f)]
	public float unlockRotation;
	[Range(0, 360f)]
	public float contactArea;
	public Transform pointer;
	public WheelVisualisation childWheelVisualisation;

	public Wheel Wheel { get; private set; }
	private Wheel _childWheel;

	public void Awake()
	{
		if (childWheelVisualisation != null)
		{
			_childWheel = childWheelVisualisation.Wheel;
		}

		Wheel = new Wheel(unlockRotation / 360f, _childWheel, contactArea / 360f); // _childWheel is allowed to be null
	}

	public void Update()
	{
		pointer.transform.rotation = Quaternion.AngleAxis(Wheel.CurrentRotation, Vector3.back);
	}
}
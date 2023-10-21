using UnityEngine;

public class SafeDial : MonoBehaviour
{
	public float rotationSensitivity;
	[Range(0, 1)]
	public float shiftSensitivityModifier;

	public Transform pointer;
	public WheelVisualisation innerWheelVisualisation;

	public Safe Safe { get; set; }
	private Wheel _innerWheel;

	private void Start()
	{
		_innerWheel = innerWheelVisualisation.Wheel;
		Safe        = new Safe(_innerWheel);
	}

	private void Update()
	{
		if (Safe.Unlocked)
		{
			Debug.Log("Safe is unlocked!");
		}

		Debug.Log($"Number of wheels aligned: {_innerWheel.NumberOfWheelsAligned}");

		float dialRotation = 0f;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			dialRotation -= rotationSensitivity;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			dialRotation += rotationSensitivity;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			dialRotation *= shiftSensitivityModifier;
		}

		Safe.RotateDial(dialRotation * Time.deltaTime);

		pointer.transform.rotation = Quaternion.AngleAxis(_innerWheel.CurrentRotation, Vector3.back);
	}
}
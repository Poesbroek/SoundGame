using JetBrains.Annotations;

/// <summary>
/// Represents an inner wheel from a safe, which rotates around the spindle. When every wheen is aligned, the safe can be unlocked after rotating clockwise.
/// </summary>
public class Wheel
{
	/// <summary>
	/// The exact rotation at which the wheel is considered aligned.
	/// </summary>
	public float UnlockRotation { get; private set; }

	/// <summary>
	/// The rotation at which the wheel is currently at.
	/// </summary>
	public float CurrentRevolutions { get; private set; } = 0f;
	/// <summary>
	/// The total width of the 'notch' in the wheel.
	/// Whenever the CurrentRotation is within this range, the wheel is considered aligned (unlocked).
	/// </summary>
	public float ContactArea { get; private set; }

	public float CurrentRotation => CurrentRevolutions % 1f * 360f;

	/// <summary>
	/// The wheel that is immediately turned/influenced by this wheel. If this is null, this wheel is the most outer wheel.
	/// </summary>
	[CanBeNull]
	public Wheel ChildWheel { get; private set; }

	// We need to track the wheel's stance
	private float _rotationLeftBound = 0f;
	private float _rotationRightBound = 1f;

	/// <summary>
	/// Creates a representation of an inner wheel from a safe, which rotates around the spindle. When every wheen is aligned, the safe can be unlocked after rotating clockwise.
	/// </summary>
	/// <param name="unlockRotation">The exact rotation at which the wheel is considered 'unlocked'. [0..1f)</param>
	/// <param name="contactArea">The total width of the 'notch' in the wheel, expressed in percentage of the wheel's circumference.
	/// Whenever the CurrentRotation falls within this area with the unlockRotation, the wheel is considered 'unlocked'. (0..1f]</param>
	/// <param name="childWheel">The wheel that is immediately turned/influenced by this wheel. If this is null, this wheel is the most outer wheel</param>
	public Wheel(float unlockRotation, [CanBeNull] Wheel childWheel, float contactArea = 3f/100)
	{
		if (unlockRotation is < 0 or >= 1)
			throw new System.ArgumentOutOfRangeException(nameof(unlockRotation), "Must be in range [0..1f)");

		if (contactArea is <= 0 or > 1)
			throw new System.ArgumentOutOfRangeException(nameof(contactArea), "Must be in range (0..1f]");

		UnlockRotation = unlockRotation;
		ContactArea    = contactArea;
		ChildWheel     = childWheel;
	}

	/// <summary>
	/// Rotates this wheel by the given amount of revolutions.
	/// If the ChildWheel is not null, and this wheel has made a full revolution, the ChildWheel is rotated as well.
	/// </summary>
	/// <param name="revolutions">The amount of revolutions to rotate the wheel.</param>
	/// <returns>The number of wheels that have been turned by turning this wheel. (Minimal of 1: this wheel.)</returns><
	public int Rotate(float revolutions)
	{
		CurrentRevolutions += revolutions;
		float childRotationRight = CurrentRevolutions - _rotationRightBound;
		float childRotationLeft = _rotationLeftBound - CurrentRevolutions;

		if (childRotationRight > 0)
		{
			_rotationRightBound += childRotationRight;
			_rotationLeftBound  += childRotationRight;

			if (ChildWheel != null)
			{
				return 1 + ChildWheel.Rotate(childRotationRight);
			}
		}
		if (childRotationLeft > 0)
		{
			_rotationRightBound -= childRotationLeft;
			_rotationLeftBound  -= childRotationLeft;

			if (ChildWheel != null)
			{
				return 1 + ChildWheel.Rotate(-childRotationLeft);
			}
		}

		return 1;
	}

	/// <summary> Returns true if this wheel is aligned (unlocked). </summary>
	public bool IsAligned => DegreeBetweenRevolutions(CurrentRevolutions, UnlockRotation) <= ContactArea / 2f;

	/// <summary> Returns true if this wheel, and his ChildWheel recursively, is aligned as well. </summary>
	public bool AllAligned => IsAligned && (ChildWheel == null || ChildWheel.AllAligned);

	public int NumberOfWheelsAligned => (ChildWheel != null ? ChildWheel.NumberOfWheelsAligned : 0) + (IsAligned ? 1 : 0);

	public int NumberOfWheels => 1 + (ChildWheel != null ? ChildWheel.NumberOfWheels : 0);

	public static float DegreeBetweenRevolutions(float rotation1, float rotation2)
	{
		var signedDelta = UnityEngine.Mathf.DeltaAngle(rotation1 % 1f * 360f, rotation2 % 1f * 360f);
		return UnityEngine.Mathf.Abs(signedDelta / 360f);
		// TODO this whole method can be a lot faster
	}

	public static float RotationTowards(float currentRotation, float targetRotation, bool clockwise)
		=> clockwise
			? targetRotation >= currentRotation
				? targetRotation - currentRotation
				: 360f - currentRotation + targetRotation
			: targetRotation <= currentRotation
				? targetRotation - currentRotation
				: -currentRotation - (360 - targetRotation);
}
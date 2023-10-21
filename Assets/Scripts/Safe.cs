public class Safe
{
	/// <summary>
	/// The most inner wheel, who turns all other wheels.
	/// </summary>
	private readonly Wheel _innerWheel;

	/// <summary>
	/// Creates a representation of a safe, with 1 or more wheels.
	/// Interactions with the safe only go through the dial, which only affects the most inner wheel.
	/// This inner wheel rotates all other wheels, when rotated enough by the dial.
	/// </summary>
	/// <param name="innerWheel">The most inner wheel, who turns all other wheels.</param>
	public Safe(Wheel innerWheel)
	{
		_innerWheel = innerWheel;
	}

	public bool Unlocked => _innerWheel.AllAligned;

	/// <summary>
	/// Rotates the dial of the safe with the specified amount of degrees.
	/// This directly rotates the inner wheel.
	/// </summary>
	/// <param name="rotation">The rotation in degrees</param>
	/// <returns>The number of wheels turned within this safe. Will be 1 or more.</returns>
	public int RotateDial(float rotation) => _innerWheel.Rotate(rotation / 360f);

	/// <summary>
	/// The rotation of the innerWheel in degrees, which is the same as the imaginary dial of the safe.
	/// </summary>
	public float CurrentDialRotation => _innerWheel.CurrentRotation;
}
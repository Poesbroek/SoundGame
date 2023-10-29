using System;

public class Safe
{
	public event EventHandler UnlockedEvent;
	public event EventHandler<DialRotatedEventArgs> DialRotatedEvent;

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
	/// <param name="startRotations">Here you can specify what the starting rotations of each wheel must be. These rotations will be set by rotating the dial many times.</param>
	public Safe(Wheel innerWheel, params float[] startRotations)
	{
		_innerWheel = innerWheel;
		SetStartingRotations(startRotations);
	}

	private void SetStartingRotations(float[] startRotations)
	{
		if (startRotations.Length > _innerWheel.NumberOfWheels)
		{
			throw new ArgumentOutOfRangeException(nameof(startRotations),
				"More starting rotations were specified than there are wheels");
		}

		float currentRotation = 0f;
		int sign = 1;

		for (int i = startRotations.Length - 1; i >= 0; i--, sign *= -1)
		{
			// First we calculate how to rotate the current wheel
			float setRotation = Wheel.RotationTowards(currentRotation, (startRotations[i] + 360f) % 360f, sign > 0);
			// Then we rotate the dial enough revolutions + the needed rotation, to set the current wheel's rotation
			// We rotate with i revolutions, to get 'grip' on the current wheel
			RotateDial(sign * i * 360 + setRotation);
			// Then we remember the current stance for the next iteration
			currentRotation = startRotations[i];
		}
	}

	/// <summary>
	/// Tests if all wheels are aligned correctly with their UnlockRotation.
	/// </summary>
	public bool Unlocked => _innerWheel.AllAligned;

	/// <summary>
	/// Rotates the dial of the safe with the specified amount of degrees.
	/// This directly rotates the inner wheel.
	/// </summary>
	/// <param name="rotation">The rotation in degrees</param>
	/// <returns>The number of wheels turned within this safe. Will be 1 or more.</returns>
	public int RotateDial(float rotation)
	{
		var wheelsRotated = _innerWheel.Rotate(rotation / 360f);

		// Trigger events
		DialRotatedEvent?.Invoke(this, new DialRotatedEventArgs(wheelsRotated));
		if (Unlocked)
		{
			UnlockedEvent?.Invoke(this, EventArgs.Empty);
		}

		return wheelsRotated;
	}

	/// <summary>
	/// The rotation of the innerWheel in degrees, which is the same as the imaginary dial of the safe.
	/// </summary>
	public float CurrentDialRotation => _innerWheel.CurrentRotation;
}

public class DialRotatedEventArgs : EventArgs
{
	public DialRotatedEventArgs(int wheelsRotated)
	{
		WheelsRotated = wheelsRotated;
	}

	public int WheelsRotated { get; }
}
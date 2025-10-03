using UnityEngine;

// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public abstract class AnimUnitBase : MonoBehaviour, IAnimUnit
{
	public abstract int UnitCode { get; set; }
	public abstract int SequenceCode { get; set; }
	public abstract bool IsPlaying { get; set; }
	public abstract bool IsCompleted { get; set; }

	public abstract float Duration { get; set; }

	public virtual void Init()
	{
		IsCompleted = false;
		HardReset();
	}

	public virtual void HardReset()
	{
		// transform, setActive, material
	}

	public virtual void PlayUnit()
	{
		IsPlaying = true;
		// animation process
		UnitEnded();
	}

	public virtual void HaltUnit()
	{
		UnitEnded();
	}

	public virtual void UnitEnded()
	{
		// !!!
		IsPlaying = false;
		IsCompleted = true;
	}
}
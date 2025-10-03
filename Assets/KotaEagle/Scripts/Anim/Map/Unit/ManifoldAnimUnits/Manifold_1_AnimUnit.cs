// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public class Manifold_1_AnimUnit : AnimUnitBase
{
	public override int UnitCode { get; set; } = 1;
	public override int SequenceCode { get; set; } = 1;
	public override bool IsPlaying { get; set; }
	public override bool IsCompleted { get; set; }
	public override float Duration { get; set; }

	public override void Init() { }

	public override void HardReset()
	{
		base.HardReset();
	}

	public override void PlayUnit()
	{
		base.PlayUnit();
	}

	public override void HaltUnit()
	{
		base.HaltUnit();
	}

	public override void UnitEnded()
	{
		base.UnitEnded();
	}
}
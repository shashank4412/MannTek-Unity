
// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public interface IAnimSequence
{
	void Init(); //List<IAnimUnit>
	void ApplySavedProgress(AnimDramaProgress animDramaProgress);

	void PlaySequence();
	void HaltSequence();
	void SequenceEnded();

	void SwitchToNextUnit();
	void SwitchToPreviousUnit();
}
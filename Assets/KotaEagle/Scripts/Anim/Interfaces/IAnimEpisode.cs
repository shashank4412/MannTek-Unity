
// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public interface IAnimEpisode
{
	void Init(); //List<IAnimSequence> animSequences
	void ApplySavedProgress(AnimDramaProgress animDramaProgress);

	void PlayEpisode();
	void HaltEpisode();
	void EpisodeEnded();

	void SwitchToNextSequence();
	void SwitchToPreviousSequence();
}
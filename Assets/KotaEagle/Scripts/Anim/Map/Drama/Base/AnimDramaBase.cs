using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public abstract class AnimDramaBase : MonoBehaviour, IAnimDrama
{
	public abstract Stack<IAnimEpisode> Playing { get; set; }
	public abstract Stack<IAnimEpisode> History { get; set; }
	public abstract string DramaName { get; set; }
	public abstract int DramaCode { get; set; }

	public virtual void Init()
	{
		var animEpsd = new List<IAnimEpisode>(GameObject.FindObjectsByType<AnimEpisodeBase>(FindObjectsSortMode.None));

		foreach (var obj in FilterFromAllAnimEpisodes(animEpsd))
			Playing.Push(obj);
		History.Clear();
	}
	public virtual List<IAnimEpisode> FilterFromAllAnimEpisodes(List<IAnimEpisode> animEpisodes)
	{
		return animEpisodes.Where(epsd => ((AnimEpisodeBase)epsd).DramaCode == DramaCode).OrderByDescending(epsd => ((AnimEpisodeBase)epsd).EpisodeCode).ToList();
	}
	public virtual void ApplySavedProgress(AnimDramaProgress animDramaProgress)
	{

	}

	public void PlayDrama()
	{
		if (Playing.Count == 0 || Playing.Peek() == null) return;

		var current = Playing.Peek();
		current.PlayEpisode();
	}
	public void HaltDrama()
	{
		// stop playing unit as well
		if (Playing.Count == 0 || Playing.Peek() == null) return;

		var current = Playing.Peek();
		current.HaltEpisode();
	}
	public virtual void DramaEnded()
	{
		// !!!
		throw new System.NotImplementedException();
	}

	public void SwitchToNextEpisode()
	{
		// pop the top from playing as lastPlayed
		// add the lastPlayed into the top of the history.
		if (Playing.Count == 0) return;

		// stop current
		HaltDrama();

		var top = Playing.Pop();
		History.Push(top);

		PlayDrama();
	}
	public virtual void SwitchToPreviousEpisode()
	{
		// pop the top from history as nextPlaying
		// add the nextPlaying into the top of the playing.
		if (History.Count == 0) return;

		// stop current
		HaltDrama();

		var topHistory = History.Pop();
		Playing.Push(topHistory);

		PlayDrama();
	}
}
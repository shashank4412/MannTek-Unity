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
public abstract class AnimSequenceBase : MonoBehaviour, IAnimSequence
{
	public abstract Stack<IAnimUnit> Playing { get; set; }
	public abstract Stack<IAnimUnit> History { get; set; }
	public abstract string SequenceName { get; set; }
	public abstract int SequenceCode { get; set; }
	public abstract int EpisodeCode { get; set; }

	public virtual void Init()
	{
		var animUnits = new List<IAnimUnit>(GameObject.FindObjectsByType<AnimUnitBase>(FindObjectsSortMode.None));

		foreach (var obj in FilterFromAllAnimUnits(animUnits))
			Playing.Push(obj);
		History.Clear();
	}
	public virtual List<IAnimUnit> FilterFromAllAnimUnits(List<IAnimUnit> animUnits)
	{
		return animUnits.Where(unit => ((AnimUnitBase)unit).SequenceCode == SequenceCode).OrderByDescending(unit => ((AnimUnitBase)unit).UnitCode).ToList();
	}
	public virtual void ApplySavedProgress(AnimDramaProgress animDramaProgress)
	{

	}


	public virtual void PlaySequence()
	{
		if (Playing.Count == 0 || Playing.Peek() == null) return;

		var current = Playing.Peek();
		current.PlayUnit();
	}
	public virtual void HaltSequence()
	{
		// stop playing unit as well
		if (Playing.Count == 0 || Playing.Peek() == null) return;

		var current = Playing.Peek();
		current.HaltUnit();
	}
	public virtual void SequenceEnded()
	{
		// !!!
		throw new System.NotImplementedException();
	}


	public virtual void SwitchToNextUnit()
	{
		// pop the top from playing as lastPlayed
		// add the lastPlayed into the top of the history.
		if (Playing.Count == 0) return;

		// stop current
		HaltSequence();

		var top = Playing.Pop();
		History.Push(top);

		PlaySequence();
	}
	public virtual void SwitchToPreviousUnit()
	{
		// pop the top from history as nextPlaying
		// add the nextPlaying into the top of the playing.
		if (History.Count == 0) return;

		// stop current
		HaltSequence();

		var topHistory = History.Pop();
		Playing.Push(topHistory);

		PlaySequence();
	}
}
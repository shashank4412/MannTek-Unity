
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
public abstract class AnimEpisodeBase : MonoBehaviour, IAnimEpisode
{
    public abstract Stack<IAnimSequence> Playing { get; set; }
    public abstract Stack<IAnimSequence> History { get; set; }
    public abstract string EpisodeName { get; set; }
    public abstract int EpisodeCode { get; set; }
    public abstract int DramaCode { get; set; }

    public virtual void Init()
    {
        var animSeq = new List<IAnimSequence>(GameObject.FindObjectsByType<AnimSequenceBase>(FindObjectsSortMode.None));

        foreach (var obj in FilterFromAllAnimSequences(animSeq))
            Playing.Push(obj);
        History.Clear();
    }
    public virtual List<IAnimSequence> FilterFromAllAnimSequences(List<IAnimSequence> animSequences)
    {
        return animSequences.Where(seq => ((AnimSequenceBase)seq).EpisodeCode == EpisodeCode).OrderByDescending(seq => ((AnimSequenceBase)seq).SequenceCode).ToList();
    }
    public virtual void ApplySavedProgress(AnimDramaProgress animDramaProgress)
    {

    }

    public virtual void PlayEpisode()
    {
        if (Playing.Count == 0 || Playing.Peek() == null) return;

        var current = Playing.Peek();
        current.PlaySequence();
    }
    public virtual void HaltEpisode()
    {
        // stop playing unit as well
        if (Playing.Count == 0 || Playing.Peek() == null) return;

        var current = Playing.Peek();
        current.HaltSequence();
    }
    public virtual void EpisodeEnded()
    {
        // !!!
        throw new System.NotImplementedException();
    }

    public virtual void SwitchToNextSequence()
    {
        // pop the top from playing as lastPlayed
        // add the lastPlayed into the top of the history.
        if (Playing.Count == 0) return;

        // stop current
        HaltEpisode();

        var top = Playing.Pop();
        History.Push(top);

        PlayEpisode();
    }
    public virtual void SwitchToPreviousSequence()
    {
        // pop the top from history as nextPlaying
        // add the nextPlaying into the top of the playing.
        if (History.Count == 0) return;

        // stop current
        HaltEpisode();

        var topHistory = History.Pop();
        Playing.Push(topHistory);

        PlayEpisode();
    }
}
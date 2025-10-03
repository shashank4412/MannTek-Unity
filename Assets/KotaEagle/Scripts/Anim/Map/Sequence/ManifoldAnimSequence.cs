using System.Collections.Generic;

// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

[UnityEngine.Scripting.Preserve]
public class ManifoldAnimSequence : AnimSequenceBase
{
  public override Stack<IAnimUnit> Playing { get; set; }
  public override Stack<IAnimUnit> History { get; set; }
  public override string SequenceName { get; set; } = Literal.WIP_KEY;
  public override int SequenceCode { get; set; } = 1;
  public override int EpisodeCode { get; set; } = 1;

  public override void Init()
  {
    base.Init();
  }

  public override void PlaySequence()
  {
    // Play the top of the playing
    base.PlaySequence();
  }
  public override void HaltSequence()
  {
    // Stop the top of the playing
    base.HaltSequence();
  }
  public override void SequenceEnded()
  {
    base.SequenceEnded();
  }

  public override void SwitchToNextUnit()
  {
    // pop the top from playing as lastPlayed
    // add the lastPlayed into the top of the history.
    base.SwitchToNextUnit();
  }
  public override void SwitchToPreviousUnit()
  {
    // pop the top from history as nextPlaying
    // add the nextPlaying into the top of the playing.
    base.SwitchToPreviousUnit();
  }
}
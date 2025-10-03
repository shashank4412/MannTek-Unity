using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

public class AnimController : MonoBehaviour
{
	List<IAnimDrama> animDramas;
	int selectedDramaCode;
	int currentlyPlayingDrama;
	int lastPlayedDrama;

	private void Start()
	{
		Init();
	}
	private void Init()
	{
		var animDramas = new List<IAnimDrama>(GameObject.FindObjectsByType<AnimDramaBase>(FindObjectsSortMode.None));
		this.animDramas = SortAllAnimDrama(animDramas);
	}
	public virtual List<IAnimDrama> SortAllAnimDrama(List<IAnimDrama> animDramas)
	{
		// dramas order in ascending order in the list ( 1,2,3, ... )
		return animDramas.OrderBy(drama => ((AnimDramaBase)drama).DramaCode).ToList();
	}

	// create function to exit/halt the currently playing drama !!!
	// create function to open the selected drama !!!
	// create function to retrieve and apply last saved progress to the opened drama/training, using AnimDramaData !!!

}
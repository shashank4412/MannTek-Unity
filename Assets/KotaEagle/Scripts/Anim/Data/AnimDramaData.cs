using System;

// AnimController is a AnimPlayer. One in a scene. AnimPlayer has multiple sequences
// IAnimDrama is group of episodes. take it as training
// IAnimEpisode is a group of sequences. take episode as an episode.
// IAnimSequence is a group of units. take sequence as a activity(made of multiple acts).
// IAnimUnit is a unit. take it as single action.
// Drama -> Episode -> Sequence -> Unit

public static class AnimDramData
{
	public static void SaveDramaProgress(AnimDramaProgress animDramaProgress, int dramaCode)
	{
		DiskStorage diskStorage = new DiskStorage();
		diskStorage.SaveData(animDramaProgress, dramaCode.ToString());
	}
	public static AnimDramaProgress RetrieveDramaProgress(int dramaCode)
	{
		DiskStorage diskStorage = new DiskStorage();
		return diskStorage.GetSavedData<AnimDramaProgress>(dramaCode.ToString());
	}
}


[Serializable]
public class AnimDramaProgress
{

	// make this class NewtonSoft ready !!!
	public int dramaCode;
	public int episodeCode;
	public int sequenceCode;
	public int unitCode;

	public AnimDramaProgress(int dramaCode, int episodeCode, int sequenceCode, int unitCode)
	{
		this.dramaCode = dramaCode;
		this.episodeCode = episodeCode;
		this.sequenceCode = sequenceCode;
		this.unitCode = unitCode;
	}
}

[Serializable]
public class User
{
	// make this class NewtonSoft ready !!!
	public string userID;
	public string pass;

	public User(string userID, string pass)
	{
		this.userID = userID;
		this.pass = pass;
	}
}
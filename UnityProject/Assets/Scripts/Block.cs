using UnityEngine;
using System.Collections.Generic;
public class Block : MonoBehaviour
{
	public enum Status
	{
		White,
		Red,
		Blue
	}
	readonly Dictionary<Status, Color> mColorDict = new Dictionary<Status, Color>
	{
		{Status.White, Color.white},
		{Status.Red, Color.red},
		{Status.Blue, Color.blue},
	};
	readonly List<Status> mSortPriority = new List<Status>
	{
		Status.White,
		Status.Red,
		Status.Blue,
	};
	public Vector2Int Pos{set; get;}
	public Status CurrentStatus{set; get;}
	public void UpdateStatus()
	{
		NextStatus();
		var ren = GetComponent<Renderer>();
		ren.material.color = mColorDict[CurrentStatus];
	}
	void NextStatus()
	{
		int index = mSortPriority.IndexOf(CurrentStatus);
		if(index == -1)
		{
			return;
		}
		int next = index + 1;
		if(next >= mSortPriority.Count)
		{
			next = 0;
		}
		if(next < 0)
		{
			next = mSortPriority.Count - 1;
		}
		CurrentStatus = mSortPriority[next];
	}
}

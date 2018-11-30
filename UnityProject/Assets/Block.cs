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
	public Vector2Int Pos{set; get;}
	public Status CurrentStatus{set; get;}
	static readonly Dictionary<Status, Color> mColorDict = new Dictionary<Status, Color>
	{
		{Status.White, Color.white},
		{Status.Red, Color.red},
		{Status.Blue, Color.blue},
	};
	public void UpdateStatus()
	{
		SetStatus(Status.Blue);
	}
	public void SetStatus(Status inStatus)
	{
		var ren = GetComponent<Renderer>();
		ren.material.color = mColorDict[inStatus];
	}
}

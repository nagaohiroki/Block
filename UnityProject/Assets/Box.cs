using UnityEngine;
using System.Collections.Generic;
public class Box : MonoBehaviour
{
	[SerializeField]
	Block mBlock;
	Dictionary<Vector2Int, Block> mBoxList;
	Vector2Int mSize = new Vector2Int(8, 8);
	void Start()
	{
		mBoxList = new Dictionary<Vector2Int, Block>();
		for(int y = 0; y < mSize.y; ++y)
		{
			for(int x = 0; x < mSize.x; ++x)
			{
				var box = Instantiate(mBlock);
				box.gameObject.SetActive(true);
				box.Pos = new Vector2Int(x, y);
				box.transform.SetParent(transform);
				var pos = box.transform.position;
				pos.x = x * 2.0f;
				pos.z = y * 2.0f;
				box.transform.position = pos;
				mBoxList.Add(box.Pos, box);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
			{
				ChangeStatus(hit.collider.gameObject.GetComponent<Block>());
			}
		}
	}
	void ChangeStatus(Block inBlock)
	{
		if(inBlock == null)
		{
			return;
		}
		inBlock.UpdateStatus();
		var dirs = new List<Vector2Int>
		{
			Vector2Int.down,
			Vector2Int.up,
			Vector2Int.left,
			Vector2Int.right,
		};
		foreach(var dir in dirs)
		{
			var pos = inBlock.Pos + dir;
			if(!mBoxList.ContainsKey(pos))
			{
				continue;
			}
			mBoxList[pos].UpdateStatus();
		}
	}
}

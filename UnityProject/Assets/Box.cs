using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Box : MonoBehaviour
{
	[SerializeField]
	Block mBlock;
	[SerializeField]
	Text mTelop;
	[SerializeField]
	Vector2Int mSize;
	Dictionary<Vector2Int, Block> mBoxList;
	bool mIsClear;
	readonly List<Vector2Int> mDirs = new List<Vector2Int>
	{
		Vector2Int.zero,
		Vector2Int.down,
		Vector2Int.up,
		Vector2Int.left,
		Vector2Int.right,
	};
	void Reset()
	{
		mIsClear = false;
		if(mTelop != null)
		{
			mTelop.gameObject.SetActive(false);
		}
		mBoxList = new Dictionary<Vector2Int, Block>();
		const float margin = 2.0f;
		for(int y = 0; y < mSize.y; ++y)
		{
			for(int x = 0; x < mSize.x; ++x)
			{
				var box = Instantiate(mBlock);
				box.gameObject.SetActive(true);
				box.Pos = new Vector2Int(x, y);
				box.transform.SetParent(transform);
				var pos = box.transform.position;
				pos.x = x * margin - ((mSize.x - 1) * margin * 0.5f);
				pos.z = y * margin - ((mSize.y - 1) * margin * 0.5f);
				box.transform.position = pos;
				mBoxList.Add(box.Pos, box);
			}
		}
	}
	bool CheckClear()
	{
		foreach(var item in mBoxList)
		{
			if(item.Value == null)
			{
				continue;
			}
			if(item.Value.CurrentStatus != Block.Status.Blue)
			{
				return false;
			}
		}
		return true;
	}
	void ChangeStatus(Block inBlock)
	{
		if(inBlock == null)
		{
			return;
		}
		foreach(var dir in mDirs)
		{
			var pos = inBlock.Pos + dir;
			if(!mBoxList.ContainsKey(pos))
			{
				continue;
			}
			mBoxList[pos].UpdateStatus();
		}
	}
	void Start()
	{
		Reset();
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Reset();
		}
		if(mIsClear)
		{
			return;
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			//AutoPlayer();
		}
		if(Input.GetButtonDown("Fire1"))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
			{
				ChangeStatus(hit.collider.gameObject.GetComponent<Block>());
			}
			mIsClear = CheckClear();
			if(mIsClear)
			{
				if(mTelop != null)
				{
					mTelop.gameObject.SetActive(true);
				}
			}
		}
	}

	void AutoPlayer(Vector2Int inPos)
	{
		ChangeStatus(mBoxList[inPos]);
	}

}

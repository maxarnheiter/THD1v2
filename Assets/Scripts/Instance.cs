using UnityEngine;
using System.Collections;

public class Instance
{
	
	public Prefab prefab;
	public GameObject gameObject;
	public Transform transform;
	public Stack stack;
	public SpriteRenderer spriteRenderer;

	public Instance(GameObject obj) 
	{
		this.prefab = obj.GetComponent<Prefab>();
		this.gameObject = obj;
		this.transform = obj.transform;
		this.stack = obj.GetComponent<Stack>();
		this.spriteRenderer = obj.GetComponent<SpriteRenderer>();
	}

    public int TypeToInt()
    {
        switch (this.prefab.prefabType)
        {
            case PrefabType.Ground:
                return 0;
                break;
            case PrefabType.Corner:
                return 1;
                break;
            default:
                return 2;
                break;
        }
        return 2;
    }
}

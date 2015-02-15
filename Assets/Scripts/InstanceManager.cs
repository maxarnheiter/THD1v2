using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public static class InstanceManager
{
    public static Dictionary<int, Instance> instances;

    static Map _map;
    static Map map
    {
        get
        {
            if (_map == null)
                _map = GameObject.FindObjectOfType<Map>();
            return _map;
        }
        set
        { _map = value; }
    }

    static InstanceManager()
    {
        instances = new Dictionary<int, Instance>();
    }

    public static void Instantiate(Prefab prefab, Vector2 position, int floor)
    {
        var newPrefab = Object.Instantiate(prefab, new Vector3(position.x, position.y, (float)(floor * -1)), Quaternion.identity) as Prefab;

        var newObject = newPrefab.gameObject;

        newObject.transform.parent = map.transform;

        instances.Add(newObject.GetInstanceID(), new Instance(newObject));
    }

    public static void Destroy(Instance instance)
    {
        if (instance == null)
            return;

        instances.Remove(instance.gameObject.GetInstanceID());

        GameObject.DestroyImmediate(instance.gameObject);
    }

    public static Instance GetTopFromPosition(Vector3 position)
    {
        if (instances == null || instances.Count <= 0)
            return null;

        return instances.Where(i => i.Value.transform.position == position).OrderByDescending(i => i.Value.TypeToInt()).ThenByDescending(i => i.Value.stack.id).FirstOrDefault().Value;
    }

    public static List<Instance> GetAllFromPosition(Vector3 position)
    {
        if (instances == null || instances.Count <= 0)
            return null;

        return instances.Where(i => i.Value.transform.position == position).Select(i => i.Value).ToList();
    }

    public static List<Instance> GetAllSortedFromPosition(Vector3 position)
    {
        if (instances == null || instances.Count <= 0)
            return null;

        return instances.Where(i => i.Value.transform.position == position).OrderByDescending(i => i.Value.TypeToInt()).ThenByDescending(i => i.Value.stack.id).Select(i => i.Value).ToList();
    }
}


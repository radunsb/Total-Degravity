using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    int cursor = 0;
    GameObject prototype;
    List<GameObject> pool;
    public bool canGrow;

    public ObjectPool(GameObject prototype, bool resizable, int count)
    {
        this.prototype = prototype;
        this.canGrow = resizable;
        // add count instances of prototype to the pool
        pool = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            // create game object
            GameObject o = Object.Instantiate(prototype);
            // set object to inactive
            o.SetActive(false);
            // add object to pool
            pool.Add(o);
        }
    }

    public GameObject GetObject()
    {
        int start = cursor;
        do
        {
            // if current GameObject is inactive activate and return it
            if (!pool[cursor].activeSelf)
            {
                pool[cursor].SetActive(true);
                return pool[cursor];
            }
            // increment starting back at 0 when reaching end of list
            cursor = (cursor + 1) % pool.Count;
        } while (cursor != start);

        if (canGrow)
        {
            GameObject o = Object.Instantiate(prototype);
            pool.Add(o);
            return o;
        }
        else
        {
            // no objects available and cannot grow
            return null;
        }
    }
}

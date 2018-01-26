using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathManager { 
    static List<PathCreator> paths;

    public static PathCreator GetRandomPath()
    {
        int randIdx = Random.Range(0, paths.Count);
        return paths[randIdx];
    }

    public static void AddPath( PathCreator path )
    {
        if (paths == null)
        {
            paths = new List<PathCreator>();
        }

        paths.Add(path);
    }
}

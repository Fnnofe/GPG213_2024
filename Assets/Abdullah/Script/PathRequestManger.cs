using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManger : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    static PathRequestManger instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();

    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }
    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStar, currentPathRequest.pathEnd);


        }


    }
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();

    }


    struct PathRequest
    {
        public Vector3 pathStar, pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStar = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }



}



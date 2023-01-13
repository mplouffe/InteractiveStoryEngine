using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]
    private List<Actor> m_actors;

    private Dictionary<string, Actor> m_callSheet;
    private Queue<Performance> m_scene;
    private float m_beatDuration = 3;
    private float m_currentBeatDuration;

    private void Awake()
    {
        m_callSheet = new Dictionary<string, Actor>(m_actors.Count);
        foreach(var actor in m_actors)
        {
            m_callSheet.Add(actor.name, actor);
        }
    }
}

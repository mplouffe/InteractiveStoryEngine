using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]
    private List<Actor> m_actors;

    private Dictionary<string, Actor> m_callSheet;
    private Queue<Performance> m_scene;
    private float m_beatDuration = 3;
    private float m_currentBeatDuration;

    // HACKY FOR TESTING
    private bool m_testStarted = false;

    private void Awake()
    {
        // HACKY FOR TESTING
        var startingPositionArray = new Vector3[]
        {
            Vector3.Scale(ActorHelper.OffstageRight, new Vector3(1, -1.7f, 1)),
            -ActorHelper.OffstageRight
        };
        // MAGIC VALUES ABOUND!!! yer a WIZARD 'arry.

        m_callSheet = new Dictionary<string, Actor>(m_actors.Count);
        int i = 0;
        foreach (var actor in m_actors)
        {
            m_callSheet.Add(actor.name, Instantiate(actor, startingPositionArray[i], Quaternion.identity));
            i++;
        }
    }

    private void Update()
    {
        if (!m_testStarted && Time.time > 1)
        {
            m_callSheet["Russel"].HitMark(new Vector3(4, 1, 0), 0.5f);
            m_callSheet["Zack"].HitMark(new Vector3(-4, 1, 0), 0.4f);
            m_testStarted = true;
        }
    }
}

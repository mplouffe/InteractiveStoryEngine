using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Actor : MonoBehaviour
{
    private enum ActorState
    {
        Idle,
        Unfocused,
        Focused
    }

    [SerializeField] private string m_actorName;

    [SerializeField] private SpriteRenderer m_actorSpriteRenderer;

    [SerializeField] private List<Repertoire> m_actorRange;

    private Dictionary<Emotion, Sprite> m_actorRangeDictionary;

    private ActorState m_actorState;
    private Emotion m_currentEmotion;

    private bool m_markHit = true;
    private Vector3 m_mark;
    private float m_moveDuration;
    private float m_movementStart;

    private void Awake()
    {
        m_actorRangeDictionary = new Dictionary<Emotion, Sprite>(m_actorRange.Count);
        foreach(var performance in m_actorRange)
        {
            m_actorRangeDictionary.Add(performance.Emotion, performance.Sprite);
        }
    }

    private void Update()
    {
        if (!m_markHit)
        {
            float moveDuration = Time.time - m_movementStart;
            if (moveDuration >= m_moveDuration)
            {
                transform.position = m_mark;
                m_markHit = true;
            }
            else
            {
                var t = moveDuration / m_moveDuration;
                Vector3 currentPosition = Vector3.Lerp(transform.position, m_mark, t);
                transform.position = currentPosition;
            }
        }
    }

    public void FocusActor()
    {
        if (m_actorState != ActorState.Focused)
        {
            m_actorSpriteRenderer.color = ActorHelper.FocusedColor;
            m_actorSpriteRenderer.transform.localScale = ActorHelper.FocusedScale;
            m_actorState = ActorState.Focused;
        }
    }

    public void UnfocusActor()
    {
        if (m_actorState != ActorState.Unfocused)
        {
            m_actorSpriteRenderer.color = ActorHelper.NotFocusedColor;
            m_actorSpriteRenderer.transform.localScale = ActorHelper.NotFocusedScale;
            m_actorState = ActorState.Unfocused;
        }
    }

    public void IdleActor()
    {
        if (m_actorState != ActorState.Idle)
        {
            m_actorSpriteRenderer.color = ActorHelper.FocusedColor;
            m_actorSpriteRenderer.transform.localScale = ActorHelper.NotFocusedScale;
            m_actorState = ActorState.Idle;
        }
    }

    public void Perform(Performance action)
    {
        FocusActor();
        
        DialogueWindow.SayDialogue(m_actorName, action.Lines);

        if (m_currentEmotion != action.Emotion)
        {
            if (m_actorRangeDictionary.TryGetValue(action.Emotion, out Sprite performance))
            {
                m_actorSpriteRenderer.sprite = performance;
            }
        }
    }

    public  void HitMark(Vector3 mark, float moveDuration)
    {
        m_mark = mark;
        m_moveDuration = moveDuration;
        m_movementStart = Time.time;
        m_markHit = false;
    }
}

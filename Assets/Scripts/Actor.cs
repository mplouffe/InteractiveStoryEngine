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

    [SerializeField] private float m_height;

    private Dictionary<Emotion, Sprite> m_actorRangeDictionary;

    private ActorState m_actorState;
    private Emotion m_currentEmotion;

    private bool m_markHit = true;
    private Vector3 m_mark;
    private Vector3 m_startingPosition;
    private float m_moveTotalDuration;
    private float m_movementDuration;

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
            m_movementDuration += Time.deltaTime;
            if (m_movementDuration >= m_moveTotalDuration)
            {
                transform.position = m_mark;
                m_markHit = true;
            }
            else
            {
                var t = m_movementDuration / m_moveTotalDuration;
                Vector3 currentPosition = Vector3.Lerp(m_startingPosition, m_mark, t);
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
        m_startingPosition = transform.position;
        m_mark = new Vector3(mark.x, m_height, mark.z);
        m_moveTotalDuration = moveDuration;
        m_movementDuration = 0;
        m_markHit = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Actor : MonoBehaviour
{
    [SerializeField] private string m_actorName;

    [SerializeField] private SpriteRenderer m_actorSpriteRenderer;

    [SerializeField] private List<Repertoire> m_actorRange;

    private Dictionary<Emotion, Sprite> m_actorRangeDictionary;

    private bool m_actorFocused;
    private Emotion m_currentEmotion;

    private void Awake()
    {
        m_actorRangeDictionary = new Dictionary<Emotion, Sprite>(m_actorRange.Count);
        foreach(var performance in m_actorRange)
        {
            m_actorRangeDictionary.Add(performance.Emotion, performance.Sprite);
        }
    }

    public void FocusActor()
    {
        if (!m_actorFocused)
        {
            m_actorSpriteRenderer.color = ActorHelper.FocusedColor;
            m_actorSpriteRenderer.transform.localScale = ActorHelper.FocusedScale;
            m_actorFocused = true;
        }
    }

    public void UnfocuseActor()
    {
        if (m_actorFocused)
        {
            m_actorSpriteRenderer.color = ActorHelper.NotFocusedColor;
            m_actorSpriteRenderer.transform.localScale = ActorHelper.NotFocusedScale;
            m_actorFocused = false;
        }
    }

    public void Perform(Performance action)
    {
        DialogueWindow.SayDialogue(m_actorName, action.Lines);

        if (m_currentEmotion != action.Emotion)
        {
            if (m_actorRangeDictionary.TryGetValue(action.Emotion, out Sprite performance))
            {
                m_actorSpriteRenderer.sprite = performance;
            }
        }
        FocusActor();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_dialogueText;

    [SerializeField]
    private RectTransform m_dialogueTransform;

    [SerializeField]
    private TextMeshProUGUI m_nameText;

    [SerializeField]
    private RectTransform m_nameRectTransform;

    public static DialogueWindow Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple Instances of DialogueWindow found. Destroying...");
            Destroy(Instance);
        }
        Instance = this;
    }

    private void ShowDialogue(string actor, string line)
    {
        if (!string.Equals(actor, m_nameText.text))
        {
            m_nameText.text = actor;
        }

        m_dialogueText.text = line;
    }

    public static void SayDialogue(string actor, string line)
    {
        if (Instance == null)
        {
            Debug.LogError("Trying to use dialogue on a null instance.");
            return;
        }

        Instance.ShowDialogue(actor, line);
    }
}

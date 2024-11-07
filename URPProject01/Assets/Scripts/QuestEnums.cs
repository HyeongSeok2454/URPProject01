using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    public enum QuestType
    {
        Collection,
        Kill,
        Dialog,
        Exploration,
        Escort
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public object Skill { get; private set; }
    public List<string> RequiredSkillds { get; private set; }
    public bool isUnlocked { get; set; }
    public Vector2 Position { get; set; }
    public string SkillSeries {  get; private set; }
    public int SkillLevel { get; private set; }
    public bool IsMaxLevel { get; set; }

    public SkillNode(string id, string name, object skill , Vector2 position , string skillSeries, int skillLevel, List<string> requiredSkillIds = null)
    {
        Id = id;
        Name = name;
        Skill = skill;
        Position = position;
        SkillSeries = skillSeries;
        RequiredSkillds = requiredSkillIds ?? new List<string>();
        isUnlocked = false;
    }

    public class SkillTree
    {
        public List<SkillNode> Nodes { get; private set; } = new List<SkillNode>();
        private Dictionary<string, SkillNode> nodeDictionary;

        public SkillTree()
        {
            Nodes = new List<SkillNode>();
            nodeDictionary = new Dictionary<string, SkillNode>();
        }

        public void AddNode(SkillNode node)
        {
            Nodes.Add(node);
            nodeDictionary[node.Id] = node;
        }

        public bool UnlockSkill(string skillId)
        {
            if (nodeDictionary.TryGetValue(skillId, out SkillNode node))
            {
                if (node.isUnlocked) return false;

                foreach (var requriedSkillId in node.RequiredSkillds)
                {
                    if (!nodeDictionary[requiredSkillId].isUnlocked)
                    {
                        return false;
                    }
                }
                node.isUnlocked = true;
                return true;
            }
        }
    }

    public bool LockSkill(string skillId)
    {
        if (nodeDitionary.TryGetValue(skillId, out SkillNode node))
        {
            if(!node.isUnlocked) return false;

            foreach(var otherNode in Nodes)
            {
                if (otherNode.isUnlocked && otherNode.RequiredSkillds.Contains(SkillId))
                {
                    return false;
                }
            }

            node.isUnlocked = false;
            return true;
        }

        return false;
    }
}
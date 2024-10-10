using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    public SkillTree skillTree;
    public GameObject skillnodePrefabs;
    public RectTransform skillTreePanel;
    public float NodeSpacing = 100f;
    public Text SkillPointText;
    public int totalSkillPoint = 10;

    private Dictionary<string, Button> skillButtons = new Dictionary<string, Button>();

    void Start()
    {
        InitalizeSkillTree();
        CreateSkillNodeUI();
        UpdateSkillPointsUI();
    }

    void InitalizeSkillTree()
    {
        skillTree = new SkillTree();

        skillTree.AddNode(new SkillNode("Fireball1", "Fireball1",
            new SkillTreeUI<ISkillTarget, DamageEffect>("Fireball1", new DamageEffect(20)),
            new Vector2(0, 0), "Fireball", 1));

        skillTree.AddNode(new SkillNode("Fireball2", "Fireball2",
            new SkillTreeUI<ISkillTarget, DamageEffect>("Fireball2", new DamageEffect(30)),
            new Vector2(0, 1), "Fireball", 2, new List<string> { "Fireball1" }));

        skillTree.AddNode(new SkillNode("Fireball3", "Fireball3",
            new SkillTreeUI<ISkillTarget, DamageEffect>("Fireball3", new DamageEffect(40)),
            new Vector2(0, 2), "Fireball", 3, new List<string> { "Fireball2" }));

        skillTree.AddNode(new SkillNode("Fireball4", "Fireball4",
            new SkillTreeUI<ISkillTarget, DamageEffect>("Fireball4", new DamageEffect(50)),
            new Vector2(0, 3), "Fireball", 4, new List<string> { "Fireball3" }));

        skillTree.AddNode(new SkillNode("FireBolt1", "FireBolt1",
            new SkillTreeUI<ISkillTarget, DamageEffect>("FireBolt1", new DamageEffect(90)),
            new Vector2(1, 2), "FireBolt", 1, new List<string> { "Fireball2" }));

        skillTree.AddNode(new SkillNode("FireBolt2", "FireBolt2",
            new SkillTreeUI<ISkillTarget, DamageEffect>("FireBolt2", new DamageEffect(140)),
            new Vector2(2, 2), "FireBolt", 2, new List<string> { "FireBolt1" }));

        skillTree.AddNode(new SkillNode("FireBolt3", "FireBolt3",
            new SkillTreeUI<ISkillTarget, DamageEffect>("FireBolt3", new DamageEffect(240)),
            new Vector2(3, 2), "FireBolt", 3, new List<string> { "FireBolt2" }));
    }

    void CreaeSkillTreeUI()
    {
        foreach (var node in skillTree.Nodes)
        {
            CreareSkillNodeUI(node);
        }
    }

    void CreateSkillNodeUI(SkillNode node)
    {
        GameObject nodeObj = Instantiate(skillnodePrefabs, skillTreePanel);
        RectTransform rectTransform = nodeObj.GetComponent<rectTransform>();
        rectTransform.anchoredPosition = node.Position * NodeSpacing;

        skillButtons button = nodeObj.GetComponent<button> ();
        Text text = nodeObj.GetComponentInChildren<text> ();
        text.text = node.Name;

        button.onClick.AddListener(() => OnSkillNodeClicked(node));
        skillButtons[node.Name] = button;
        UpdateNodeUI(node);
    }

    private void OnSkillNodeClicked(string skillId)
    {
        SkillNode node = skillTree.GetNode(skillId);

        if (node == null) return;

        if (node.isUnlocked)
        {
            if (skillTree.LockSkill(skillId))
            {
                totalSkillPoint++;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
            else
            {
                Debug.Log("관련 연계 스킬이 있어서 해제가 안됩니다.");
            }
        }
        else if (totalSkillPoint > 0 && CanUnlockSkill(node))
        {
            if (skillTree.UnlockSkill(skillId))
            {
                totalSkillPoint--;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
        }
    }

    private void UpdateNodeUI(SkillNode node)
    {
        if (skillButtons. TryGetValue(node.Id, out Button button))
        {
            bool canUnlock = !node.isUnlocked && CanUnlockSkill(node);
            button.interactable = (canUnlock && totalSkillPoint > 0) || node.isUnlocked;
            button.GetComponent<Image>().color = node.isUnlocked ? Color.green : (canUnlock ? Color.yellow : Color.red);
        }
    }

    private bool CanUnlockSkill(SkillNode node)
    {
        foreach (var requiedSkillId in node.RequiredSkillIds)
        {
            if(!string.IsSkillUnlock(requiredSkillId))
            {
                return false;
            }
        }

        return true;
    }

    void UpdateSkillPointsUI()
    {
        SkillPointText.text = $"Skill Points: {totalSkillPoint)";
    }

    void UpdateConnectedSkills(string skillId)
    {
        foreach(var node in skillTree.Nodes)
        {
            if(node.RequiredSkillds.Contains(skillId))
            {
                UpdateNodeUI(node);
            }
        }
    }

}

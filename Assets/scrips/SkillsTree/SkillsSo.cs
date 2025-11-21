using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName= "SkillTree/Skill")]
public class SkillsSo : ScriptableObject
{
    public string SkillName;
    public int maxLevel;
    public Sprite skillIcon;

}

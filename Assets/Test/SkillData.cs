using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public float cooldown;
    public GameObject skillPrefab;
    public AnimationClip animationClip;
    public AudioClip soundEffect;
}

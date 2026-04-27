using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesDataBase", menuName = "Scriptable Objects/UpgradesDataBase")]
public class UpgradesDBSo : ScriptableObject
{
    public List<UpgradeSO> upgradeSOs;
    public List<CharacterSO> characterSOs;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Colors", menuName = "Colors")]
public class EnemyColors : ScriptableObject
{
    public List<Material> Materials;
}

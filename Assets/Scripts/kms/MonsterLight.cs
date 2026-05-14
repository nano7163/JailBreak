using UnityEngine;

public class MonsterLight : MonoBehaviour
{
    [SerializeField] private MoveMonster moveMonster;
    void OnTriggerEnter2D(Collider2D collision)
    {
        moveMonster.TurnMonster();
    }
}

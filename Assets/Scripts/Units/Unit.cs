using UnityEngine;

public class Unit : MonoBehaviour
{
    [System.Serializable]
    public class UnitData
    {
        public string unitId;
        public string unitName;
        public UnitType unitType;
        public int level = 1;
        public int health = 100;
        public int maxHealth = 100;
        public int attack = 10;
        public int defense = 5;
        public float speed = 5f;
        public Vector3 position;
        public Vector3 targetPosition;
        public bool isMoving = false;
    }

    public UnitData unitData;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDirection;

    public enum UnitType
    {
        Warrior,
        Archer,
        Mage,
        Knight,
        Scout,
        Healer
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitData.position = transform.position;
    }

    private void Update()
    {
        if (unitData.isMoving)
        {
            MoveToTarget();
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        unitData.targetPosition = target;
        unitData.isMoving = true;
    }

    private void MoveToTarget()
    {
        Vector3 direction = (unitData.targetPosition - transform.position).normalized;
        transform.position += direction * unitData.speed * Time.deltaTime;
        unitData.position = transform.position;

        if (Vector3.Distance(transform.position, unitData.targetPosition) < 0.1f)
        {
            unitData.isMoving = false;
            transform.position = unitData.targetPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(1, damage - unitData.defense);
        unitData.health -= actualDamage;

        if (unitData.health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(unitData.unitName + " died!");
        Destroy(gameObject);
    }

    public int GetAttackDamage()
    {
        return unitData.attack + Random.Range(-2, 3); // Some variance
    }
}

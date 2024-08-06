using UnityEngine;
public abstract class Entity : GMono
{
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    [SerializeField] private EntityStats stats;

    public EntityStats Stats => stats;

    [SerializeField] private EntityMoving moving;

    public EntityMoving Moving => moving;

    [SerializeField] private CapsuleCollider capCollider;

    public CapsuleCollider CapCollider => capCollider;

    [SerializeField] private Transform attackPoint;

    public Transform AttackPoint => attackPoint;

    [SerializeField] private EntityAnim anim;

    public EntityAnim Anim => anim;

    [SerializeField] private EntityAttack attack;

    public EntityAttack Atack => attack;

    [SerializeField] private EntitySwordrain eSwordrain;

    public EntitySwordrain ESwordrain => eSwordrain;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEntityStats();
        LoadEntityMoving();
        LoadCollider();
        LoadAttackPoint();
        LoadAnimator();
        LoadModel();
        LoadAnimation();
        LoadAttack();
        LoadSwordrain();
    }

    private void LoadEntityStats()
    {
        if(stats != null) return;

        stats = GetComponentInChildren<EntityStats>();
    }

    private void LoadEntityMoving()
    {
        if(moving != null) return;

        moving = GetComponentInChildren<EntityMoving>();
    }

    private void LoadCollider()
    {
        if(capCollider != null) return;

        capCollider = transform.Find("Collider").GetComponent<CapsuleCollider>();
    }

    private void LoadAttackPoint()
    {
        if(attackPoint != null) return;

        attackPoint = transform.Find("AttackPoint");
    }

    private void LoadAnimator()
    {
        if(animator != null) return;

        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Model");
    }

    private void LoadAnimation()
    {
        if(anim != null) return;

        anim = transform.GetComponentInChildren<EntityAnim>();
    }

    private void LoadAttack()
    {
        if(attack != null) return;

        attack = GetComponentInChildren<EntityAttack>();
    }

    private void LoadSwordrain()
    {
        if(eSwordrain != null) return;

        eSwordrain = GetComponentInChildren<EntitySwordrain>();
    }
}
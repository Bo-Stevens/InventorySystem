using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JumpState { Grounded, Rising, Hanging, Falling };
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementComponent : MonoBehaviour
{
    public event CharacterLanded OnCharacterLanded;

    [HideInInspector] public bool Interrupted;
    [HideInInspector] public Vector2 MovementDirection;
    [HideInInspector] public JumpState jumpState = JumpState.Falling;

    [Range(0, 50)] public float MovementSpeed;

    [Range(1, 100)] [SerializeField] float characterWeight;
    [Range(1, 10)] [SerializeField] float fallGravityMultiplier;
    [Range(0, 5 )] [SerializeField] float hangTime;
    [Range(0, 1 )] [SerializeField] float hangGravityMultiplier;
    [Range(0, 40)] [SerializeField] float jumpForce;
    [Range(0, 1 )] [SerializeField] float momentumResistance;
    [Range(0, 3 )] [SerializeField] float coyoteTime; 
    [Range(0, 10)] [SerializeField] int timesCharacterCanJumpConsecutively;


    SpriteRenderer spriteRenderer;
    Collider2D characterCollider;
    ContactFilter2D contactFilter;
    Vector3 velocity;

    protected float gravitationForce;
    float timeSpentHanging;
    float halfHeight;
    float clampedDeltaTime;
    int jumpCount;

    public event OnCharacterMoved directionChanged;

    protected virtual void UpdateCharacter() { }
    public delegate void CharacterLanded();

    public void Init()
    {

        gravitationForce = ActorPhysicsMovementManager.Instance.Gravity;
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfHeight = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;
        characterCollider = GetComponent<Collider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().simulated = true;
        contactFilter.layerMask = LayerMask.NameToLayer("Projectile");

        directionChanged += DirectionChanged;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        if(jumpState == JumpState.Grounded) velocity = newVelocity;
        else
        {
            velocity = new Vector3(Mathf.Lerp(velocity.x, newVelocity.x, (momentumResistance - 1) * -1), newVelocity.y);
        }

        if(newVelocity.x != 0)
        {
            if (newVelocity.x > 0) MovementDirection = new Vector2(1, 0);
            else MovementDirection = new Vector2(-1, 0);
        }

        if(directionChanged != null) directionChanged(MovementDirection);
    }
    
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private void Update()
    {
        MoveCharacter();
        ResolveCollisions();
    }

    void MoveCharacter()
    {
        if (Time.deltaTime > .1f) clampedDeltaTime = .1f;
        else clampedDeltaTime = Time.deltaTime;
        if (jumpState != JumpState.Grounded)
        {
            EvaluateJumpState();
            velocity = new Vector3(velocity.x, velocity.y - (gravitationForce * characterWeight * clampedDeltaTime));
        }

        Vector3 movementAmount = Util.SnapToNearestPixel(velocity, 32f);
        transform.position += movementAmount * clampedDeltaTime;
    }

    protected void Jump()
    {
        if (jumpState != JumpState.Grounded && jumpCount >= timesCharacterCanJumpConsecutively) return;

        velocity = new Vector3(velocity.x, jumpForce);
        gravitationForce = ActorPhysicsMovementManager.Instance.Gravity;
        jumpState = JumpState.Rising;
        jumpCount += 1;
    }

    void Land()
    {
        if(jumpState != JumpState.Grounded) OnCharacterLanded.Invoke();
        velocity.y = -ActorPhysicsMovementManager.Instance.Gravity / 2f;
        jumpCount = 0;
        jumpState = JumpState.Grounded;
        gravitationForce = ActorPhysicsMovementManager.Instance.Gravity;
    }

    void Fall()
    {
        if (jumpState == JumpState.Grounded) jumpCount += 1;
        jumpState = JumpState.Falling;
        gravitationForce = ActorPhysicsMovementManager.Instance.Gravity * fallGravityMultiplier;
    }

    void EvaluateJumpState()
    {
        if(jumpState == JumpState.Rising && velocity.y < .5f)
        {
            jumpState = JumpState.Hanging;
            gravitationForce = ActorPhysicsMovementManager.Instance.Gravity * hangGravityMultiplier;
        }
        else if(jumpState == JumpState.Hanging)
        {
            timeSpentHanging += Time.deltaTime;
            if(timeSpentHanging >= hangTime)
            {
                Fall();
                timeSpentHanging = 0;
            }
        }
    }

    Collider2D[] ResolveCollisions()
    {
        Collider2D[] collisions = new Collider2D[5];
        Physics2D.OverlapCollider(characterCollider, contactFilter, collisions);

        bool landedOnGround = false;
        int numCollisions = 0;
        for(int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i] == null) continue;
            if (collisions[i] == characterCollider) continue;
            if (collisions[i].isTrigger) continue;
            ColliderDistance2D distance = collisions[i].Distance(characterCollider);
            if (distance.isOverlapped)
            {
                numCollisions += 1;
                Vector2 dist = distance.pointA - distance.pointB;
                transform.position = new Vector3(Mathf.Round((transform.position.x + dist.x) * 1000f) / 1000f, Mathf.Round((transform.position.y + dist.y) * 1000f) / 1000f);
                
                if(Vector2.Angle(distance.normal, Vector2.up) < 90 && velocity.y <= 0 )
                {
                    Land();
                    landedOnGround = true;
                }
            }
            if (numCollisions > 1) Debug.Log("Too many collisions still");
        }

        if (!landedOnGround && jumpState == JumpState.Grounded) Fall();
        return collisions;
    }

    void DirectionChanged(Vector2 newDirection)
    {
        if (newDirection.x > 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (newDirection.x < 0) GetComponent<SpriteRenderer>().flipX = true;
    }

    public delegate void OnCharacterMoved(Vector2 direction);
}

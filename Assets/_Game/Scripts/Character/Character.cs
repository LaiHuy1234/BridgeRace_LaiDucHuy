using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private PlayerBrick playerBrickPrefab;
    [SerializeField] private Transform brickHolder;
    [SerializeField] protected Transform skin;
    [SerializeField] private Animator anim;

    private string currentAnim;
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    [HideInInspector] public Stage stage;

    public int BrickCount => playerBricks.Count;

    public virtual void OnInit()
    {
        ClearBrick();
        skin.rotation = Quaternion.identity;
    }

    //private IState<Character> currentState;

    //private void Start()
    //{
    //    ChangeState(new IdleState());
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (currentState != null)
    //    {
    //        currentState.OnExecute(this);
    //    }
    //}

    //public void ChangeState(IState<Character> state)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.OnExit(this);
    //    }

    //    currentState = state;

    //    if (currentState != null)
    //    {
    //        currentState.OnEnter(this);
    //    }
    //}

    public bool CanMove(Vector3 nextPoint)
    {
        //Check mau bac thang
        //k cung mau -> xay cau
        //het gach + ko cung mau + huong di len

        bool isCanmove = true;
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 1.5f, stairLayer))
        {
            Stair stair = hit.collider.GetComponent<Stair>();

            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }

            if (stair.colorType != colorType && playerBricks.Count == 0 && skin.forward.z > 0)
            {
                isCanmove = false;
            }
        }

        return isCanmove;
    }

    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = Vector3.up * 0.25f * playerBricks.Count;
        playerBricks.Add(playerBrick);
    }

    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }

    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }

        playerBricks.Clear();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    //check diem tiep theo co phai ground khong(1: tra ve vi tri tiep theo; 0: tra ve vi tri hien tai)
    public bool CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        return Physics.Raycast(nextPoint, Vector3.down, out hit, 1.5f, groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();

            if (brick.colorType == colorType)
            {
                brick.OnDespawn();
                AddBrick();
                Destroy(brick.gameObject);
            }
        }
    }
}
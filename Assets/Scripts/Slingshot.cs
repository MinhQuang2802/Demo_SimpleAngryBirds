using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slingshot : MonoBehaviour
{

    [SerializeField] LineRenderer[] lineRenderers;
    [SerializeField] Transform[] stripPositions;
    [SerializeField] Transform center;
    [SerializeField] Transform idlePosition;

    [SerializeField] Vector3 currentPosition;

    [SerializeField] float maxLength;

    [SerializeField] float bottomBoundary;

    bool isMouseDown;

    [SerializeField] GameObject birdPrefab;

    [SerializeField] float birdPositionOffset;

    Rigidbody2D bird;
    Collider2D birdCollider;

    //linerender
    private Vector3 startPos;
    private Vector3 endPos;

    [SerializeField] GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    private Vector3 forceAtPlayer;
    [SerializeField] float forceFactor;
    [SerializeField] int number;
   
    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        trajectoryDots = new GameObject[number];
        CreateBird();
    }

    void CreateBird()
    {
       
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        bird.isKinematic = true;

        ResetStrips();
    }

    void Update()
    {
        if (isMouseDown) // drag
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition
                - center.position, maxLength);
            endPos = currentPosition;
            
            currentPosition = ClampBoundary(currentPosition);
            forceAtPlayer = endPos - startPos;
            SetStrips(currentPosition);
            for (int i = 0; i < number; i++)
            {
                if(trajectoryDots[i] != null)
                {
                    trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
                }
                
            }
            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
            ResetStrips();
        }
    }

    private void OnMouseDown()//click
    {
        isMouseDown = true;
        startPos = bird.position;
        for (int i = 0; i < number; i++)
        {
            trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
        }
    }

    private void OnMouseUp()//leave
    {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
  
        bird.gravityScale = 1;
        bird.velocity = new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor);
     
        bird.isKinematic = false;
        bird = null;
        birdCollider = null;
 
        Level_Manager.i++;
        if (Level_Manager.i == 4)
        {
            return;
        }
        Invoke("CreateBird",0.5f);
      
    }


  
    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(endPos.x, endPos.y) + //X0
                new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime + //ut
                0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }
}

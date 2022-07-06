using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    // jumping settings
    [SerializeField]
    private float jumpPower;

    [SerializeField]
    [Range(0f, 10f)]
    private float jumpDuration;

    [SerializeField]
    [Range(0f, 10f)]
    private float z_value;

    // touch variables
    Vector3 touchPosition; 

    // jumping variables
    bool isJumping = false;
    Sequence jumping;
    bool isStarted = false;
    float time = 0f;
    bool isDead = false;


    private void Update()
    {

        // Handling player input
        if (Input.touchCount > 0)
        {
            isStarted = true;
            touchPosition = Input.GetTouch(0).position;
            touchPosition.z = z_value;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            transform.DOMoveX(touchPosition.x - transform.position.x, Time.deltaTime).SetRelative(true).From();
        }

        HandleJumping();

        if(isStarted)
            CheckDeath();
    }


    void HandleJumping()
    {
        if (isJumping || !isStarted || isDead)
            return;

        // update jumping status
        isJumping = true;

        // performe jumping
        jumping = transform.DOJump(new Vector3(0, 0, GameCostants.jump_distance), jumpPower, 1, jumpDuration).SetEase(Ease.Linear).SetRelative(true);
        
        //update jumping status and ball position
        jumping.OnComplete(UpdateJumpingStatus);
    }

    void UpdateJumpingStatus()
    {
        isJumping = false;
    }

    void CheckDeath()
    {
        time += Time.deltaTime;

        if(time > jumpDuration + 0.1f)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //hitting platform 
        if (other.GetComponent<PointMaker>() != null)
        {
            Debug.Log("reset time");
            time = 0f;
        } 
    }

}

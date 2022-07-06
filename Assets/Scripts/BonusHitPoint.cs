using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHitPoint : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float probability;

    // Start is called before the first frame update
    void Start()
    {
        // randomizing probability of appearance
        int random = Random.Range(0, 100);

        if (random < probability)
        {
            this.gameObject.SetActive(true);

            // randomizing position
            float x = Random.Range(-1f, 1f);
            this.transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        }
        else
            this.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.gameObject.GetComponent<BallController>();

        // if other is the ball
        if (ball != null)
        {
            // Update UI
            BonusPoint evt = new BonusPoint();
            EventManager.Broadcast(evt);

            Destroy(this.gameObject);
        }
    }
}

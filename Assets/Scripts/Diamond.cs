using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float probability;

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 100);

        if (random < probability)
            this.gameObject.SetActive(true);
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
            AddDiamondEvent evt = new AddDiamondEvent();
            EventManager.Broadcast(evt);

            Destroy(this.gameObject);
        }
    }
}

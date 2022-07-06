using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PointMaker : MonoBehaviour
{
    [SerializeField]
    Transform hit_VFX;

    [SerializeField]
    Transform point_VFX;

    [SerializeField]
    Transform platform;


    float VFX_duration = 0.1f;
    float xz_scale = 2.5f;
    float y_scale = 0.5f;
    float invisible_scale = 1e-05f;
    float y_transition = 1f;
    float platform_transition = 3f;

    private void Start()
    {
        EventManager.AddListener<BonusPoint>(updateCurrentPointValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.gameObject.GetComponent<BallController>();

        // if other is the ball
        if (ball != null)
        {
            // Update UI
            AddPointEvent evt = new AddPointEvent();
            EventManager.Broadcast(evt);

            // Start VFX effect
            StartCoroutine(playVFX());
        }
    }

    private IEnumerator playVFX()
    {
        // platform animation
        platform.DOMoveY(platform.position.y - 10f, platform_transition).SetAutoKill(true);

        // text animation
        point_VFX.gameObject.SetActive(true);
        point_VFX.DOMoveY(y_transition, 0.5f).SetAutoKill(true);
        point_VFX.GetChild(0).GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetAutoKill(true);

        // cylinder animation
        hit_VFX.DOScale(new Vector3(xz_scale, y_scale, xz_scale), VFX_duration).SetAutoKill(true);
        yield return new WaitForSeconds(VFX_duration);
        hit_VFX.DOScale(new Vector3(invisible_scale, invisible_scale, invisible_scale), VFX_duration).SetAutoKill(true);
        yield return new WaitForSeconds(y_transition);
        Destroy(hit_VFX.gameObject);

        Destroy(point_VFX.gameObject);
        EventManager.RemoveListener<BonusPoint>(updateCurrentPointValue);

        Destroy(transform.parent.gameObject);

    }

    public void updateCurrentPointValue(BonusPoint evt)
    {
        point_VFX.GetChild(0).GetComponent<TextMeshPro>().text = "+" + GameCostants.current_point_value.ToString();
    }


    private void OnDestroy()
    {
        EventManager.RemoveListener<BonusPoint>(updateCurrentPointValue);
    }


}

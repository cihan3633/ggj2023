using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float fadeTime = 1.5f;
    public Vector2 forceMinMax;

    private void Start()
    {
        float forceAmount = Random.Range(forceMinMax.x, forceMinMax.y);
        // To shell jumping from gun
        myRigidbody.AddForce(transform.right * forceAmount);

        // To shell rotation
        myRigidbody.AddTorque(Random.insideUnitSphere * forceAmount);

        StartCoroutine(FadeShellAndDestroy());
    }

    IEnumerator FadeShellAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime);

        Material shellMaterial = GetComponent<Renderer>().material;
        Color initialColor = shellMaterial.color;
        Color transparentColor = Color.clear;
        float fadePercent = 0;
        float fadeSpeed = 1 / fadeTime;

        while (fadePercent <= 1)
        {
            fadePercent += Time.deltaTime * fadeSpeed;
            shellMaterial.color = Color.Lerp(initialColor, transparentColor, fadePercent);
            yield return null;
        }
        Destroy(gameObject);
    }

}

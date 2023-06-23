using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEnemy : MonoBehaviour
{
    public Material flashMaterial;
    private float duration = 0.02f;
    private int flashCount = 3;

    void Start()
    {
       
    }
    public void Hit()
    {
        FindAllSpriteRenderers(gameObject.transform);
    }

    private void FindAllSpriteRenderers(Transform parent)
    {
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Material originalMaterial = spriteRenderer.material;
            Flash(spriteRenderer, originalMaterial);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            FindAllSpriteRenderers(child);
        }
    }
    public void Flash(SpriteRenderer spriteRenderer, Material originalMaterial)
    {
            StartCoroutine(FlashRoutine(spriteRenderer, originalMaterial));
    }

    private IEnumerator FlashRoutine(SpriteRenderer spriteRenderer, Material originalMaterial)
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(duration);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(duration);
        }
    }
}

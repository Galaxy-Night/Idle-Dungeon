using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TapDamage : MonoBehaviour
{
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDestroy(1f));
        text.CrossFadeAlpha(0f, 1f, true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * .2f);
    }

    private IEnumerator WaitAndDestroy(float wait) {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
	}
}

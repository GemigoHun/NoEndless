using System.Collections;
using UnityEngine;

public class TileMoving : TileAb
{
    [SerializeField] private float speed = 1;

    public IEnumerator Moving(Vector3 to)
    {
        float counter = 0;

        while(counter < speed)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, to, counter / speed);
            counter += Time.deltaTime;

            // if(transform.parent.position.y < 4.8f)
            // {
            //     transform.parent.localScale = new Vector3(1, 1, 1);
            // }

            yield return null;
        }

        transform.parent.position = to;
    }

    public IEnumerator Moving(Vector3 to, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        float counter = 0;

        while(counter < speed)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, to, counter / speed);
            counter += Time.deltaTime;

            // if(transform.parent.position.y < 4.8f)
            // {
            //     transform.parent.localScale = new Vector3(1, 1, 1);
            // }

            yield return null;
        }

        transform.parent.position = to;
    }

    public void Moving(Transform to)
    {
        Vector3 toV3 = to.position;
        Moving(toV3);
    }
}
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public static Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}

using UnityEngine;

public class FibonacciSphere : MonoBehaviour
{
    [Range(0, 500)] public int rayCount = 200;
    [Range(0, 10f)] public float rayLength = 1f;

    [Range(0, 360)] public float fieldOfView = 90f;

    public enum Direction { Up, Forward, Left, Right, Back, Down }

    public Direction direction = Direction.Up;

    /// <summary> -- golden angle in radians -- </summary>
    static float Phi = Mathf.PI * (3f - Mathf.Sqrt(5f));

    static float Pi2 = Mathf.PI * 2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (var i = 0; i < rayCount; ++i)
        {
            var _p = Point(rayLength, i, rayCount, 0f, fieldOfView / 360, 0f, 360f);

            if (direction != Direction.Up) _p = Shift(_p, direction);

            Gizmos.DrawSphere(transform.position + _p, 0.04f);
        }

    }
    public static Vector3 Point(float radius, int index, int total, float min = 0f, float max = 360f , float angleStartDeg = 0f, float angleRangeDeg = 360)
    {
        // y goes from min (-) to max (+)
        var y = ((index / (total - 1f)) * max) * 2f - 1f;

        // radius at y
        var rY = Mathf.Sqrt(1 - y * y);

        // golden angle increment
        var theta = Phi * index;


        var x = Mathf.Cos(theta) * rY;
        var z = Mathf.Sin(theta) * rY;

        return new Vector3(x, y, z) * radius;
    }

    public static Vector3 Shift(Vector3 p, Direction direction = Direction.Up)
    {
        switch (direction)
        {
            default:
            case Direction.Up: return new Vector3(p.x, p.y, p.z);
            case Direction.Right: return new Vector3(p.y, p.x, p.z);
            case Direction.Left: return new Vector3(-p.y, p.x, p.z);
            case Direction.Down: return new Vector3(p.x, -p.y, p.z);
            case Direction.Forward: return new Vector3(p.x, p.z, p.y);
            case Direction.Back: return new Vector3(p.x, p.z, -p.y);
        }
    }
}
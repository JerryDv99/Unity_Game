using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    static public Matrix4x4 Translate(Vector3 position)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        matrix.m03 = position.x;
        matrix.m13 = position.y;
        matrix.m23 = position.z;

        return matrix;
    }

    static public Matrix4x4 RotationX(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  0    0    0   0
        //  0   cos -sin  0
        //  0   sin  cos  0
        //  0    0    0   0

        matrix.m11 = matrix.m22 = Mathf.Cos(_angle);
        matrix.m12 = -Mathf.Sin(_angle);
        matrix.m21 = Mathf.Sin(_angle);

        return matrix;
    }

    static public Matrix4x4 RotationY(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  cos  0   sin  0
        //  0    1    0   0
        // -sin  0   cos  0
        //  0    0    0   1

        matrix.m00 = matrix.m22 = Mathf.Cos(_angle);
        matrix.m02 = Mathf.Sin(_angle);
        matrix.m20 = -Mathf.Sin(_angle);

        return matrix;
    }

    static public Matrix4x4 RotationZ(float _angle)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  cos -sin  0   0
        //  sin  cos  0   0
        //  0    0    1   0
        //  0    0    0   1

        matrix.m00 = matrix.m11 = Mathf.Cos(_angle);
        matrix.m01 = -Mathf.Sin(_angle);
        matrix.m10 = Mathf.Sin(_angle);

        return matrix;
    }

    static public Matrix4x4 Scale(Vector3 _scale)
    {
        Matrix4x4 matrix = Matrix4x4.identity;

        // 00   01   02   03   x
        // 10   11   12   13   y
        // 20   21   22   23   z
        // 30   31   32   33   w
        //  x    y    z    w

        //  x    0    0   0
        //  0    y    0   0
        //  0    0    z   0
        //  0    0    0   1

        matrix.m00 = _scale.x;
        matrix.m11 = _scale.y;
        matrix.m22 = _scale.z;

        return matrix;
    }
}

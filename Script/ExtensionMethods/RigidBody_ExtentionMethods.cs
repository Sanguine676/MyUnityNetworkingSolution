using UnityEngine;
//

//
public static class RigidBody_ExtentionMethods
{
    //
    public static void SleepFixedFrame(this Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
    }

    //
    public static void SleepYFixedFrame(this Rigidbody rb)
    {
        Vector3 velocity = rb.velocity;
        rb.velocity = new Vector3(velocity.x, 0.0f, velocity.z);
    }

    //
    public static void SleepXZFixedFrame(this Rigidbody rb)
    {
        float yVelocity = rb.velocity.y;
        rb.velocity = new Vector3(0.0f, yVelocity, 0.0f);
    }

    //
    public static void ScaleVelocity(this Rigidbody rb, Vector3 scalerVec)
    {
        Vector3 velocity = rb.velocity;
        velocity.Scale(scalerVec);
        rb.velocity = velocity;
    }
}

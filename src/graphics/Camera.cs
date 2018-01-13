﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSDView.math;
using OpenTK;

namespace LSDView.graphics
{
    public class Camera
    {
        public Transform Transform { get; private set; }

        public Matrix4 View => Matrix4.LookAt(Transform.Position, Transform.Forward, Transform.Up);

        public Camera() { Transform = new Transform(); }

        public void LookAt(Vector3 pos)
        {
            Vector3 forward = Transform.Forward;
            Vector3 newForward = Vector3.Normalize(pos - Transform.Position);
            Vector3 rotAxis = Vector3.Cross(forward, newForward);
            float angle = Vector3.CalculateAngle(forward, newForward);
            Transform.Rotate(angle, rotAxis, false);
        }

        /// <summary>
        /// Rotate around a target at a distance.
        /// </summary>
        /// <param name="longitude">The angle to rotate around the target from east to west</param>
        /// <param name="latitude">The angle to rotate around the target from north to south</param>
        /// <param name="target">The target</param>
        /// <param name="distance">The distance the camera should be away from the target</param>
        public void ArcBall(float longitude, float latitude, Vector3 target, float distance)
        {
            //Matrix3 rot = Matrix3.CreateFromAxisAngle(Transform.Up, longitude);
            //Vector3 diff = Vector3.UnitZ * distance;
            //Vector3 newPos = rot * diff;

            Transform.Position = Matrix3.CreateFromAxisAngle(Transform.Up, longitude)
                                  * ((Transform.Position - target).Normalized() * distance) + target;
            Transform.Position = Matrix3.CreateFromAxisAngle(Transform.Right, latitude)
                                  * ((Transform.Position - target).Normalized() * distance) + target;
            LookAt(target);
        }
    }
}

using System;
using UnityEngine;

namespace CarGame
{
    [Serializable]
    public class AxisInfo
    {
        public WheelCollider LeftWheel;
        public WheelCollider RightWheel;

        public bool IsSteering;
        public bool IsMotor;
    }
}

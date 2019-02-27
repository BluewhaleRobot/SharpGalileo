using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGalileo.models
{
    public class GalileoStatus
    {
        public long timestamp;
        public int angleGoalStatus;
        public int busyStatus;
        public int chargeStatus;
        public float controlSpeedTheta;
        public float controlSpeedX;
        public float currentAngle;
        public float currentPosX;
        public float currentPosY;
        public float currentSpeedTheta;
        public float currentSpeedX;
        public int gbaStatus;
        public int gcStatus;
        public int loopStatus;
        public int mapStatus;
        public int navStatus;
        public float power;
        public float targetDistance;
        public int targetNumID;
        public int targetStatus;
        public int visualStatus;
    }
}

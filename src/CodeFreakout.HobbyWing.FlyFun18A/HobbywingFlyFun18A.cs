using System;
using System.Threading;
using Microsoft.SPOT.Hardware;

namespace CodeFreakout.HobbyWing.FlyFun18A
{
    public class HobbywingFlyFun18A
    {
        private readonly PWM _speedController;
        private int _currentSpeed;
        private bool _isStarted;

        public HobbywingFlyFun18A(Cpu.PWMChannel pwmChannel)
        {
            _speedController = new PWM(pwmChannel, 20000, 2000, PWM.ScaleFactor.Microseconds, false);
        }

        public void Start()
        {
            if (_isStarted) return;

            _speedController.Start();
            Thread.Sleep(3000);
            _speedController.Duration = 700;
            Thread.Sleep(3000);
            _isStarted = true;
        }

        public int Speed
        {
            get { return _currentSpeed; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _currentSpeed = value;
                var newDuration = (uint)(((double)_currentSpeed / 100) * 1300) + 700;
                _speedController.Duration = newDuration;
            }
        }
    }
}
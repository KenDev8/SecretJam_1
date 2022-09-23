using UnityEngine;

namespace KenDev
{
    public class TimerCounter : MonoBehaviour
    {
        private bool targetReached = false;
        public bool stopOnTarget = true;

        // timer parameters
        private bool timerEnabled = false;
        private int timerTargetSeconds = 0;
        private float timerTimeDelta = 0f;
        private int timerSeconds = 0;
        private int timerMinutes = 0;
        private int timerHours = 0;

        public delegate void OnTimerUpdateHandler(int _hr, int _min, int _sec);
        public event OnTimerUpdateHandler OnTimerUpdate;

        public delegate void OnTargetReachHandler();
        public event OnTargetReachHandler OnTargetReach;

        // integer counter parameters
        private int counter = 0;
        private int counterTarget = 0;
        private int counterSign = 1;

        public delegate void OnCounterUpdateHandler(int _counter);
        public event OnCounterUpdateHandler OnCounterUpdate;

        private void Update()
        {
            if (!timerEnabled)
                return;

            TimerCount();
        }

        // timer methods
        public void TimerSet(int _timerTargetSeconds, bool _enableOnSet = false)
        {
            TimerDisable();
            TimerReset();
            timerTargetSeconds = Mathf.Abs(_timerTargetSeconds);
            if (_enableOnSet)
                TimerEnable();

        }

        private void TimerUpdate(int _hr, int _min, int _sec)
        {
            timerSeconds = _sec;
            timerMinutes = _min;
            timerHours = _hr;

            OnTimerUpdate?.Invoke(_hr, _min, _sec);
            //print(timerSeconds);
        }

        private void TimerCount()
        {
            timerTimeDelta += Time.deltaTime;

            if (timerSeconds != (int)(timerTimeDelta % 60))
                TimerUpdate
                    (
                        (int)(timerTimeDelta / 3600),
                        (int)(timerTimeDelta / 60),
                        (int)(timerTimeDelta % 60)
                    );

            if ((timerHours * 3600 + timerMinutes * 60 + timerSeconds) >= timerTargetSeconds)
                TargetReached();
        }

        public void TimerEnable()
        {
            timerEnabled = true;
        }

        public void TimerDisable()
        {
            timerEnabled = false;
        }

        public void TimerReset()
        {
            timerTimeDelta = 0;
            targetReached = false;
            TimerUpdate(0, 0, 0);
        }

        // counter methods
        public void CounterSet(int _counterTarget, bool _positiveCount = true)
        {
            TimerDisable();

            if (_positiveCount)
            {
                counterSign = 1;
                counterTarget = _counterTarget;
                CounterUpdate(0);
            }
            else
            {
                counterSign = -1;
                counterTarget = 0;
                CounterUpdate(_counterTarget);
            }

        }

        public void CounterCount(bool _none = false)
        {
            //print(counter);
            counter += counterSign;
            CounterUpdate(counter);

            if
            (
                (counterSign == 1 && counter >= counterTarget) ||
                (counterSign == -1 && counter <= counterTarget)
            )
            {
                TargetReached();
            }
        }

        private void CounterUpdate(int _counter)
        {
            counter = _counter;
            OnCounterUpdate?.Invoke(counter);
        }

        // general methods
        private void TargetReached()
        {
            targetReached = true;
            if (stopOnTarget)
            {
                TimerDisable();
            }

            OnTargetReach?.Invoke();
        }
    }

}

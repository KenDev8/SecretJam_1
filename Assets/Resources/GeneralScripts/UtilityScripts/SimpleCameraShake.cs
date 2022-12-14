using UnityEngine;
using Cinemachine;

namespace KenDev
{
    public class SimpleCameraShake : MonoBehaviour
    {
        // _________________ Signelton Pattern _________________//
        public static SimpleCameraShake _instance;
        public static SimpleCameraShake Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SimpleCameraShake>();
                }
                return _instance;
            }
        }
        // _________________ Signelton Pattern _________________//



        public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
        public float ShakeAmplitude = 3.2f;         // Cinemachine Noise Profile Parameter
        public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

        private float ShakeElapsedTime = 0f;

        // Cinemachine Shake
        public CinemachineVirtualCamera VirtualCamera;
        private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

        // Use this for initialization
        void Start()
        {
            // Get Virtual Camera Noise Profile
            if (VirtualCamera != null)
                virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        // Update is called once per frame
        void Update()
        {
            // If the Cinemachine componet is not set, avoid update
            if (VirtualCamera != null && virtualCameraNoise != null)
            {
                // If Camera Shake effect is still playing
                if (ShakeElapsedTime > 0)
                {
                    // Set Cinemachine Camera Noise parameters
                    virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                    virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                    // Update Shake Timer
                    ShakeElapsedTime -= Time.deltaTime;
                }
                else
                {
                    // If Camera Shake effect is over, reset variables
                    virtualCameraNoise.m_AmplitudeGain = 0f;
                    ShakeElapsedTime = 0f;
                }
            }
        }

        public void Shake()
        {
            ShakeElapsedTime = ShakeDuration;
        }
    }

}

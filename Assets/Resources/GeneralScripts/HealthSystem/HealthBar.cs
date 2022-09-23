using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KenDev
{
    public enum AnchorType
    {
        Top,
        Mid,
        Bot
    }

    public class HealthBar : MonoBehaviour
    {
        [Header("UI Elements")]
        public TMP_Text healthBarName;
        public Canvas healthBarCanvas;
        private RectTransform rect;
        private Slider slider;

        private float maxHealth = 100f;
        private float curHealth;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            rect = GetComponent<RectTransform>();
        }

        public void SetAnchor(AnchorType _anchor)
        {
            float minX = 0.5f, minY = 0.5f;
            float maxX = 0.5f, maxY = 0.5f;

            switch (_anchor)
            {
                case AnchorType.Top:
                    minX = 0.5f;
                    maxX = 0.5f;
                    minY = 1f;
                    maxY = 1f;
                    break;
                case AnchorType.Mid:
                    maxX = 0.5f;
                    minX = 0.5f;
                    minY = 0.5f;
                    maxY = 0.5f;
                    break;
                case AnchorType.Bot:
                    minX = 1f;
                    maxX = 1f;
                    minY = 0.5f;
                    maxY = 0.5f;
                    break;

            }
            rect.anchorMin = new Vector2(minX, minY);
            rect.anchorMax = new Vector2(maxX, maxY);
        }

        public void SetHealthBar(string _name = "Character Name", float _maxHealth = 100)
        {
            //rect.SetPositionAndRotation(position, Quaternion.identity);
            healthBarName.text = _name;
            maxHealth = _maxHealth;
            curHealth = maxHealth;
            slider.maxValue = maxHealth;

            UpdateHealth(curHealth);
        }

        public void UpdateHealth(float _health)
        {
            if (_health > maxHealth)
                return;

            if (slider != null)
                slider.value = _health;
        }

        public void SetCanvas(Canvas _canvas)
        {
            healthBarCanvas = _canvas;
            transform.SetParent(healthBarCanvas.transform, false);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void SetHealthBarDisplay(bool _state)
        {
            if (healthBarCanvas != null)
                healthBarCanvas.enabled = _state;
            else
                print("No Canvas Set");
        }
    }

}

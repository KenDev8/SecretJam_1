using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public class MyUtilities : MonoBehaviour
    {
        // _________________ Signelton Pattern _________________//
        public static MyUtilities _instance;
        public static MyUtilities Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MyUtilities>();
                }
                return _instance;
            }
        }
        // _________________ Signelton Pattern _________________//


        //________________________ Collision ______________________//

        //
        // Summary:
        //     returns true if: _colliderLayer != _layerForCheck,  false otherwise
        public bool CheckCollisionWithLayer(int _colliderLayer, LayerMask _layerForCheck)
        {
            if (((1 << _colliderLayer) & _layerForCheck) != 0)
                return false;

            return true;
        }
        //________________________ Collision ______________________//


        //_________________________ Rays _________________________//
        public RaycastHit2D Raycast(Vector2 _origin, Vector2 _direction, bool _debug = false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(_origin, _direction);

            // if Ray debug is desired then draw (default is false)
            if (_debug)
            {
                // if hit then ray is red, if didnt hit then green
                Color color = hitInfo ? Color.red : Color.green;
                Debug.DrawRay(_origin, _direction, color);
            }

            return hitInfo;
        }

        public RaycastHit2D Raycast(Vector2 _origin, Vector2 _direction, float _length, LayerMask _layer, bool _debug = false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(_origin, _direction, _length, _layer);

            // if Ray debug is desired then draw (default is false)
            if (_debug)
            {
                // if hit then ray is red, if didnt hit then green
                Color color = hitInfo ? Color.red : Color.green;
                Debug.DrawRay(_origin, _direction * _length, color);
            }

            return hitInfo;
        }

        public RaycastHit2D[] RayCastAll(Vector2 _origin, Vector2 _direction, bool _debug = false)
        {
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(_origin, _direction);

            // if Ray debug is desired then draw (default is false)
            if (_debug)
            {
                // if hit then ray is red, if didnt hit then green
                foreach (RaycastHit2D ray in hitInfo)
                {
                    Color color = ray ? Color.red : Color.green;
                    Debug.DrawRay(_origin, _direction, color);
                }
            }

            return hitInfo;
        }

        public RaycastHit2D[] RayCastAll(Vector2 _origin, Vector2 _direction, float _length, LayerMask _layer, bool _debug = false)
        {
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(_origin, _direction, _length, _layer);

            // if Ray debug is desired then draw (default is false)
            if (_debug)
            {
                // if hit then ray is red, if didnt hit then green
                foreach (RaycastHit2D ray in hitInfo)
                {
                    Color color = ray ? Color.red : Color.green;
                    Debug.DrawRay(_origin, _direction, color);
                }
            }

            return hitInfo;
        }
        //_________________________ Rays _________________________//

        //_________________________ Text _________________________//


        public void BasicTypeWriterEffect(TMPro.TMP_Text _textObj, string _text, GameObject _continueButton = null, float _typeSpeed = 0.06f)
        {
            StartCoroutine(TypeWriterRoutine(_textObj, _text, _continueButton, _typeSpeed));
        }
        public IEnumerator TypeWriterRoutine(TMPro.TMP_Text _textObj, string _text, GameObject _continueButton, float _typeSpeed)
        {
            string txt = "";
            for (int i = 0; i < _text.Length; i++)
            {
                // type writer effect
                txt += _text[i];
                _textObj.text = txt;
                yield return new WaitForSeconds(_typeSpeed);
            }

            if (_continueButton != null)
                _continueButton.SetActive(true);
        }
        //_________________________ Text _________________________//

    }


}

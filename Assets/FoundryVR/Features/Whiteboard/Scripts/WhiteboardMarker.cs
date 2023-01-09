using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;
    
    void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        _tipHeight = tip.localScale.y;
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        bool raycastHit = Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight);
        bool tagIsWhiteboard = _touch.transform.CompareTag("Whiteboard");

        if (!raycastHit || !tagIsWhiteboard)
        {
            _whiteboard = null;
            _touchedLastFrame = false;
            return;
        }
        
        if (_whiteboard == null)
        {
            _whiteboard = _touch.transform.GetComponent<Whiteboard>();
        }

        _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

        var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (penSize / 2));
        var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (penSize / 2));

        if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

        if (_touchedLastFrame)
        {
            _whiteboard.texture.SetPixels(x, y, penSize, penSize, _colors);

            for (float f = 0.01f; f < 1.00f; f += 0.01f)
            {
                var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                _whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, _colors);
            }

            transform.rotation = _lastTouchRot;

            _whiteboard.texture.Apply();
        }

        _lastTouchPos = new Vector2(x, y);
        _lastTouchRot = transform.rotation;
        _touchedLastFrame = true;
    }
}

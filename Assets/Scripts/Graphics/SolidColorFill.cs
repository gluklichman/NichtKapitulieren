using UnityEngine;
using System.Collections.Generic;

namespace Renovation
{
    namespace Graphics
    {
        /// <summary>
        /// This component renders collider as a solid color area
        /// Now works only with box colliders
        /// </summary>
        [ExecuteInEditMode]
        public class SolidColorFill : MonoBehaviour
        {
            public BoxCollider2D _collider = null;
            public Color _fillColor = Color.white;

            private bool _inited = false;
            private Sprite _sprite = null;
            private SpriteRenderer _renderer = null;
            private Transform _renderObject = null;

            // Use this for initialization
            void Start()
            {
                if (_collider == null)
                {
                    _collider = GetComponent<BoxCollider2D>();
                }
                if (!(_collider is BoxCollider2D))
                {
                    Debug.LogWarning("Only box colliders are handled", this);
                    return;
                }

                _inited = true;
                PrepareRenderObject();
                PrepareSprite();
                UpdateSprite();
            }

            private void PrepareRenderObject()
            {
                if (!_inited)
                {
                    return;
                }

                DestroyAllRenderers();
                GameObject renderObject = new GameObject(GlobalConstants.RENDER_OBJECT_NAME);
                renderObject.transform.parent = transform;
                _renderer = renderObject.AddComponent<SpriteRenderer>();
                _renderObject = renderObject.transform;
            }

            private void DestroyAllRenderers()
            {
                List<Transform> renderChildren = new List<Transform>();
                for (int i=0; i<transform.childCount; ++i)
                {
                    if (transform.GetChild(i).name == GlobalConstants.RENDER_OBJECT_NAME)
                    {
                        renderChildren.Add(transform.GetChild(i));
                    }
                }
                for (int i=0; i<renderChildren.Count; ++i)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(renderChildren[i].gameObject);
                    }
                    else
                    {
                        DestroyImmediate(renderChildren[i].gameObject);
                    }
                }
            }

            private void PrepareSprite()
            {
                if (!_inited)
                {
                    return;
                }
           
                if (_sprite != null)
                {
                    return;
                }
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.white);
                texture.Apply();
                _sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                _renderer.sprite = _sprite;
            }

            private void UpdateSprite()
            {
                if (!_inited
                    || !_sprite)
                {
                    return;
                }
                BoxCollider2D boxCollider = _collider as BoxCollider2D;
                float pixelPerUnit = _sprite.pixelsPerUnit;
                _renderObject.localScale = _collider.size * pixelPerUnit;
                
                _renderObject.localPosition = boxCollider.offset;
                _renderObject.localEulerAngles = Vector3.zero;

                _renderer.color = _fillColor;
            }

            // Update is called once per frame
            void Update()
            {
                UpdateSprite();
            }
        }
    }
}

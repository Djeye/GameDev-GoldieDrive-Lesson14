using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private const string OUTLINE_ENABLED = "_OutlineEnabled";
    private const string OUTLINE_COLOR_NAME = "_SolidOutline";

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject label;

    [SerializeField] private string _itemName;
    [SerializeField] private bool _isCorrect;

    private Renderer rend;
    private Material outline;

    private Vector2 _startPos;
    private bool _isActive = true, _isDragged = false, _trunkEntered = false;

    private Trunk trunk;

    private Color red = Color.red / 1.5f, green  = Color.green / 1.5f, yellow = Color.yellow / 1.5f;

    private Sounds sounds;
    void Start()
    {
        outline = sprite.GetComponent<Renderer>().material;

        outline.SetInt(OUTLINE_ENABLED, 0);

        label.transform.position = transform.position;
        label.SetActive(false);

        _startPos = transform.position;

        sounds = GetComponentInParent<Sounds>();
    }

    private void OnMouseDown()
    {
        if (_isActive && !Trunk.endGame)
        {
            sounds.SpawnHighlight();
            outline.SetInt(OUTLINE_ENABLED, 0);
            label.SetActive(false);
            _isDragged = true;
        }

    }

    private void OnMouseDrag()
    {
        if (_isActive && !Trunk.endGame)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousepos.x, mousepos.y);

        }
    }

    private void OnMouseUp()
    {
        if (_isActive && !Trunk.endGame)
        {
            if (_trunkEntered && !trunk.IsFull())
            {
                _isActive = false;

                if (_isCorrect)
                {
                    sounds.SpawnApprove();
                    trunk.SetOnPos(transform);
                    outline.SetInt(OUTLINE_ENABLED, 1);
                    outline.SetColor(OUTLINE_COLOR_NAME, green);

                }
                else
                {
                    sounds.SpawnError();
                    transform.position = _startPos;
                    outline.SetInt(OUTLINE_ENABLED, 1);
                    outline.SetColor(OUTLINE_COLOR_NAME, red);
                }

            }
            else
            {
                transform.position = _startPos;
            }
        }
        _isDragged = false;
    }

    private void OnMouseEnter()
    {
        if (!_isDragged && _isActive && !Trunk.endGame)
        {
            sounds.SpawnPick();
            label.transform.position = transform.position + new Vector3(0, 1f, 0);
            outline.SetInt(OUTLINE_ENABLED, 1);
            label.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!_isDragged && _isActive && !Trunk.endGame)
        {
            outline.SetInt(OUTLINE_ENABLED, 0);
            label.SetActive(false);
        }
    }

    private void GlowOutlineOn()
    {
        outline.SetInt(OUTLINE_ENABLED, 1);
        outline.SetColor(OUTLINE_COLOR_NAME, yellow);
    }

    private void GlowOutlineOff()
    {
        outline.SetInt(OUTLINE_ENABLED, 0);
        outline.SetColor(OUTLINE_COLOR_NAME, Color.white);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Trunk.endGame)
        {
            if (_isActive)
            {
                trunk = collision.GetComponent<Trunk>();
                GlowOutlineOn();

                _trunkEntered = true;
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Trunk.endGame)
        {
            if (_isActive)
            {
                GlowOutlineOff();
                _trunkEntered = false;

            }
        }
    }
}

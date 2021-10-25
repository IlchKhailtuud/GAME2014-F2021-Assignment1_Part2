using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public PropTypeSprite[] propSpArr;
    private Sprite defaultSp;
    private SpriteRenderer sr;
    private PropType propType;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultSp = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameObjectTag.BombEffect))
        {
            //disbale collision between the enemeies and props after props are revealed
            tag = GameObjectTag.Prop;
            
            GetComponent<Collider2D>().isTrigger = true;
            int index = UnityEngine.Random.Range(0, propSpArr.Length);
            sr.sprite = propSpArr[index].sp;
            propType = propSpArr[index].type;

            StartCoroutine(PlayAnim());
        }

        if (other.CompareTag(GameObjectTag.Player))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            
            playerController.StatusBoost((int)propType);
            
            reset();
            ObjectManager.Instance().returnGameObject(gameObject, GameObjectType.Prop);
        }
    }

    //reset prop properties after returning to the object pool
    private void reset()
    {
        sr.sprite = defaultSp;
        tag = GameObjectTag.DestructibleBrick;
        GetComponent<Collider2D>().isTrigger = false;
    }

    //make the prop sparkle when revealed
    IEnumerator PlayAnim()
    {
        for (int i = 0; i < 2; i++)
        {
            sr.color = Color.yellow;
            yield return new WaitForSeconds(0.25f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
    }
}

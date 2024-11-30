using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.GridLayoutGroup;

public class Bullet : GameUnit
{
    public float moveSpeed = 2f;
    private Vector3 direction;
    private float rotationSpeed = 360f;
    private Character _character;
    public void SetDirection(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        Invoke(nameof(OnDespawn), 3f);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public void SetUsingPeopel(Character character) => this._character = character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (character == null || _character == character) return;
            OnDespawn();
            _character.UpSize();
            if (_character is Player player)
            {
                player.CoinPlayer++;
                FollowCamera.Instance.offest = new Vector3(FollowCamera.Instance.offest.x, FollowCamera.Instance.offest.y+0.5f, -FollowCamera.Instance.offest.y+0.3f);
                DataManager.Instance.CoinData += player.CoinPlayer;
                DataManager.Instance.SaveCoinPlayerData(DataManager.Instance.CoinData);
            }
        }
    }
}

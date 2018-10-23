using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public interface IUnit {
        int GetPlayerId();
        Vector2 GetPos();
        GameObject GetGameObject();
        bool GetDead();
        bool IsMine();
        void OnDestroy();
        
        int Damaged(int _damage); // Return DamagedHp
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Colonize.Unit {
	public interface IUnit {
        
        bool GetDead();
        bool IsMine();
        

        int GetPlayerId();
        int GetNum();
        int Damaged(int _damage); // Return DamagedHp

        GameObject GetGameObject();
        Vector2 GetPos();

        void OnDestroy();
    }
}
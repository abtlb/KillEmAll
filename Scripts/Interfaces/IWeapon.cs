using UnityEngine;

public interface IWeapon
{
    void Shoot();
    bool IsPlayer { get; }
    float TimeToShoot { get; }
    void Stop();
}

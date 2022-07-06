using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVault : MonoBehaviour
{
    private static SoundVault _i;

    public static SoundVault i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<SoundVault>("SoundVault"));
            return _i;
        }
    }

    public AudioClip ShootClip;
}

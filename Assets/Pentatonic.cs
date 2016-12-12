using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pentatonic {

	public static void PlaySound(AudioSource sound, int intervals)
    {
        sound.pitch = Mathf.Pow(1.125f, (float)Random.Range(0, intervals));
        sound.Play();
    }
}

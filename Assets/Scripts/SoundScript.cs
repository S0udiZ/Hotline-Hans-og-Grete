using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] AudioSource sound_step;
    [SerializeField] AudioSource sound_swap;
    [SerializeField] AudioSource sound_lever;
    [SerializeField] AudioSource sound_button;
    [SerializeField] AudioSource sound_plate_active;
    [SerializeField] AudioSource sound_plate_deactive;
    [SerializeField] AudioSource sound_door_open;
    [SerializeField] AudioSource sound_door_close;
    [SerializeField] AudioSource sound_plate_slide;
    public void PlaySound(string sound)
    {
        Debug.Log("plau-sound: " + sound);
        if (sound == "step")
        {
            sound_step.Play();
        }
        if (sound == "swap")
        {
            sound_step.Play();
        }
    }
}

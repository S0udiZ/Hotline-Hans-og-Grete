using UnityEngine;

public class CamSounds : MonoBehaviour
{
    [SerializeField] private AudioSource pipe;
    [SerializeField] private AudioSource step;
    [SerializeField] private AudioSource swap;

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "pipe":
                {
                    pipe.Play();
                    break;
                }
            case "step":
                {
                    step.Play();
                    break;
                }
            case "swap":
                {
                    swap.Play();
                    break;
                }
        }
        return;
    }
}

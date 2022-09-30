using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //_________________ Signelton Pattern _________________//
    public static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    //_________________ Signelton Pattern _________________//

    //____________________ Audio Clips ____________________// 

    [Header("Music")]
    public AudioClip mainMusicClip;
    public AudioClip graveFightClip;

    [Header("Ambient")]
    public AudioClip caveAmbientClip;
    public AudioClip graveAmbientClip;

    [Header("Player Movement SFX")]
    public AudioClip runClip;

    [Header("Player SFX")]
    public AudioClip shootClip;

    [Header("Stings SFX")]
    public AudioClip skellyHitClip;
    public AudioClip skellyDeathClip;
    public AudioClip bonePickUpClip;
    public AudioClip pileUpgradeClip;
    public AudioClip pileFallClip;
    public AudioClip portalOpenClip;



    //____________________ Audio Clips ____________________// 

    //___________________ Audio Sources ___________________// 
    private AudioSource musicSource;
    private AudioSource ambientSource;
    private AudioSource playerMovementSource;
    private AudioSource playerSFXSource;
    private AudioSource stingSource;
    

    //___________________ Audio Sources ___________________// 

    //___________________ Mixer Groups ___________________// 

    
    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup playerMovementGroup;
    public AudioMixerGroup playerSFXGroup;
    public AudioMixerGroup stingGroup;
    

    //___________________ Mixer Groups ___________________// 

    private void Awake()
    {
        // Generate the relevant Audio Sources
        musicSource     = gameObject.AddComponent<AudioSource>() as AudioSource;
        ambientSource   = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerMovementSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSFXSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource     = gameObject.AddComponent<AudioSource>() as AudioSource;

        // disable play on awake 
        musicSource.playOnAwake     = false;
        ambientSource.playOnAwake   = false;
        playerMovementSource.playOnAwake   = false;
        playerSFXSource.playOnAwake  = false;
        stingSource.playOnAwake     = false;

        // Assign each audio source a mixer group
        musicSource.outputAudioMixerGroup       = musicGroup;
        ambientSource.outputAudioMixerGroup     = ambientGroup;
        playerMovementSource.outputAudioMixerGroup     = playerMovementGroup;
        playerSFXSource.outputAudioMixerGroup    = playerSFXGroup;
        stingSource.outputAudioMixerGroup       = stingGroup;

        // set initial clips (normally looping clips)
        if(mainMusicClip != null)
        {
            musicSource.clip = mainMusicClip;
            musicSource.loop = true;
        }

        if(caveAmbientClip != null)
        {
            ambientSource.clip = caveAmbientClip;
            ambientSource.loop = true;
        }

        PlayCaveAmbient();

    }

    public void PlayPileFall()
    {
        stingSource.clip = pileFallClip;
        stingSource.Play();
    }
    public void PlayPlayerRun()
    {
        playerMovementSource.clip = runClip;
        RandomizeSound(playerMovementSource);
        playerMovementSource.Play();
    }

    public void PlayShoot()
    {

        playerSFXSource.clip = shootClip;
        RandomizeSound(playerSFXSource);
        playerSFXSource.Play();
    }

    public void PlaySkellyHit()
    {
        stingSource.clip = skellyHitClip;
        RandomizeSound(stingSource);
        stingSource.Play();
    }

    public void PlaySkellyDeath()
    {
        stingSource.clip = skellyDeathClip;
        RandomizeSound(stingSource);
        stingSource.Play();
    }

    public void PlayPileUpgrade()
    {
        stingSource.clip = pileUpgradeClip;
        stingSource.Play();
    }

    public void PlayPortalOpen()
    {
        stingSource.clip = portalOpenClip;
        stingSource.Play();
    }

    public void PlayPickUp()
    {
        stingSource.clip = bonePickUpClip;
        stingSource.Play();
    }

    public void PlayGraveFight()
    {
        musicSource.clip = graveFightClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.clip = null;
        musicSource.Stop();
    }

    public void PlayGraveYardAmbient()
    {
        ambientSource.clip = graveAmbientClip;
        ambientSource.Play();
    }

    public void PlayCaveAmbient()
    {
        ambientSource.clip = caveAmbientClip;
        ambientSource.Play();
    }

    private void RandomizeSound(AudioSource _source)
    {
        _source.pitch = Random.Range(0.8f, 1.3f);
    }
}

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

    [Header("Ambient")]
    public AudioClip caveAmbientClip;

    [Header("Stings SFX")]
    public AudioClip monsterClickClip;

    [Header("Dialogue SFX")]
    public AudioClip responseSelectionSFXClip;
    public AudioClip responseConfirmSFXClip;

    [Header("Text Speaking SFX")]
    public AudioClip[] TorielSpeakClips;

    [Header("Girl SFX")]
    public AudioClip walkingSFXClip;
    public AudioClip deathSFXClip;


    //____________________ Audio Clips ____________________// 

    //___________________ Audio Sources ___________________// 
    private AudioSource musicSource;
    private AudioSource ambientSource;
    private AudioSource stingSource;
    private AudioSource dialogueSource;
    private AudioSource speakerSource;
    private AudioSource girlSource;

    //___________________ Audio Sources ___________________// 

    //___________________ Mixer Groups ___________________// 

    
    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup stingGroup;
    public AudioMixerGroup speakerGroup;
    public AudioMixerGroup dialogueGroup;
    public AudioMixerGroup girlGroup;

    //___________________ Mixer Groups ___________________// 

    private void Awake()
    {
        // Generate the relevant Audio Sources
        musicSource     = gameObject.AddComponent<AudioSource>() as AudioSource;
        ambientSource   = gameObject.AddComponent<AudioSource>() as AudioSource;
        speakerSource   = gameObject.AddComponent<AudioSource>() as AudioSource;
        dialogueSource  = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource     = gameObject.AddComponent<AudioSource>() as AudioSource;
        girlSource      = gameObject.AddComponent<AudioSource>() as AudioSource;

        // disable play on awake 
        musicSource.playOnAwake     = false;
        ambientSource.playOnAwake   = false;
        speakerSource.playOnAwake   = false;
        dialogueSource.playOnAwake  = false;
        stingSource.playOnAwake     = false;
        girlSource.playOnAwake      = false;

        // Assign each audio source a mixer group
        musicSource.outputAudioMixerGroup       = musicGroup;
        ambientSource.outputAudioMixerGroup     = ambientGroup;
        speakerSource.outputAudioMixerGroup     = speakerGroup;
        dialogueSource.outputAudioMixerGroup    = dialogueGroup;
        stingSource.outputAudioMixerGroup       = stingGroup;
        girlSource.outputAudioMixerGroup        = girlGroup;

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


    }

    // whenever a character "speaks" a text 
    public void PlaySpeakerSFX(string name)
    {
        switch(name)
        {
            case "Toriel":
                int c = Random.Range(0, TorielSpeakClips.Length);
                speakerSource.clip = TorielSpeakClips[c];
                speakerSource.Play();
                break;

        }
    }

    // whenever a response button is selected
    public void PlayResponseSelectionSFX()
    {
        dialogueSource.clip = responseSelectionSFXClip;
        dialogueSource.Play();
    }

    // whenever a response button is chosen
    public void PlayResponseConfirmSFX()
    {
        dialogueSource.clip = responseConfirmSFXClip;
        dialogueSource.Play();
    }
}

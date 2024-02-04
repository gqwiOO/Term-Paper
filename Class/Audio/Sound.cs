using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Game1;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TermPaper.Class.Audio;

public class Sound
{

    public static Song Key;
    public static Song sweden;
    public static Song aria;
    public static List<Song> music;


    public void Load()
    {
        Key = Globals.Content.Load<Song>("Sound/Key");
        sweden = Globals.Content.Load<Song>("Sound/Sweden");
        aria = Globals.Content.Load<Song>("Sound/Aria");

        music = new List<Song> { Key, sweden, aria };
    }
    
    public static Dictionary<string,SoundEffectInstance> soundDict = new Dictionary<string,SoundEffectInstance>()
    {
        ["hurt"] = Globals.Content.Load<SoundEffect>("Sound/SlavicSound").CreateInstance(),
        ["spawnSound"] = Globals.Content.Load<SoundEffect>("Sound/spawn-01").CreateInstance(),
        ["walkingSound"] = Globals.Content.Load<SoundEffect>("Sound/WalkSound").CreateInstance(),
        ["ClickSound"] = Globals.Content.Load<SoundEffect>("Sound/ButtonSound").CreateInstance()
    };

    public static void PlaySoundEffect(string key, float volume)
    {
        if (soundDict[key].State != SoundState.Playing)
        {
            soundDict[key].Play();
            soundDict[key].Volume = volume;
        }
    }

    public static void PlayMusic()
    {
        for (int i = 0; i < music.Count; i++)
        {
            MediaPlayer.Play(music[i]);
        }
    }
    
}
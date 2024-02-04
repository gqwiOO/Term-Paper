using System.Collections.Generic;
using Game1;
using Microsoft.Xna.Framework.Audio;

namespace TermPaper.Class.Audio;

public static class Sound
{
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
}
using System.Collections.Generic;
using Game1;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TermPaper.Class.Audio;

static class Sound
{
    private static Song music;
    public static SoundEffect healSound;
    public static SoundEffect _hurt;
    public static SoundEffect _spawnSound;
    public static SoundEffect _walkingSound;

    public static Dictionary<string, SoundEffect> soundDict = new Dictionary<string, SoundEffect>()
    {
        ["hurt"] = Globals.Content.Load<SoundEffect>("Sound/SlavicSound"),
        ["spawnSound"] = Globals.Content.Load<SoundEffect>("Sound/spawn-01"),
        ["walkingSound"] = Globals.Content.Load<SoundEffect>("Sound/WalkSound")
    };

    public static void PlaySoundEffect(string key)
    {
        soundDict[key].Play();
    }
        
    public static Song Music
    {
        get
        {
            return music;
        }
        private set
        {

        }
    }
}
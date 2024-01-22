using System;
using System.Collections.Generic;
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
    private static readonly Random rand = new Random();

    public static void Load(Dictionary<string, SoundEffect> soundDict)
    {
        _hurt = soundDict["hurt"];
        _walkingSound = soundDict["walkingSound"];
        _spawnSound = soundDict["spawnSound"];
    }


}
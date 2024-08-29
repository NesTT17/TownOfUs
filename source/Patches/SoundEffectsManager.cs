using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

namespace TownOfUs
{
    // Class to preload all audio/sound effects that are contained in the embedded resources.
    // The effects are made available through the soundEffects Dict / the get and the play methods.
    public static class SoundEffectsManager
        
    {
        private static Dictionary<string, AudioClip> soundEffects = new();

        public static void Load()
        {
            soundEffects = new Dictionary<string, AudioClip>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resourceNames = assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.Contains("TownOfUs.Resources.SoundEffects.") && resourceName.Contains(".raw"))
                {
                    soundEffects.Add(resourceName, Utils.loadAudioClipFromResources(resourceName));
                }
            }
        }

        public static AudioClip get(string path)
        {
            // Convenience: As as SoundEffects are stored in the same folder, allow using just the name as well
            if (!path.Contains(".")) path = "TownOfUs.Resources.SoundEffects." + path + ".raw";
            AudioClip returnValue;
            return soundEffects.TryGetValue(path, out returnValue) ? returnValue : null;
        }


        public static void play(string path, float volume=0.8f, bool loop = false)
        {
            AudioClip clipToPlay = get(path);
            stop(path);
            if (Constants.ShouldPlaySfx() && clipToPlay != null) {
                AudioSource source = SoundManager.Instance.PlaySound(clipToPlay, false, volume);
                source.loop = loop;
            }
        }

        public static void stop(string path) {
            var soundToStop = get(path);
            if (soundToStop != null)
                if (Constants.ShouldPlaySfx()) SoundManager.Instance.StopSound(soundToStop);
        }

        public static void stopAll() {
            if (soundEffects == null) return;
            foreach (var path in soundEffects.Keys) stop(path);
        }
    }
}

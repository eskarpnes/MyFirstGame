﻿//
// Copyright 2017 Valve Corporation. All rights reserved. Subject to the following license:
// https://valvesoftware.github.io/steam-audio/license.html
//

using UnityEngine;
using UnityEditor;

namespace SteamAudio
{
    public class SteamAudioMixerReturnGUI : IAudioEffectPluginGUI
    {
        public override string Name
        {
            get { return "Steam Audio Mixer Return"; }
        }

        public override string Description
        {
            get { return "Enables Accelerated Mixing for sources spatialized using the Steam Audio Unity native plugin."; }
        }

        public override string Vendor
        {
            get { return "Valve Corporation"; }
        }

        public override bool OnGUI(IAudioEffectPlugin plugin)
        {
            if (steamAudioManager == null)
                steamAudioManager = GameObject.FindObjectOfType<SteamAudioManager>();

            if (steamAudioManager == null)
            {
                EditorGUILayout.HelpBox("A Steam Audio Manager does not exist in the scene. Click Window > Steam" +
                    " Audio.", MessageType.Error);
                return false;
            }

            if (steamAudioManager.audioEngine != AudioEngine.UnityNative)
            {
                EditorGUILayout.HelpBox("This Audio Mixer effect requires the audio engine to be set to Unity Native." +
                    " Click Window > Steam Audio to change this.", MessageType.Error);
                return false;
            }

            var binauralValue = 0.0f;
            plugin.GetFloatParameter("Binaural", out binauralValue);

            var binaural = (binauralValue == 1.0f);
            binaural = EditorGUILayout.Toggle("Binaural", binaural);
            binauralValue = (binaural) ? 1.0f : 0.0f;

            plugin.SetFloatParameter("Binaural", binauralValue);
            
            return false;
        }

        SteamAudioManager   steamAudioManager   = null;
    }
}
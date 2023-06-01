﻿using Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundsToggle;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundsSlider;


        private IPersistentProgress _persistentProgress;

        private void OnDestroy() => 
            Unsubscribe();

        public void Construct(IPersistentProgress persistentProgress)
        {
            _persistentProgress = persistentProgress;
            UpdateCurrentParameters();
            Subscribe();
        }

        private void Subscribe()
        {
            _musicToggle.onValueChanged.AddListener(ChangeMusicMuted);
            _soundsToggle.onValueChanged.AddListener(ChangeSoundsMuted);
            _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            _soundsSlider.onValueChanged.AddListener(ChangeSoundsVolume);
        }

        private void Unsubscribe()
        {
            _musicToggle.onValueChanged.RemoveListener(ChangeMusicMuted);
            _soundsToggle.onValueChanged.RemoveListener(ChangeSoundsMuted);
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
            _soundsSlider.onValueChanged.RemoveListener(ChangeSoundsVolume);
        }

        private void ChangeMusicVolume(float volume) => 
            _persistentProgress.MusicSettings.MusicVolume = volume;

        private void ChangeSoundsVolume(float volume) => 
            _persistentProgress.MusicSettings.SoundsVolume = volume;

        private void ChangeMusicMuted(bool muted) =>
            _persistentProgress.MusicSettings.MusicMuted = muted;

        private void ChangeSoundsMuted(bool muted) =>
            _persistentProgress.MusicSettings.SoundsMuted = muted;

        private void UpdateCurrentParameters()
        {
            _musicToggle.isOn = _persistentProgress.MusicSettings.MusicMuted;
            _soundsToggle.isOn = _persistentProgress.MusicSettings.SoundsMuted;

            _soundsSlider.value = _persistentProgress.MusicSettings.SoundsVolume;
            _musicSlider.value = _persistentProgress.MusicSettings.MusicVolume;
        }
    }
}
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;

namespace Connect4Game.SoundManag
{
    public class SoundManager
    {
        private readonly IAudioManager audioManager;

        public SoundManager(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public async Task PlaySoundAsync(string fileName)
        {
            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                if (stream == null)
                {
                    Console.WriteLine($"Stream for '{fileName}' is null.");
                    return;
                }

                var player = audioManager.CreatePlayer(stream);
                if (player == null)
                {
                    Console.WriteLine("Failed to create audio player.");
                    return;
                }

                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play sound '{fileName}': {ex.Message}");
            }

        }

        public Task PlayDropSound() => PlaySoundAsync("drop.mp3");
        public Task PlayResetSound() => PlaySoundAsync("reset.mp3");
    }
}

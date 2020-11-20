using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


namespace Moonshot.UI {
	public class Settings : MonoBehaviour {

		[SerializeField] private AudioMixer masterMixer;
		[SerializeField] private TMP_Dropdown resolutionDropdown;
		[SerializeField] private Toggle fullscreenToggle;

		Resolution[] resolutions;
		bool fullscreen;

		void Start() {
			fullscreen = Screen.fullScreen;
			fullscreenToggle.isOn = fullscreen;
			resolutions = Screen.resolutions;

			resolutionDropdown.ClearOptions();

			List<string> options = new List<string>();
			int currentResolutionIndex = 0;

			Debug.Log($"Current Resolution: {Screen.currentResolution.width} x {Screen.currentResolution.height}");
			for (int i = 0; i < resolutions.Length; i++) {
				Resolution _r = resolutions[i];

				string option = $"{_r.width} x {_r.height}";
				options.Add(option);

				if (_r.width == Screen.currentResolution.width && _r.height == Screen.currentResolution.height) {
					Debug.Log($"Setting Current Resolutiong {_r.width} x {_r.height}: {i}");
					currentResolutionIndex = i;
				}
			}

			resolutionDropdown.AddOptions(options);
			resolutionDropdown.value = currentResolutionIndex;
		}

		public void SetResolution(int selectedResolutionIndex) {
			Screen.SetResolution(
				resolutions[selectedResolutionIndex].width,
				resolutions[selectedResolutionIndex].height,
				fullscreen
				);
		}

		public void SetFullscreen(bool _fullscreen) {
			fullscreen = _fullscreen;
			Screen.fullScreen = _fullscreen;
		}

		public void AdjustVolume(float volume) {
			masterMixer.SetFloat("master_volume", volume);
		}
	}
}

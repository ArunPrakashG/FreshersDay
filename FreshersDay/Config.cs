using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace FreshersDay {
	public class ButtonInfo {
		[JsonProperty] public string ButtonName { get; set; } = "1";
		[JsonIgnore] public Button Button { get; set; }
		[JsonProperty] public bool IsDisabled { get; set; } = false;
		[JsonProperty] public string ButtonId { get; set; } = "task1";
		[JsonProperty] public bool EventRegistered { get; set; } = false;
		[JsonProperty] public string TaskTitle { get; set; } = "Here is your task...";
		[JsonProperty] public string TaskMessage { get; set; } = "വേപ്പില കഴിക്കുക";
		[JsonProperty] public bool IsGirlsTask { get; set; } = false;
		[JsonProperty] public string AudioFilePath { get; set; } = @"AudioFiles/1.wav";
	}

	public class Config {
		[JsonProperty]
		public List<ButtonInfo> TaskList { get; set; } = new List<ButtonInfo>();

		[JsonProperty] public string TaskNotificationSoundPath { get; set; }
		[JsonProperty] public int TaskWindowCloseDelayInMinutes { get; set; } = 2;
		[JsonProperty] public int TaskAudioPlayDelayInSeconds { get; set; } = 3;
		[JsonProperty] public string WindowTopTitle { get; set; }

		public Config SaveConfig(Config config) {
			JsonSerializer serializer = new JsonSerializer();
			JsonConvert.SerializeObject(config, Formatting.Indented);
			string pathName = @"Config.json";
			using (StreamWriter sw = new StreamWriter(pathName, false)) {
				using (JsonWriter writer = new JsonTextWriter(sw)) {
					writer.Formatting = Formatting.Indented;
					serializer.Serialize(writer, config);
					sw.Dispose();
					return config;
				}
			}
		}

		public Config LoadConfig() {
			if (!File.Exists(@"Config.json")) {
				GenerateDefaultConfig();
			}

			string JSON;
			using (FileStream Stream = new FileStream(@"Config.json", FileMode.Open, FileAccess.Read)) {
				using (StreamReader ReadSettings = new StreamReader(Stream)) {
					JSON = ReadSettings.ReadToEnd();
				}
			}

			Config returnConfig = JsonConvert.DeserializeObject<Config>(JSON);
			return returnConfig;
		}

		public bool GenerateDefaultConfig() {
			if (File.Exists(@"Config.json")) {
				return true;
			}

			Config Config = new Config();
			int i = 1;

			while (true) {
				if (i >= 37) {
					break;
				}

				Config.TaskList.Add(new ButtonInfo() {
					ButtonName = i.ToString(),
					ButtonId = $"task{i}",
					AudioFilePath = $@"AudioFiles/{i}.wav",
					IsGirlsTask = i > 0 && i <= 18
				});
				i++;
			}

			JsonSerializer serializer = new JsonSerializer();
			JsonConvert.SerializeObject(Config, Formatting.Indented);
			string pathName = @"Config.json";
			using (StreamWriter sw = new StreamWriter(pathName, false))
			using (JsonWriter writer = new JsonTextWriter(sw)) {
				writer.Formatting = Formatting.Indented;
				serializer.Serialize(writer, Config);
				sw.Dispose();
			}
			return true;
		}
	}
}

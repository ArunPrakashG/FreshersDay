using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using static FreshersDay.Config;

namespace FreshersDay {
	public class TaskConfig {
		[JsonIgnore]
		public Button Button { get; set; }

		[JsonIgnore]
		public bool IsDisabled { get; set; } 

		[JsonIgnore]
		public bool EventRegistered { get; set; }

		[JsonProperty]
		public string TaskMessage { get; set; }

		[JsonProperty]
		public TaskType TaskType { get; set; }

		[JsonProperty]
		public string AudioFilePath { get; set; }
	}

	public class Config {
		[JsonProperty]
		public List<TaskConfig> Tasks { get; set; }

		[JsonProperty]
		public string NotificationPath { get; set; }

		[JsonProperty]
		public int WindowCloseDelayMinutes { get; set; } = 2;

		[JsonProperty]
		public int TaskAudioDelaySeconds { get; set; } = 3;

		[JsonProperty]
		public string WindowTitle { get; set; }

		[JsonProperty]
		public bool TextToSpeech { get; set; }

		private const string ConfigPath = "config.json";

		public async Task<bool> SaveConfig() {
			try {
				var json = JsonConvert.SerializeObject(this, Formatting.Indented);
				using(FileStream stream = new FileStream(ConfigPath, FileMode.OpenOrCreate, FileAccess.Write)) {
					using(StreamWriter writer = new StreamWriter(stream)) {						
						await writer.WriteAsync(json);
					}
				}

				return true;
			}
			catch {
				return false;
			}			
		}

		public async Task<bool> LoadConfig() {
			if (!File.Exists(ConfigPath)) {
				await GenerateDefaultConfig();
			}

			try {
				string json;
				using (FileStream stream = new FileStream(ConfigPath, FileMode.Open, FileAccess.Read)) {
					using (StreamReader streamReader = new StreamReader(stream)) {
						json = await streamReader.ReadToEndAsync();
					}
				}

				var config = JsonConvert.DeserializeObject<Config>(json);
				Tasks = config.Tasks;
				NotificationPath = config.NotificationPath;
				WindowCloseDelayMinutes = config.WindowCloseDelayMinutes;
				TaskAudioDelaySeconds = config.TaskAudioDelaySeconds;
				WindowTitle = config.WindowTitle;
				return true;
			}
			catch {
				return false;
			}
		}

		public async Task<bool> GenerateDefaultConfig() {
			if (File.Exists(ConfigPath)) {
				return true;
			}

			for(int i = 0; i < 35; i++) {
				Tasks.Add(new TaskConfig() {
					AudioFilePath = "",
					TaskMessage = "",
					TaskType = i <= 18 ? TaskType.Girls : TaskType.Common
				});
			}

			WindowTitle = "BCA Freshers Day";
			return await SaveConfig().ConfigureAwait(false);
		}

		public enum TaskType {
			Girls,
			Boys,
			Punishment,
			Common
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ControlzEx.Native;
using Newtonsoft.Json;

namespace FreshersDay
{
	public class ButtonInfo
	{
		[JsonProperty] public string ButtonName { get; set; } = "1";
		public Button Button { get; set; }
		[JsonProperty] public bool IsDisabled { get; set; } = false;
		[JsonProperty] public bool EventRegistered { get; set; } = false;
		[JsonProperty] public string TaskTitle { get; set; } = "Here is your task...";
		[JsonProperty] public string TaskMessage { get; set; } = "വേപ്പില കഴിക്കുക";
		[JsonProperty] public bool IsGirlsTask { get; set; } = false;
		[JsonProperty] public string AudioFilePath { get; set; } = @"AudioFiles/1.wav";
	}

	public class Config
	{
		[JsonProperty]
		public List<ButtonInfo> TaskList { get; set; } = new List<ButtonInfo>();

		public Config SaveConfig(Config config)
		{
			JsonSerializer serializer = new JsonSerializer();
			JsonConvert.SerializeObject(config, Formatting.Indented);
			string pathName = @"Config.json";
			using (StreamWriter sw = new StreamWriter(pathName, false))
			{
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					writer.Formatting = Formatting.Indented;
					serializer.Serialize(writer, config);
					sw.Dispose();
					return config;
				}
			}
		}

		public Config LoadConfig()
		{
			if (!File.Exists(@"Config.json"))
			{
				GenerateDefaultConfig();
			}

			string JSON;
			using (FileStream Stream = new FileStream(@"Config.json", FileMode.Open, FileAccess.Read))
			{
				using (StreamReader ReadSettings = new StreamReader(Stream))
				{
					JSON = ReadSettings.ReadToEnd();
				}
			}

			Config returnConfig = JsonConvert.DeserializeObject<Config>(JSON);
			return returnConfig;
		}

		public bool GenerateDefaultConfig()
		{
			if (File.Exists(@"Config.json"))
			{
				return true;
			}

			Config Config = new Config();
			int i = 0;

			while (true)
			{
				if(i > 36)
				{
					break;
				}

				Config.TaskList.Add(new ButtonInfo()
				{
					ButtonName = i.ToString()
				});
				i++;
			}

			JsonSerializer serializer = new JsonSerializer();
			JsonConvert.SerializeObject(Config, Formatting.Indented);
			string pathName = @"Config.json";
			using (StreamWriter sw = new StreamWriter(pathName, false))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.Indented;
				serializer.Serialize(writer, Config);
				sw.Dispose();
			}
			return true;
		}
	}
}

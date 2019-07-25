using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace FreshersDay
{
	//public class ButtonInfo
	//{
	//	public Button Button { get; set; }
	//	public bool IsDisabled { get; set; }
	//	public bool EventRegistered { get; set; }
	//	public string TaskMessage { get; set; }
	//	public bool IsGirlsTask { get; set; }
	//	public string AudioFilePath { get; set; }
	//	public string TaskTitle { get; set; } = "Your task is...";
	//}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private Config TaskConfig = new Config();

		private void InitEvents()
		{
			TaskConfig = TaskConfig.LoadConfig();
			LoadButtons();

			//foreach (Button bttn in FindVisualChildren<Button>(grid))
			//{
			//	TaskButtonCollection.Add((bttn, new ButtonInfo()
			//	{
			//		Button = bttn,
			//		IsDisabled = false,
			//		EventRegistered = false,
			//		AudioFilePath = $"AudioFiles/{bttn.Content}.wav",
			//		IsGirlsTask = false,
			//		TaskMessage = SelectTask(bttn),
			//		TaskTitle = "Your task is..."
			//	}));
			//}

			//if (TaskButtonCollection.Count > 0)
			//{
			//	foreach ((Button, ButtonInfo) button in TaskButtonCollection)
			//	{
			//		button.Item1.Click += Button_Click;
			//		button.Item2.EventRegistered = true;
			//	}

			//	CoreInvoke(() => { lastTaskLabel.Content = $"Click on the respective button!"; });
			//}
		}

		private void LoadButtons()
		{
			foreach (Button bttn in FindVisualChildren<Button>(grid))
			{
				foreach (ButtonInfo task in TaskConfig.TaskList)
				{
					if (task.ButtonName.Equals(bttn.Content))
					{
						task.Button = bttn;
						task.Button.Click += Button_Click;
						task.EventRegistered = true;
					}
				}
			}

			CoreInvoke(() => { lastTaskLabel.Content = $"Click on the respective button!"; });
		}
		
		public static void ScheduleTask(Action action, TimeSpan delay)
		{
			if (action == null)
			{
				return;
			}

			Timer TaskSchedulerTimer = null;

			TaskSchedulerTimer = new Timer(e =>
			{
				InBackground(action);

				if (TaskSchedulerTimer != null)
				{
					TaskSchedulerTimer.Dispose();
					TaskSchedulerTimer = null;
				}
			}, null, delay, delay);
		}

		private void PlaySound(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
			{
				return;
			}

			ScheduleTask(() =>
			{
				MediaPlayer Player = new MediaPlayer();
				Player.Open(new Uri(filePath));
				Player.Play();
			}, TimeSpan.FromSeconds(2));
		}

		private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T) child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public static void InBackground(Action action, bool longRunning = false)
		{
			if (action == null)
			{
				return;
			}

			TaskCreationOptions options = TaskCreationOptions.DenyChildAttach;

			if (longRunning)
			{
				options |= TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness;
			}

			Task.Factory.StartNew(action, CancellationToken.None, options, TaskScheduler.Default);
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			InitEvents();
		}

		private ButtonInfo GetTaskInfo(Button clickedButton)
		{
			//if (clickedButton == null)
			//{
			//	return (task1, new ButtonInfo()
			//	{
			//		Button = task1,
			//		IsDisabled = false,
			//		EventRegistered = false,
			//		AudioFilePath = $"AudioFiles/{task1.Content}.wav",
			//		IsGirlsTask = false,
			//		TaskMessage = SelectTask(task1),
			//		TaskTitle = "Your task is..."
			//	}, 0);
			//}

			//for (int i = 0; i < TaskButtonCollection.Count; i++)
			//{
			//	if (TaskButtonCollection[i].Item1.Equals(clickedButton))
			//	{
			//		return (TaskButtonCollection[i].Item1, TaskButtonCollection[i].Item2, i);
			//	}
			//}

			//return (task1, new ButtonInfo()
			//{
			//	Button = task1,
			//	IsDisabled = false,
			//	EventRegistered = false,
			//	AudioFilePath = $"AudioFiles/{task1.Content}.wav",
			//	IsGirlsTask = false,
			//	TaskMessage = SelectTask(task1),
			//	TaskTitle = "Your task is..."
			//}, 0);

			foreach (ButtonInfo task in TaskConfig.TaskList)
			{
				if (task.Button == clickedButton)
				{
					return task;
				}
			}

			return new ButtonInfo()
			{
				Button = task1,
				IsDisabled = false,
				EventRegistered = false,
				AudioFilePath = $"AudioFiles/{task1.Content}.wav",
				IsGirlsTask = false,
				TaskMessage = SelectTask(task1),
				TaskTitle = "Your task is..."
			};
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			Button clickedButton = (Button) sender;

			if (clickedButton == null)
			{
				return;
			}

			clickedButton.Dispatcher.Invoke(() =>
			{
				clickedButton.Background = new SolidColorBrush(Colors.Gray);
				clickedButton.IsEnabled = false;
				clickedButton.Click -= Button_Click;
			});

			ButtonInfo buttonInfo = GetTaskInfo(clickedButton);

			buttonInfo.IsDisabled = true;
			buttonInfo.EventRegistered = false;

			CoreInvoke(() => { lastTaskLabel.Content = buttonInfo.TaskMessage; });
			PlaySound(buttonInfo.AudioFilePath);
			await ShowMessageDialog(buttonInfo.TaskTitle, buttonInfo.TaskMessage, MessageDialogStyle.Affirmative).ConfigureAwait(false);
		}

		private string SelectTask(Button clickedButton)
		{
			switch (clickedButton.Content)
			{
				case "1":
					{
						return "വേപ്പില കഴിക്കുക";
					}

				case "2":
					{
						return "ബെല്ലി ഡാൻസ്";
					}

				case "3":
					{
						return "ടിക് ടോക് അപാരതാ";
					}

				case "4":
					{
						return "പ്രൊപോസൽ ചെയ്യുക";
					}

				case "5":
					{
						return "പാവയ്ക്കാ ജൂസ് കുടിക്കുക";
					}

				case "6":
					{
						return "പുളി കഴിക്കുക (മുഖത്തെ ഏക്സ്പ്രെഷൻ മാറാതെ)";
					}

				case "7":
					{
						return "കുഞ്ഞിനെ ഉറക്കുക";
					}

				default:
					{
						return "വേപ്പില കഴിക്കുക";
					}
			}
		}

		public void CoreInvoke(Action action, Dispatcher dispatcher = null)
		{
			if (dispatcher == null)
			{
				dispatcher = Dispatcher;
			}

			dispatcher.Invoke(action);
		}

		public async Task<MessageDialogResult> ShowMessageDialog(string title = "Your task is...", string message = "ERROR: Unknown task", MessageDialogStyle style = MessageDialogStyle.Affirmative)
		{
			MessageDialogResult Result = new MessageDialogResult();

			await Dispatcher.Invoke(async () =>
			{
				Result = await this.ShowMessageAsync(title, message, style).ConfigureAwait(false);
			}).ConfigureAwait(false);
			return Result;
		}

		public async Task<ProgressDialogController> ShowProgressDialog(string title = "Please wait...", string message = "ERROR: Unknown task", bool cancelable = false)
		{
			ProgressDialogController Controller = null;
			await Dispatcher.Invoke(async () =>
			{
				Controller = await this.ShowProgressAsync(title, message, cancelable).ConfigureAwait(false);
			}).ConfigureAwait(false);
			return Controller;
		}

		private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			TaskConfig.SaveConfig(TaskConfig);
		}
	}
}

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace FreshersDay {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow {
		public MainWindow() {
			InitializeComponent();
		}

		private Config TaskConfig { get; set; } = new Config();
		private int ClickedButtonCount { get; set; }
		private List<string> TaskCollectionList { get; set; } = new List<string>() {
			"വേപ്പില കഴിക്കുക",
			"ബെല്ലി ഡാൻസ്",
			"ടിക് ടോക് അപാരതാ",
			"പ്രൊപോസൽ ചെയ്യുക",
			"പാവയ്ക്കാ ജൂസ് കുടിക്കുക",
			"പുളി കഴിക്കുക (മുഖത്തെ ഏക്സ്പ്രെഷൻ മാറാതെ)",
			"കുഞ്ഞിനെ ഉറക്കുക"
		};

		private async void InitEvents() {
			ProgressDialogController progressDialog = await ShowProgressDialog("Loading...", "Loading tasks...").ConfigureAwait(false);
			TaskConfig = TaskConfig.LoadConfig();
			progressDialog.SetProgress(0.8);
			await Task.Delay(100).ConfigureAwait(false);
			progressDialog.SetMessage("Loading task buttons...");
			CoreInvoke(LoadButtons);
			await Task.Delay(130).ConfigureAwait(false);
			progressDialog.SetProgress(1);
			await progressDialog.CloseAsync().ConfigureAwait(false);
		}

		private void LoadButtons() {
			foreach (Button bttn in FindVisualChildren<Button>(grid)) {
				foreach (ButtonInfo task in TaskConfig.TaskList) {
					if (bttn.Name.Equals(task.ButtonId, StringComparison.OrdinalIgnoreCase)) {
						task.Button = bttn;
						bttn.Background = task.IsGirlsTask ? Brushes.Pink : Brushes.DeepSkyBlue;
						bttn.Click += Button_Click;
						task.EventRegistered = true;
						task.IsDisabled = false;
						bttn.Content = task.ButtonName;
					}
				}
			}

			CoreInvoke(() => { lastTaskLabel.Content = $"Click on the respective button!"; });
		}

		public static void ScheduleTask(Action action, TimeSpan delay) {
			if (action == null) {
				return;
			}

			Timer TaskSchedulerTimer = null;

			TaskSchedulerTimer = new Timer(e => {
				InBackground(action);

				if (TaskSchedulerTimer != null) {
					TaskSchedulerTimer.Dispose();
					TaskSchedulerTimer = null;
				}
			}, null, delay, delay);
		}

		private void PlaySound(string filePath) {
			if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath)) {
				return;
			}

			if (!File.Exists(filePath)) {
				return;
			}

			ScheduleTask(() => {
				SoundPlayer Player = new SoundPlayer(filePath);
				Player.Play();
			}, TimeSpan.FromSeconds(TaskConfig.TaskAudioPlayDelayInSeconds));
		}

		private void PlaySound(string filePath, bool noDelay) {
			if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath)) {
				return;
			}

			if (!File.Exists(filePath)) {
				return;
			}

			SoundPlayer Player = new SoundPlayer(filePath);
			Player.Play();
		}

		private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
			if (depObj != null) {
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T) {
						yield return (T) child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child)) {
						yield return childOfChild;
					}
				}
			}
		}

		public static void InBackground(Action action, bool longRunning = false) {
			if (action == null) {
				return;
			}

			TaskCreationOptions options = TaskCreationOptions.DenyChildAttach;

			if (longRunning) {
				options |= TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness;
			}

			Task.Factory.StartNew(action, CancellationToken.None, options, TaskScheduler.Default);
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) => InitEvents();

		private ButtonInfo GetTaskInfo(Button clickedButton) {
			foreach (ButtonInfo task in TaskConfig.TaskList) {
				if (task.Button == clickedButton) {
					return task;
				}
			}

			return new ButtonInfo() {
				Button = task1,
				IsDisabled = false,
				EventRegistered = false,
				AudioFilePath = $"AudioFiles/{task1.Content}.wav",
				IsGirlsTask = Convert.ToInt32(task1.Content) > 0 && Convert.ToInt32(task1.Content) <= 18,
				TaskMessage = SelectRandomTask(),
				TaskTitle = "Your task is..."
			};
		}

		private async void Button_Click(object sender, RoutedEventArgs e) {
			Button clickedButton = (Button) sender;
			ClickedButtonCount++;

			if (clickedButton == null) {
				return;
			}

			if ((!string.IsNullOrEmpty(TaskConfig.TaskNotificationSoundPath) ||
				 !string.IsNullOrWhiteSpace(TaskConfig.TaskNotificationSoundPath)) &&
				File.Exists(TaskConfig.TaskNotificationSoundPath)) {
				PlaySound(TaskConfig.TaskNotificationSoundPath, true);
			}

			clickedButton.Dispatcher.Invoke(() => {
				clickedButton.Background = new SolidColorBrush(Colors.Gray);
				clickedButton.IsEnabled = false;
				clickedButton.Click -= Button_Click;
			});

			int remainingTasks = 36 - ClickedButtonCount;
			CoreInvoke(() => { remainingLabel.Content = $"{remainingTasks}/36"; });

			ButtonInfo buttonInfo = GetTaskInfo(clickedButton);

			buttonInfo.IsDisabled = true;
			buttonInfo.EventRegistered = false;

			CoreInvoke(() => { lastTaskLabel.Content = buttonInfo.TaskMessage; });
			PlaySound(buttonInfo.AudioFilePath);
			ScheduleTask(() => { StimulateKeypress(VirtualKeyCode.ESCAPE); }, TimeSpan.FromMinutes(TaskConfig.TaskWindowCloseDelayInMinutes));
			await ShowMessageDialog(buttonInfo.TaskTitle, buttonInfo.TaskMessage, MessageDialogStyle.Affirmative).ConfigureAwait(false);
		}

		private void StimulateKeypress(VirtualKeyCode keyCode = VirtualKeyCode.ESCAPE) {
			InputSimulator inputSim = new InputSimulator();
			inputSim.Keyboard.KeyDown(VirtualKeyCode.ESCAPE);
		}

		private string SelectRandomTask() {
			Random random = new Random();
			int index = random.Next(TaskCollectionList.Count);
			return TaskCollectionList[index];
		}

		public void CoreInvoke(Action action, Dispatcher dispatcher = null) {
			if (dispatcher == null) {
				dispatcher = Dispatcher;
			}

			dispatcher.Invoke(action);
		}

		public async Task<MessageDialogResult> ShowMessageDialog(string title = "Your task is...", string message = "ERROR: Unknown task", MessageDialogStyle style = MessageDialogStyle.Affirmative) {
			MessageDialogResult Result = await this.ShowMessageAsync(title, message, style, new MetroDialogSettings() {
				OwnerCanCloseWithDialog = true
			});
			return Result;
		}

		public async Task<ProgressDialogController> ShowProgressDialog(string title = "Please wait...", string message = "ERROR: Unknown task", bool cancelable = false) {
			ProgressDialogController Controller = null;
			await Dispatcher.Invoke(async () => {
				Controller = await this.ShowProgressAsync(title, message, cancelable).ConfigureAwait(false);
			}).ConfigureAwait(false);
			return Controller;
		}

		private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			TaskConfig.SaveConfig(TaskConfig);
		}
	}
}

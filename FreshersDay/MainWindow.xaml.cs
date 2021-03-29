using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace FreshersDay {

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow {
		private static readonly Random Random;
		private static readonly Config Config;

		static MainWindow() {
			Random = new Random();
			Config = new Config();
		}

		public MainWindow() {
			InitializeComponent();
			Loaded += async (sender, e) => {
				ProgressDialogController progressDialog = await ShowProgressDialog("Loading...", "Loading tasks...").ConfigureAwait(false);
				await Config.LoadConfig();

				Dispatcher.Invoke(() => ProgramTitle.Content = Config.WindowTitle ?? "...");

				Dispatcher.Invoke(() => {
					if (!Config.EnablePunishmentTasks) {
						PunishmentTasksGrid.IsEnabled = false;
						PunishmentTasksGrid.Visibility = Visibility.Collapsed;
					}
				});				
				
				progressDialog.SetProgress(0.8);
				await Task.Delay(100).ConfigureAwait(false);
				progressDialog.SetMessage("Loading Task Buttons...");
				Dispatcher.Invoke(InitTaskButtons);
				await Task.Delay(130).ConfigureAwait(false);
				Dispatcher.Invoke(() => RemainingTasksLabel.Content = $"{GetAvailableTasksCount()}/{GetTotalTasksCount()}");
				progressDialog.SetProgress(1);
				await progressDialog.CloseAsync().ConfigureAwait(false);
			};
		}

		private void InitTaskButtons() {
			if(Config.Tasks == null) {
				return;
			}

			InitTasksForElement(TasksGrid, default);
			InitTasksForElement(PunishmentTasksGrid, Config.TaskType.Punishment);
		}

		private void InitTasksForElement(DependencyObject element, Config.TaskType taskType = default) {
			int index = 0;
			foreach (var bttn in FindVisualChildren<Button>(element)) {
				if (bttn == null) {
					continue;
				}

				try {
					bttn.Click += OnTaskButtonClicked;
					//bttn.Content = (index + 1).ToString();
					var task = taskType == Config.TaskType.Punishment ? Config.Tasks.Where(x => x.TaskType == Config.TaskType.Punishment).ElementAtOrDefault(index) : Config.Tasks.ElementAtOrDefault(index);

					if (task == null) {
						bttn.IsEnabled = false;
						bttn.Background = Brushes.Gray;
						continue;
					}

					task.Button = bttn;
					task.EventRegistered = true;
					task.IsDisabled = false;

					switch (task.TaskType) {
						case Config.TaskType.Punishment:
							bttn.Background = Brushes.Coral;
							break;
						case Config.TaskType.Girls:
							bttn.Background = Brushes.Pink;
							break;
						case Config.TaskType.Boys:
							bttn.Background = Brushes.DeepSkyBlue;
							break;
						case Config.TaskType.Common:
							bttn.Background = Brushes.White;
							break;
					}
				}
				catch(Exception e) {
					Console.WriteLine(e);
					continue;
				}
				finally {
					index++;
				}
			}
		}

		private async void OnTaskButtonClicked(object sender, RoutedEventArgs e) {
			Button clickedButton = sender as Button;
			var taskConfig = GetTaskForClickedButton(clickedButton);

			PlaySound(Config.NotificationPath, TimeSpan.Zero);

			clickedButton.Dispatcher.Invoke(() => {
				clickedButton.Background = Brushes.Gray;
				clickedButton.IsEnabled = false;				
				clickedButton.Click -= OnTaskButtonClicked;
			});

			Dispatcher.Invoke(() => {
				RemainingTasksLabel.Content = $"{GetAvailableTasksCount()}/{GetTotalTasksCount()}";
				PreviousTaskLabel.Content = taskConfig.TaskMessage;
			});

			clickedButton.IsEnabled = false;
			taskConfig.EventRegistered = false;
			taskConfig.IsDisabled = true;
			clickedButton.Click -= OnTaskButtonClicked;

			PlaySound(taskConfig.AudioFilePath, TimeSpan.FromSeconds(Config.TaskAudioDelaySeconds));
			Timer timer = ScheduleTask(() => { StimulateKeypress(VirtualKeyCode.ESCAPE); }, TimeSpan.FromMinutes(Config.WindowCloseDelayMinutes));
			await TextToSpeech(taskConfig.TaskMessage);
			await ShowMessageDialog("Your Task is ...", taskConfig.TaskMessage, MessageDialogStyle.Affirmative).ConfigureAwait(false);
			timer?.Dispose();
		}

		private void ResetTasks(Config.TaskType taskType) {
			foreach (var task in Config.Tasks) {
				if (task.TaskType != taskType) {
					continue;
				}

				if (task.Button == null || task.Button.IsEnabled) {
					continue;
				}

				Dispatcher.Invoke(() => task.Button.IsEnabled = true);
				task.IsDisabled = false;

				if (!task.EventRegistered) {
					task.Button.Click += OnTaskButtonClicked;
					task.EventRegistered = true;					
				}

				switch (task.TaskType) {
					case Config.TaskType.Punishment:
						Dispatcher.Invoke(() => task.Button.Background = Brushes.Coral);
						break;
					case Config.TaskType.Girls:
						Dispatcher.Invoke(() => task.Button.Background = Brushes.Pink);
						break;
					case Config.TaskType.Boys:
						Dispatcher.Invoke(() => task.Button.Background = Brushes.DeepSkyBlue);
						break;
					case Config.TaskType.Common:
						Dispatcher.Invoke(() => task.Button.Background = Brushes.White);
						break;
				}
			}

			Dispatcher.Invoke(() => RemainingTasksLabel.Content = $"{GetAvailableTasksCount()}/{GetTotalTasksCount()}");
		}

		private void PunishmentButtonDoubleClick(object sender, MouseButtonEventArgs e) => ResetTasks(Config.TaskType.Punishment);

		private void TaskResetDoubleClick(object sender, MouseButtonEventArgs e) {
			ResetTasks(Config.TaskType.Boys);
			ResetTasks(Config.TaskType.Girls);
			ResetTasks(Config.TaskType.Common);
			ResetTasks(Config.TaskType.Punishment);
		}

		private TaskConfig GetTaskForClickedButton(Button clickedButton) {
			if(clickedButton == null) {
				return null;
			}

			try {
				foreach (var task in Config.Tasks) {
					if (task.Button.Equals(clickedButton)) {
						return task;
					}
				}

				return null;
			}
			catch {
				return null;
			}			
		}

		private async Task<bool> TextToSpeech(string text) {
			if (!Config.TextToSpeech) {
				return false;
			}

			Regex regex = new Regex("^[a-zA-Z0-9. -_?]*$");

			if (string.IsNullOrEmpty(text) || !regex.IsMatch(text)) {
				return false;
			}

			SpeechSynthesizer synthesizer = new SpeechSynthesizer {
				Volume = 100,
				Rate = -2			
			};

			synthesizer.SetOutputToDefaultAudioDevice();
			Prompt result = synthesizer.SpeakAsync(text);
			return await Task.FromResult(result.IsCompleted);
		}

		private void SelectRandomAvailableTask() {
			List<Button> taskableButtons = new List<Button>();

			try {
				foreach (Button bttn in FindVisualChildren<Button>(TasksGrid)) {
					if (!bttn.IsEnabled) {
						continue;
					}

					taskableButtons.Add(bttn);
				}

				var taskButton = taskableButtons.ElementAt(Random.Next(0, taskableButtons.Count));
				taskButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
			catch { }
		}

		public static Timer ScheduleTask(Action action, TimeSpan delay) {
			if (action == null) {
				return null;
			}

			Timer TaskSchedulerTimer = null;

			TaskSchedulerTimer = new Timer(e => {
				InBackground(action);

				if (TaskSchedulerTimer != null) {
					TaskSchedulerTimer.Dispose();
					TaskSchedulerTimer = null;
				}
			}, null, delay, delay);
			return TaskSchedulerTimer;
		}

		private void PlaySound(string filePath, TimeSpan delay) {
			if(string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) {
				return;
			}

			SoundPlayer Player = new SoundPlayer(filePath);

			if(delay == TimeSpan.Zero) {
				Player.Play();
				return;
			}

			ScheduleTask(() => {				
				Player.Play();
			}, delay);
		}

		private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
			if (depObj != null) {
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T t) {
						yield return t;
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

		private int GetAvailableTasksCount() {
			int totalAvailableButtons = 0;
			foreach (var bttn in FindVisualChildren<Button>(TasksGrid)) {
				if (bttn.IsEnabled) {
					totalAvailableButtons++;
				}
			}

			foreach (var bttn in FindVisualChildren<Button>(PunishmentTasksGrid)) {
				if (bttn.IsEnabled) {
					totalAvailableButtons++;
				}
			}

			return totalAvailableButtons;
		}

		private int GetTotalTasksCount() {
			if (!Config.EnablePunishmentTasks) {
				return Config.Tasks.Count - 6;
			}

			return Config.Tasks.Count;
		}

		private void StimulateKeypress(VirtualKeyCode keyCode = VirtualKeyCode.ESCAPE) => new InputSimulator().Keyboard.KeyDown(keyCode);

		public async Task<MessageDialogResult> ShowMessageDialog(string title = "Your task is...", string message = "ERROR: Unknown task", MessageDialogStyle style = MessageDialogStyle.Affirmative) {
			MessageDialogResult Result = MessageDialogResult.Affirmative;
			await Dispatcher.Invoke(async () => {
				Result = await this.ShowMessageAsync(title, message, style, new MetroDialogSettings() { OwnerCanCloseWithDialog = true });
			}).ConfigureAwait(false);

			return Result;
		}

		public async Task<ProgressDialogController> ShowProgressDialog(string title = "Please wait...", string message = "ERROR: Unknown task", bool cancelable = false) {
			ProgressDialogController Controller = null;
			await Dispatcher.Invoke(async () => {
				Controller = await this.ShowProgressAsync(title, message, cancelable).ConfigureAwait(false);
			}).ConfigureAwait(false);
			return Controller;
		}

		

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			SelectRandomAvailableTask();
		}
	}
}

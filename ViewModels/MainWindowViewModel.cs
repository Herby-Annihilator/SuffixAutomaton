using System;
using System.Collections.Generic;
using System.Text;
using SuffixAutomaton.ViewModels.Base;
using SuffixAutomaton.Data;
using SuffixAutomaton.Model;
using System.Windows.Input;
using System.Windows;
using SuffixAutomaton.Infrastructure.Commands;
using System.IO;

namespace SuffixAutomaton.ViewModels
{
	class MainWindowViewModel : ViewModel
	{
		private SuffixAutomatonManager manager;
		public MainWindowViewModel()
		{
			manager = new SuffixAutomatonManager(null);
			Title = "Hello, nigas";
			SaveToFileCommand = new LambdaCommand(OnSaveToFileCommandExecuted, CanSaveToFileCommandExecute);
			RestoreFromFileCommand = new LambdaCommand(OnRestoreFromFileCommandExecuted, CanRestoreFromFileCommandExecute);
			BuiltAutomatonCommand = new LambdaCommand(OnBuiltAutomatonCommandExecuted, CanBuiltAutomatonCommandExecute);
			StartSearchCommand = new LambdaCommand(OnStartSearchCommandExecuted, CanStartSearchCommandExecute);
		}
		private string title;
		public string Title { get => title; set => Set(ref title, value); }

		private string status;
		public string Status { get => status; set => Set(ref status, value); }

		private List<StringOccurrenceInfo> stringOccurrenceInfo = new List<StringOccurrenceInfo>();
		public List<StringOccurrenceInfo> StringOccurrenceInfo
		{
			get => stringOccurrenceInfo;
			set => Set(ref stringOccurrenceInfo, value);
		}

		private string sourceText;
		public string SourceText { get => sourceText; set => Set(ref sourceText, value); }

		private List<ConditionsPairIDs> conditionsPairIDs;
		public List<ConditionsPairIDs> ConditionsPairIDs { get => conditionsPairIDs; 
			private set => Set(ref conditionsPairIDs, manager.GetConditionsPairIDs()); }

		private string matrix;
		public string Matrix { get => matrix; private set => Set(ref matrix, value); }

		#region Commands
		public ICommand SaveToFileCommand { get; }
		private void OnSaveToFileCommandExecuted(object param)
		{
			StreamWriter writer = new StreamWriter("input.dat");
			writer.Write(SourceText);
			writer.Close();
			Status = "Сохранено";
		}
		private bool CanSaveToFileCommandExecute(object param)
		{
			return !string.IsNullOrWhiteSpace(SourceText);
		}

		public ICommand RestoreFromFileCommand { get; }

		private void OnRestoreFromFileCommandExecuted(object param)
		{
			try
			{
				StreamReader reader = new StreamReader("input.dat");
				SourceText = reader.ReadToEnd();
				reader.Close();
				Status = "Восстановлено";
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				Status = "Не выполнено. Причина: " + e.Message;
			}
		}
		private bool CanRestoreFromFileCommandExecute(object param)
		{
			return File.Exists("input.dat");
		}

		public ICommand BuiltAutomatonCommand { get; }

		private void OnBuiltAutomatonCommandExecuted(object param)
		{
			try
			{
				Matrix = "";
				manager.SourceText = SourceText;
				manager.BuiltSuffixAutomaton();
				ConditionsPairIDs = manager.GetConditionsPairIDs();
				char[][] matr = manager.ShowMatrix();
				for (int i = 0; i < matr.GetLength(0); i++)
				{
					for (int j = 0; j < matr[i].GetLength(0); j++)
					{
						Matrix += ((matr[i][j] - 48) < 0 ? 130 : (matr[i][j] - 48)) + ", ";
					}
					Matrix += "\r\n";
				}
				Status = "Автомат простроен";
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				Status = "Не выполнено. Причина: " + e.Message;
			}
		}

		private bool CanBuiltAutomatonCommandExecute(object param)
		{
			return !string.IsNullOrWhiteSpace(SourceText);
		}

		public ICommand StartSearchCommand { get; }
		private void OnStartSearchCommandExecuted(object param)
		{
			Status = StringOccurrenceInfo.Count.ToString();
			for (int i = 0; i < StringOccurrenceInfo.Count; i++)
			{
				if (StringOccurrenceInfo[i] != null)
				{
					manager.CountTheNumberOfOccurences(StringOccurrenceInfo[i]);
				}
			}
		}
		private bool CanStartSearchCommandExecute(object param)
		{
			return !(StringOccurrenceInfo.Count == 0) && manager.Exist();
		}
		#endregion
	}
}

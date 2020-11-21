using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuffixAutomaton.Data
{
	internal class StringOccurrenceInfo : INotifyPropertyChanged
	{
		private string substring;
		public string Substring { get => substring; set => Set(ref substring, value); }

		private int occurrencesCount;
		public int OccurrencesCount { get => occurrencesCount; set => Set(ref occurrencesCount, value); }

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}

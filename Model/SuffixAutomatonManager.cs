using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary.DataStructures.SuffixAutomaton;
using SuffixAutomaton.Data;

namespace SuffixAutomaton.Model
{
	internal class SuffixAutomatonManager
	{
		private MyLibrary.DataStructures.SuffixAutomaton.SuffixAutomaton suffixAutomaton;
		public string SourceText { get; set; }
		public List<StringOccurrenceInfo> StringOccurrenceInfo { get; set; }

		public SuffixAutomatonManager(string sourceText)
		{
			SourceText = sourceText;
			StringOccurrenceInfo = new List<StringOccurrenceInfo>();
		}

		public void BuiltSuffixAutomaton()
		{
			if (!string.IsNullOrWhiteSpace(SourceText))
			{
				suffixAutomaton = new MyLibrary.DataStructures.SuffixAutomaton.SuffixAutomaton(SourceText);
			}
		}

		public char[][] ShowMatrix()
		{
			return suffixAutomaton.GetMatrix();
		}
		public List<ConditionsPairIDs> GetConditionsPairIDs()
		{
			List<ConditionsPairIDs> toReturn = new List<ConditionsPairIDs>();
			int[][] pairs = suffixAutomaton.GetConditionsOfSuffixLinks();
			for (int i = 0; i < pairs.GetLength(0); i++)
			{
				toReturn.Add(new ConditionsPairIDs(pairs[i][0], pairs[i][1]));
			}
			return toReturn;
		}

		public bool Exist() => suffixAutomaton?.IsBuilted ?? false;

		public void CountTheNumberOfOccurences(StringOccurrenceInfo info)
		{
			if (!string.IsNullOrEmpty(info.Substring))
				info.OccurrencesCount = suffixAutomaton.CountSubstringOccurences(info.Substring);
			else
				info.OccurrencesCount = 0;
		}
	}
}

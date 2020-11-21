using System;
using System.Collections.Generic;
using System.Text;

namespace SuffixAutomaton.Data
{
	public class ConditionsPairIDs
	{
		public int FirstConditionID { get; private set; }
		public int SecondConditionID { get; private set; }
		public Direction Direction { get; private set; }
		public ConditionsPairIDs(int first, int second)
		{
			Direction = Direction.Right;
			FirstConditionID = first;
			SecondConditionID = second;
		}
	}
	public enum Direction 
	{
		Left,
		Right,
		Up,
		Down,
	}
}

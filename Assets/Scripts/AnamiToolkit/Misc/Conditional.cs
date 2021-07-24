using System.Collections;
using System.Collections.Generic;

namespace AnamiToolkit.Misc
{
	public class Conditional
	{
		public Conditional()
		{
			m_conditions = new List<string>();
		}

		public Conditional( List<string> conditions )
		{
			m_conditions = conditions;
		}

		public void AddCondition( string condition )
		{
			m_conditions.Add ( condition );
		}

		public bool ConditionsMet()
		{
			foreach ( string condition in m_conditions )
			{
				if ( !m_conditionsCompleted.Contains( condition ) )
					return false;
			}

			return true;
		}

		public bool ConditionMet( string condition )
		{
			return m_conditionsCompleted.Contains ( condition );
		}

		public static void CompleteCondition( string condition )
		{
			if ( m_conditionsCompleted == null )
				m_conditionsCompleted = new List<string> ();

			m_conditionsCompleted.Add ( condition );
		}

		private List<string> m_conditions;
		private static List<string> m_conditionsCompleted;
	}

}
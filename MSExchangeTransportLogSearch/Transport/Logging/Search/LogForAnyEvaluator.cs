using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000013 RID: 19
	internal class LogForAnyEvaluator : LogQuantifierEvaluator
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003C5C File Offset: 0x00001E5C
		public override bool Evaluate(LogCursor row)
		{
			Array array = row.GetField(this.FieldIndex) as Array;
			if (array == null)
			{
				return false;
			}
			if (this.Condition == null)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				this.Variable.Value = array.GetValue(i);
				if (this.Condition.Evaluate(row))
				{
					return true;
				}
			}
			return false;
		}
	}
}

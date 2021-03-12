using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000014 RID: 20
	internal class LogForEveryEvaluator : LogQuantifierEvaluator
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public override bool Evaluate(LogCursor row)
		{
			Array array = row.GetField(this.FieldIndex) as Array;
			if (array == null)
			{
				return true;
			}
			if (this.Condition == null)
			{
				return true;
			}
			for (int i = 0; i < array.Length; i++)
			{
				this.Variable.Value = array.GetValue(i);
				if (!this.Condition.Evaluate(row))
				{
					return false;
				}
			}
			return true;
		}
	}
}

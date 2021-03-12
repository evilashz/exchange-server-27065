using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000A RID: 10
	internal class LogAndEvaluator : LogCompoundEvaluator
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public override bool Evaluate(LogCursor row)
		{
			for (int i = 0; i < this.Conditions.Count; i++)
			{
				if (!this.Conditions[i].Evaluate(row))
				{
					return false;
				}
			}
			return true;
		}
	}
}

using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000B RID: 11
	internal class LogOrEvaluator : LogCompoundEvaluator
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003B18 File Offset: 0x00001D18
		public override bool Evaluate(LogCursor row)
		{
			for (int i = 0; i < this.Conditions.Count; i++)
			{
				if (this.Conditions[i].Evaluate(row))
				{
					return true;
				}
			}
			return false;
		}
	}
}

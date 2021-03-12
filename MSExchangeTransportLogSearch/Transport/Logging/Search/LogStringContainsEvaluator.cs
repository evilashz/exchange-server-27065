using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001B RID: 27
	internal class LogStringContainsEvaluator : LogBinaryStringOperatorEvaluator
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003F38 File Offset: 0x00002138
		public override bool Evaluate(LogCursor row)
		{
			string text = this.Left.GetValue(row) as string;
			string text2 = this.Right.GetValue(row) as string;
			if (this.IgnoreCase)
			{
				if (!this.Left.AssumeUppercase)
				{
					text = text.ToUpperInvariant();
				}
				if (!this.Right.AssumeUppercase)
				{
					text2 = text2.ToUpperInvariant();
				}
			}
			return text.Contains(text2);
		}
	}
}

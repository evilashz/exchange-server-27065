using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001A RID: 26
	internal class LogStringEndsWithEvaluator : LogBinaryStringOperatorEvaluator
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003EC8 File Offset: 0x000020C8
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
			return text.EndsWith(text2);
		}
	}
}

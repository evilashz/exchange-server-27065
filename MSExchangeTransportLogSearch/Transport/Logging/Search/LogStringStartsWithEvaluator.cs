using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000019 RID: 25
	internal class LogStringStartsWithEvaluator : LogBinaryStringOperatorEvaluator
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003E58 File Offset: 0x00002058
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
			return text.StartsWith(text2);
		}
	}
}

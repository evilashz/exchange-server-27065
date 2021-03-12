using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000017 RID: 23
	internal class LogStringComparisonEvaluator : LogComparisonEvaluator
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003DFC File Offset: 0x00001FFC
		public override bool Evaluate(LogCursor row)
		{
			string strA = this.Left.GetValue(row) as string;
			string strB = this.Right.GetValue(row) as string;
			int result = string.Compare(strA, strB, this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			return base.ApplyOperator(result);
		}

		// Token: 0x04000033 RID: 51
		public bool IgnoreCase;
	}
}

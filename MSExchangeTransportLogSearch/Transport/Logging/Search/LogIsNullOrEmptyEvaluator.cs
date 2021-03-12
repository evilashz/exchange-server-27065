using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001D RID: 29
	internal class LogIsNullOrEmptyEvaluator : LogUnaryOperatorEvaluator
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003FB0 File Offset: 0x000021B0
		public override bool Evaluate(LogCursor row)
		{
			object value = this.Operand.GetValue(row);
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			return text != null && text.Length == 0;
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000011 RID: 17
	internal class LogFieldEvaluator : LogOperandEvaluator
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public override object GetValue(LogCursor row)
		{
			object field = row.GetField(this.Index);
			if (this.Index == 10)
			{
				string messageId = field as string;
				return CsvFieldCache.NormalizeMessageID(messageId);
			}
			return field;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003C27 File Offset: 0x00001E27
		public override Type GetValueType(CsvTable table)
		{
			return table.Fields[this.Index].Type;
		}

		// Token: 0x0400002D RID: 45
		public int Index = -1;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000010 RID: 16
	internal class LogConstantEvaluator : LogOperandEvaluator
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public override object GetValue(LogCursor row)
		{
			return this.Value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003BC9 File Offset: 0x00001DC9
		public override Type GetValueType(CsvTable table)
		{
			if (this.Value != null)
			{
				return this.Value.GetType();
			}
			return typeof(object);
		}

		// Token: 0x0400002C RID: 44
		public object Value;
	}
}

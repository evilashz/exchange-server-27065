using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200000F RID: 15
	internal class LogVariableEvaluator : LogOperandEvaluator
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003B83 File Offset: 0x00001D83
		public override object GetValue(LogCursor row)
		{
			return this.Value;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B8C File Offset: 0x00001D8C
		public override Type GetValueType(CsvTable table)
		{
			Type type = table.Fields[this.BoundFieldIndex].Type;
			return type.GetElementType();
		}

		// Token: 0x0400002A RID: 42
		public object Value;

		// Token: 0x0400002B RID: 43
		public int BoundFieldIndex = -1;
	}
}

using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B4 RID: 180
	internal class Int64TraceFormatter : TraceFormatter<long>
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000127B7 File Offset: 0x000109B7
		public override void Format(ITraceBuilder builder, long value)
		{
			builder.AddArgument(value);
		}
	}
}

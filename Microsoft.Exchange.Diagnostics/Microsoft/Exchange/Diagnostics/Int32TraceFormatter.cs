using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B3 RID: 179
	internal class Int32TraceFormatter : TraceFormatter<int>
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x000127A6 File Offset: 0x000109A6
		public override void Format(ITraceBuilder builder, int value)
		{
			builder.AddArgument(value);
		}
	}
}

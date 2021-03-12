using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B5 RID: 181
	internal class GuidTraceFormatter : TraceFormatter<Guid>
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000127C8 File Offset: 0x000109C8
		public override void Format(ITraceBuilder builder, Guid value)
		{
			builder.AddArgument(value);
		}
	}
}

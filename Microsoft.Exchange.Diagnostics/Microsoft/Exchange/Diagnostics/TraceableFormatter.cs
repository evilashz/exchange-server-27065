using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B6 RID: 182
	internal class TraceableFormatter<T> : TraceFormatter<T> where T : ITraceable
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x000127D9 File Offset: 0x000109D9
		public override void Format(ITraceBuilder builder, T value)
		{
			if (value != null)
			{
				value.TraceTo(builder);
				return;
			}
			builder.AddArgument(string.Empty);
		}
	}
}

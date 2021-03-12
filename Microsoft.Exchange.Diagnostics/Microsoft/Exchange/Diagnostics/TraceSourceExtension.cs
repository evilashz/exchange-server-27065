using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001E4 RID: 484
	internal static class TraceSourceExtension
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x00039539 File Offset: 0x00037739
		public static void SetMaxDataSize(this TraceSource traceSource, int value)
		{
			traceSource.Attributes["maxdatasize"] = value.ToString();
		}
	}
}

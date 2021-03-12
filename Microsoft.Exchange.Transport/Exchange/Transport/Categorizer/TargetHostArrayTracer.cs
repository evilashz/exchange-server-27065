using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000275 RID: 629
	internal class TargetHostArrayTracer : ArrayTracerWithWrapper<TargetHost, TargetHostTraceWrapper>
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x0006FD39 File Offset: 0x0006DF39
		public TargetHostArrayTracer(TargetHost[] array) : base(array)
		{
		}
	}
}

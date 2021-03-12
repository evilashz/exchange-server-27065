using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x0200000E RID: 14
	internal interface IParser
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000064 RID: 100
		IEnumerable<Guid> Guids { get; }

		// Token: 0x06000065 RID: 101
		unsafe void Parse(EtwTraceNativeComponents.EVENT_RECORD* rawData);
	}
}

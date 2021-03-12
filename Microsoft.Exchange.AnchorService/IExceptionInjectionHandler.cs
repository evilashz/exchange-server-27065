using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000022 RID: 34
	public interface IExceptionInjectionHandler
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018E RID: 398
		ExceptionInjectionCallback Callback { get; }
	}
}

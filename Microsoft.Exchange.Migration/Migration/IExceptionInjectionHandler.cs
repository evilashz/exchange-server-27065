using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000008 RID: 8
	public interface IExceptionInjectionHandler
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29
		ExceptionInjectionCallback Callback { get; }
	}
}

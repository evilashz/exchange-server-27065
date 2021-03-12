using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000011 RID: 17
	public interface IDisposableImpl : IDisposable
	{
		// Token: 0x0600021E RID: 542
		DisposeTracker InternalGetDisposeTracker();

		// Token: 0x0600021F RID: 543
		void InternalDispose(bool calledFromDispose);
	}
}

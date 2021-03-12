using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000004 RID: 4
	public static class DisposeTrackerFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002117 File Offset: 0x00000317
		public static IDisposable Get<T>(T trackable) where T : IDisposable
		{
			return DisposeTracker.Get<T>(trackable);
		}
	}
}

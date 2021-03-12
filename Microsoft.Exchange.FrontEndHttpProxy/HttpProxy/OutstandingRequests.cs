using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000097 RID: 151
	internal static class OutstandingRequests
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00019F54 File Offset: 0x00018154
		internal static void AddRequest(ProxyRequestHandler request)
		{
			if (OutstandingRequests.TrackOutstandingRequests.Value)
			{
				try
				{
					OutstandingRequests.hashsetLock.EnterWriteLock();
					OutstandingRequests.requests.Add(request);
				}
				finally
				{
					try
					{
						OutstandingRequests.hashsetLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			PerfCounters.HttpProxyCountersInstance.OutstandingProxyRequests.Increment();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00019FC4 File Offset: 0x000181C4
		internal static void RemoveRequest(ProxyRequestHandler request)
		{
			if (OutstandingRequests.TrackOutstandingRequests.Value)
			{
				try
				{
					OutstandingRequests.hashsetLock.EnterWriteLock();
					OutstandingRequests.requests.Remove(request);
				}
				finally
				{
					try
					{
						OutstandingRequests.hashsetLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			PerfCounters.HttpProxyCountersInstance.OutstandingProxyRequests.Decrement();
		}

		// Token: 0x04000386 RID: 902
		private static readonly BoolAppSettingsEntry TrackOutstandingRequests = new BoolAppSettingsEntry(HttpProxySettings.Prefix("TrackOutstandingRequests"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000387 RID: 903
		private static ReaderWriterLockSlim hashsetLock = new ReaderWriterLockSlim();

		// Token: 0x04000388 RID: 904
		private static HashSet<ProxyRequestHandler> requests = new HashSet<ProxyRequestHandler>();
	}
}

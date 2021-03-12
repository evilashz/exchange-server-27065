using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B4 RID: 180
	internal class SessionDataCache : DisposeTrackableBase
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000156A8 File Offset: 0x000138A8
		internal MemoryStream OutputStream
		{
			get
			{
				MemoryStream result;
				try
				{
					Monitor.Enter(this.lockObject);
					ExAssert.RetailAssert(this.outputStream != null, "Output stream should be initialized before using.");
					ExAssert.RetailAssert(this.state == SessionDataCacheState.Building || this.state == SessionDataCacheState.Obsolete, "Output stream should not be invoked in any other state than building or obsolete: " + this.state.ToString());
					result = this.outputStream;
				}
				finally
				{
					if (Monitor.IsEntered(this.lockObject))
					{
						Monitor.Exit(this.lockObject);
					}
				}
				return result;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00015740 File Offset: 0x00013940
		internal UTF8Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00015748 File Offset: 0x00013948
		internal bool StartBuilding()
		{
			bool flag = false;
			bool result;
			try
			{
				Monitor.Enter(this.lockObject);
				if (this.state == SessionDataCacheState.Uninitialized)
				{
					this.signalEvent.Reset();
					this.lastSessionDataBuildStartTime = ExDateTime.Now;
					this.lastSessionDataBuildEndTime = ExDateTime.MinValue;
					this.outputStream = new MemoryStream();
					this.state = SessionDataCacheState.Building;
					flag = true;
				}
				result = flag;
			}
			finally
			{
				if (Monitor.IsEntered(this.lockObject))
				{
					Monitor.Exit(this.lockObject);
				}
				ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), string.Format("[SessionDataCache] Started building session data cache. StartTime = {0}, state = {1}", this.lastSessionDataBuildStartTime.ToString(), this.state));
				OwaSingleCounters.SessionDataCacheBuildsStarted.Increment();
			}
			return result;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00015830 File Offset: 0x00013A30
		internal void CompleteBuilding()
		{
			try
			{
				Monitor.Enter(this.lockObject);
				ExAssert.RetailAssert(this.state == SessionDataCacheState.Building || this.state == SessionDataCacheState.Obsolete, "Ready building session data in an invalid state: " + this.state);
				if (this.state == SessionDataCacheState.Obsolete)
				{
					this.InternalDispose(true);
				}
				else
				{
					this.lastSessionDataBuildEndTime = ExDateTime.Now;
					this.state = SessionDataCacheState.Ready;
					this.signalEvent.Set();
					Timer staleTimer = null;
					staleTimer = new Timer(delegate(object param0)
					{
						this.Dispose();
						DisposeGuard.DisposeIfPresent(staleTimer);
					}, null, SessionDataCache.FreshnessTime, Timeout.InfiniteTimeSpan);
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.lockObject))
				{
					Monitor.Exit(this.lockObject);
				}
				ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), string.Format("[SessionDataCache] Ready building session data cache. StartTime = {0}, EndTime = {1}, state = {2}", this.lastSessionDataBuildStartTime.ToString(), this.lastSessionDataBuildEndTime.ToString(), this.state));
				OwaSingleCounters.SessionDataCacheBuildsCompleted.Increment();
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001595C File Offset: 0x00013B5C
		internal void UseIfReady(RequestDetailsLogger logger, Stream responseStream, out bool used)
		{
			ExAssert.RetailAssert(responseStream != null, "responseStream is null");
			used = false;
			if (this.state != SessionDataCacheState.Obsolete)
			{
				bool flag = false;
				RequestDetailsLogger.LogEvent(logger, SessionDataMetadata.SessionDataCacheFirstTimeRetriveveBegin);
				this.InternalUseIfReady(responseStream, out used, out flag);
				RequestDetailsLogger.LogEvent(logger, SessionDataMetadata.SessionDataCacheFirstTimeRetriveveEnd);
				if (flag)
				{
					OwaSingleCounters.SessionDataCacheWaitedForPreload.Increment();
					bool flag2 = this.signalEvent.WaitOne(5000);
					if (!flag2)
					{
						OwaSingleCounters.SessionDataCacheWaitTimeout.Increment();
					}
					RequestDetailsLogger.LogEvent(logger, SessionDataMetadata.SessionDataCacheSecondTimeRetriveveBegin);
					this.InternalUseIfReady(responseStream, out used, out flag);
					RequestDetailsLogger.LogEvent(logger, SessionDataMetadata.SessionDataCacheSecondTimeRetriveveEnd);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(logger, SessionDataMetadata.SessionDataCacheWaitTimeOut, !flag2);
				}
				if (used)
				{
					OwaSingleCounters.SessionDataCacheUsed.Increment();
				}
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(logger, SessionDataMetadata.SessionDataCacheUsed, used);
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00015A38 File Offset: 0x00013C38
		private void InternalUseIfReady(Stream responseStream, out bool used, out bool needToWait)
		{
			needToWait = false;
			used = false;
			try
			{
				Monitor.Enter(this.lockObject);
				if (this.state == SessionDataCacheState.Obsolete)
				{
					ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), string.Format("[SessionDataCache] Not using session data cache. It has already been consumed or never will be = {0}", this.state));
				}
				else if (this.state == SessionDataCacheState.Uninitialized)
				{
					ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), string.Format("[SessionDataCache] Not using session data cache. Preload has not started yet = {0}", this.state));
					this.state = SessionDataCacheState.Obsolete;
				}
				else if (this.state == SessionDataCacheState.Ready && this.lastSessionDataBuildEndTime >= ExDateTime.Now.Add(-SessionDataCache.FreshnessTime))
				{
					this.outputStream.Seek(0L, SeekOrigin.Begin);
					this.outputStream.CopyTo(responseStream);
					this.outputStream.Dispose();
					this.outputStream = null;
					used = true;
					this.state = SessionDataCacheState.Obsolete;
				}
				else if (this.state == SessionDataCacheState.Building)
				{
					needToWait = true;
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.lockObject))
				{
					Monitor.Exit(this.lockObject);
				}
				ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), string.Format("[SessionDataCache] Trying to use session data cache. Obsolete = {0}, needToWait = {1}, state = {2}", used, needToWait, this.state));
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00015B98 File Offset: 0x00013D98
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.lockObject)
				{
					if (this.state != SessionDataCacheState.Building)
					{
						DisposeGuard.DisposeIfPresent(this.outputStream);
					}
					this.state = SessionDataCacheState.Obsolete;
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00015BF0 File Offset: 0x00013DF0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SessionDataCache>(this);
		}

		// Token: 0x040003E2 RID: 994
		private const int WaitTime = 5000;

		// Token: 0x040003E3 RID: 995
		private static readonly TimeSpan FreshnessTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x040003E4 RID: 996
		private readonly object lockObject = new object();

		// Token: 0x040003E5 RID: 997
		private readonly ManualResetEvent signalEvent = new ManualResetEvent(false);

		// Token: 0x040003E6 RID: 998
		private MemoryStream outputStream;

		// Token: 0x040003E7 RID: 999
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x040003E8 RID: 1000
		private SessionDataCacheState state;

		// Token: 0x040003E9 RID: 1001
		private ExDateTime lastSessionDataBuildStartTime = ExDateTime.MinValue;

		// Token: 0x040003EA RID: 1002
		private ExDateTime lastSessionDataBuildEndTime = ExDateTime.MinValue;
	}
}

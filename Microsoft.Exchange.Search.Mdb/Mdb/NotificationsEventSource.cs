using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000026 RID: 38
	internal class NotificationsEventSource : INotificationsEventSource, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00007C78 File Offset: 0x00005E78
		internal NotificationsEventSource(MdbInfo mdbInfo)
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("NotificationsEventSource", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.MdbNotificationsFeederTracer, (long)this.GetHashCode());
			this.mdbInfo = mdbInfo;
			this.componentGuid = ComponentInstance.Globals.Search.Id;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007CDF File Offset: 0x00005EDF
		private MapiEventManager EventManager
		{
			get
			{
				if (this.eventManager == null)
				{
					this.RefreshEventManager();
				}
				return this.eventManager;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007CF5 File Offset: 0x00005EF5
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationsEventSource>(this);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007CFD File Offset: 0x00005EFD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007D0C File Offset: 0x00005F0C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007D58 File Offset: 0x00005F58
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, ReadEventsFlags flags, out long endCounter)
		{
			long outEndCounter = 0L;
			MapiEvent[] result;
			lock (this.lockObject)
			{
				this.CheckDisposed();
				try
				{
					MapiEvent[] array = MapiUtil.TranslateMapiExceptionsWithReturnValue<MapiEvent[]>(this.diagnosticsSession, Strings.FailedToReadNotifications(this.mdbInfo.Guid), () => this.EventManager.ReadEvents(startCounter, eventCountWanted, 0, null, flags, false, out outEndCounter));
					endCounter = outEndCounter;
					result = array;
				}
				catch (ComponentException arg)
				{
					this.diagnosticsSession.TraceError<ComponentException>("Failed to read events. Exception = {0}", arg);
					this.CleanupEventManager();
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007E40 File Offset: 0x00006040
		public MapiEvent ReadLastEvent()
		{
			MapiEvent result;
			lock (this.lockObject)
			{
				this.CheckDisposed();
				try
				{
					result = MapiUtil.TranslateMapiExceptionsWithReturnValue<MapiEvent>(this.diagnosticsSession, Strings.FailedToReadNotifications(this.mdbInfo.Guid), () => this.EventManager.ReadLastEvent(false));
				}
				catch (ComponentException arg)
				{
					this.diagnosticsSession.TraceError<ComponentException>("Failed to read last event. Exception = {0}", arg);
					this.CleanupEventManager();
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007ED8 File Offset: 0x000060D8
		public long ReadFirstEventCounter()
		{
			this.diagnosticsSession.TraceDebug("Get the first event in the system", new object[0]);
			long num;
			MapiEvent[] array = this.ReadEvents(0L, 1, ReadEventsFlags.IncludeMoveDestinationEvents, out num);
			num = ((array.Length > 0) ? array[0].EventCounter : num);
			this.diagnosticsSession.TraceDebug<long>("Event {0} was found.", num);
			return num;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007F2C File Offset: 0x0000612C
		public long GetNetworkLatency(int samples)
		{
			if (samples <= 0)
			{
				throw new ArgumentOutOfRangeException("samples");
			}
			this.diagnosticsSession.TraceDebug<MdbInfo>("Calculating network latency for {0}", this.mdbInfo);
			this.ReadLastEvent();
			Stopwatch stopwatch = new Stopwatch();
			long num = 0L;
			for (int i = 0; i < samples; i++)
			{
				if (i != 0)
				{
					Thread.Sleep(TimeSpan.FromSeconds(1.0));
				}
				stopwatch.Restart();
				this.ReadLastEvent();
				stopwatch.Stop();
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				long timeInStore = this.GetTimeInStore();
				num += elapsedMilliseconds - timeInStore;
				this.diagnosticsSession.TraceDebug<int, long, long>("Sample {0}, elapsed: {1} ms, time in store: {2} ms", i + 1, elapsedMilliseconds, timeInStore);
			}
			long num2 = num / (long)samples;
			this.diagnosticsSession.TraceDebug<MdbInfo, long>("Network latency for {0}: {1} ms", this.mdbInfo, num2);
			return num2;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007FF0 File Offset: 0x000061F0
		private void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.lockObject)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					this.disposed = true;
					this.CleanupEventManager();
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008080 File Offset: 0x00006280
		private long GetTimeInStore()
		{
			long result;
			lock (this.lockObject)
			{
				this.CheckDisposed();
				try
				{
					result = MapiUtil.TranslateMapiExceptionsWithReturnValue<long>(this.diagnosticsSession, Strings.FailedToReadNotifications(this.mdbInfo.Guid), () => (long)this.exRpcAdmin.GetStorePerRPCStats().timeInServer.TotalMilliseconds);
				}
				catch (ComponentException arg)
				{
					this.diagnosticsSession.TraceError<ComponentException>("Failed to calculate network latency. Exception = {0}", arg);
					this.CleanupEventManager();
					throw;
				}
			}
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008118 File Offset: 0x00006318
		private void CleanupEventManager()
		{
			this.diagnosticsSession.TraceDebug("Clean up event manager", new object[0]);
			this.eventManager = null;
			if (this.exRpcAdmin != null)
			{
				this.exRpcAdmin.Dispose();
				this.exRpcAdmin = null;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000081D0 File Offset: 0x000063D0
		private void RefreshEventManager()
		{
			this.diagnosticsSession.TraceDebug<MdbInfo>("Refresh a new event manager for server {0}", this.mdbInfo);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ISearchServiceConfig searchConfig = Factory.Current.CreateSearchServiceConfig();
				this.exRpcAdmin = MapiUtil.TranslateMapiExceptionsWithReturnValue<ExRpcAdmin>(this.diagnosticsSession, Strings.FailedToOpenAdminRpcConnection, () => ExRpcAdmin.Create("Client=CI", (searchConfig.ReadFromPassiveEnabled && !this.mdbInfo.IsLagCopy) ? LocalServer.GetServer().Fqdn : this.mdbInfo.OwningServer, null, null, null));
				disposeGuard.Add<ExRpcAdmin>(this.exRpcAdmin);
				this.eventManager = MapiUtil.TranslateMapiExceptionsWithReturnValue<MapiEventManager>(this.diagnosticsSession, Strings.FailedCreateEventManager(this.mdbInfo.Guid), () => MapiEventManager.Create(this.exRpcAdmin, this.componentGuid, this.mdbInfo.Guid));
				disposeGuard.Success();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000082A4 File Offset: 0x000064A4
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x040000B7 RID: 183
		private readonly MdbInfo mdbInfo;

		// Token: 0x040000B8 RID: 184
		private readonly Guid componentGuid;

		// Token: 0x040000B9 RID: 185
		private readonly object lockObject = new object();

		// Token: 0x040000BA RID: 186
		private IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000BB RID: 187
		private ExRpcAdmin exRpcAdmin;

		// Token: 0x040000BC RID: 188
		private MapiEventManager eventManager;

		// Token: 0x040000BD RID: 189
		private volatile bool disposed;

		// Token: 0x040000BE RID: 190
		private DisposeTracker disposeTracker;
	}
}

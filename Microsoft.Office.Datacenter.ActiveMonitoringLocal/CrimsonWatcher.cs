using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000072 RID: 114
	internal abstract class CrimsonWatcher<T> : CrimsonOperation<T> where T : IPersistence, new()
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001B822 File Offset: 0x00019A22
		internal CrimsonWatcher(EventBookmark bookmark, bool isReadExistingEvents) : this(bookmark, isReadExistingEvents, null)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B82D File Offset: 0x00019A2D
		internal CrimsonWatcher(EventBookmark bookmark, bool isReadExistingEvents, string channelName) : base(bookmark, channelName)
		{
			this.isReadExistingEvents = isReadExistingEvents;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001B84C File Offset: 0x00019A4C
		internal void InitializeIfRequired()
		{
			if (!base.IsInitialized)
			{
				EventLogQuery queryObject = base.GetQueryObject();
				EventBookmark bookMark = base.BookMark;
				this.watcher = new EventLogWatcher(queryObject, bookMark, this.isReadExistingEvents);
				this.watcher.EventRecordWritten += this.EventArrivedHandler;
				base.IsInitialized = true;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		internal override void Cleanup()
		{
			this.Stop();
			lock (this.locker)
			{
				if (this.watcher != null)
				{
					this.watcher.Dispose();
					this.watcher = null;
				}
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B94C File Offset: 0x00019B4C
		internal void Start(bool isSyncMode)
		{
			if (this.stopRequested)
			{
				return;
			}
			this.InitializeIfRequired();
			if (isSyncMode)
			{
				this.watcher.Enabled = true;
				return;
			}
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				lock (this.locker)
				{
					if (!this.stopRequested)
					{
						this.watcher.Enabled = true;
					}
				}
			});
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B991 File Offset: 0x00019B91
		internal void Start()
		{
			this.Start(false);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B99A File Offset: 0x00019B9A
		internal void Stop()
		{
			this.stopRequested = true;
			this.watcher.Enabled = false;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		protected void EventArrivedHandler(object sender, EventRecordWrittenEventArgs arg)
		{
			try
			{
				if (!this.stopRequested)
				{
					if (arg != null && arg.EventRecord != null)
					{
						using (EventRecord eventRecord = arg.EventRecord)
						{
							T o = base.EventToObject(eventRecord);
							this.ResultArrivedHandler(o);
						}
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.DataAccessTracer, TracingContext.Default, string.Format("EventArrivedHandler excepiton: {0}", ex.ToString()), null, "EventArrivedHandler", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonWatcher.cs", 187);
			}
		}

		// Token: 0x0600069A RID: 1690
		protected abstract void ResultArrivedHandler(T o);

		// Token: 0x04000441 RID: 1089
		private readonly bool isReadExistingEvents;

		// Token: 0x04000442 RID: 1090
		private EventLogWatcher watcher;

		// Token: 0x04000443 RID: 1091
		private object locker = new object();

		// Token: 0x04000444 RID: 1092
		private bool stopRequested;
	}
}

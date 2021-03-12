using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PrimingDispatchDriver : DisposeTrackableBase, IDispatchDriver
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00010134 File Offset: 0x0000E334
		public PrimingDispatchDriver(SyncLogSession syncLogSession, TimeSpan primingDispatchTime)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
			this.workThreadsEvent = new ManualResetEvent(true);
			this.primerTimerWorker = new Timer(new TimerCallback(this.PrimingWorkerCallback), null, primingDispatchTime, primingDispatchTime);
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000260 RID: 608 RVA: 0x00010196 File Offset: 0x0000E396
		// (remove) Token: 0x06000261 RID: 609 RVA: 0x0001019F File Offset: 0x0000E39F
		public event EventHandler<EventArgs> PrimingEvent
		{
			add
			{
				this.InternalPrimingEvent += value;
			}
			remove
			{
				this.InternalPrimingEvent -= value;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000262 RID: 610 RVA: 0x000101A8 File Offset: 0x0000E3A8
		// (remove) Token: 0x06000263 RID: 611 RVA: 0x000101E0 File Offset: 0x0000E3E0
		private event EventHandler<EventArgs> InternalPrimingEvent;

		// Token: 0x06000264 RID: 612 RVA: 0x00010218 File Offset: 0x0000E418
		public void AddDiagnosticInfoTo(XElement componentElement)
		{
			componentElement.Add(new XElement("lastPrimerStartTime", (this.lastPrimerStartTime != null) ? this.lastPrimerStartTime.Value.ToString("o") : string.Empty));
			TimeSpan? timeSpan = null;
			if (this.lastPrimerStartTime != null)
			{
				timeSpan = new TimeSpan?(ExDateTime.UtcNow - this.lastPrimerStartTime.Value);
			}
			componentElement.Add(new XElement("timeSinceLastPrimerStart", (timeSpan != null) ? timeSpan.Value.ToString() : string.Empty));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000102D1 File Offset: 0x0000E4D1
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.primerTimerWorker != null)
			{
				this.workThreadsEvent.WaitOne();
				this.primerTimerWorker.Dispose();
				this.primerTimerWorker = null;
				this.workThreadsEvent.Close();
				this.workThreadsEvent = null;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001030E File Offset: 0x0000E50E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PrimingDispatchDriver>(this);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010318 File Offset: 0x0000E518
		private void PrimingWorkerCallback(object state)
		{
			this.syncLogSession.LogDebugging((TSLID)431UL, "PrimingWorkerCallback", new object[0]);
			if (Monitor.TryEnter(this.primerLock))
			{
				this.workThreadsEvent.Reset();
				try
				{
					this.lastPrimerStartTime = new ExDateTime?(ExDateTime.UtcNow);
					if (this.InternalPrimingEvent != null)
					{
						this.InternalPrimingEvent(this, null);
					}
					return;
				}
				finally
				{
					this.workThreadsEvent.Set();
					Monitor.Exit(this.primerLock);
				}
			}
			this.syncLogSession.LogDebugging((TSLID)432UL, "Primer is still running, unable to run again this interval", new object[0]);
		}

		// Token: 0x04000151 RID: 337
		private Timer primerTimerWorker;

		// Token: 0x04000152 RID: 338
		private object primerLock = new object();

		// Token: 0x04000153 RID: 339
		private ManualResetEvent workThreadsEvent;

		// Token: 0x04000154 RID: 340
		private SyncLogSession syncLogSession;

		// Token: 0x04000155 RID: 341
		private ExDateTime? lastPrimerStartTime = null;
	}
}

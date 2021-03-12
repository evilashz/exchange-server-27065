using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Compliance.Logging
{
	// Token: 0x020007D0 RID: 2000
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GccProtocolActivityLoggerSession : DisposeTrackableBase
	{
		// Token: 0x06004B03 RID: 19203 RVA: 0x00139D88 File Offset: 0x00137F88
		public GccProtocolActivityLoggerSession(StoreSession storeSession)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.storeSessionReference = GCHandle.Alloc(storeSession, GCHandleType.Weak);
				this.sessionResourceIdentifier = "Unknown";
				this.sessionClientIPAddress = storeSession.ClientIPAddress;
				this.sessionServerIPAddress = storeSession.ServerIPAddress;
				this.sessionTimestamp = ExDateTime.MinValue;
				this.gccLogger = GccProtocolActivityLoggerSingleton.Get(storeSession.ClientInfoString);
				this.needToLog = false;
				if (this.gccLogger != null)
				{
					this.timeoutTimer = new System.Timers.Timer();
					this.timeoutTimer.Elapsed += this.LoggerTimeoutEventHandler;
					this.timeoutTimer.Interval = GccProtocolActivityLogger.Config.ReportIntervalMilliseconds;
					this.timeoutTimer.AutoReset = false;
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00139E68 File Offset: 0x00138068
		~GccProtocolActivityLoggerSession()
		{
			base.Dispose(false);
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x00139E98 File Offset: 0x00138098
		// (set) Token: 0x06004B06 RID: 19206 RVA: 0x00139EA0 File Offset: 0x001380A0
		public bool MessagesWereDownloaded { get; set; }

		// Token: 0x06004B07 RID: 19207 RVA: 0x00139EA9 File Offset: 0x001380A9
		public void StartSession()
		{
			this.InternalStartSession();
			this.TagCurrentIntervalAsLogworthy();
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00139EB7 File Offset: 0x001380B7
		public void EndSession()
		{
			if (this.gccLogger == null)
			{
				return;
			}
			if (this.timeoutTimer != null)
			{
				this.timeoutTimer.Stop();
				this.timeoutTimer.Dispose();
				this.timeoutTimer = null;
			}
			this.InternalEndSession();
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00139EED File Offset: 0x001380ED
		public void SetStoreSessionInfo(string gccResourceIdentifier, IPAddress clientIPAddress, IPAddress serverIPAddress)
		{
			this.sessionResourceIdentifier = gccResourceIdentifier;
			this.sessionClientIPAddress = clientIPAddress;
			this.sessionServerIPAddress = serverIPAddress;
			this.TagCurrentIntervalAsLogworthy();
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00139F0A File Offset: 0x0013810A
		public void TagCurrentIntervalAsLogworthy()
		{
			this.needToLog = true;
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00139F13 File Offset: 0x00138113
		private void InternalStartSession()
		{
			this.sessionTimestamp = ExDateTime.UtcNow;
			if (this.timeoutTimer != null)
			{
				this.timeoutTimer.Start();
			}
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00139F34 File Offset: 0x00138134
		public void InternalEndSession()
		{
			if (!this.needToLog)
			{
				if (this.timeoutTimer != null)
				{
					this.timeoutTimer.Start();
				}
				return;
			}
			if (Interlocked.Increment(ref this.useCount) > 1)
			{
				if (this.timeoutTimer != null)
				{
					this.timeoutTimer.Start();
				}
				Interlocked.Decrement(ref this.useCount);
				return;
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			try
			{
				this.gccLogger.Append(this.sessionResourceIdentifier, this.sessionClientIPAddress, this.sessionServerIPAddress, this.sessionTimestamp, utcNow - this.sessionTimestamp, this.MessagesWereDownloaded);
			}
			finally
			{
				Interlocked.Decrement(ref this.useCount);
				this.needToLog = false;
				if (this.timeoutTimer != null)
				{
					this.timeoutTimer.Start();
				}
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0013A000 File Offset: 0x00138200
		private void LoggerTimeoutEventHandler(object sender, ElapsedEventArgs e)
		{
			if (this.storeSessionReference.IsAllocated)
			{
				this.InternalEndSession();
				this.InternalStartSession();
				return;
			}
			this.EndSession();
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0013A022 File Offset: 0x00138222
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.timeoutTimer != null)
			{
				this.timeoutTimer.Dispose();
				this.timeoutTimer = null;
			}
			this.storeSessionReference.Free();
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0013A04C File Offset: 0x0013824C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<GccProtocolActivityLoggerSession>(this);
		}

		// Token: 0x040028DC RID: 10460
		private GCHandle storeSessionReference;

		// Token: 0x040028DD RID: 10461
		private string sessionResourceIdentifier;

		// Token: 0x040028DE RID: 10462
		private IPAddress sessionClientIPAddress;

		// Token: 0x040028DF RID: 10463
		private IPAddress sessionServerIPAddress;

		// Token: 0x040028E0 RID: 10464
		private ExDateTime sessionTimestamp;

		// Token: 0x040028E1 RID: 10465
		private GccProtocolActivityLogger gccLogger;

		// Token: 0x040028E2 RID: 10466
		private bool needToLog;

		// Token: 0x040028E3 RID: 10467
		private System.Timers.Timer timeoutTimer;

		// Token: 0x040028E4 RID: 10468
		private int useCount;
	}
}

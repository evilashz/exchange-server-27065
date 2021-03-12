using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200098F RID: 2447
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SiteMailboxSynchronizer : DisposeTrackableBase
	{
		// Token: 0x06005A42 RID: 23106 RVA: 0x00176924 File Offset: 0x00174B24
		public SiteMailboxSynchronizer(IExchangePrincipal siteMailboxPrincipal, string client)
		{
			if (!SiteMailboxSynchronizer.initialized)
			{
				lock (SiteMailboxSynchronizer.initSyncObject)
				{
					if (!SiteMailboxSynchronizer.initialized)
					{
						SiteMailboxSynchronizer.SyncInterval = TimeSpan.FromSeconds((double)StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\SiteMailbox", "SyncInterval", 900, (int x) => x > 0));
						SiteMailboxSynchronizer.initialized = true;
					}
				}
			}
			this.clientString = client + "_SiteMailboxSychronizer";
			this.siteMailboxPrincipal = siteMailboxPrincipal;
			this.ScheduleSynchronization(TimeSpan.FromMilliseconds(0.0));
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x00176A0F File Offset: 0x00174C0F
		public bool TryToSyncNow()
		{
			return this.TryThreadSafeCall(delegate
			{
				this.ScheduleSynchronization(TimeSpan.FromMilliseconds(0.0));
				return true;
			});
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x00176A36 File Offset: 0x00174C36
		public bool TryToDispose()
		{
			return this.TryThreadSafeCall(delegate
			{
				if (this.hasBeenSyncedOnce)
				{
					this.Dispose();
					return true;
				}
				return false;
			});
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x00176A4A File Offset: 0x00174C4A
		public Guid MailboxGuid
		{
			get
			{
				base.CheckDisposed();
				return this.siteMailboxPrincipal.MailboxInfo.MailboxGuid;
			}
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x00176B08 File Offset: 0x00174D08
		private void OnSynchronize(object state)
		{
			this.ThreadSafeCall(delegate
			{
				if (base.IsDisposed)
				{
					return;
				}
				this.hasBeenSyncedOnce = true;
				if (this.synchronizerTimer != null)
				{
					this.synchronizerTimer.Dispose();
					this.synchronizerTimer = null;
				}
				try
				{
					if (this.lastMembershipSyncTime + SiteMailboxSynchronizer.SyncInterval < DateTime.UtcNow)
					{
						this.lastMembershipSyncTime = DateTime.UtcNow;
						Utils.TriggerSiteMailboxSync(this.siteMailboxPrincipal, this.clientString, false);
					}
					else
					{
						Utils.TriggerSiteMailboxSync(this.siteMailboxPrincipal, this.clientString, true);
					}
				}
				finally
				{
					this.ScheduleSynchronization(SiteMailboxSynchronizer.SyncInterval);
				}
			});
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x00176BA8 File Offset: 0x00174DA8
		private void ScheduleSynchronization(TimeSpan dueTime)
		{
			this.ThreadSafeCall(delegate
			{
				if (this.IsDisposed)
				{
					return;
				}
				if (this.synchronizerTimer == null)
				{
					this.synchronizerTimer = new Timer(new TimerCallback(this.OnSynchronize), null, dueTime, TimeSpan.FromMilliseconds(-1.0));
					return;
				}
				this.synchronizerTimer.Change(dueTime, TimeSpan.FromMilliseconds(-1.0));
			});
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x00176BDB File Offset: 0x00174DDB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SiteMailboxSynchronizer>(this);
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x00176BFF File Offset: 0x00174DFF
		protected override void InternalDispose(bool disposing)
		{
			this.ThreadSafeCall(delegate
			{
				if (this.synchronizerTimer != null)
				{
					this.synchronizerTimer.Dispose();
					this.synchronizerTimer = null;
				}
			});
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x00176C14 File Offset: 0x00174E14
		private void ThreadSafeCall(Action functionDelegate)
		{
			try
			{
				Monitor.Enter(this.threadSafeLock);
				functionDelegate();
			}
			finally
			{
				if (Monitor.IsEntered(this.threadSafeLock))
				{
					Monitor.Exit(this.threadSafeLock);
				}
			}
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x00176C60 File Offset: 0x00174E60
		private bool TryThreadSafeCall(Func<bool> functionDelegate)
		{
			try
			{
				if (Monitor.TryEnter(this.threadSafeLock))
				{
					return functionDelegate();
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.threadSafeLock))
				{
					Monitor.Exit(this.threadSafeLock);
				}
			}
			return false;
		}

		// Token: 0x040031D2 RID: 12754
		private const string RegKeySiteMailbox = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\SiteMailbox";

		// Token: 0x040031D3 RID: 12755
		private const string RegValueSyncInterval = "SyncInterval";

		// Token: 0x040031D4 RID: 12756
		private static bool initialized = false;

		// Token: 0x040031D5 RID: 12757
		private static readonly object initSyncObject = new object();

		// Token: 0x040031D6 RID: 12758
		private readonly string clientString;

		// Token: 0x040031D7 RID: 12759
		private static TimeSpan SyncInterval = TimeSpan.MaxValue;

		// Token: 0x040031D8 RID: 12760
		private readonly IExchangePrincipal siteMailboxPrincipal;

		// Token: 0x040031D9 RID: 12761
		private Timer synchronizerTimer;

		// Token: 0x040031DA RID: 12762
		private readonly object threadSafeLock = new object();

		// Token: 0x040031DB RID: 12763
		private DateTime lastMembershipSyncTime = DateTime.MinValue;

		// Token: 0x040031DC RID: 12764
		private bool hasBeenSyncedOnce;
	}
}

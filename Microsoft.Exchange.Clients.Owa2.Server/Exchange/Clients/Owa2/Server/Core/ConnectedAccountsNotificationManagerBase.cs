using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000166 RID: 358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConnectedAccountsNotificationManagerBase : DisposeTrackableBase, IConnectedAccountsNotificationManager, IDisposable
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x00031BD8 File Offset: 0x0002FDD8
		protected ConnectedAccountsNotificationManagerBase(Guid userMailboxGuid, Guid userMdbGuid, string userMailboxServerFQDN, IConnectedAccountsConfiguration configuration, ISyncNowNotificationClient notificationClient) : this(userMailboxGuid, userMdbGuid, userMailboxServerFQDN, configuration, notificationClient, new Func<TimerCallback, object, TimeSpan, TimeSpan, IGuardedTimer>(ConnectedAccountsNotificationManagerBase.CreateGuardedTimer))
		{
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		protected ConnectedAccountsNotificationManagerBase(Guid userMailboxGuid, Guid userMdbGuid, string userMailboxServerFQDN, IConnectedAccountsConfiguration configuration, ISyncNowNotificationClient notificationClient, Func<TimerCallback, object, TimeSpan, TimeSpan, IGuardedTimer> createGuardedTimer)
		{
			SyncUtilities.ThrowIfGuidEmpty("userMailboxGuid", userMailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("userMdbGuid", userMdbGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userMailboxServerFQDN", userMailboxServerFQDN);
			SyncUtilities.ThrowIfArgumentNull("configuration", configuration);
			SyncUtilities.ThrowIfArgumentNull("notificationClient", notificationClient);
			SyncUtilities.ThrowIfArgumentNull("createGuardedTimer", createGuardedTimer);
			this.configuration = configuration;
			this.notificationClient = notificationClient;
			this.userMailboxGuid = userMailboxGuid;
			this.userMdbGuid = userMdbGuid;
			this.userMailboxServerFQDN = userMailboxServerFQDN;
			if (this.configuration.PeriodicSyncNowEnabled)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug<Guid, TimeSpan>((long)this.GetHashCode(), "ConnectedAccountsNotificationManager::Setting up periodicSyncNowTimer for User:{0}, PeriodicSyncNowInterval:{1}", this.userMdbGuid, this.configuration.PeriodicSyncNowInterval);
				this.periodicSyncNowTimer = createGuardedTimer(new TimerCallback(this.SendPeriodicSyncNowRequest), null, this.configuration.PeriodicSyncNowInterval, this.configuration.PeriodicSyncNowInterval);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00031CE0 File Offset: 0x0002FEE0
		protected Guid UserMailboxGuid
		{
			get
			{
				return this.userMailboxGuid;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00031CE8 File Offset: 0x0002FEE8
		protected Guid UserMdbGuid
		{
			get
			{
				return this.userMdbGuid;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00031CF0 File Offset: 0x0002FEF0
		protected string UserMailboxServerFQDN
		{
			get
			{
				return this.userMailboxServerFQDN;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00031CF8 File Offset: 0x0002FEF8
		protected IGuardedTimer PeriodicSyncNowTimer
		{
			get
			{
				return this.periodicSyncNowTimer;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00031D00 File Offset: 0x0002FF00
		protected ISyncNowNotificationClient NotificationClient
		{
			get
			{
				return this.notificationClient;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00031D08 File Offset: 0x0002FF08
		protected IConnectedAccountsConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00031D10 File Offset: 0x0002FF10
		void IConnectedAccountsNotificationManager.SendLogonTriggeredSyncNowRequest()
		{
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "SendLogonTriggeredSyncNowRequest::UserMailboxGuid:{0}, UserMdbGuid:{1}, userMailboxServerFQDN:{2}, LogonTriggeredSyncNowEnabled:{3}.", new object[]
			{
				this.userMailboxGuid,
				this.userMdbGuid,
				this.userMailboxServerFQDN,
				this.configuration.LogonTriggeredSyncNowEnabled
			});
			if (this.configuration.LogonTriggeredSyncNowEnabled)
			{
				this.SendSyncNowNotification(new Action<Guid, Guid, string>(this.notificationClient.NotifyOWALogonTriggeredSyncNowNeeded));
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00031D98 File Offset: 0x0002FF98
		void IConnectedAccountsNotificationManager.SendRefreshButtonTriggeredSyncNowRequest()
		{
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "SendRefreshButtonTriggeredSyncNowRequest::UserMailboxGuid:{0}, UserMdbGuid:{1}, userMailboxServerFQDN:{2}, RefreshButtonTriggeredSyncNowEnabled:{3}.", new object[]
			{
				this.userMailboxGuid,
				this.userMdbGuid,
				this.userMailboxServerFQDN,
				this.configuration.RefreshButtonTriggeredSyncNowEnabled
			});
			if (this.configuration.RefreshButtonTriggeredSyncNowEnabled)
			{
				ExDateTime currentTime = this.GetCurrentTime();
				TimeSpan timeSpan = currentTime - this.lastRefreshButtonTriggeredSyncNowRequest;
				if (timeSpan < this.configuration.RefreshButtonTriggeredSyncNowSuppressThreshold)
				{
					ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "SendRefreshButtonTriggeredSyncNowRequest:: Suppress request for this User (MailboxGuid:{0}, MdbGuid:{1}), timeSinceLastRequest:{2}, SuppressThreshold:{3}.", new object[]
					{
						this.userMailboxGuid,
						this.userMdbGuid,
						timeSpan,
						this.configuration.RefreshButtonTriggeredSyncNowSuppressThreshold
					});
					return;
				}
				this.SendSyncNowNotification(new Action<Guid, Guid, string>(this.notificationClient.NotifyOWARefreshButtonTriggeredSyncNowNeeded));
				this.lastRefreshButtonTriggeredSyncNowRequest = currentTime;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00031EA8 File Offset: 0x000300A8
		public static bool ShouldSetupNotificationManagerForUser(MailboxSession mailboxSession, UserContext userContext)
		{
			if (mailboxSession != null && mailboxSession.MailboxOwner != null && !mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid.Equals(Guid.Empty) && !mailboxSession.MailboxOwner.MailboxInfo.MailboxDatabase.IsNullOrEmpty() && !string.IsNullOrEmpty(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn))
			{
				return true;
			}
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)userContext.GetHashCode(), "UserContext.InvokeConnectedAccountsSync::RequiredMailBoxSessionPropertiesNotSet, skip setting up the ConnectedAccountsNotificationManager.");
			return false;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00031F64 File Offset: 0x00030164
		private void SendSyncNowNotification(Action<Guid, Guid, string> notificationMethod)
		{
			this.ExecuteOperationOnThreadPoolThreadWithUnhandledExceptionHandler(delegate
			{
				notificationMethod(this.userMailboxGuid, this.userMdbGuid, this.userMailboxServerFQDN);
			});
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00031F98 File Offset: 0x00030198
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug<Guid, Guid, string>((long)this.GetHashCode(), "ConnectedAccountsNotificationManager.InternalDispose called for User (MailboxGuid:{0},MdbGuid:{1},MailboxServerFQDN:{2}).", this.userMailboxGuid, this.userMdbGuid, this.userMailboxServerFQDN);
				if (this.periodicSyncNowTimer != null)
				{
					this.periodicSyncNowTimer.Dispose(false);
					this.periodicSyncNowTimer = null;
				}
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00031FEB File Offset: 0x000301EB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectedAccountsNotificationManagerBase>(this);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00031FF3 File Offset: 0x000301F3
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00032078 File Offset: 0x00030278
		protected virtual void ExecuteOperationOnThreadPoolThreadWithUnhandledExceptionHandler(Action userOperation)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				try
				{
					userOperation();
				}
				catch (Exception ex)
				{
					ExTraceGlobals.ConnectedAccountsTracer.TraceError<Exception>((long)this.GetHashCode(), "Unhandled exception caught during SendRPCNotificationToMailboxServer call: {0}", ex);
					if (Globals.SendWatsonReports)
					{
						ExTraceGlobals.ConnectedAccountsTracer.TraceError((long)this.GetHashCode(), "Sending watson report.");
						ExWatson.SendReport(ex, ReportOptions.None, null);
					}
				}
			});
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000320AB File Offset: 0x000302AB
		private static IGuardedTimer CreateGuardedTimer(TimerCallback timerCallback, object state, TimeSpan dueTime, TimeSpan period)
		{
			return new GuardedTimer(timerCallback, state, dueTime, period);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000320B8 File Offset: 0x000302B8
		private void SendPeriodicSyncNowRequest(object state)
		{
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug<Guid, Guid, string>((long)this.GetHashCode(), "SendPeriodicSyncNowRequest::UserMailboxGuid:{0}, UserMdbGuid:{1}, userMailboxServerFQDN:{2}.", this.userMailboxGuid, this.userMdbGuid, this.userMailboxServerFQDN);
			this.SendSyncNowNotification(new Action<Guid, Guid, string>(this.notificationClient.NotifyOWAActivityTriggeredSyncNowNeeded));
		}

		// Token: 0x04000803 RID: 2051
		private readonly IConnectedAccountsConfiguration configuration;

		// Token: 0x04000804 RID: 2052
		private readonly ISyncNowNotificationClient notificationClient;

		// Token: 0x04000805 RID: 2053
		private readonly Guid userMailboxGuid;

		// Token: 0x04000806 RID: 2054
		private readonly Guid userMdbGuid;

		// Token: 0x04000807 RID: 2055
		private readonly string userMailboxServerFQDN;

		// Token: 0x04000808 RID: 2056
		private IGuardedTimer periodicSyncNowTimer;

		// Token: 0x04000809 RID: 2057
		private ExDateTime lastRefreshButtonTriggeredSyncNowRequest = ExDateTime.MinValue;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000199 RID: 409
	internal class PendingRequestChannel : DisposeTrackableBase
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000385B0 File Offset: 0x000367B0
		public PendingRequestChannel(PendingRequestManager pendingRequestManager, string channelId)
		{
			this.pendingRequestManager = pendingRequestManager;
			this.channelId = channelId;
			this.notificationMark = 0L;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00038605 File Offset: 0x00036805
		internal ChunkedHttpResponse ChunkedHttpResponse
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003860D File Offset: 0x0003680D
		internal bool IsActive
		{
			get
			{
				return this.response != null && this.response.IsClientConnected;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00038624 File Offset: 0x00036824
		internal bool ShouldBeFinalized
		{
			get
			{
				return this.checkClientInactiveCounter > 2 && this.pendingRequestManager.GetChannelCount() > 1;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003863F File Offset: 0x0003683F
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00038647 File Offset: 0x00036847
		internal long MaxTicksPerPendingRequest
		{
			get
			{
				return this.maxTicksPerPendingRequest;
			}
			set
			{
				this.maxTicksPerPendingRequest = value;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00038650 File Offset: 0x00036850
		internal IAsyncResult BeginSendNotification(AsyncCallback callback, object extraData, PendingRequestEventHandler pendingRequestHandler, bool hierarchySubscriptionActive, string channelId)
		{
			bool flag = this.lockTracker.SetPipeAvailable(false);
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[PendingRequestChannel.BeginSendNotification] Setting the pipe to AVAILABLE");
			try
			{
				this.pendingRequestEventHandler = pendingRequestHandler;
				this.asyncResult = new OwaAsyncResult(callback, extraData, channelId);
				try
				{
					this.response = (ChunkedHttpResponse)extraData;
					this.WriteIsRequestAlive(true);
					this.notificationMark = 0L;
					if (!hierarchySubscriptionActive)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[PendingRequestChannel.BeginSendNotification] hierarchySubscriptionActive is false");
						this.WriteReinitializeSubscriptions();
					}
					this.disposePendingRequest = false;
				}
				finally
				{
					flag = !this.lockTracker.TryReleaseLock();
				}
				if (flag)
				{
					this.WriteNotification(true);
				}
				this.startPendingRequestTime = DateTime.UtcNow.Ticks;
				this.lastDisconnectedTime = 0L;
				if (this.pendingRequestAliveTimer == null)
				{
					this.pendingRequestAliveTimer = new Timer(new TimerCallback(this.ElapsedConnectionAliveTimeout), null, 40000, 40000);
				}
				if (this.accountValidationTimer == null && this.response.AccountValidationContext != null)
				{
					this.accountValidationTimer = new Timer(new TimerCallback(this.AccountValidationTimerCallback), null, 300000, 300000);
				}
			}
			catch (Exception e)
			{
				this.HandleException(e, true);
			}
			return this.asyncResult;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000387A4 File Offset: 0x000369A4
		internal void EndSendNotification(IAsyncResult async)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!", owaAsyncResult.Exception, this);
			}
			this.disposePendingRequest = false;
			this.WriteIsRequestAlive(false);
			if (owaAsyncResult.Exception != null)
			{
				throw new OwaNotificationPipeException("An exception happened while handling the pending connection asynchronously", owaAsyncResult.Exception);
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00038800 File Offset: 0x00036A00
		internal void RecordFinishPendingRequest()
		{
			this.lockTracker.SetPipeUnavailable();
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Setting the pipe to UNAVAILABLE");
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00038823 File Offset: 0x00036A23
		internal bool HandleFinishRequestFromClient()
		{
			return this.HandleFinishRequestFromClient(false);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003882C File Offset: 0x00036A2C
		internal bool HandleFinishRequestFromClient(bool requestRestart)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "The client requested the end of the current notification pipe.Should a restart request be sent ? {0}", requestRestart);
			this.disposePendingRequest = true;
			if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
			{
				try
				{
					this.CloseCurrentPendingRequest(false, requestRestart);
				}
				finally
				{
					this.lockTracker.TryReleaseLock();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00038890 File Offset: 0x00036A90
		internal void AddPayload(List<NotificationPayloadBase> payloadList)
		{
			lock (this.syncRoot)
			{
				if (this.payloadList != null)
				{
					if (this.reloadNeeded)
					{
						NotificationStatisticsManager.Instance.NotificationDropped(payloadList, NotificationState.Dispatching);
					}
					else if (this.payloadList.Count < 250)
					{
						this.payloadList.AddRange(payloadList);
					}
					else
					{
						this.reloadNeeded = true;
						this.notificationMark += (long)this.payloadList.Count;
						this.payloadList.Clear();
					}
				}
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00038934 File Offset: 0x00036B34
		internal void WritePayload(bool asyncOperation, List<NotificationPayloadBase> payloadList)
		{
			this.AddPayload(payloadList);
			if (this.lockTracker.TryAcquireLock())
			{
				this.WriteNotification(asyncOperation);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00038954 File Offset: 0x00036B54
		internal void HandleException(Exception e, bool finishSync)
		{
			if (this.disposePendingRequest)
			{
				return;
			}
			if (this.asyncResult == null)
			{
				return;
			}
			try
			{
				this.asyncResult.Exception = e;
			}
			catch (OwaInvalidOperationException)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception not reported on pending get request. Exception:{0};", e);
				return;
			}
			if (this.lockTracker.IsLockOwner())
			{
				this.asyncResult.CompleteRequest(finishSync);
				return;
			}
			if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
			{
				try
				{
					this.asyncResult.CompleteRequest(finishSync);
					return;
				}
				finally
				{
					this.lockTracker.TryReleaseLock();
				}
			}
			this.disposePendingRequest = true;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00038A04 File Offset: 0x00036C04
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PendingRequestChannel>(this);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00038A0C File Offset: 0x00036C0C
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed && isDisposing)
			{
				if (this.pendingRequestAliveTimer != null)
				{
					this.pendingRequestAliveTimer.Dispose();
					this.pendingRequestAliveTimer = null;
				}
				if (this.accountValidationTimer != null)
				{
					this.accountValidationTimer.Dispose();
					this.accountValidationTimer = null;
				}
				lock (this.syncRoot)
				{
					if (this.payloadList != null)
					{
						this.payloadList.Clear();
						this.payloadList = null;
					}
				}
				if (this.pendingRequestEventHandler != null && !this.pendingRequestEventHandler.IsDisposed && this.lockTracker.TryReleaseAllLocks(new PendingNotifierLockTracker.ReleaseAllLocksCallback(this.pendingRequestEventHandler.Dispose)))
				{
					this.pendingRequestEventHandler.Dispose();
				}
				this.pendingRequestEventHandler = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00038AF4 File Offset: 0x00036CF4
		private void WriteNotification(bool asyncOperation)
		{
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!");
			}
			bool flag = false;
			while (!flag)
			{
				if (this.disposePendingRequest)
				{
					this.CloseCurrentPendingRequest(asyncOperation, true);
					return;
				}
				try
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("[");
					lock (this.syncRoot)
					{
						if (this.payloadList != null)
						{
							if (!this.reloadNeeded)
							{
								foreach (NotificationPayloadBase notificationPayloadBase in this.payloadList)
								{
									RemoteNotificationPayload remoteNotificationPayload = notificationPayloadBase as RemoteNotificationPayload;
									if (remoteNotificationPayload != null)
									{
										stringBuilder.Append(remoteNotificationPayload.RemotePayload).Append(",");
										this.notificationMark += (long)remoteNotificationPayload.NotificationsCount;
									}
									else
									{
										stringBuilder.Append(JsonConverter.ToJSON(notificationPayloadBase)).Append(",");
										this.notificationMark += 1L;
									}
								}
								if (stringBuilder.Length > 1)
								{
									stringBuilder.Remove(stringBuilder.Length - 1, 1);
									stringBuilder.Append("]");
									this.Write(stringBuilder.ToString());
								}
								NotificationStatisticsManager.Instance.NotificationDispatched(this.channelId, this.payloadList);
								this.payloadList.Clear();
							}
							else
							{
								ReloadAllNotificationPayload payload = new ReloadAllNotificationPayload
								{
									Source = new TypeLocation(base.GetType())
								};
								NotificationStatisticsManager.Instance.NotificationCreated(payload);
								NotificationStatisticsManager.Instance.NotificationDispatched(this.channelId, payload);
							}
							this.reloadNeeded = false;
							this.WriteNotificationMark(this.notificationMark);
						}
					}
				}
				finally
				{
					flag = this.lockTracker.TryReleaseLock();
				}
				if (flag)
				{
					return;
				}
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00038D10 File Offset: 0x00036F10
		private void AccountValidationTimerCallback(object state)
		{
			if (this.response.AccountValidationContext != null)
			{
				AccountState accountState = this.response.AccountValidationContext.CheckAccount();
				if (accountState != AccountState.AccountEnabled)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "The account is no longer in an 'Enabled' state");
					this.disposePendingRequest = true;
					if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
					{
						try
						{
							this.CloseCurrentPendingRequest(false, true);
						}
						finally
						{
							this.lockTracker.TryReleaseLock();
						}
					}
				}
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00038D94 File Offset: 0x00036F94
		private void ElapsedConnectionAliveTimeout(object state)
		{
			bool requestRestart = false;
			OwaAsyncResult owaAsyncResult = this.asyncResult;
			this.pendingRequestManager.FireKeepAlive();
			if (DateTime.UtcNow.Ticks - this.startPendingRequestTime > this.MaxTicksPerPendingRequest)
			{
				this.disposePendingRequest = true;
				requestRestart = true;
			}
			try
			{
				if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
				{
					try
					{
						if (DateTime.UtcNow.Ticks - this.lastWriteTime >= 100000000L)
						{
							if (this.disposePendingRequest)
							{
								this.CloseCurrentPendingRequest(false, requestRestart);
								this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted);
								return;
							}
							this.WriteEmptyNotification();
						}
					}
					catch (Exception e)
					{
						this.HandleException(e, false);
						this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted);
						return;
					}
					if (!this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted))
					{
						try
						{
							this.WriteNotification(false);
						}
						catch (Exception e2)
						{
							this.HandleException(e2, false);
						}
					}
				}
			}
			finally
			{
				if (this.IsActive)
				{
					this.checkClientInactiveCounter = 0;
				}
				else
				{
					this.checkClientInactiveCounter++;
				}
				if (this.ShouldBeFinalized)
				{
					this.pendingRequestManager.RemovePendingGetChannel(this.channelId);
				}
				if (!this.pendingRequestManager.HasAnyActivePendingGetChannel())
				{
					if (this.lastDisconnectedTime == 0L)
					{
						this.lastDisconnectedTime = DateTime.UtcNow.Ticks;
						goto IL_1A9;
					}
					if (DateTime.UtcNow.Ticks - this.lastDisconnectedTime <= 700000000L)
					{
						goto IL_1A9;
					}
					this.lastDisconnectedTime = 0L;
					try
					{
						this.pendingRequestManager.FireClientDisconnect();
						goto IL_1A9;
					}
					catch (Exception ex)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Exception during ClientDisconnected event: {0}", ex.ToString());
						goto IL_1A9;
					}
				}
				this.lastDisconnectedTime = 0L;
				IL_1A9:;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00038F80 File Offset: 0x00037180
		private void CloseCurrentPendingRequest(bool completedSynchronously, bool requestRestart)
		{
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!");
			}
			if (requestRestart)
			{
				try
				{
					this.response.RestartRequest();
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Send code to the client that will restart the notification pipe");
				}
				catch (OwaNotificationPipeWriteException)
				{
				}
			}
			this.asyncResult.CompleteRequest(completedSynchronously);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00038FEC File Offset: 0x000371EC
		private void WriteIsRequestAlive(bool isAlive)
		{
			this.response.WriteIsRequestAlive(isAlive, this.notificationMark);
			this.lastWriteTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00039020 File Offset: 0x00037220
		private void WriteReinitializeSubscriptions()
		{
			this.response.WriteReinitializeSubscriptions();
			this.lastWriteTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003904C File Offset: 0x0003724C
		private void WriteNotificationMark(long mark)
		{
			this.response.WritePendingGeMark(mark);
			this.lastWriteTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00039078 File Offset: 0x00037278
		private void WriteEmptyNotification()
		{
			this.response.WriteEmptyNotification();
			this.lastWriteTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000390A4 File Offset: 0x000372A4
		private void Write(string notification)
		{
			this.response.Write(notification);
			this.lastWriteTime = DateTime.UtcNow.Ticks;
		}

		// Token: 0x040008E3 RID: 2275
		private const int AccountValidationIntervalInMilliSeconds = 300000;

		// Token: 0x040008E4 RID: 2276
		private const int EmptyNotificationIntervalInMilliSeconds = 40000;

		// Token: 0x040008E5 RID: 2277
		private const int MinEmptyNotificationIntervalInMilliSeconds = 10000;

		// Token: 0x040008E6 RID: 2278
		private const int MinimumWaitTimeForClientActivityInSeconds = 70;

		// Token: 0x040008E7 RID: 2279
		private const int MaxPayloadThreshold = 250;

		// Token: 0x040008E8 RID: 2280
		private const int MaxChecksBeforeFinalize = 2;

		// Token: 0x040008E9 RID: 2281
		private static readonly long DefaultMaxTicksPerPendingRequest = (long)((ulong)-1294967296);

		// Token: 0x040008EA RID: 2282
		private readonly object syncRoot = new object();

		// Token: 0x040008EB RID: 2283
		private readonly string channelId;

		// Token: 0x040008EC RID: 2284
		private volatile bool disposePendingRequest;

		// Token: 0x040008ED RID: 2285
		private OwaAsyncResult asyncResult;

		// Token: 0x040008EE RID: 2286
		private ChunkedHttpResponse response;

		// Token: 0x040008EF RID: 2287
		private long lastWriteTime;

		// Token: 0x040008F0 RID: 2288
		private long lastDisconnectedTime;

		// Token: 0x040008F1 RID: 2289
		private long startPendingRequestTime;

		// Token: 0x040008F2 RID: 2290
		private int checkClientInactiveCounter;

		// Token: 0x040008F3 RID: 2291
		private Timer pendingRequestAliveTimer;

		// Token: 0x040008F4 RID: 2292
		private Timer accountValidationTimer;

		// Token: 0x040008F5 RID: 2293
		private PendingNotifierLockTracker lockTracker = new PendingNotifierLockTracker();

		// Token: 0x040008F6 RID: 2294
		private long maxTicksPerPendingRequest = PendingRequestChannel.DefaultMaxTicksPerPendingRequest;

		// Token: 0x040008F7 RID: 2295
		private List<NotificationPayloadBase> payloadList = new List<NotificationPayloadBase>();

		// Token: 0x040008F8 RID: 2296
		private bool reloadNeeded;

		// Token: 0x040008F9 RID: 2297
		private PendingRequestManager pendingRequestManager;

		// Token: 0x040008FA RID: 2298
		private bool disposed;

		// Token: 0x040008FB RID: 2299
		private PendingRequestEventHandler pendingRequestEventHandler;

		// Token: 0x040008FC RID: 2300
		private long notificationMark;
	}
}

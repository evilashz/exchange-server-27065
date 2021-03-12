using System;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200015E RID: 350
	internal abstract class MapiNotificationHandlerBase : DisposeTrackableBase
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000CD0 RID: 3280 RVA: 0x0002F928 File Offset: 0x0002DB28
		// (remove) Token: 0x06000CD1 RID: 3281 RVA: 0x0002F960 File Offset: 0x0002DB60
		internal event MapiNotificationHandlerBase.BeforeDisposeEventHandler OnBeforeDisposed;

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002F995 File Offset: 0x0002DB95
		public MapiNotificationHandlerBase(IMailboxContext userContext, bool remoteSubscription) : this(null, userContext, remoteSubscription)
		{
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		public MapiNotificationHandlerBase(string subscriptionId, IMailboxContext userContext, bool remoteSubscription)
		{
			this.SubscriptionId = subscriptionId;
			this.userContext = userContext;
			this.connectionAliveTimerCount = 1;
			this.syncRoot = new object();
			this.remoteSubscription = remoteSubscription;
			if (!Globals.Owa2ServerUnitTestsHook)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "MapiNotificationHandlerBase.Constructor. Type: {0}, RemoteSusbcription: {1}.", base.GetType().Name, this.remoteSubscription);
				if ((this.remoteSubscription || this is ConnectionDroppedNotificationHandler) && this.userContext.NotificationManager != null)
				{
					this.userContext.NotificationManager.RemoteKeepAliveEvent += this.RemoteKeepAlive;
				}
				if (remoteSubscription)
				{
					if (this.userContext.NotificationManager != null)
					{
						this.userContext.NotificationManager.StartRemoteKeepAliveTimer();
					}
				}
				else
				{
					this.userContext.PendingRequestManager.KeepAlive += this.KeepAlive;
				}
				this.verboseLoggingEnabled = Globals.LogVerboseNotifications;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002FA8D File Offset: 0x0002DC8D
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0002FA95 File Offset: 0x0002DC95
		public string SubscriptionId { get; protected set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002FA9E File Offset: 0x0002DC9E
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0002FAA6 File Offset: 0x0002DCA6
		internal Subscription Subscription
		{
			get
			{
				return this.mapiSubscription;
			}
			set
			{
				this.mapiSubscription = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0002FAAF File Offset: 0x0002DCAF
		internal IMailboxContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002FAB7 File Offset: 0x0002DCB7
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0002FABF File Offset: 0x0002DCBF
		internal QueryResult QueryResult
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
		internal bool NeedToReinitSubscriptions
		{
			get
			{
				return this.needReinitSubscriptions;
			}
			set
			{
				this.needReinitSubscriptions = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002FAD9 File Offset: 0x0002DCD9
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0002FAE1 File Offset: 0x0002DCE1
		internal bool MissedNotifications
		{
			get
			{
				return this.missedNotifications;
			}
			set
			{
				this.missedNotifications = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002FAEA File Offset: 0x0002DCEA
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0002FAF2 File Offset: 0x0002DCF2
		internal bool NeedRefreshPayload { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002FAFB File Offset: 0x0002DCFB
		internal bool RemoteSubscription
		{
			get
			{
				return this.remoteSubscription;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002FB03 File Offset: 0x0002DD03
		protected object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002FB0B File Offset: 0x0002DD0B
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0002FB13 File Offset: 0x0002DD13
		private protected bool IsDisposed_Reentrant
		{
			protected get
			{
				return this.isDisposed_reentrant;
			}
			private set
			{
				this.isDisposed_reentrant = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002FB1C File Offset: 0x0002DD1C
		protected string EventPrefix
		{
			get
			{
				if (this.eventPrefix == null)
				{
					this.eventPrefix = "MAPI." + base.GetType().Name + ".";
				}
				return this.eventPrefix;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002FC38 File Offset: 0x0002DE38
		internal static void DisposeXSOObjects(object o, IMailboxContext userContext)
		{
			bool flag = false;
			try
			{
				if (!userContext.MailboxSessionLockedByCurrentThread())
				{
					ExTraceGlobals.UserContextTracer.TraceDebug(0, 0L, "MapiNotificationHandlerBase.DisposeXSOObjects(): Mailbox session not locked. Attempting to grab the lock.");
					userContext.LockAndReconnectMailboxSession(3000);
					flag = true;
				}
				IDisposable xsoObject = o as IDisposable;
				if (o != null)
				{
					try
					{
						OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
						{
							try
							{
								xsoObject.Dispose();
							}
							catch (StoragePermanentException ex2)
							{
								ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "MapiNotificationHandlerBase. Unable to dispose object.  exception {0}", ex2.Message);
							}
							catch (StorageTransientException ex3)
							{
								ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "MapiNotificationHandlerBase. Unable to dispose object.  exception {0}", ex3.Message);
							}
							catch (MapiExceptionObjectDisposed mapiExceptionObjectDisposed)
							{
								ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.Unable to dispose object.  exception {0}", mapiExceptionObjectDisposed.Message);
							}
							catch (ThreadAbortException ex4)
							{
								ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "MapiNotificationHandlerBase Unable to dispose object.  exception {0}", ex4.Message);
							}
							catch (ResourceUnhealthyException ex5)
							{
								ExTraceGlobals.UserContextTracer.TraceError<string>(0L, "MapiNotificationHandlerBase Unable to dispose object.  exception {0}", ex5.Message);
							}
						});
					}
					catch (GrayException ex)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.DisposeXSOObjects Unable to dispose object.  exception {0}", ex.Message);
					}
				}
			}
			finally
			{
				if (flag)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug(0, 0L, "MapiNotificationHandlerBase.DisposeXSOObjects(): Attempting to release the lock taken.");
					userContext.UnlockAndDisconnectMailboxSession();
				}
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002FCF4 File Offset: 0x0002DEF4
		internal virtual void Subscribe()
		{
			lock (this.syncRoot)
			{
				if (base.IsDisposed)
				{
					throw new InvalidOperationException("Cannot call Subscribe on a Disposed object");
				}
				this.InitSubscription();
			}
		}

		// Token: 0x06000CE8 RID: 3304
		internal abstract void HandleNotificationInternal(Notification notif, MapiNotificationsLogEvent logEvent, object context);

		// Token: 0x06000CE9 RID: 3305
		internal abstract void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent);

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002FD48 File Offset: 0x0002DF48
		internal virtual void HandleConnectionDroppedNotification(Notification notification)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.HandleConnectionDroppedNotification. Type: {0}", base.GetType().Name);
			lock (this.syncRoot)
			{
				MapiNotificationsLogEvent logEvent = new MapiNotificationsLogEvent(this.UserContext.ExchangePrincipal, this.UserContext.Key.ToString(), this, this.EventPrefix + "HandleConnectionDroppedNotification");
				if (!this.IsDisposed_Reentrant)
				{
					this.needReinitSubscriptions = true;
				}
				if (this.ShouldLog(logEvent))
				{
					OwaServerTraceLogger.AppendToLog(logEvent);
				}
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002FDF4 File Offset: 0x0002DFF4
		internal void HandleNotification(Notification notification)
		{
			this.HandleNotification(notification, null);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002FE28 File Offset: 0x0002E028
		internal void HandleNotification(Notification notification, object context)
		{
			MapiNotificationsLogEvent logEvent = new MapiNotificationsLogEvent(this.UserContext.ExchangePrincipal, this.UserContext.Key.ToString(), this, this.EventPrefix + "HandleNotification");
			try
			{
				if (base.IsDisposed)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.HandleNotification for {0}: Ignoring notification because we're disposed.", base.GetType().Name);
				}
				else if (this.MissedNotifications)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.HandleNotification for {0}: Ignoring notification because we've missed notifications.", base.GetType().Name);
				}
				else if (this.NeedToReinitSubscriptions)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.HandleNotification for {0}: Ignoring notification because we need to re-init subscription.", base.GetType().Name);
				}
				else
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						this.HandleNotificationInternal(notification, logEvent, context);
					});
				}
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string, string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.HandleNotification for {0} encountered an exception: {1}", base.GetType().Name, ex.ToString());
				logEvent.HandledException = ex;
				this.MissedNotifications = true;
			}
			finally
			{
				if (this.ShouldLog(logEvent))
				{
					OwaServerTraceLogger.AppendToLog(logEvent);
				}
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002FFA0 File Offset: 0x0002E1A0
		internal virtual void DisposeSubscriptions()
		{
			this.DisposeSubscriptions(true);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000300FC File Offset: 0x0002E2FC
		internal virtual void DisposeSubscriptions(bool disposeQueryResult)
		{
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					try
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool, Type>((long)this.GetHashCode(), "MapiNotificationHandlerBase.DisposeInternal. doNotDisposeQueryResult: {0}, Type: {1}", disposeQueryResult, this.GetType());
						if (disposeQueryResult && this.result != null)
						{
							MapiNotificationHandlerBase.DisposeXSOObjects(this.result, this.UserContext);
							this.result = null;
						}
						if (this.mapiSubscription != null)
						{
							MapiNotificationHandlerBase.DisposeXSOObjects(this.mapiSubscription, this.UserContext);
							this.mapiSubscription = null;
						}
					}
					catch (StoragePermanentException ex2)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceError<string, SmtpAddress, Type>((long)this.GetHashCode(), "Unexpected exception in MapiNotificationHandlerBase Dispose. User: {1}. Exception: {0}, type: {2}", ex2.Message, this.UserContext.PrimarySmtpAddress, this.GetType());
					}
					catch (StorageTransientException ex3)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceError<string, SmtpAddress, Type>((long)this.GetHashCode(), "Unexpected exception in MapiNotificationHandlerBase Dispose. User: {1}. Exception: {0}, type: {2}", ex3.Message, this.UserContext.PrimarySmtpAddress, this.GetType());
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.Dispose Unable to dispose object.  exception {0}", ex.Message);
			}
		}

		// Token: 0x06000CEF RID: 3311
		protected abstract void InitSubscriptionInternal();

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000302E8 File Offset: 0x0002E4E8
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool, SmtpAddress, Type>((long)this.GetHashCode(), "MapiNotificationHandlerBase.Dispose. IsDisposing: {0}, User: {1}, Type: {2}", isDisposing, this.UserContext.PrimarySmtpAddress, base.GetType());
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					lock (this.syncRoot)
					{
						if (this.OnBeforeDisposed != null)
						{
							ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "MapiNotificationHandlerBase.InternalDispose Call OnBeforeDisposed.");
							this.OnBeforeDisposed(new ConnectionDroppedNotificationHandler.ConnectionDroppedEventHandler(this.HandleConnectionDroppedNotification));
						}
						if ((this.remoteSubscription || this is ConnectionDroppedNotificationHandler) && this.userContext.NotificationManager != null)
						{
							this.userContext.NotificationManager.RemoteKeepAliveEvent -= this.RemoteKeepAlive;
						}
						if (!this.remoteSubscription && this.userContext.PendingRequestManager != null)
						{
							this.userContext.PendingRequestManager.KeepAlive -= this.KeepAlive;
						}
						if ((!Globals.Owa2ServerUnitTestsHook && !this.IsDisposed_Reentrant) || this.Subscription != null || this.QueryResult != null)
						{
							if (isDisposing)
							{
								this.IsDisposed_Reentrant = true;
								this.CleanupSubscriptions();
							}
						}
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.Dispose Unable to dispose object.  exception {0}", ex.Message);
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003037C File Offset: 0x0002E57C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiNotificationHandlerBase>(this);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00030384 File Offset: 0x0002E584
		protected void InitSubscription()
		{
			MapiNotificationsLogEvent logEvent = new MapiNotificationsLogEvent(this.UserContext.ExchangePrincipal, this.UserContext.Key.ToString(), this, this.EventPrefix + "InitSubscription");
			if (this.IsDisposed_Reentrant)
			{
				return;
			}
			try
			{
				this.userContext.LockAndReconnectMailboxSession(3000);
				this.NeedRefreshPayload = false;
				if (this.NeedToReinitSubscriptions)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<Type>((long)this.GetHashCode(), "MapiNotificationHandlerBase.InitSubscription need to cleanup subscription before reinit for type: {0}", base.GetType());
					this.CleanupSubscriptions();
					this.NeedRefreshPayload = true;
				}
				if (this.Subscription == null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<Type, SmtpAddress>((long)this.GetHashCode(), "Notification Handler type: {0} needs to init subscriptions. User: {1}", base.GetType(), this.UserContext.PrimarySmtpAddress);
					if (this.QueryResult != null)
					{
						MapiNotificationHandlerBase.DisposeXSOObjects(this.QueryResult, this.UserContext);
					}
					this.QueryResult = null;
					this.InitSubscriptionInternal();
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<Type>((long)this.GetHashCode(), "MapiNotificationHandlerBase.InitSubscription subscription successfully initialized for type: {0}", base.GetType());
				}
				this.NeedToReinitSubscriptions = false;
			}
			finally
			{
				if (this.userContext.MailboxSessionLockedByCurrentThread())
				{
					this.userContext.UnlockAndDisconnectMailboxSession();
				}
				if (this.ShouldLog(logEvent))
				{
					OwaServerTraceLogger.AppendToLog(logEvent);
				}
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000304C8 File Offset: 0x0002E6C8
		private void KeepAlive(object sender, EventArgs e)
		{
			int num = Interlocked.Increment(ref this.connectionAliveTimerCount);
			if (num % 5 == 0)
			{
				MapiNotificationsLogEvent logEvent = new MapiNotificationsLogEvent(this.UserContext.ExchangePrincipal, this.UserContext.Key.ToString(), this, this.EventPrefix + "KeepAlive");
				this.ProcessKeepAlive(logEvent);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00030520 File Offset: 0x0002E720
		private void RemoteKeepAlive(object sender, EventArgs e)
		{
			MapiNotificationsLogEvent logEvent = new MapiNotificationsLogEvent(this.UserContext.ExchangePrincipal, this.UserContext.Key.ToString(), this, this.EventPrefix + "RemoteKeepAlive");
			this.ProcessKeepAlive(logEvent);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00030584 File Offset: 0x0002E784
		private void ProcessKeepAlive(MapiNotificationsLogEvent logEvent)
		{
			try
			{
				if (!base.IsDisposed)
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						this.HandlePendingGetTimerCallback(logEvent);
					});
				}
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string, string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.KeepAlive for {0} encountered an exception: {1}", base.GetType().Name, ex.ToString());
				logEvent.HandledException = ex;
			}
			finally
			{
				if (this.ShouldLog(logEvent))
				{
					OwaServerTraceLogger.AppendToLog(logEvent);
				}
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00030638 File Offset: 0x0002E838
		private bool ShouldLog(MapiNotificationsLogEvent logEvent)
		{
			return this.verboseLoggingEnabled || logEvent.HandledException != null || base.IsDisposed;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003065C File Offset: 0x0002E85C
		private void CleanupSubscriptions()
		{
			if (this.Subscription != null)
			{
				MapiNotificationHandlerBase.DisposeXSOObjects(this.Subscription, this.UserContext);
			}
			this.Subscription = null;
			if (this.QueryResult != null)
			{
				MapiNotificationHandlerBase.DisposeXSOObjects(this.QueryResult, this.UserContext);
			}
			this.QueryResult = null;
		}

		// Token: 0x040007E0 RID: 2016
		private readonly bool verboseLoggingEnabled;

		// Token: 0x040007E1 RID: 2017
		private readonly bool remoteSubscription;

		// Token: 0x040007E2 RID: 2018
		private object syncRoot;

		// Token: 0x040007E3 RID: 2019
		private Subscription mapiSubscription;

		// Token: 0x040007E4 RID: 2020
		private IMailboxContext userContext;

		// Token: 0x040007E5 RID: 2021
		private QueryResult result;

		// Token: 0x040007E6 RID: 2022
		private bool isDisposed_reentrant;

		// Token: 0x040007E7 RID: 2023
		private bool missedNotifications;

		// Token: 0x040007E8 RID: 2024
		private bool needReinitSubscriptions;

		// Token: 0x040007E9 RID: 2025
		private int connectionAliveTimerCount;

		// Token: 0x040007EA RID: 2026
		private string eventPrefix;

		// Token: 0x0200015F RID: 351
		// (Invoke) Token: 0x06000CF9 RID: 3321
		internal delegate void BeforeDisposeEventHandler(ConnectionDroppedNotificationHandler.ConnectionDroppedEventHandler connectionDroppedEventHandler);
	}
}

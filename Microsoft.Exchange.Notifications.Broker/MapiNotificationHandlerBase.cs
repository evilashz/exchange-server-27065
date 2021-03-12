using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000008 RID: 8
	internal abstract class MapiNotificationHandlerBase : DisposeTrackableBase, INotificationHandler
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002987 File Offset: 0x00000B87
		protected MapiNotificationHandlerBase(string name, MailboxSessionContext sessionContext)
		{
			this.Name = name;
			this.sessionContext = sessionContext;
			this.syncRoot = new object();
			this.servicedSubscriptions = new Dictionary<Guid, BrokerSubscription>();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029B3 File Offset: 0x00000BB3
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000029BB File Offset: 0x00000BBB
		internal string Name { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000029CC File Offset: 0x00000BCC
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029D5 File Offset: 0x00000BD5
		internal MailboxSessionContext SessionContext
		{
			get
			{
				return this.sessionContext;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000029DD File Offset: 0x00000BDD
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000029E5 File Offset: 0x00000BE5
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000029EE File Offset: 0x00000BEE
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000029F6 File Offset: 0x00000BF6
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029FF File Offset: 0x00000BFF
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A07 File Offset: 0x00000C07
		internal bool NeedRefreshPayload { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A10 File Offset: 0x00000C10
		internal int ServicedSubscriptionCount
		{
			get
			{
				int count;
				lock (this.syncRoot)
				{
					count = this.servicedSubscriptions.Count;
				}
				return count;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A58 File Offset: 0x00000C58
		protected ReadOnlyDictionary<Guid, BrokerSubscription> ServicedSubscriptionsReadOnly
		{
			get
			{
				if (this.servicedSubscriptionsReadOnly == null)
				{
					this.servicedSubscriptionsReadOnly = new ReadOnlyDictionary<Guid, BrokerSubscription>(this.servicedSubscriptions);
				}
				return this.servicedSubscriptionsReadOnly;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A79 File Offset: 0x00000C79
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A81 File Offset: 0x00000C81
		private protected bool WasConnectionDropped { protected get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A8A File Offset: 0x00000C8A
		protected object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A92 File Offset: 0x00000C92
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A9A File Offset: 0x00000C9A
		private protected bool IsDisposedReentrant
		{
			protected get
			{
				return this.isDisposedReentrant;
			}
			private set
			{
				this.isDisposedReentrant = value;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AA4 File Offset: 0x00000CA4
		void INotificationHandler.SubscriptionRemoved(BrokerSubscription subscription)
		{
			lock (this.syncRoot)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Subscription to be removed: {0}", subscription.SubscriptionId);
				this.servicedSubscriptions.Remove(subscription.SubscriptionId);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B0C File Offset: 0x00000D0C
		internal virtual void Subscribe(BrokerSubscription brokerSubscription)
		{
			lock (this.syncRoot)
			{
				if (this.IsDisposedReentrant)
				{
					throw new InvalidOperationException("Cannot call Subscribe on a Disposed object");
				}
				this.servicedSubscriptions[brokerSubscription.SubscriptionId] = brokerSubscription;
				this.InitSubscription();
			}
		}

		// Token: 0x06000044 RID: 68
		internal abstract void HandleNotificationInternal(Notification notif, object context);

		// Token: 0x06000045 RID: 69
		internal abstract void KeepAliveInternal();

		// Token: 0x06000046 RID: 70 RVA: 0x00002B74 File Offset: 0x00000D74
		internal void HandleConnectionDroppedNotification(Notification notification)
		{
			lock (this.syncRoot)
			{
				if (!this.IsDisposedReentrant)
				{
					this.WasConnectionDropped = true;
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BC0 File Offset: 0x00000DC0
		internal void HandleNotification(Notification notification)
		{
			this.HandleNotification(notification, null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BEC File Offset: 0x00000DEC
		internal void HandleNotification(Notification notification, object context)
		{
			using (BrokerLogger.StartEvent("HandleNotification"))
			{
				lock (this.SyncRoot)
				{
					try
					{
						ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "HandleNotification start");
						if (this.IsDisposedReentrant)
						{
							ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "Notification is rejected because this handler is being disposed");
							BrokerLogger.Set(LogField.RejectReason, "Disposing");
						}
						else if (this.MissedNotifications)
						{
							ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "Notification is rejected because there were previously missed notifications");
							BrokerLogger.Set(LogField.RejectReason, "MissedNotifications");
						}
						else if (this.WasConnectionDropped)
						{
							ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "Notification is rejected because this handler needs to reinit subscriptions");
							BrokerLogger.Set(LogField.RejectReason, "WasConnectionDropped");
						}
						else
						{
							GrayException.MapAndReportGrayExceptions(delegate()
							{
								this.HandleNotificationInternal(notification, context);
							});
							ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "HandleNotification end");
						}
					}
					catch (GrayException ex)
					{
						this.MissedNotifications = true;
						ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "Caught an exception while handling a MAPI notification: {0}", ex.ToString());
						BrokerLogger.Set(LogField.Exception, ex.ToString());
					}
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D98 File Offset: 0x00000F98
		internal void KeepAlive()
		{
			if (this.IsDisposedReentrant)
			{
				return;
			}
			this.KeepAliveInternal();
		}

		// Token: 0x0600004A RID: 74
		protected abstract void InitSubscriptionInternal(MailboxSession session);

		// Token: 0x0600004B RID: 75 RVA: 0x00002E68 File Offset: 0x00001068
		protected override void InternalDispose(bool isDisposing)
		{
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						lock (this.syncRoot)
						{
							if (isDisposing)
							{
								this.IsDisposedReentrant = true;
								this.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
								{
									this.CleanupSubscriptions();
								});
							}
						}
					}
					catch (StoragePermanentException exception2)
					{
						exception = exception2;
					}
					catch (StorageTransientException exception3)
					{
						exception = exception3;
					}
				});
			}
			catch (GrayException exception)
			{
				GrayException exception4;
				exception = exception4;
			}
			finally
			{
				if (exception != null)
				{
					ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "Exception caught: {0}", exception.ToString());
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EFC File Offset: 0x000010FC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiNotificationHandlerBase>(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F53 File Offset: 0x00001153
		protected void InitSubscription()
		{
			if (this.IsDisposedReentrant)
			{
				return;
			}
			this.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
			{
				this.NeedRefreshPayload = (this.WasConnectionDropped || this.MissedNotifications);
				if (this.Subscription == null || this.WasConnectionDropped)
				{
					this.CleanupSubscriptions();
					this.InitSubscriptionInternal(session);
				}
				this.WasConnectionDropped = false;
				this.MissedNotifications = false;
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FB0 File Offset: 0x000011B0
		protected void SendPayloadsToQueue(ApplicationNotification payload)
		{
			this.SendPayloadsToQueue(delegate(BrokerSubscription brokerSubscription)
			{
				ApplicationNotification applicationNotification = (ApplicationNotification)payload.Clone();
				applicationNotification.ConsumerSubscriptionId = brokerSubscription.Parameters.ConsumerSubscriptionId;
				return applicationNotification;
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FDC File Offset: 0x000011DC
		protected void SendPayloadsToQueue(Func<BrokerSubscription, ApplicationNotification> getPayload)
		{
			lock (this.syncRoot)
			{
				ExTraceGlobals.GeneratorTracer.TraceDebug<int>((long)this.GetHashCode(), "Generating notifications for the {0} subscriptions on this handler", this.servicedSubscriptions.Values.Count);
				foreach (BrokerSubscription brokerSubscription in this.servicedSubscriptions.Values)
				{
					BrokerNotification notification = new BrokerNotification
					{
						NotificationId = Guid.NewGuid(),
						ConsumerId = brokerSubscription.ConsumerId,
						CreationTime = DateTime.UtcNow,
						SubscriptionId = brokerSubscription.SubscriptionId,
						ChannelId = brokerSubscription.ChannelId,
						SequenceNumber = brokerSubscription.GetNextSequenceNumber(),
						ReceiverMailboxGuid = brokerSubscription.Receiver.MailboxGuid,
						ReceiverMailboxSmtp = brokerSubscription.Receiver.MailboxSmtp,
						Payload = getPayload(brokerSubscription)
					};
					if (Generator.Singleton.MailboxIsHostedLocally(brokerSubscription.Receiver.MailboxGuid))
					{
						LocalMultiQueue.Singleton.Put(brokerSubscription.ConsumerId, notification);
					}
					else
					{
						RemoteMessenger remoteMessenger = RemoteMessengerFactory.CreateForNotification(brokerSubscription);
						if (remoteMessenger != null)
						{
							RemoteMultiQueue.Singleton.Put(notification, remoteMessenger);
						}
						else
						{
							BrokerLogger.Set(LogField.RejectReason, "DestMbxNotFound");
							ExTraceGlobals.GeneratorTracer.TraceError((long)this.GetHashCode(), "Could not find server hosting the destination mailbox");
						}
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000032C8 File Offset: 0x000014C8
		protected void DisposeXSOObjects(params IDisposable[] xsoObjects)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					foreach (IDisposable disposable in xsoObjects)
					{
						if (disposable != null)
						{
							try
							{
								disposable.Dispose();
							}
							catch (StoragePermanentException ex2)
							{
								ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase. Unable to dispose object.  exception {0}", ex2.Message);
							}
							catch (StorageTransientException ex3)
							{
								ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase. Unable to dispose object.  exception {0}", ex3.Message);
							}
							catch (MapiExceptionObjectDisposed mapiExceptionObjectDisposed)
							{
								ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase.Unable to dispose object.  exception {0}", mapiExceptionObjectDisposed.Message);
							}
							catch (ThreadAbortException ex4)
							{
								ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase Unable to dispose object.  exception {0}", ex4.Message);
							}
							catch (ResourceUnhealthyException ex5)
							{
								ExTraceGlobals.GeneratorTracer.TraceError<string>((long)this.GetHashCode(), "MapiNotificationHandlerBase Unable to dispose object.  exception {0}", ex5.Message);
							}
						}
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.GeneratorTracer.TraceError<string>(0L, "Exception caught: {0}", ex.ToString());
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003330 File Offset: 0x00001530
		private void CleanupSubscriptions()
		{
			this.DisposeXSOObjects(new IDisposable[]
			{
				this.Subscription,
				this.QueryResult
			});
			this.Subscription = null;
			this.QueryResult = null;
		}

		// Token: 0x04000032 RID: 50
		private readonly object syncRoot;

		// Token: 0x04000033 RID: 51
		private readonly Dictionary<Guid, BrokerSubscription> servicedSubscriptions;

		// Token: 0x04000034 RID: 52
		private Subscription mapiSubscription;

		// Token: 0x04000035 RID: 53
		private MailboxSessionContext sessionContext;

		// Token: 0x04000036 RID: 54
		private QueryResult result;

		// Token: 0x04000037 RID: 55
		private bool isDisposedReentrant;

		// Token: 0x04000038 RID: 56
		private bool missedNotifications;

		// Token: 0x04000039 RID: 57
		private ReadOnlyDictionary<Guid, BrokerSubscription> servicedSubscriptionsReadOnly;
	}
}

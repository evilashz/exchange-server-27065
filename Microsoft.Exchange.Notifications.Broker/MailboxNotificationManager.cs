using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000D RID: 13
	internal sealed class MailboxNotificationManager : DisposeTrackableBase
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003D90 File Offset: 0x00001F90
		internal MailboxNotificationManager(BrokerSubscription subscription)
		{
			OrganizationId organizationId = subscription.Sender.OrganizationId;
			Guid mailboxGuid = subscription.Sender.MailboxGuid;
			CultureInfo cultureInfo = subscription.Parameters.CultureInfo;
			ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			ExchangePrincipal mailboxPrincipal = ExchangePrincipal.FromMailboxGuid(adSettings, mailboxGuid, null);
			this.sessionContext = new MailboxSessionContext(mailboxPrincipal, cultureInfo);
			this.notificationHandlers = new Dictionary<string, MapiNotificationHandlerBase>();
			this.connectionDroppedSubscription = new BrokerSubscription
			{
				SubscriptionId = ConnectionDroppedSubscription.WellKnownSubscriptionId,
				Parameters = new ConnectionDroppedSubscription()
			};
			this.keepAliveTimer = new GuardedTimer(new TimerCallback(this.FireKeepAlive), null, MailboxNotificationManager.KeepAliveTimerInterval, MailboxNotificationManager.KeepAliveTimerInterval);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003E48 File Offset: 0x00002048
		internal int HandlerCount
		{
			get
			{
				int count;
				lock (this.syncRoot)
				{
					count = this.notificationHandlers.Count;
				}
				return count;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E90 File Offset: 0x00002090
		internal MapiNotificationHandlerBase Subscribe(BrokerSubscription brokerSubscription)
		{
			MapiNotificationHandlerBase result;
			lock (this.syncRoot)
			{
				MapiNotificationHandlerBase mapiNotificationHandlerBase = null;
				if (!this.isDisposed)
				{
					string handlerMapString = this.GetHandlerMapString(brokerSubscription.Parameters);
					if (!this.notificationHandlers.TryGetValue(handlerMapString, out mapiNotificationHandlerBase))
					{
						NotificationType notificationType = brokerSubscription.Parameters.NotificationType;
						switch (notificationType)
						{
						case NotificationType.NewMail:
							mapiNotificationHandlerBase = new NewMailNotificationHandler(handlerMapString, this.sessionContext, brokerSubscription.Parameters);
							break;
						case NotificationType.Conversation:
							mapiNotificationHandlerBase = new ConversationNotificationHandler(handlerMapString, this.sessionContext, brokerSubscription.Parameters);
							break;
						default:
							if (notificationType != NotificationType.UnseenCount)
							{
								throw new NotSupportedException(string.Format("Notification Type '{0}', is not Supported", brokerSubscription.Parameters.NotificationType));
							}
							mapiNotificationHandlerBase = new UnseenCountNotificationHandler(handlerMapString, this.sessionContext, brokerSubscription.Parameters);
							break;
						}
						this.CreateConnectionDroppedHandlerIfNeeded();
						this.notificationHandlers[handlerMapString] = mapiNotificationHandlerBase;
					}
					mapiNotificationHandlerBase.Subscribe(brokerSubscription);
				}
				result = mapiNotificationHandlerBase;
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F9C File Offset: 0x0000219C
		internal void CleanupInactiveHandlers()
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "CleanupInactiveHandlers start: MailboxGuid - {0}, Culture - {1}", this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid, this.sessionContext.SessionCulture.Name);
			List<MapiNotificationHandlerBase> inactiveHandlers = this.GetInactiveHandlers();
			if (inactiveHandlers != null && inactiveHandlers.Count > 0)
			{
				foreach (MapiNotificationHandlerBase mapiNotificationHandlerBase in inactiveHandlers)
				{
					mapiNotificationHandlerBase.Dispose();
				}
			}
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "CleanupInactiveHandlers End: MailboxGuid - {0}, Culture - {1}", this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid, this.sessionContext.SessionCulture.Name);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004074 File Offset: 0x00002274
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (this.syncRoot)
				{
					this.isDisposed = true;
					if (this.keepAliveTimer != null)
					{
						this.keepAliveTimer.Dispose(true);
						this.keepAliveTimer = null;
					}
					if (this.notificationHandlers != null)
					{
						foreach (MapiNotificationHandlerBase mapiNotificationHandlerBase in this.notificationHandlers.Values)
						{
							mapiNotificationHandlerBase.Dispose();
						}
						this.notificationHandlers.Clear();
						this.notificationHandlers = null;
					}
					if (this.sessionContext != null)
					{
						this.sessionContext.Dispose();
						this.sessionContext = null;
					}
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004150 File Offset: 0x00002350
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxNotificationManager>(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004158 File Offset: 0x00002358
		private string GetHandlerMapString(BaseSubscription baseSubscription)
		{
			IEnumerable<Tuple<string, object>> differentiators = baseSubscription.Differentiators;
			StringBuilder stringBuilder = new StringBuilder(256);
			foreach (Tuple<string, object> tuple in differentiators)
			{
				stringBuilder.AppendFormat("{0}{1}", tuple.Item1, tuple.Item2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000041CC File Offset: 0x000023CC
		private void CreateConnectionDroppedHandlerIfNeeded()
		{
			MapiNotificationHandlerBase mapiNotificationHandlerBase = null;
			string handlerMapString = this.GetHandlerMapString(this.connectionDroppedSubscription.Parameters);
			if (!this.notificationHandlers.TryGetValue(handlerMapString, out mapiNotificationHandlerBase))
			{
				mapiNotificationHandlerBase = new ConnectionDroppedNotificationHandler(handlerMapString, this.sessionContext, new Action<Notification>(this.ConnectionDroppedEventCallback));
				this.notificationHandlers[handlerMapString] = mapiNotificationHandlerBase;
				mapiNotificationHandlerBase.Subscribe(this.connectionDroppedSubscription);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004258 File Offset: 0x00002458
		private void ConnectionDroppedEventCallback(Notification notification)
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ConnectionDroppedEventCallback start: MailboxGuid - {0}, Culture - {1}", this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid, this.sessionContext.SessionCulture.Name);
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				ICollection<MapiNotificationHandlerBase> collection = this.GetNotificationHandlers();
				num2 = collection.Count;
				using (IEnumerator<MapiNotificationHandlerBase> enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MapiNotificationHandlerBase handler = enumerator.Current;
						try
						{
							GrayException.MapAndReportGrayExceptions(delegate()
							{
								handler.HandleConnectionDroppedNotification(notification);
							});
						}
						catch (GrayException arg)
						{
							num++;
							stringBuilder.AppendFormat("[{0} - {1}]", handler.Name, arg);
							ExTraceGlobals.GeneratorTracer.TraceError<string, GrayException>((long)this.GetHashCode(), "Caught an exception during HandleConnectionDroppedNotification of Handler {0}. Exception - {1}", handler.Name, arg);
						}
					}
				}
			}
			finally
			{
				BrokerLogger.Set(LogField.HandlerTotalCount, num2);
				BrokerLogger.Set(LogField.HandlerFailureCount, num);
				BrokerLogger.Set(LogField.Exception, stringBuilder);
				ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "ConnectionDroppedEventCallback End: MailboxGuid - {0} : Culture - {1} : HandlerTotalCount - {2} : HandlerFailureCount - {3}", new object[]
				{
					this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid,
					this.sessionContext.SessionCulture.Name,
					num2,
					num
				});
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004488 File Offset: 0x00002688
		private void FireKeepAlive(object state)
		{
			ExTraceGlobals.GeneratorTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "FireKeepAlive start: MailboxGuid - {0}, Culture - {1}", this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid, this.sessionContext.SessionCulture.Name);
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			using (BrokerLogger.StartEvent("KeepAlive"))
			{
				try
				{
					ICollection<MapiNotificationHandlerBase> collection = this.GetNotificationHandlers();
					num2 = collection.Count;
					Exception exception = null;
					using (IEnumerator<MapiNotificationHandlerBase> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MapiNotificationHandlerBase handler = enumerator.Current;
							try
							{
								GrayException.MapAndReportGrayExceptions(delegate()
								{
									try
									{
										handler.KeepAlive();
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
									num++;
									stringBuilder.AppendFormat("[{0} - {1}]", handler.Name, exception);
									ExTraceGlobals.GeneratorTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Caught an exception during keepAlive event of Handler {0}. Exception - {1}", handler.Name, exception);
								}
							}
						}
					}
				}
				finally
				{
					BrokerLogger.Set(LogField.HandlerTotalCount, num2);
					BrokerLogger.Set(LogField.HandlerFailureCount, num);
					BrokerLogger.Set(LogField.Exception, stringBuilder);
					ExTraceGlobals.GeneratorTracer.TraceDebug((long)this.GetHashCode(), "FireKeepAlive End: MailboxGuid - {0} : Culture - {1} : HandlerTotalCount - {2} : HandlerFailureCount - {3}", new object[]
					{
						this.sessionContext.MailboxPrincipal.MailboxInfo.MailboxGuid,
						this.sessionContext.SessionCulture.Name,
						num2,
						num
					});
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000046E4 File Offset: 0x000028E4
		private ICollection<MapiNotificationHandlerBase> GetNotificationHandlers()
		{
			ICollection<MapiNotificationHandlerBase> result;
			lock (this.syncRoot)
			{
				result = this.notificationHandlers.Values.ToArray<MapiNotificationHandlerBase>();
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004744 File Offset: 0x00002944
		private List<MapiNotificationHandlerBase> GetInactiveHandlers()
		{
			List<MapiNotificationHandlerBase> list = null;
			lock (this.syncRoot)
			{
				if (!base.IsDisposed)
				{
					list = new List<MapiNotificationHandlerBase>();
					KeyValuePair<string, MapiNotificationHandlerBase>[] array = (from kvp in this.notificationHandlers
					where kvp.Value.ServicedSubscriptionCount == 0
					select kvp).ToArray<KeyValuePair<string, MapiNotificationHandlerBase>>();
					foreach (KeyValuePair<string, MapiNotificationHandlerBase> keyValuePair in array)
					{
						list.Add(keyValuePair.Value);
						this.notificationHandlers.Remove(keyValuePair.Key);
					}
					if (this.notificationHandlers.Count == 1)
					{
						string handlerMapString = this.GetHandlerMapString(this.connectionDroppedSubscription.Parameters);
						if (this.notificationHandlers.ContainsKey(handlerMapString))
						{
							list.Add(this.notificationHandlers[handlerMapString]);
							this.notificationHandlers.Remove(handlerMapString);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04000045 RID: 69
		private static readonly TimeSpan KeepAliveTimerInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000046 RID: 70
		private MailboxSessionContext sessionContext;

		// Token: 0x04000047 RID: 71
		private Dictionary<string, MapiNotificationHandlerBase> notificationHandlers;

		// Token: 0x04000048 RID: 72
		private object syncRoot = new object();

		// Token: 0x04000049 RID: 73
		private bool isDisposed;

		// Token: 0x0400004A RID: 74
		private BrokerSubscription connectionDroppedSubscription;

		// Token: 0x0400004B RID: 75
		private GuardedTimer keepAliveTimer;
	}
}

using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200025B RID: 603
	internal sealed class SubscriptionNotificationHandler : NotificationHandlerBase
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0007BB4C File Offset: 0x00079D4C
		public SubscriptionNotificationHandler(UserContext userContext, MailboxSession mailboxSession) : base(userContext, mailboxSession)
		{
			this.payload = new SubscriptionPayload(userContext, mailboxSession, this);
			this.payload.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0007BB70 File Offset: 0x00079D70
		internal override void HandleNotification(Notification notif)
		{
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.TotalMailboxNotifications.Increment();
			}
			lock (this.syncRoot)
			{
				if (this.isDisposed || this.missedNotifications || this.needReinitSubscriptions)
				{
					return;
				}
			}
			QueryNotification queryNotification = notif as QueryNotification;
			if (queryNotification != null)
			{
				if (QueryNotificationType.RowDeleted != queryNotification.EventType)
				{
					if (queryNotification.Row.Length < SubscriptionNotificationHandler.querySubscriptionProperties.Length)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<QueryNotificationType, int, int>((long)this.GetHashCode(), "notification with type {0} has {1} rows, less than expected {2} rows.", queryNotification.EventType, queryNotification.Row.Length, SubscriptionNotificationHandler.querySubscriptionProperties.Length);
						return;
					}
					for (int i = 0; i < SubscriptionNotificationHandler.querySubscriptionProperties.Length; i++)
					{
						if (queryNotification.Row[i] == null)
						{
							ExTraceGlobals.CoreCallTracer.TraceDebug<QueryNotificationType, int>((long)this.GetHashCode(), "notification with type {0} has row {1} equal to null.", queryNotification.EventType, i);
							return;
						}
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag2 = false;
				bool flag3 = false;
				try
				{
					this.userContext.Lock();
					flag2 = true;
					this.missedNotifications = false;
					Utilities.ReconnectStoreSession(this.mailboxSession, this.userContext);
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
					{
						this.UpdateSubscriptionCache(queryNotification, stringWriter);
					}
				}
				catch (OwaLockTimeoutException ex)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out for notification thread. Exception: {0}", ex.Message);
					this.missedNotifications = true;
					flag3 = true;
				}
				catch (Exception ex2)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleNewMailNotification on the notification thread. Exception: {0}", ex2.Message);
					this.missedNotifications = true;
					flag3 = true;
				}
				finally
				{
					if (this.userContext.LockedByCurrentThread() && flag2)
					{
						Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
						this.userContext.Unlock();
					}
				}
				try
				{
					if (0 < stringBuilder.Length && !flag3)
					{
						this.payload.AddPayload(stringBuilder);
						this.payload.PickupData();
					}
				}
				catch (Exception ex3)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleNewMailNotification on the notification thread. Exception: {0}", ex3.Message);
					this.missedNotifications = true;
				}
				return;
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0007BDD4 File Offset: 0x00079FD4
		internal override void HandlePendingGetTimerCallback()
		{
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					return;
				}
			}
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				this.userContext.Lock();
				flag2 = true;
				Utilities.ReconnectStoreSession(this.mailboxSession, this.userContext);
				lock (this.syncRoot)
				{
					if (this.needReinitSubscriptions)
					{
						this.InitSubscription();
						this.needReinitSubscriptions = false;
						this.missedNotifications = true;
					}
					if (this.missedNotifications)
					{
						if (!RecipientCache.RunGetCacheOperationUnderExceptionHandler(delegate
						{
							SubscriptionCache.GetCache(this.userContext, false);
						}, new RecipientCache.ExceptionHandler(this.HandleSubscriptionLoadException), this.GetHashCode()))
						{
							return;
						}
					}
				}
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out in the pending GET timer callback. Exception: {0}", ex.Message);
				this.missedNotifications = true;
				flag3 = true;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in pending GET timer callback thread. Exception: {0}", ex2.Message);
				this.missedNotifications = true;
				flag3 = true;
			}
			finally
			{
				if (this.userContext.LockedByCurrentThread() && flag2)
				{
					Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
					this.userContext.Unlock();
				}
			}
			try
			{
				if (this.missedNotifications && !flag3)
				{
					StringBuilder stringBuilder = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
					{
						SubscriptionNotificationHandler.RefreshClientCache(stringWriter, this.userContext.SubscriptionCache);
					}
					if (0 < stringBuilder.Length)
					{
						this.payload.AddPayload(stringBuilder);
						this.payload.PickupData();
					}
					this.missedNotifications = false;
				}
			}
			catch (Exception ex3)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in pending GET timer callback thread. Exception: {0}", ex3.Message);
				this.missedNotifications = true;
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0007C014 File Offset: 0x0007A214
		protected override void InitSubscription()
		{
			lock (this.syncRoot)
			{
				if (this.mapiSubscription == null)
				{
					using (Folder folder = Folder.Bind(this.mailboxSession, DefaultFolderType.Inbox))
					{
						this.DisposeInternal();
						this.result = folder.ItemQuery(ItemQueryType.Associated, null, null, SubscriptionNotificationHandler.querySubscriptionProperties);
						this.result.GetRows(1);
						this.mapiSubscription = Subscription.Create(this.result, new NotificationHandler(this.HandleNotification));
					}
				}
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0007C0C0 File Offset: 0x0007A2C0
		private static void RefreshClientCache(TextWriter writer, SubscriptionCache cache)
		{
			writer.Write("updSc(\"");
			cache.RenderToJavascript(writer);
			writer.WriteLine("\");");
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0007C0F0 File Offset: 0x0007A2F0
		private void UpdateSubscriptionCache(QueryNotification notification, TextWriter writer)
		{
			bool flag = null == this.userContext.SubscriptionCache;
			if (!RecipientCache.RunGetCacheOperationUnderExceptionHandler(delegate
			{
				SubscriptionCache.GetCache(this.userContext);
			}, new RecipientCache.ExceptionHandler(this.HandleSubscriptionLoadException), this.GetHashCode()))
			{
				return;
			}
			SubscriptionCache subscriptionCache = this.userContext.SubscriptionCache;
			if (flag)
			{
				SubscriptionNotificationHandler.RefreshClientCache(writer, subscriptionCache);
				return;
			}
			int num;
			if (QueryNotificationType.RowDeleted == notification.EventType)
			{
				num = subscriptionCache.Delete(notification.Index);
			}
			else
			{
				object obj = notification.Row[0];
				if (!(obj is Guid))
				{
					return;
				}
				Guid id = (Guid)obj;
				string text = notification.Row[1] as string;
				if (text == null)
				{
					return;
				}
				string text2 = notification.Row[2] as string;
				if (text2 == null)
				{
					return;
				}
				obj = notification.Row[3];
				if (!(obj is int))
				{
					return;
				}
				SendAsState sendAsState = (SendAsState)obj;
				obj = notification.Row[4];
				if (!(obj is int))
				{
					return;
				}
				AggregationStatus status = (AggregationStatus)obj;
				string address = Utilities.DecodeIDNDomain(text);
				SubscriptionCacheEntry entry = new SubscriptionCacheEntry(id, address, text2, notification.Index, this.mailboxSession.PreferedCulture);
				switch (notification.EventType)
				{
				case QueryNotificationType.RowAdded:
					if (!SubscriptionManager.IsValidForSendAs(sendAsState, status))
					{
						return;
					}
					num = subscriptionCache.Add(entry);
					goto IL_14B;
				case QueryNotificationType.RowModified:
					num = subscriptionCache.Modify(entry, sendAsState, status);
					goto IL_14B;
				}
				num = -1;
			}
			IL_14B:
			if (-1 < num)
			{
				SubscriptionNotificationHandler.RefreshClientCache(writer, subscriptionCache);
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0007C253 File Offset: 0x0007A453
		private void HandleSubscriptionLoadException(Exception e, int hashCode)
		{
			ExTraceGlobals.CoreCallTracer.TraceError<string>((long)hashCode, "Failed to get subscription cache from server on the notification thread. Exception: {0}", e.Message);
			this.missedNotifications = true;
		}

		// Token: 0x04000DDB RID: 3547
		private static readonly PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			MessageItemSchema.SharingInstanceGuid,
			AggregationSubscriptionMessageSchema.SharingInitiatorSmtp,
			AggregationSubscriptionMessageSchema.SharingInitiatorName,
			MessageItemSchema.SharingSendAsState,
			AggregationSubscriptionMessageSchema.SharingAggregationStatus
		};

		// Token: 0x04000DDC RID: 3548
		private SubscriptionPayload payload;

		// Token: 0x0200025C RID: 604
		private enum QuerySubscriptionRow
		{
			// Token: 0x04000DDE RID: 3550
			Id,
			// Token: 0x04000DDF RID: 3551
			SmtpAddress,
			// Token: 0x04000DE0 RID: 3552
			DisplayName,
			// Token: 0x04000DE1 RID: 3553
			SendAsState,
			// Token: 0x04000DE2 RID: 3554
			Status
		}

		// Token: 0x0200025D RID: 605
		// (Invoke) Token: 0x06001450 RID: 5200
		private delegate void GenericOperation();
	}
}

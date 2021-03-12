using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000203 RID: 515
	internal class PushNotificationAssistantAdapter
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x0007346C File Offset: 0x0007166C
		public PushNotificationAssistantAdapter(PushNotificationAssistantConfig config, IDatabaseInfo databaseInfo, IXSOFactory xsoFactory, PushNotificationSubscriptionTableEntry tableEntry, PushNotificationBatchManager notificationManager)
		{
			this.AssistantConfig = config;
			this.IDatabaseInfo = databaseInfo;
			this.XSOFactory = xsoFactory;
			this.MailboxTable = tableEntry;
			this.NotificationBatchManager = notificationManager;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000734A4 File Offset: 0x000716A4
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x000734AC File Offset: 0x000716AC
		private protected PushNotificationAssistantConfig AssistantConfig { protected get; private set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000734B5 File Offset: 0x000716B5
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x000734BD File Offset: 0x000716BD
		private protected IXSOFactory XSOFactory { protected get; private set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000734C6 File Offset: 0x000716C6
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x000734CE File Offset: 0x000716CE
		private protected IDatabaseInfo IDatabaseInfo { protected get; private set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x000734D7 File Offset: 0x000716D7
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x000734DF File Offset: 0x000716DF
		private protected PushNotificationSubscriptionTableEntry MailboxTable { protected get; private set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x000734E8 File Offset: 0x000716E8
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x000734F0 File Offset: 0x000716F0
		private protected PushNotificationBatchManager NotificationBatchManager { protected get; private set; }

		// Token: 0x060013D1 RID: 5073 RVA: 0x000734F9 File Offset: 0x000716F9
		internal void OnStart(EventBasedStartInfo startInfo)
		{
			this.assistantCache.OnStart(this.IDatabaseInfo);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0007350C File Offset: 0x0007170C
		internal void OnShutdown()
		{
			this.assistantCache.OnShutdown();
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0007351C File Offset: 0x0007171C
		internal bool IsEventInteresting(IMapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType != ObjectType.MAPI_MESSAGE && mapiEvent.ItemType != ObjectType.MAPI_FOLDER)
			{
				return false;
			}
			if (PushNotificationMapiEventAnalyzer.IsSubscriptionChangeEvent(mapiEvent))
			{
				return true;
			}
			if (!this.AssistantConfig.IsPublishingEnabled)
			{
				return false;
			}
			if (PushNotificationMapiEventAnalyzer.IsNewMessageEvent(mapiEvent))
			{
				return this.assistantCache.IsCacheUpdateRequiredForEmailSubscription(mapiEvent);
			}
			return PushNotificationMapiEventAnalyzer.IsIpmFolderContentChangeEvent(mapiEvent) && this.assistantCache.IsValidFolderMessageForEmailSubscription(mapiEvent);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00073581 File Offset: 0x00071781
		internal void HandleEvent(IMapiEvent mapiEvent, IMailboxSession session, IStoreObject item)
		{
			if (PushNotificationMapiEventAnalyzer.IsSubscriptionChangeEvent(mapiEvent))
			{
				this.HandleSubscriptionChangeEvent(session, item);
				return;
			}
			this.HandleNotificationEvent(mapiEvent, session, item);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000735A0 File Offset: 0x000717A0
		private void HandleSubscriptionChangeEvent(IMailboxSession session, IStoreObject item)
		{
			IPushNotificationSubscriptionItem pushNotificationSubscriptionItem = item as IPushNotificationSubscriptionItem;
			if (pushNotificationSubscriptionItem == null)
			{
				throw new InvalidStoreObjectInstanceException((item != null) ? item.GetType() : null);
			}
			try
			{
				PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationServerSubscription.FromJson(pushNotificationSubscriptionItem.SerializedNotificationSubscription);
				this.assistantCache.UpdateSubscriptionData(session.MailboxGuid, pushNotificationServerSubscription);
				if (pushNotificationServerSubscription.GetSubscriptionOption() == this.MailboxTable.ReadSubscriptionOnMailboxTable(session))
				{
					ExTraceGlobals.PushNotificationAssistantTracer.TraceDebug<Guid>((long)this.GetHashCode(), "PushNotificationAssistantAdapter.HandleSubscriptionChangeEvent: Mailbox Header Table is up to date for {0}.", session.MailboxGuid);
					PushNotificationsAssistantPerfCounters.TotalSubscriptionsUpdated.Increment();
				}
				else
				{
					this.MailboxTable.UpdateSubscriptionOnMailboxTable(session, pushNotificationServerSubscription);
					PushNotificationHelper.LogSubscriptionUpdated(pushNotificationSubscriptionItem, pushNotificationServerSubscription, session.MailboxGuid);
					PushNotificationsAssistantPerfCounters.TotalNewSubscriptionsCreated.Increment();
					PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions.Increment();
				}
			}
			catch (Exception ex)
			{
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToUpdateSubscriptionOnMailboxTable, ex.ToString(), new object[]
				{
					pushNotificationSubscriptionItem.SerializedNotificationSubscription,
					session.MailboxGuid,
					ex.ToTraceString()
				});
				throw;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000736A8 File Offset: 0x000718A8
		private void HandleNotificationEvent(IMapiEvent mapiEvent, IMailboxSession session, IStoreObject item)
		{
			PushNotificationContext pushNotificationContext = new PushNotificationContext
			{
				OriginalTime = ExDateTime.UtcNow
			};
			if (this.assistantCache.ShouldProcessUnseenEmailEvent(session, this.XSOFactory, mapiEvent) && this.PopulateSubscriptionsAndTenantId(mapiEvent, session, pushNotificationContext))
			{
				if (this.assistantCache.IsBackgroundSyncEnabled(mapiEvent.MailboxGuid))
				{
					pushNotificationContext.BackgroundSyncType = BackgroundSyncType.Email;
				}
				pushNotificationContext.UnseenEmailCount = new int?(this.ResolveUnseenEmailCount(mapiEvent, pushNotificationContext.Subscriptions));
				PushNotificationHelper.LogMailboxNotificationResolution(mapiEvent.MailboxGuid, pushNotificationContext);
				this.NotificationBatchManager.Add(mapiEvent.MailboxGuid, pushNotificationContext);
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00073738 File Offset: 0x00071938
		private bool PopulateSubscriptionsAndTenantId(IMapiEvent mapiEvent, IMailboxSession session, PushNotificationContext pushNotificationContext)
		{
			PushNotificationContext pushNotificationContext2;
			if (this.NotificationBatchManager.TryGetPushNotification(mapiEvent.MailboxGuid, out pushNotificationContext2))
			{
				pushNotificationContext.Subscriptions = pushNotificationContext2.Subscriptions;
				pushNotificationContext.TenantId = pushNotificationContext2.TenantId;
			}
			else
			{
				using (IPushNotificationStorage pushNotificationStorage = this.XSOFactory.FindPushNotificationStorage(session))
				{
					if (pushNotificationStorage != null)
					{
						pushNotificationContext.TenantId = pushNotificationStorage.TenantId;
						pushNotificationContext.Subscriptions = this.ResolveSubscriptions(session, pushNotificationStorage);
					}
				}
			}
			return pushNotificationContext.Subscriptions != null && pushNotificationContext.Subscriptions.Count > 0;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000737D4 File Offset: 0x000719D4
		private List<PushNotificationServerSubscription> ResolveSubscriptions(IMailboxSession session, IPushNotificationStorage storage)
		{
			List<PushNotificationServerSubscription> list = storage.GetActiveNotificationSubscriptions(session, this.AssistantConfig.SubscriptionExpirationInHours);
			if (list == null || list.Count == 0)
			{
				PushNotificationsAssistantPerfCounters.TotalSubscriptionsExpiredCleanup.Increment();
				PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions.Decrement();
				PushNotificationHelper.LogSubscriptionExpired(session.MailboxGuid);
				this.assistantCache.RemoveSubscriptions(session.MailboxGuid);
				this.MailboxTable.DisableSubscriptionOnMailboxTable(session);
				storage.DeleteExpiredSubscriptions(this.AssistantConfig.SubscriptionExpirationInHours);
				list = null;
			}
			else
			{
				list = list.Distinct(PushNotificationAssistantAdapter.SubscriptionComparer.Default).ToList<PushNotificationServerSubscription>();
				PushNotificationHelper.LogActiveSubscriptions(session.MailboxGuid, list);
				if (session.IsMailboxOof())
				{
					List<PushNotificationServerSubscription> list2 = new List<PushNotificationServerSubscription>();
					foreach (PushNotificationServerSubscription pushNotificationServerSubscription in list)
					{
						if ((pushNotificationServerSubscription.GetSubscriptionOption() & PushNotificationSubscriptionOption.SuppressNotificationsWhenOof) == PushNotificationSubscriptionOption.NoSubscription)
						{
							list2.Add(pushNotificationServerSubscription);
						}
						else
						{
							PushNotificationHelper.LogSuppressedNotifications(session.MailboxGuid, pushNotificationServerSubscription);
						}
					}
					list = list2;
				}
			}
			return list;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x000738DC File Offset: 0x00071ADC
		private int ResolveUnseenEmailCount(IMapiEvent mapiEvent, List<PushNotificationServerSubscription> subscriptions)
		{
			int result;
			if (!PushNotificationMapiEventAnalyzer.IsIpmFolderContentChangeEvent(mapiEvent))
			{
				long num = this.assistantCache.ReadEmailWatermarkFromCache(mapiEvent.MailboxGuid);
				long num2 = (subscriptions[0].InboxUnreadCount != null) ? subscriptions[0].InboxUnreadCount.Value : num;
				if (num <= num2)
				{
					result = 1;
					this.assistantCache.UpdateEmailWatermarkToCache(mapiEvent.MailboxGuid, num - 1L);
				}
				else
				{
					result = (int)(num - num2);
					this.assistantCache.UpdateEmailWatermarkToCache(mapiEvent.MailboxGuid, num2);
				}
			}
			else
			{
				long num2 = this.assistantCache.ReadEmailWatermarkFromCache(mapiEvent.MailboxGuid);
				long num = mapiEvent.UnreadItemCount;
				if (num <= num2)
				{
					result = 1;
					this.assistantCache.UpdateEmailWatermarkToCache(mapiEvent.MailboxGuid, num - 1L);
				}
				else
				{
					result = (int)(num - num2);
				}
			}
			return result;
		}

		// Token: 0x04000C10 RID: 3088
		private PushNotificationDataHandler assistantCache = new PushNotificationDataHandler();

		// Token: 0x02000204 RID: 516
		private class SubscriptionComparer : IEqualityComparer<PushNotificationServerSubscription>
		{
			// Token: 0x060013DA RID: 5082 RVA: 0x000739A6 File Offset: 0x00071BA6
			public bool Equals(PushNotificationServerSubscription x, PushNotificationServerSubscription y)
			{
				return object.ReferenceEquals(x, y) || (x != null && y != null && x.AppId == y.AppId && x.DeviceNotificationId == y.DeviceNotificationId);
			}

			// Token: 0x060013DB RID: 5083 RVA: 0x000739E4 File Offset: 0x00071BE4
			public int GetHashCode(PushNotificationServerSubscription obj)
			{
				if (obj == null)
				{
					return 0;
				}
				int num = (obj.AppId != null) ? obj.AppId.GetHashCode() : 0;
				int num2 = (obj.DeviceNotificationId != null) ? obj.DeviceNotificationId.GetHashCode() : 0;
				return num ^ num2;
			}

			// Token: 0x04000C16 RID: 3094
			public static readonly PushNotificationAssistantAdapter.SubscriptionComparer Default = new PushNotificationAssistantAdapter.SubscriptionComparer();
		}
	}
}

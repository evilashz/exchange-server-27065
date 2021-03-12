using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000015 RID: 21
	internal class UnseenCountNotificationHandler : MapiNotificationHandlerBase
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00005E5A File Offset: 0x0000405A
		public UnseenCountNotificationHandler(string name, MailboxSessionContext sessionContext, BaseSubscription parameters) : base(name, sessionContext)
		{
			this.adSession = sessionContext.MailboxPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings().CreateRecipientSession(null);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005E88 File Offset: 0x00004088
		internal override void HandleNotificationInternal(Notification notification, object context)
		{
			if (!(notification is QueryNotification))
			{
				return;
			}
			if (base.IsDisposed)
			{
				return;
			}
			this.GeneratePayloads();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005EB0 File Offset: 0x000040B0
		internal override void KeepAliveInternal()
		{
			lock (base.SyncRoot)
			{
				base.InitSubscription();
				if (base.NeedRefreshPayload)
				{
					this.GeneratePayloads();
					base.NeedRefreshPayload = false;
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000601C File Offset: 0x0000421C
		protected virtual Dictionary<Guid, ExDateTime> GetUnSeenData()
		{
			Dictionary<Guid, ExDateTime> notifierData = new Dictionary<Guid, ExDateTime>();
			base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
			{
				this.unseenItemsReader.LoadLastNItemReceiveDates(session);
				IEnumerable<UserMailboxLocator> userMailboxLocators = from UnseenCountSubscription unseen in 
					from s in this.ServicedSubscriptionsReadOnly.Values
					select s.Parameters
				select new UserMailboxLocator(this.adSession, unseen.UserExternalDirectoryObjectId, unseen.UserLegacyDN);
				IMemberSubscriptionItem[] memberSubscriptions = this.groupNotificationLocator.GetMemberSubscriptions(session, userMailboxLocators);
				int num = 0;
				foreach (BrokerSubscription brokerSubscription in this.ServicedSubscriptionsReadOnly.Values)
				{
					notifierData[brokerSubscription.SubscriptionId] = memberSubscriptions[num++].LastUpdateTimeUTC;
				}
			});
			return notifierData;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006060 File Offset: 0x00004260
		protected override void InitSubscriptionInternal(MailboxSession session)
		{
			if (!base.SessionContext.MailboxSessionLockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling method UnseenCountNotificationHandler.InitSubscriptionInternal");
			}
			if (this.unseenItemsReader != null)
			{
				this.unseenItemsReader.Dispose();
			}
			this.groupNotificationLocator = new ModernGroupNotificationLocator(this.adSession);
			this.unseenItemsReader = UnseenItemsReader.Create(session);
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			using (Folder folder = Folder.Bind(session, defaultFolderId))
			{
				base.QueryResult = this.GetQueryResult(folder);
				base.QueryResult.GetRows(1);
				base.Subscription = Subscription.Create(base.QueryResult, new NotificationHandler(base.HandleNotification));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006144 File Offset: 0x00004344
		protected override void InternalDispose(bool isDisposing)
		{
			lock (base.SyncRoot)
			{
				if (isDisposing && this.unseenItemsReader != null)
				{
					base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
					{
						base.DisposeXSOObjects(new IDisposable[]
						{
							this.unseenItemsReader
						});
						this.unseenItemsReader = null;
					});
				}
				base.InternalDispose(isDisposing);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000061B0 File Offset: 0x000043B0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UnseenCountNotificationHandler>(this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000061B8 File Offset: 0x000043B8
		private QueryResult GetQueryResult(Folder folder)
		{
			return folder.ItemQuery(ItemQueryType.None, null, UnseenCountNotificationHandler.UnseenItemSortBy, UnseenCountNotificationHandler.UnseenItemQueryProperties);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000061E8 File Offset: 0x000043E8
		private void GeneratePayloads()
		{
			Dictionary<Guid, ExDateTime> data = this.GetUnSeenData();
			base.SendPayloadsToQueue((BrokerSubscription brokerSubscription) => this.GetPayload(data, brokerSubscription));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006220 File Offset: 0x00004420
		private UnseenCountNotification GetPayload(Dictionary<Guid, ExDateTime> data, BrokerSubscription brokerSubscription)
		{
			return new UnseenCountNotification
			{
				ConsumerSubscriptionId = brokerSubscription.Parameters.ConsumerSubscriptionId,
				UnseenData = new UnseenDataType(this.unseenItemsReader.GetUnseenItemCount(data[brokerSubscription.SubscriptionId]), ExDateTimeConverter.ToUtcXsdDateTime(data[brokerSubscription.SubscriptionId]))
			};
		}

		// Token: 0x04000068 RID: 104
		private static readonly PropertyDefinition[] UnseenItemQueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000069 RID: 105
		private static readonly SortBy[] UnseenItemSortBy = new SortBy[]
		{
			new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
		};

		// Token: 0x0400006A RID: 106
		private readonly IRecipientSession adSession;

		// Token: 0x0400006B RID: 107
		private ModernGroupNotificationLocator groupNotificationLocator;

		// Token: 0x0400006C RID: 108
		private IUnseenItemsReader unseenItemsReader;
	}
}

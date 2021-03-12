using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionMessageHelper
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001D537 File Offset: 0x0001B737
		public SubscriptionMessageHelper() : this(CommonLoggingHelper.SyncLogSession)
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001D544 File Offset: 0x0001B744
		protected SubscriptionMessageHelper(SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001D560 File Offset: 0x0001B760
		public virtual void SaveSubscription(MailboxSession mailboxSession, ISyncWorkerData subscription)
		{
			StoreObjectId subscriptionMessageId = subscription.SubscriptionMessageId;
			if (subscriptionMessageId == null)
			{
				using (Folder folder = Folder.Bind(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
				{
					using (MessageItem messageItem = MessageItem.CreateAssociated(mailboxSession, folder.Id))
					{
						this.SaveSubscriptionToMessage(subscription, messageItem);
						messageItem.Load(new PropertyDefinition[]
						{
							ItemSchema.Id
						});
						subscription.SubscriptionMessageId = messageItem.Id.ObjectId;
					}
					return;
				}
			}
			AggregationSubscriptionType subscriptionType = subscription.SubscriptionType;
			PropertyDefinition[] propertyDefinitions = SubscriptionManager.GetPropertyDefinitions(subscriptionType);
			using (MessageItem messageItem2 = MessageItem.Bind(mailboxSession, subscriptionMessageId, propertyDefinitions))
			{
				messageItem2.OpenAsReadWrite();
				this.SaveSubscriptionToMessage(subscription, messageItem2);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001D63C File Offset: 0x0001B83C
		public virtual void SaveSubscriptionToMessage(ISyncWorkerData subscription, MessageItem message)
		{
			this.syncLogSession.RetailAssert(subscription.Status != AggregationStatus.InvalidVersion, "Invalid Version messages cannot be saved back. They are to be marked in memory only.", new object[0]);
			subscription.SetToMessageObject(message);
			message.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001D670 File Offset: 0x0001B870
		public virtual void DeleteSubscription(MailboxSession mailboxSession, StoreId messageId)
		{
			mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				messageId
			});
		}

		// Token: 0x04000338 RID: 824
		private readonly SyncLogSession syncLogSession;
	}
}

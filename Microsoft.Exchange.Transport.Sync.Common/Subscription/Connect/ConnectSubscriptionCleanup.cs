using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Facebook;
using Microsoft.Exchange.Net.LinkedIn;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000DA RID: 218
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConnectSubscriptionCleanup : IConnectSubscriptionCleanup
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001FED5 File Offset: 0x0001E0D5
		public ConnectSubscriptionCleanup(ISubscriptionManager manager)
		{
			SyncUtilities.ThrowIfArgumentNull("manager", manager);
			this.SubscriptionManager = manager;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001FEEF File Offset: 0x0001E0EF
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001FEF7 File Offset: 0x0001E0F7
		private ISubscriptionManager SubscriptionManager { get; set; }

		// Token: 0x06000688 RID: 1672 RVA: 0x0001FF00 File Offset: 0x0001E100
		public void Cleanup(MailboxSession mailbox, IConnectSubscription subscription, bool sendRpcNotification = true)
		{
			SyncUtilities.ThrowIfArgumentNull("mailbox", mailbox);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			this.DisableSubscription(mailbox, subscription);
			this.DeleteContacts(mailbox, subscription);
			this.DeleteOscSyncEnabledOnServerInSyncLock(mailbox, subscription);
			this.TryRemovePermissions(subscription);
			this.DeleteSubscription(mailbox, subscription, sendRpcNotification);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001FF40 File Offset: 0x0001E140
		internal void DisableSubscription(MailboxSession mailbox, IConnectSubscription subscription)
		{
			subscription.Status = AggregationStatus.Disabled;
			subscription.DetailedAggregationStatus = DetailedAggregationStatus.RemoveSubscription;
			string str = string.IsNullOrEmpty(subscription.Diagnostics) ? string.Empty : Environment.NewLine;
			subscription.Diagnostics = subscription.Diagnostics + str + "Disabling before removing data and the subscription eventually";
			this.SubscriptionManager.UpdateSubscriptionToMailbox(mailbox, subscription);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001FF9C File Offset: 0x0001E19C
		internal void DeleteContacts(MailboxSession mailbox, IConnectSubscription subscription)
		{
			try
			{
				StoreObjectId storeObjectId = this.RetrieveFolderId(mailbox, subscription);
				StoreObjectId parentFolderId = mailbox.GetParentFolderId(storeObjectId);
				using (Folder folder = Folder.Bind(mailbox, parentFolderId))
				{
					AggregateOperationResult aggregateOperationResult = folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
					{
						storeObjectId
					});
					if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
					{
						throw new FailedDeletePeopleConnectSubscriptionException(subscription.SubscriptionType);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002001C File Offset: 0x0001E21C
		internal void TryRemovePermissions(IConnectSubscription subscription)
		{
			AggregationSubscriptionType subscriptionType = subscription.SubscriptionType;
			IRemoveConnectSubscription removeConnectSubscription;
			if (subscriptionType != AggregationSubscriptionType.Facebook)
			{
				if (subscriptionType != AggregationSubscriptionType.LinkedIn)
				{
					throw new InvalidOperationException("Unknown subscription type: " + subscription.SubscriptionType);
				}
				removeConnectSubscription = ConnectSubscriptionCleanup.InitLinkedInProviderImpl();
			}
			else
			{
				removeConnectSubscription = ConnectSubscriptionCleanup.InitFacebookProviderImpl();
			}
			removeConnectSubscription.TryRemovePermissions(subscription);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002006E File Offset: 0x0001E26E
		internal void DeleteSubscription(MailboxSession mailbox, IConnectSubscription subscription, bool sendRpcNotification = true)
		{
			this.SubscriptionManager.DeleteSubscription(mailbox, subscription, sendRpcNotification);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00020080 File Offset: 0x0001E280
		internal void DeleteOscSyncEnabledOnServerInSyncLock(MailboxSession mailbox, IConnectSubscription subscription)
		{
			try
			{
				StoreObjectId messageId = new OscSyncLockLocator(mailbox).Find(subscription.Name, subscription.UserId);
				using (MessageItem messageItem = MessageItem.Bind(mailbox, messageId, ConnectSubscriptionCleanup.PropertiesToLoadFromSyncLock))
				{
					if (messageItem.GetValueOrDefault<bool>(MessageItemSchema.OscSyncEnabledOnServer, false))
					{
						messageItem.OpenAsReadWrite();
						messageItem.Delete(MessageItemSchema.OscSyncEnabledOnServer);
						messageItem.Save(SaveMode.ResolveConflicts);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00020108 File Offset: 0x0001E308
		private static IRemoveConnectSubscription InitFacebookProviderImpl()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
			IFacebookClient client = new FacebookClient(new Uri(peopleConnectApplicationConfig.GraphApiEndpoint));
			return new RemoveFacebookSubscription(client);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00020138 File Offset: 0x0001E338
		private static IRemoveConnectSubscription InitLinkedInProviderImpl()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			ILinkedInWebClient client = new LinkedInWebClient(new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri), NullTracer.Instance);
			return new RemoveLinkedInSubscription(client);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00020190 File Offset: 0x0001E390
		private StoreObjectId RetrieveFolderId(MailboxSession mailbox, IConnectSubscription subscription)
		{
			return new OscFolderLocator(mailbox).Find(subscription.Name, subscription.UserId);
		}

		// Token: 0x04000387 RID: 903
		internal const string DiagnosticInformation = "Disabling before removing data and the subscription eventually";

		// Token: 0x04000388 RID: 904
		private static readonly PropertyDefinition[] PropertiesToLoadFromSyncLock = new StorePropertyDefinition[]
		{
			MessageItemSchema.OscSyncEnabledOnServer
		};
	}
}

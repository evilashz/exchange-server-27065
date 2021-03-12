using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionManager : ISubscriptionManager
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0001BC60 File Offset: 0x00019E60
		public static void DeleteSubscription(MailboxSession mailboxSession, Guid subscriptionGuid)
		{
			StoreId messageId = null;
			int subscriptionCount = 0;
			SubscriptionManager.ForEachSubscriptionInMailbox(mailboxSession, delegate(object[] item)
			{
				subscriptionCount++;
				if (!(item[2] is PropertyError) && !(item[1] is PropertyError))
				{
					Guid b = (Guid)item[2];
					if (subscriptionGuid == b)
					{
						AggregationSubscriptionType subscriptionType;
						if (!(item[0] is string))
						{
							subscriptionType = AggregationSubscriptionType.Unknown;
						}
						else
						{
							subscriptionType = AggregationSubscription.GetSubscriptionKind((string)item[0]);
						}
						try
						{
							SubscriptionManager.DeleteSubscriptionSyncState(mailboxSession, subscriptionGuid, subscriptionType);
							ReportData reportData = SkippedItemUtilities.GetReportData(subscriptionGuid);
							reportData.Delete(mailboxSession.Mailbox.MapiStore);
						}
						catch (LocalizedException ex)
						{
							CommonLoggingHelper.SyncLogSession.LogError((TSLID)9989UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to delete report data from subscription with ID {0}, due to error: {1}", new object[]
							{
								subscriptionGuid,
								ex
							});
						}
						messageId = (StoreId)item[1];
						SubscriptionManager.instance.UpdateMailboxTableAndPerformSubscriptionOperation<StoreId>(mailboxSession, messageId, new Action<MailboxSession, StoreId>(SubscriptionManager.instance.messageHelper.DeleteSubscription));
						subscriptionCount--;
					}
				}
				return true;
			});
			if (messageId == null)
			{
				throw new ObjectNotFoundException(Strings.SubscriptionNotFound(subscriptionGuid.ToString()));
			}
			if (subscriptionCount == 0)
			{
				SubscriptionManager.instance.mailboxTableSubscriptionPropertyHelper.UpdateContentAggregationFlags(mailboxSession, ContentAggregationFlags.None);
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001BCE8 File Offset: 0x00019EE8
		public static bool TryDeleteSubscription(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			Exception ex = null;
			try
			{
				SubscriptionManager.Instance.DeleteSubscription(mailboxSession, subscription, true);
			}
			catch (LocalizedException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)106UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to delete subscription with ID {0}, due to error: {1}", new object[]
				{
					subscription.SubscriptionGuid,
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001BD70 File Offset: 0x00019F70
		public static bool TryDeleteSubscription(MailboxSession mailboxSession, Guid subscriptionGuid)
		{
			Exception ex = null;
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			try
			{
				SubscriptionManager.DeleteSubscription(mailboxSession, subscriptionGuid);
			}
			catch (LocalizedException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)68UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to load delete subscription with ID {0}, due to error: {1}", new object[]
				{
					subscriptionGuid,
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public static AggregationSubscription GetSubscription(MailboxSession mailboxSession, Guid subscriptionGuid)
		{
			return SubscriptionManager.GetSubscription(mailboxSession, subscriptionGuid, true);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public static AggregationSubscription GetSubscription(MailboxSession mailboxSession, Guid subscriptionGuid, bool upgradeIfRequired)
		{
			if (!SyncUtilities.IsDatacenterMode())
			{
				return null;
			}
			AggregationSubscriptionType subscriptionType;
			StoreId storeId = SubscriptionManager.FindSubscription(mailboxSession, subscriptionGuid, out subscriptionType);
			if (storeId == null)
			{
				throw new ObjectNotFoundException(Strings.SubscriptionNotFound(subscriptionGuid.ToString()));
			}
			return SubscriptionManager.LoadSubscription(mailboxSession, storeId, subscriptionType, upgradeIfRequired);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001BE34 File Offset: 0x0001A034
		public static List<AggregationSubscription> GetAllSubscriptions(MailboxSession mailboxSession, AggregationSubscriptionType subscriptionTypeFilter)
		{
			return SubscriptionManager.GetAllSubscriptions(mailboxSession, subscriptionTypeFilter, true);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001BE40 File Offset: 0x0001A040
		public static List<TransactionalRequestJob> GetAllSyncRequests(MailboxSession session)
		{
			Guid databaseGuid = session.MailboxOwner.MailboxInfo.GetDatabaseGuid();
			List<TransactionalRequestJob> result;
			using (RequestJobProvider requestJobProvider = new RequestJobProvider(databaseGuid))
			{
				requestJobProvider.AttachToMDB(databaseGuid);
				RequestIndexEntryQueryFilter filter = new RequestIndexEntryQueryFilter();
				IConfigurable[] array = requestJobProvider.Find<TransactionalRequestJob>(filter, null, true, null);
				List<TransactionalRequestJob> list = new List<TransactionalRequestJob>(array.Length);
				foreach (IConfigurable configurable in array)
				{
					list.Add(configurable as TransactionalRequestJob);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public static List<AggregationSubscription> GetAllSubscriptions(MailboxSession mailboxSession, AggregationSubscriptionType subscriptionTypeFilter, bool upgradeIfRequired)
		{
			if (!SyncUtilities.IsDatacenterMode())
			{
				return new List<AggregationSubscription>();
			}
			List<AggregationSubscription> allSubscriptions = new List<AggregationSubscription>();
			SubscriptionManager.ForEachSubscriptionInMailbox(mailboxSession, delegate(object[] item)
			{
				if (!(item[0] is PropertyError) && !(item[1] is PropertyError))
				{
					string messageClass = (string)item[0];
					if ((AggregationSubscription.GetSubscriptionKind(messageClass) & subscriptionTypeFilter) != AggregationSubscriptionType.Unknown)
					{
						StoreId storeId = (StoreId)item[1];
						AggregationSubscriptionType subscriptionKind = AggregationSubscription.GetSubscriptionKind((string)item[0]);
						AggregationSubscription aggregationSubscription = null;
						Exception ex = null;
						try
						{
							aggregationSubscription = SubscriptionManager.LoadSubscription(mailboxSession, storeId, subscriptionKind, upgradeIfRequired);
						}
						catch (PropertyErrorException ex2)
						{
							ex = ex2;
						}
						catch (CorruptDataException ex3)
						{
							ex = ex3;
						}
						catch (InvalidDataException ex4)
						{
							ex = ex4;
						}
						if (aggregationSubscription != null)
						{
							aggregationSubscription.InstanceKey = (item[4] as byte[]);
							allSubscriptions.Add(aggregationSubscription);
						}
						else if (ex != null)
						{
							CommonLoggingHelper.SyncLogSession.LogError((TSLID)69UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to load subscription with messageId: {0}, due to error: {1}", new object[]
							{
								storeId.ToBase64String(),
								ex
							});
						}
					}
				}
				return true;
			});
			return allSubscriptions;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001C04C File Offset: 0x0001A24C
		public static SendAsSubscriptionsAndPeopleConnectResult GetAllSendAsSubscriptionsAndPeopleConnect(MailboxSession mailboxSession)
		{
			List<AggregationSubscription> allSubscriptions = SubscriptionManager.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.AllThatSupportSendAsAndPeopleConnect, true);
			List<PimAggregationSubscription> allSendAsSubscriptions = SubscriptionManager.GetAllSendAsSubscriptions(allSubscriptions);
			bool peopleConnectionsExistInformation = SubscriptionManager.GetPeopleConnectionsExistInformation(allSubscriptions);
			return new SendAsSubscriptionsAndPeopleConnectResult(allSendAsSubscriptions, peopleConnectionsExistInformation);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001C078 File Offset: 0x0001A278
		public static List<PimAggregationSubscription> GetAllSendAsSubscriptions(MailboxSession mailboxSession)
		{
			return SubscriptionManager.GetAllSendAsSubscriptions(mailboxSession, true);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001C084 File Offset: 0x0001A284
		public static List<PimAggregationSubscription> GetAllSendAsSubscriptions(MailboxSession mailboxSession, bool upgradeIfRequired)
		{
			List<AggregationSubscription> allSubscriptions = SubscriptionManager.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.AllEMail, upgradeIfRequired);
			return SubscriptionManager.GetAllSendAsSubscriptions(allSubscriptions);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		private static List<PimAggregationSubscription> GetAllSendAsSubscriptions(List<AggregationSubscription> subscriptions)
		{
			List<PimAggregationSubscription> list = new List<PimAggregationSubscription>();
			foreach (AggregationSubscription aggregationSubscription in subscriptions)
			{
				PimAggregationSubscription pimAggregationSubscription = aggregationSubscription as PimAggregationSubscription;
				if (pimAggregationSubscription == null)
				{
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)70UL, ExTraceGlobals.SubscriptionManagerTracer, "Subscription is not a PimSubscription, so it's not valid for sendAs: {0}.", new object[]
					{
						aggregationSubscription.Name
					});
				}
				else if (SubscriptionManager.IsValidForSendAs(pimAggregationSubscription.SendAsState, pimAggregationSubscription.Status))
				{
					list.Add(pimAggregationSubscription);
				}
				else
				{
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)71UL, ExTraceGlobals.SubscriptionManagerTracer, "Subscription not valid for sendAs: {0}.", new object[]
					{
						pimAggregationSubscription.Name
					});
				}
			}
			return list;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001C180 File Offset: 0x0001A380
		private static bool GetPeopleConnectionsExistInformation(List<AggregationSubscription> subscriptions)
		{
			foreach (AggregationSubscription aggregationSubscription in subscriptions)
			{
				if (aggregationSubscription.AggregationType == AggregationType.PeopleConnection)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public static bool DoesUserHasAnyActiveConnectedAccounts(MailboxSession mailboxSession, AggregationSubscriptionType subscriptionFilter)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			return SubscriptionManager.FetchSubscriptionsAndCheckForAnyActiveAggregationSubscriptions(() => SubscriptionManager.GetAllSubscriptions(mailboxSession, subscriptionFilter));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001C236 File Offset: 0x0001A436
		public static bool IsValidForSendAs(SendAsState state, AggregationStatus status)
		{
			return SendAsState.Enabled == state && AggregationStatus.Disabled != status && AggregationStatus.Poisonous != status;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001C24C File Offset: 0x0001A44C
		public static void CreateSubscription(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			SubscriptionMailboxSession subMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
			SubscriptionManager.instance.CreateAndNotifyNewSubscription(subMailboxSession, subscription);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001C274 File Offset: 0x0001A474
		public static Exception UpdateSubscription(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			Exception result = null;
			try
			{
				SubscriptionManager.Instance.UpdateSubscriptionToMailbox(mailboxSession, subscription);
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		public static Exception TryUpdateSubscriptionAndSyncNow(MailboxSession mailboxSession, AggregationSubscription subscription, out bool syncNowRequestSentSuccessfully)
		{
			SubscriptionMailboxSession subMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
			Exception result;
			SubscriptionManager.instance.TrySaveAndNotifySubscriptionWithSyncNowRequest(subMailboxSession, subscription, out syncNowRequestSentSuccessfully, out result);
			return result;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public static bool TrySubscriptionSyncNow(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			SubscriptionMailboxSession subMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
			return SubscriptionManager.instance.TryRequestSyncNowForSubscription(subMailboxSession, subscription);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public static void SetSubscription(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			SubscriptionMailboxSession subMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
			SubscriptionManager.instance.SaveAndNotifySubscription(subMailboxSession, subscription);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001C324 File Offset: 0x0001A524
		public static bool SetSubscriptionAndSyncNow(MailboxSession mailboxSession, AggregationSubscription subscription)
		{
			SubscriptionMailboxSession subMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
			bool result;
			SubscriptionManager.instance.SaveAndNotifySubscriptionWithSyncNowRequest(subMailboxSession, subscription, out result);
			return result;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001C34C File Offset: 0x0001A54C
		public static MailboxSession OpenMailbox(IExchangePrincipal owner, ExchangeMailboxOpenType openAs, string clientInfoString)
		{
			SyncUtilities.ThrowIfArgumentNull("owner", owner);
			SyncUtilities.ThrowIfArgumentNull("clientInfoString", clientInfoString);
			MailboxSession result;
			switch (openAs)
			{
			case ExchangeMailboxOpenType.AsAdministrator:
				result = MailboxSession.OpenAsAdmin(owner, CultureInfo.InvariantCulture, clientInfoString);
				break;
			case ExchangeMailboxOpenType.AsTransport:
				result = MailboxSession.OpenAsTransport(owner, clientInfoString);
				break;
			case ExchangeMailboxOpenType.AsUser:
				result = MailboxSession.Open(owner, new WindowsPrincipal(WindowsIdentity.GetCurrent()), CultureInfo.InvariantCulture, clientInfoString);
				break;
			default:
				throw new ArgumentException("invalid ExchangeMailboxOpenType value " + openAs);
			}
			return result;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		public static string GenerateDeviceId(Guid subscriptionGuid)
		{
			return subscriptionGuid.ToString().Replace("-", "_");
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001C3F0 File Offset: 0x0001A5F0
		public static StoreId FindSubscription(MailboxSession mailboxSession, Guid subscriptionGuid)
		{
			AggregationSubscriptionType aggregationSubscriptionType;
			return SubscriptionManager.FindSubscription(mailboxSession, subscriptionGuid, out aggregationSubscriptionType);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001C406 File Offset: 0x0001A606
		public static AggregationSubscription LoadSubscription(MailboxSession mailboxSession, StoreId messageId, AggregationSubscriptionType subscriptionType)
		{
			return SubscriptionManager.LoadSubscription(mailboxSession, messageId, subscriptionType, true);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001C414 File Offset: 0x0001A614
		public static AggregationSubscription LoadSubscription(MailboxSession mailboxSession, StoreId messageId, AggregationSubscriptionType subscriptionType, bool upgradeSubscriptionIfRequired)
		{
			if (!SyncUtilities.IsDatacenterMode())
			{
				return null;
			}
			AggregationSubscription aggregationSubscription = null;
			CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)72UL, "LoadSubscription: Will load subscriptions with type mask {0} from messageId: {1}.", new object[]
			{
				(int)subscriptionType,
				messageId
			});
			PropertyDefinition[] propertyDefinitions = SubscriptionManager.GetPropertyDefinitions(subscriptionType);
			using (MessageItem messageItem = MessageItem.Bind(mailboxSession, messageId, propertyDefinitions))
			{
				string className = messageItem.ClassName;
				if ((className.Equals("IPM.Aggregation.Pop", StringComparison.OrdinalIgnoreCase) && (subscriptionType & AggregationSubscriptionType.Pop) != AggregationSubscriptionType.Unknown) || (className.Equals("IPM.Aggregation.DeltaSync", StringComparison.OrdinalIgnoreCase) && (subscriptionType & AggregationSubscriptionType.DeltaSyncMail) != AggregationSubscriptionType.Unknown) || (className.Equals("IPM.Aggregation.IMAP", StringComparison.OrdinalIgnoreCase) && (subscriptionType & AggregationSubscriptionType.IMAP) != AggregationSubscriptionType.Unknown) || (className.Equals("IPM.Aggregation.Facebook", StringComparison.OrdinalIgnoreCase) && (subscriptionType & AggregationSubscriptionType.Facebook) != AggregationSubscriptionType.Unknown) || (className.Equals("IPM.Aggregation.LinkedIn", StringComparison.OrdinalIgnoreCase) && (subscriptionType & AggregationSubscriptionType.LinkedIn) != AggregationSubscriptionType.Unknown))
				{
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)36UL, "SubscriptionManager.LoadSubscription: Loading subscription from messageId: {0}.", new object[]
					{
						messageItem.Id.ObjectId
					});
					aggregationSubscription = SubscriptionManager.CreateAggregationSubscription(messageItem);
					aggregationSubscription.SubscriptionMessageId = messageItem.Id.ObjectId;
					aggregationSubscription.LoadSubscription(messageItem, (!mailboxSession.MailboxOwner.ObjectId.IsNullOrEmpty()) ? mailboxSession.MailboxOwner.ObjectId : null, mailboxSession.MailboxOwnerLegacyDN);
					aggregationSubscription.UserExchangeMailboxDisplayName = mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
					aggregationSubscription.UserExchangeMailboxSmtpAddress = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
					if (upgradeSubscriptionIfRequired)
					{
						bool flag = SubscriptionManager.instance.upgrader.UpgradeSubscription(aggregationSubscription, messageItem, SubscriptionManager.instance.messageHelper);
						if (flag)
						{
							messageItem.Load(propertyDefinitions);
							aggregationSubscription.SubscriptionMessageId = messageItem.Id.ObjectId;
						}
					}
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)73UL, "LoadSubscription: Loaded subscription: {0}.", new object[]
					{
						aggregationSubscription
					});
				}
				else
				{
					CommonLoggingHelper.SyncLogSession.LogError((TSLID)74UL, ExTraceGlobals.SubscriptionManagerTracer, "LoadSubscription: Not loading subscription for message id: '{0}' since messageClass: '{1}' is not recognized", new object[]
					{
						messageId,
						className
					});
				}
			}
			return aggregationSubscription;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001C648 File Offset: 0x0001A848
		public static PropertyDefinition[] GetPropertyDefinitions(AggregationSubscriptionType subscriptionType)
		{
			if (subscriptionType <= AggregationSubscriptionType.IMAP)
			{
				switch (subscriptionType)
				{
				case AggregationSubscriptionType.Pop:
					return SubscriptionManager.PopSearchColumnsIndex;
				case (AggregationSubscriptionType)3:
					break;
				case AggregationSubscriptionType.DeltaSyncMail:
					return SubscriptionManager.DeltaSyncSearchColumnsIndex;
				default:
					if (subscriptionType == AggregationSubscriptionType.IMAP)
					{
						return SubscriptionManager.IMAPSearchColumnsIndex;
					}
					break;
				}
			}
			else if (subscriptionType == AggregationSubscriptionType.Facebook || subscriptionType == AggregationSubscriptionType.LinkedIn)
			{
				return SubscriptionManager.ConnectSearchColumnsIndex;
			}
			throw new InvalidDataException("invalid subscription type " + subscriptionType);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		internal static bool FetchSubscriptionsAndCheckForAnyActiveAggregationSubscriptions(Func<List<AggregationSubscription>> fetchSubscritpionsMethod)
		{
			SyncUtilities.ThrowIfArgumentNull("fetchSubscritpionsMethod", fetchSubscritpionsMethod);
			List<AggregationSubscription> list = fetchSubscritpionsMethod();
			foreach (AggregationSubscription aggregationSubscription in list)
			{
				if (aggregationSubscription.IsAggregation && !aggregationSubscription.Inactive)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001C77C File Offset: 0x0001A97C
		private static StoreId FindSubscription(MailboxSession mailboxSession, Guid subscriptionGuid, out AggregationSubscriptionType subscriptionType)
		{
			StoreId messageId = null;
			AggregationSubscriptionType subType = AggregationSubscriptionType.Unknown;
			SubscriptionManager.ForEachSubscriptionInMailbox(mailboxSession, delegate(object[] item)
			{
				if (SubscriptionManager.IsValidSubscriptionMessage(item) && subscriptionGuid == (Guid)item[2])
				{
					messageId = (StoreId)item[1];
					subType = AggregationSubscription.GetSubscriptionKind((string)item[0]);
				}
				return messageId == null;
			});
			subscriptionType = subType;
			return messageId;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		private static bool IsValidSubscriptionMessage(object[] item)
		{
			return !(item[2] is PropertyError) && !(item[0] is PropertyError) && !(item[1] is PropertyError);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		private static AggregationSubscription CreateAggregationSubscription(MessageItem message)
		{
			AggregationSubscriptionType subscriptionKind = AggregationSubscription.GetSubscriptionKind(message);
			AggregationSubscriptionType aggregationSubscriptionType = subscriptionKind;
			if (aggregationSubscriptionType <= AggregationSubscriptionType.IMAP)
			{
				switch (aggregationSubscriptionType)
				{
				case AggregationSubscriptionType.Pop:
					return new PopAggregationSubscription();
				case (AggregationSubscriptionType)3:
					break;
				case AggregationSubscriptionType.DeltaSyncMail:
					return new DeltaSyncAggregationSubscription();
				default:
					if (aggregationSubscriptionType == AggregationSubscriptionType.IMAP)
					{
						return new IMAPAggregationSubscription();
					}
					break;
				}
			}
			else if (aggregationSubscriptionType == AggregationSubscriptionType.Facebook || aggregationSubscriptionType == AggregationSubscriptionType.LinkedIn)
			{
				return new ConnectSubscription();
			}
			throw new InvalidDataException("invalid subscription type " + subscriptionKind);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001C85B File Offset: 0x0001AA5B
		private static void ForEachSubscriptionInMailbox(MailboxSession mailboxSession, SubscriptionManager.SubscriptionProcessor processor)
		{
			SubscriptionManager.ForEachSubscriptionInMailbox(mailboxSession, processor, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001C86C File Offset: 0x0001AA6C
		private static void ForEachSubscriptionInMailbox(MailboxSession mailboxSession, SubscriptionManager.SubscriptionProcessor processor, StoreObjectId folderId)
		{
			using (Folder folder = Folder.Bind(mailboxSession, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, SubscriptionManager.SortByItemClassInAscendingOrder, SubscriptionManager.SharingColumnsIndex))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, SubscriptionManager.AggregationMessageClassPrefixFilter))
					{
						bool flag = true;
						object[][] rows;
						do
						{
							rows = queryResult.GetRows(100);
							for (int i = 0; i < rows.Length; i++)
							{
								if (!SubscriptionManager.IsAggregationMessageClass(rows[i], 0))
								{
									goto Block_6;
								}
								flag = processor(rows[i]);
								if (!flag)
								{
									break;
								}
							}
						}
						while (flag && rows.Length > 0);
						Block_6:;
					}
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001C91C File Offset: 0x0001AB1C
		private static bool IsAggregationMessageClass(object[] item, int messageClassColumnIndex)
		{
			string text = (string)item[messageClassColumnIndex];
			return !string.IsNullOrEmpty(text) && text.StartsWith("IPM.Aggregation.", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001C948 File Offset: 0x0001AB48
		private static void DeleteSubscriptionSyncState(MailboxSession mailboxSession, Guid subscriptionGuid, AggregationSubscriptionType subscriptionType)
		{
			if (subscriptionGuid != Guid.Empty && subscriptionType != AggregationSubscriptionType.Unknown)
			{
				SyncStateStorage.DeleteSyncStateStorage(mailboxSession, new DeviceIdentity(SubscriptionManager.GenerateDeviceId(subscriptionGuid), subscriptionType.ToString(), SubscriptionManager.Protocol), null);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001C97D File Offset: 0x0001AB7D
		protected SubscriptionManager(SyncLogSession syncLogSession)
		{
			this.syncLogSession = syncLogSession;
			this.notificationClient = this.CreateNotificationClient();
			this.mailboxTableSubscriptionPropertyHelper = this.CreateMailboxTableSubscriptionPropertyHelper();
			this.messageHelper = this.CreateSubscriptionMessageHelper();
			this.upgrader = this.CreateSubscriptionUpgrader();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		private SubscriptionManager() : this(CommonLoggingHelper.SyncLogSession)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001C9C9 File Offset: 0x0001ABC9
		public static SubscriptionManager Instance
		{
			get
			{
				return SubscriptionManager.instance;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public void CreateAndNotifyNewSubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			subMailboxSession.SetPropertiesOfSubscription(subscription);
			this.UpdateMailboxTableAndSaveSubscription(subMailboxSession.MailboxSession, subscription);
			this.mailboxTableSubscriptionPropertyHelper.UpdateContentAggregationFlags(subMailboxSession.MailboxSession, ContentAggregationFlags.HasSubscriptions);
			string mailboxServerName = subMailboxSession.GetMailboxServerName();
			this.notificationClient.NotifySubscriptionAdded(subscription, mailboxServerName);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001CA2E File Offset: 0x0001AC2E
		public void SaveSubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			this.messageHelper.SaveSubscription(subMailboxSession.MailboxSession, subscription);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001CA58 File Offset: 0x0001AC58
		public void SaveAndNotifySubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			this.UpdateMailboxTableAndSaveSubscription(subMailboxSession.MailboxSession, subscription);
			string mailboxServerName = subMailboxSession.GetMailboxServerName();
			this.notificationClient.NotifySubscriptionUpdated(subscription, mailboxServerName);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		public void SaveAndNotifySubscriptionWithSyncNowRequest(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, out bool syncNowSentSuccessfully)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			subscription.LastSyncNowRequestTime = new DateTime?(DateTime.UtcNow);
			this.UpdateMailboxTableAndSaveSubscription(subMailboxSession.MailboxSession, subscription);
			string mailboxServerName = subMailboxSession.GetMailboxServerName();
			syncNowSentSuccessfully = this.notificationClient.NotifySubscriptionUpdatedAndSyncNowNeeded(subscription, mailboxServerName);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		public bool TryRequestSyncNowForSubscription(SubscriptionMailboxSession subMailboxSession, AggregationSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			string mailboxServerName = subMailboxSession.GetMailboxServerName();
			return this.notificationClient.NotifySubscriptionSyncNowNeeded(subscription, mailboxServerName);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001CB2F File Offset: 0x0001AD2F
		public bool TryCreateAndNotifyNewSubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, out Exception exception)
		{
			return this.TrySubscriptionOperation(subMailboxSession, subscription, new Action<SubscriptionMailboxSession, ISyncWorkerData>(this.CreateAndNotifyNewSubscription), out exception);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001CB46 File Offset: 0x0001AD46
		public bool TrySaveSubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, out Exception exception)
		{
			return this.TrySubscriptionOperation(subMailboxSession, subscription, new Action<SubscriptionMailboxSession, ISyncWorkerData>(this.SaveSubscription), out exception);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		public bool TrySaveAndNotifySubscription(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, out Exception exception)
		{
			return this.TrySubscriptionOperation(subMailboxSession, subscription, new Action<SubscriptionMailboxSession, ISyncWorkerData>(this.SaveAndNotifySubscription), out exception);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public bool TrySaveAndNotifySubscriptionWithSyncNowRequest(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, out bool syncNowSentSuccessfully, out Exception exception)
		{
			bool syncNowSent = false;
			bool result = this.TrySubscriptionOperation(subMailboxSession, subscription, delegate(SubscriptionMailboxSession subMailboxSessionArg, ISyncWorkerData subscriptionArg)
			{
				this.SaveAndNotifySubscriptionWithSyncNowRequest(subMailboxSessionArg, subscriptionArg, out syncNowSent);
			}, out exception);
			syncNowSentSuccessfully = syncNowSent;
			return result;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
		public void UpdateSubscriptionToMailbox(MailboxSession mailboxSession, ISyncWorkerData subscription)
		{
			SubscriptionMailboxSession subMailboxSession = this.CreateSubscriptionMailboxSession(mailboxSession);
			Exception ex;
			this.TrySaveAndNotifySubscription(subMailboxSession, subscription, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001CC00 File Offset: 0x0001AE00
		public void DeleteSubscription(MailboxSession mailboxSession, ISyncWorkerData subscription, bool sendRpcNotification = true)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SubscriptionManager.DeleteSubscription(mailboxSession, subscription.SubscriptionGuid);
			if (sendRpcNotification)
			{
				SubscriptionMailboxSession subscriptionMailboxSession = SubscriptionManager.instance.CreateSubscriptionMailboxSession(mailboxSession);
				string mailboxServerName = subscriptionMailboxSession.GetMailboxServerName();
				SubscriptionManager.instance.notificationClient.NotifySubscriptionDeleted(subscription, mailboxServerName);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001CC57 File Offset: 0x0001AE57
		protected virtual SubscriptionNotificationClient CreateNotificationClient()
		{
			return new SubscriptionNotificationClient();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001CC5E File Offset: 0x0001AE5E
		protected virtual MailboxTableSubscriptionPropertyHelper CreateMailboxTableSubscriptionPropertyHelper()
		{
			return new MailboxTableSubscriptionPropertyHelper();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001CC65 File Offset: 0x0001AE65
		protected virtual SubscriptionMessageHelper CreateSubscriptionMessageHelper()
		{
			return new SubscriptionMessageHelper();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		protected virtual SubscriptionMailboxSession CreateSubscriptionMailboxSession(MailboxSession mailboxSession)
		{
			return new SubscriptionMailboxSession(mailboxSession);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001CC74 File Offset: 0x0001AE74
		protected virtual SubscriptionUpgrader CreateSubscriptionUpgrader()
		{
			return new SubscriptionUpgrader(this.syncLogSession);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001CC84 File Offset: 0x0001AE84
		protected bool TrySubscriptionOperation(Action subscriptionOperation, out Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionOperation", subscriptionOperation);
			exception = null;
			try
			{
				subscriptionOperation();
				return true;
			}
			catch (TransientException innerException)
			{
				exception = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new SubscriptionUpdateTransientException(innerException));
			}
			catch (LocalizedException innerException2)
			{
				exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new SubscriptionUpdatePermanentException(innerException2));
			}
			this.syncLogSession.LogError((TSLID)1333UL, "Encountered error: {0}", new object[]
			{
				exception
			});
			return false;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001CD14 File Offset: 0x0001AF14
		private void UpdateMailboxTableAndSaveSubscription(MailboxSession mailboxSession, ISyncWorkerData subscription)
		{
			this.UpdateMailboxTableAndPerformSubscriptionOperation<ISyncWorkerData>(mailboxSession, subscription, new Action<MailboxSession, ISyncWorkerData>(this.messageHelper.SaveSubscription));
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001CD30 File Offset: 0x0001AF30
		private void UpdateMailboxTableAndPerformSubscriptionOperation<T>(MailboxSession mailboxSession, T operationArgument, Action<MailboxSession, T> subscriptionOperation)
		{
			this.mailboxTableSubscriptionPropertyHelper.UpdateSubscriptionListTimestamp(mailboxSession);
			subscriptionOperation(mailboxSession, operationArgument);
			this.mailboxTableSubscriptionPropertyHelper.TryUpdateSubscriptionListTimestamp(mailboxSession);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001CD74 File Offset: 0x0001AF74
		private bool TrySubscriptionOperation(SubscriptionMailboxSession subMailboxSession, ISyncWorkerData subscription, Action<SubscriptionMailboxSession, ISyncWorkerData> subscriptionOperation, out Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("subMailboxSession", subMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("subscriptionOperation", subscriptionOperation);
			return this.TrySubscriptionOperation(delegate()
			{
				subscriptionOperation(subMailboxSession, subscription);
			}, out exception);
		}

		// Token: 0x04000321 RID: 801
		private const string AggregationMessageClassPrefix = "IPM.Aggregation.";

		// Token: 0x04000322 RID: 802
		public static readonly string Protocol = "ContentAggregation";

		// Token: 0x04000323 RID: 803
		private static readonly PropertyDefinition[] SharingColumnsIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			MessageItemSchema.SharingInstanceGuid,
			AggregationSubscriptionMessageSchema.SharingSubscriptionName,
			ItemSchema.InstanceKey
		};

		// Token: 0x04000324 RID: 804
		private static readonly PropertyDefinition[] PopSearchColumnsIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MessageItemSchema.SharingDetail,
			MessageItemSchema.SharingInstanceGuid,
			MessageItemSchema.SharingProviderGuid,
			MessageItemSchema.SharingLocalUid,
			MessageItemSchema.SharingLastSync,
			MessageItemSchema.SharingDetailedStatus,
			MessageItemSchema.SharingDiagnostics,
			AggregationSubscriptionMessageSchema.SharingPoisonCallstack,
			MessageItemSchema.SharingRemotePath,
			AggregationSubscriptionMessageSchema.SharingInitiatorName,
			AggregationSubscriptionMessageSchema.SharingInitiatorSmtp,
			AggregationSubscriptionMessageSchema.SharingRemoteUser,
			AggregationSubscriptionMessageSchema.SharingRemotePass,
			AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime,
			AggregationSubscriptionMessageSchema.SharingSyncRange,
			AggregationSubscriptionMessageSchema.SharingAggregationStatus,
			AggregationSubscriptionMessageSchema.SharingMigrationState,
			AggregationSubscriptionMessageSchema.SharingAggregationType,
			AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolName,
			AggregationSubscriptionMessageSchema.SharingSubscriptionName,
			MessageItemSchema.SharingSubscriptionVersion,
			MessageItemSchema.SharingSendAsState,
			MessageItemSchema.SharingSendAsValidatedEmail,
			AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp,
			AggregationSubscriptionMessageSchema.SharingSubscriptionEvents,
			AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime,
			AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics,
			AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase,
			AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest,
			AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode
		};

		// Token: 0x04000325 RID: 805
		private static readonly PropertyDefinition[] DeltaSyncSearchColumnsIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MessageItemSchema.SharingDetail,
			MessageItemSchema.SharingInstanceGuid,
			MessageItemSchema.SharingProviderGuid,
			MessageItemSchema.SharingLocalUid,
			MessageItemSchema.SharingLastSync,
			MessageItemSchema.SharingDetailedStatus,
			MessageItemSchema.SharingDiagnostics,
			AggregationSubscriptionMessageSchema.SharingPoisonCallstack,
			MessageItemSchema.SharingRemotePath,
			AggregationSubscriptionMessageSchema.SharingInitiatorName,
			AggregationSubscriptionMessageSchema.SharingInitiatorSmtp,
			AggregationSubscriptionMessageSchema.SharingRemoteUser,
			AggregationSubscriptionMessageSchema.SharingRemotePass,
			AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime,
			AggregationSubscriptionMessageSchema.SharingAggregationStatus,
			AggregationSubscriptionMessageSchema.SharingWlidAuthPolicy,
			AggregationSubscriptionMessageSchema.SharingWlidUserPuid,
			AggregationSubscriptionMessageSchema.SharingWlidAuthToken,
			AggregationSubscriptionMessageSchema.SharingWlidAuthTokenExpireTime,
			AggregationSubscriptionMessageSchema.SharingMinSyncPollInterval,
			AggregationSubscriptionMessageSchema.SharingMinSettingPollInterval,
			AggregationSubscriptionMessageSchema.SharingSyncMultiplier,
			AggregationSubscriptionMessageSchema.SharingMaxObjectsInSync,
			AggregationSubscriptionMessageSchema.SharingMaxNumberOfEmails,
			AggregationSubscriptionMessageSchema.SharingMaxNumberOfFolders,
			AggregationSubscriptionMessageSchema.SharingMaxAttachments,
			AggregationSubscriptionMessageSchema.SharingMaxMessageSize,
			AggregationSubscriptionMessageSchema.SharingMaxRecipients,
			AggregationSubscriptionMessageSchema.SharingMigrationState,
			AggregationSubscriptionMessageSchema.SharingAggregationType,
			AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolName,
			AggregationSubscriptionMessageSchema.SharingSubscriptionName,
			MessageItemSchema.SharingSubscriptionVersion,
			MessageItemSchema.SharingSendAsState,
			MessageItemSchema.SharingSendAsValidatedEmail,
			AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp,
			AggregationSubscriptionMessageSchema.SharingSubscriptionEvents,
			AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime,
			AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics,
			AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase,
			AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest,
			AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode
		};

		// Token: 0x04000326 RID: 806
		private static readonly PropertyDefinition[] IMAPSearchColumnsIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MessageItemSchema.SharingDetail,
			MessageItemSchema.SharingInstanceGuid,
			MessageItemSchema.SharingProviderGuid,
			MessageItemSchema.SharingLocalUid,
			MessageItemSchema.SharingLastSync,
			MessageItemSchema.SharingDetailedStatus,
			MessageItemSchema.SharingDiagnostics,
			AggregationSubscriptionMessageSchema.SharingPoisonCallstack,
			MessageItemSchema.SharingRemotePath,
			AggregationSubscriptionMessageSchema.SharingInitiatorName,
			AggregationSubscriptionMessageSchema.SharingInitiatorSmtp,
			AggregationSubscriptionMessageSchema.SharingRemoteUser,
			AggregationSubscriptionMessageSchema.SharingRemotePass,
			AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime,
			AggregationSubscriptionMessageSchema.SharingSyncRange,
			AggregationSubscriptionMessageSchema.SharingAggregationStatus,
			AggregationSubscriptionMessageSchema.SharingMigrationState,
			AggregationSubscriptionMessageSchema.SharingAggregationType,
			AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolName,
			AggregationSubscriptionMessageSchema.SharingSubscriptionName,
			MessageItemSchema.SharingSubscriptionVersion,
			MessageItemSchema.SharingSendAsState,
			MessageItemSchema.SharingSendAsValidatedEmail,
			AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp,
			AggregationSubscriptionMessageSchema.SharingSubscriptionEvents,
			AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders,
			AggregationSubscriptionMessageSchema.SharingImapPathPrefix,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime,
			AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics,
			AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase,
			AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest,
			AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode
		};

		// Token: 0x04000327 RID: 807
		private static readonly PropertyDefinition[] ConnectSearchColumnsIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MessageItemSchema.SharingDetail,
			MessageItemSchema.SharingInstanceGuid,
			MessageItemSchema.SharingProviderGuid,
			MessageItemSchema.SharingLocalUid,
			MessageItemSchema.SharingLastSync,
			MessageItemSchema.SharingDetailedStatus,
			MessageItemSchema.SharingDiagnostics,
			AggregationSubscriptionMessageSchema.SharingPoisonCallstack,
			MessageItemSchema.SharingRemotePath,
			AggregationSubscriptionMessageSchema.SharingInitiatorName,
			AggregationSubscriptionMessageSchema.SharingInitiatorSmtp,
			AggregationSubscriptionMessageSchema.SharingRemoteUser,
			AggregationSubscriptionMessageSchema.SharingRemotePass,
			AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime,
			AggregationSubscriptionMessageSchema.SharingAggregationStatus,
			AggregationSubscriptionMessageSchema.SharingMigrationState,
			AggregationSubscriptionMessageSchema.SharingAggregationType,
			AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion,
			AggregationSubscriptionMessageSchema.SharingAggregationProtocolName,
			AggregationSubscriptionMessageSchema.SharingSubscriptionName,
			MessageItemSchema.SharingSubscriptionVersion,
			MessageItemSchema.SharingSendAsState,
			MessageItemSchema.SharingSendAsValidatedEmail,
			AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId,
			AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp,
			AggregationSubscriptionMessageSchema.SharingSubscriptionEvents,
			AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced,
			AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox,
			AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime,
			AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics,
			AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase,
			AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest,
			AggregationSubscriptionMessageSchema.SharingEncryptedAccessToken,
			AggregationSubscriptionMessageSchema.SharingAppId,
			AggregationSubscriptionMessageSchema.SharingUserId,
			AggregationSubscriptionMessageSchema.SharingEncryptedAccessTokenSecret,
			AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode
		};

		// Token: 0x04000328 RID: 808
		private static readonly SortBy[] SortByItemClassInAscendingOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04000329 RID: 809
		private static readonly ComparisonFilter AggregationMessageClassPrefixFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, StoreObjectSchema.ItemClass, "IPM.Aggregation.");

		// Token: 0x0400032A RID: 810
		private static readonly SubscriptionManager instance = new SubscriptionManager();

		// Token: 0x0400032B RID: 811
		private readonly SyncLogSession syncLogSession;

		// Token: 0x0400032C RID: 812
		private readonly SubscriptionNotificationClient notificationClient;

		// Token: 0x0400032D RID: 813
		private readonly MailboxTableSubscriptionPropertyHelper mailboxTableSubscriptionPropertyHelper;

		// Token: 0x0400032E RID: 814
		private readonly SubscriptionMessageHelper messageHelper;

		// Token: 0x0400032F RID: 815
		private readonly SubscriptionUpgrader upgrader;

		// Token: 0x020000C7 RID: 199
		// (Invoke) Token: 0x0600059F RID: 1439
		private delegate bool SubscriptionProcessor(object[] item);

		// Token: 0x020000C8 RID: 200
		private enum SharingColumn
		{
			// Token: 0x04000331 RID: 817
			ItemClass,
			// Token: 0x04000332 RID: 818
			ItemId,
			// Token: 0x04000333 RID: 819
			SharingInstanceGuid,
			// Token: 0x04000334 RID: 820
			SharingSubscriptionName,
			// Token: 0x04000335 RID: 821
			InstanceKey
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A59 RID: 2649
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UnifiedPolicySyncNotificationDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060060B4 RID: 24756 RVA: 0x001976ED File Offset: 0x001958ED
		public UnifiedPolicySyncNotificationDataProvider(ADSessionSettings adSessionSettings, ADUser mailboxOwner, string action) : base(adSessionSettings, mailboxOwner, action)
		{
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x001976F8 File Offset: 0x001958F8
		public UnifiedPolicySyncNotificationDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action)
		{
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x00197702 File Offset: 0x00195902
		internal UnifiedPolicySyncNotificationDataProvider()
		{
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x00197B38 File Offset: 0x00195D38
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			StoreId storeId = rootId as StoreId;
			if (storeId != null)
			{
				object ret = this.GetNotificationFromStoreId(storeId);
				if (!(ret is T))
				{
					goto IL_275;
				}
				yield return (T)((object)ret);
			}
			using (Folder inbox = Folder.Bind(base.MailboxSession, base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				filter = ((filter == null) ? UnifiedPolicySyncNotificationDataProvider.IdExistsFilter : new AndFilter(new QueryFilter[]
				{
					filter,
					UnifiedPolicySyncNotificationDataProvider.IdExistsFilter
				}));
				using (QueryResult queryResult = inbox.ItemQuery(ItemQueryType.Associated, filter, null, UnifiedPolicySyncNotificationDataProvider.PropertySet))
				{
					UnifiedPolicySyncNotificationId notificationId = rootId as UnifiedPolicySyncNotificationId;
					if (notificationId != null)
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.UnifiedPolicyNotificationId, notificationId.IdValue);
						queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
					}
					bool breakLoop = false;
					while (!breakLoop)
					{
						object[][] view = queryResult.GetRows((notificationId != null) ? 1 : ((pageSize == 0) ? 1000 : pageSize));
						breakLoop = (view.Length == 0 || notificationId != null);
						foreach (object[] row in view)
						{
							VersionedId itemVersionId = (VersionedId)row[0];
							object ret2 = this.GetNotificationFromStoreId(itemVersionId);
							if (ret2 is T)
							{
								yield return (T)((object)ret2);
							}
						}
					}
				}
			}
			IL_275:
			yield break;
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x00197B6C File Offset: 0x00195D6C
		protected override void InternalSave(ConfigurableObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			UnifiedPolicyNotificationBase unifiedPolicyNotificationBase = instance as UnifiedPolicyNotificationBase;
			if (unifiedPolicyNotificationBase == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			switch (unifiedPolicyNotificationBase.ObjectState)
			{
			case ObjectState.New:
				using (Folder folder = Folder.Bind(base.MailboxSession, base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
				{
					using (MessageItem messageItem = MessageItem.CreateAssociated(base.MailboxSession, folder.Id))
					{
						this.SetNotificationMessage(messageItem, unifiedPolicyNotificationBase);
						messageItem.Save(SaveMode.NoConflictResolutionForceSave);
						messageItem.Load();
						unifiedPolicyNotificationBase.StoreObjectId = messageItem.Id;
					}
					return;
				}
				break;
			case ObjectState.Unchanged:
				return;
			case ObjectState.Changed:
				break;
			case ObjectState.Deleted:
				goto IL_FE;
			default:
				return;
			}
			if (unifiedPolicyNotificationBase.StoreObjectId == null)
			{
				throw new ArgumentException("notification.StoreObjectId is null when saving for an update.");
			}
			using (MessageItem messageItem2 = MessageItem.Bind(base.MailboxSession, unifiedPolicyNotificationBase.StoreObjectId))
			{
				this.SetNotificationMessage(messageItem2, unifiedPolicyNotificationBase);
				messageItem2.Save(SaveMode.NoConflictResolutionForceSave);
				messageItem2.Load();
				return;
			}
			IL_FE:
			throw new InvalidOperationException(ServerStrings.ExceptionObjectHasBeenDeleted);
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x00197CB0 File Offset: 0x00195EB0
		protected override void InternalDelete(ConfigurableObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			UnifiedPolicyNotificationBase unifiedPolicyNotificationBase = instance as UnifiedPolicyNotificationBase;
			if (unifiedPolicyNotificationBase == null)
			{
				throw new NotSupportedException("Delete: " + instance.GetType().FullName);
			}
			if (unifiedPolicyNotificationBase.StoreObjectId == null)
			{
				throw new ArgumentException("notification.StoreObjectId is null when deleting the instance.");
			}
			using (Folder folder = Folder.Bind(base.MailboxSession, base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
				{
					unifiedPolicyNotificationBase.StoreObjectId
				});
			}
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x00197D50 File Offset: 0x00195F50
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UnifiedPolicySyncNotificationDataProvider>(this);
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x00197D58 File Offset: 0x00195F58
		private UnifiedPolicyNotificationBase GetNotificationFromStoreId(StoreId storeId)
		{
			UnifiedPolicyNotificationBase unifiedPolicyNotificationBase = null;
			using (MessageItem messageItem = MessageItem.Bind(base.MailboxSession, storeId))
			{
				WorkItemBase workItemBase = null;
				using (Stream stream = messageItem.PropertyBag.OpenPropertyStream(MessageItemSchema.UnifiedPolicyNotificationData, PropertyOpenMode.ReadOnly))
				{
					if (stream.Length > 0L)
					{
						byte[] array = new byte[stream.Length];
						stream.Position = 0L;
						stream.Read(array, 0, array.Length);
						workItemBase = WorkItemBase.Deserialize(array);
					}
				}
				unifiedPolicyNotificationBase = UnifiedPolicyNotificationFactory.Create(workItemBase, base.MailboxSession.MailboxOwner.ObjectId);
				workItemBase.WorkItemId = storeId.ToBase64String();
				workItemBase.HasPersistentBackUp = true;
				unifiedPolicyNotificationBase.StoreObjectId = messageItem.Id;
			}
			return unifiedPolicyNotificationBase;
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x00197E2C File Offset: 0x0019602C
		private void SetNotificationMessage(MessageItem notificationMessage, UnifiedPolicyNotificationBase notification)
		{
			notificationMessage.PropertyBag[MessageItemSchema.UnifiedPolicyNotificationId] = notification.Identity.ToString();
			using (Stream stream = notificationMessage.PropertyBag.OpenPropertyStream(MessageItemSchema.UnifiedPolicyNotificationData, PropertyOpenMode.Create))
			{
				byte[] array = notification.GetWorkItem().Serialize();
				stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x040036FD RID: 14077
		private static readonly PropertyDefinition[] PropertySet = new PropertyDefinition[]
		{
			CoreItemSchema.Id
		};

		// Token: 0x040036FE RID: 14078
		private static readonly ExistsFilter IdExistsFilter = new ExistsFilter(InternalSchema.UnifiedPolicyNotificationId);
	}
}

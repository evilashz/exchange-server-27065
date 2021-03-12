using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAF RID: 3503
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SharingFolderManagerBase<TData> where TData : ISharingSubscriptionData
	{
		// Token: 0x0600786D RID: 30829 RVA: 0x00213A04 File Offset: 0x00211C04
		protected SharingFolderManagerBase(MailboxSession mailboxSession)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x0600786E RID: 30830 RVA: 0x00213A1E File Offset: 0x00211C1E
		protected MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x0600786F RID: 30831
		protected abstract ExtendedFolderFlags SharingFolderFlags { get; }

		// Token: 0x06007870 RID: 30832 RVA: 0x00213A28 File Offset: 0x00211C28
		public IdAndName EnsureFolder(TData subscription)
		{
			Util.ThrowOnNullArgument(subscription, "subscription");
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: Check folder referred by subscription {1}.", this.mailboxSession.MailboxOwner, subscription);
			IdAndName idAndName;
			if (subscription.LocalFolderId == null || this.GetFolder(subscription) == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: Create a new folder for the subscription {1}.", this.mailboxSession.MailboxOwner, subscription);
				idAndName = this.Create(subscription);
			}
			else
			{
				this.MoveFromDeletedIfNecessary(subscription.LocalFolderId, subscription.DataType);
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: Update the existing folder for the subscription {1}.", this.mailboxSession.MailboxOwner, subscription);
				idAndName = this.Update(subscription.LocalFolderId, subscription);
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, IdAndName, TData>((long)this.GetHashCode(), "{0}: Folder {1} is ensured that referred by subscription {2}.", this.mailboxSession.MailboxOwner, idAndName, subscription);
			return idAndName;
		}

		// Token: 0x06007871 RID: 30833 RVA: 0x00213B28 File Offset: 0x00211D28
		public IdAndName GetFolder(TData subscription)
		{
			Util.ThrowOnNullArgument(subscription, "subscription");
			IdAndName idAndName = null;
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: Check folder referred by subscription {1}.", this.mailboxSession.MailboxOwner, subscription);
			StoreObjectId localFolderId = subscription.LocalFolderId;
			string value = null;
			try
			{
				using (Folder folder = Folder.Bind(this.mailboxSession, localFolderId))
				{
					value = folder.DisplayName;
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Folder doesn't exist: {1}. No folder is returned.", this.mailboxSession.MailboxOwner, localFolderId);
			}
			if (!string.IsNullOrEmpty(value))
			{
				idAndName = new IdAndName(localFolderId, new LocalizedString(value));
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, IdAndName>((long)this.GetHashCode(), "{0}: Folder is returned: {1}", this.mailboxSession.MailboxOwner, idAndName);
			}
			return idAndName;
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x00213C14 File Offset: 0x00211E14
		internal IdAndName Create(TData subscriptionData)
		{
			StoreObjectId storeObjectId = null;
			IdAndName result = null;
			bool flag = false;
			try
			{
				storeObjectId = this.CreateFolderInternal(subscriptionData);
				result = this.Update(storeObjectId, subscriptionData);
				flag = true;
			}
			finally
			{
				if (!flag && storeObjectId != null)
				{
					ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Failed to process the folder. Delete the created folder {1}", this.mailboxSession.MailboxOwner, storeObjectId);
					this.mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						storeObjectId
					});
				}
			}
			return result;
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x00213C90 File Offset: 0x00211E90
		internal IdAndName Update(StoreObjectId folderId, TData subscriptionData)
		{
			string text = null;
			using (Folder folder = Folder.Bind(this.mailboxSession, folderId, new PropertyDefinition[]
			{
				FolderSchema.PermissionChangeBlocked
			}))
			{
				text = folder.DisplayName;
				if (folder.GetValueAsNullable<bool>(FolderSchema.PermissionChangeBlocked) == null)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Remove permissions on the folder: {1}", this.mailboxSession.MailboxOwner, folderId);
					PermissionSet permissionSet = folder.GetPermissionSet();
					permissionSet.Clear();
					folder.Save();
					folder.Load();
					folder[FolderSchema.PermissionChangeBlocked] = true;
				}
				ExtendedFolderFlags extendedFolderFlags = this.SharingFolderFlags;
				ExtendedFolderFlags? valueAsNullable = folder.GetValueAsNullable<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
				if (valueAsNullable != null)
				{
					extendedFolderFlags |= valueAsNullable.Value;
				}
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId, ExtendedFolderFlags>((long)this.GetHashCode(), "{0}: Stamp extended folder flag on folder ({1}): {2}", this.mailboxSession.MailboxOwner, folderId, extendedFolderFlags);
				folder[FolderSchema.ExtendedFolderFlags] = extendedFolderFlags;
				folder.Save();
			}
			this.CreateOrUpdateSharingBinding(subscriptionData, text, folderId);
			return new IdAndName(folderId, new LocalizedString(text));
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x00213DBC File Offset: 0x00211FBC
		protected virtual void CreateOrUpdateSharingBinding(TData subscriptionData, string localFolderName, StoreObjectId folderId)
		{
		}

		// Token: 0x06007875 RID: 30837
		protected abstract LocalizedString CreateLocalFolderName(TData subscriptionData);

		// Token: 0x06007876 RID: 30838 RVA: 0x00213DC0 File Offset: 0x00211FC0
		private StoreObjectId CreateFolderInternal(TData subscriptionData)
		{
			LocalizedString localizedString = this.CreateLocalFolderName(subscriptionData);
			StoreObjectId storeObjectId = null;
			StoreObjectId defaultFolderId = this.mailboxSession.GetDefaultFolderId(subscriptionData.DataType.DefaultFolderType);
			ExTraceGlobals.SharingTracer.TraceDebug((long)this.GetHashCode(), "{0}: Creating a folder. ParentFolderId: {1}; FolderName: {2}; DataType: {3}.", new object[]
			{
				this.mailboxSession.MailboxOwner,
				defaultFolderId,
				localizedString,
				subscriptionData.DataType
			});
			using (Folder folder = Folder.Create(this.mailboxSession, defaultFolderId, subscriptionData.DataType.StoreObjectType, localizedString, CreateMode.CreateNew))
			{
				folder.SaveWithUniqueDisplayName(50);
				folder.Load();
				storeObjectId = folder.Id.ObjectId;
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Folder is created. FolderId: {1}", this.mailboxSession.MailboxOwner, storeObjectId);
			return storeObjectId;
		}

		// Token: 0x06007877 RID: 30839 RVA: 0x00213EC4 File Offset: 0x002120C4
		private void MoveFromDeletedIfNecessary(StoreObjectId folderId, SharingDataType dataType)
		{
			using (Folder folder = Folder.Bind(this.mailboxSession, DefaultFolderType.DeletedItems))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				}))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.Id, folderId);
					if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						return;
					}
				}
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Folder is in deleted items, need to move back to calendar folder. FolderId: {1}", this.mailboxSession.MailboxOwner, folderId);
			AggregateOperationResult aggregateOperationResult = this.mailboxSession.Move(this.mailboxSession.GetDefaultFolderId(dataType.DefaultFolderType), new StoreId[]
			{
				folderId
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				GroupOperationResult groupOperationResult = aggregateOperationResult.GroupOperationResults[0];
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, StoreObjectId, LocalizedException>((long)this.GetHashCode(), "{0}: Failed to move folder. FolderId: {1}, Exception: {2}", this.mailboxSession.MailboxOwner, folderId, groupOperationResult.Exception);
				throw groupOperationResult.Exception;
			}
		}

		// Token: 0x04005347 RID: 21319
		private MailboxSession mailboxSession;
	}
}

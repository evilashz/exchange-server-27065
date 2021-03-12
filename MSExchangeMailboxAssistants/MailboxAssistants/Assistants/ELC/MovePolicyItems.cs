using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000054 RID: 84
	internal class MovePolicyItems
	{
		// Token: 0x060002EB RID: 747 RVA: 0x000124C1 File Offset: 0x000106C1
		internal MovePolicyItems(ContentSetting elcPolicy, ProvisionedFolder provisionedFolder, MailboxDataForFolders mailboxData, string itemClass)
		{
			this.elcPolicy = elcPolicy;
			this.sourceFolder = provisionedFolder;
			this.mailboxData = mailboxData;
			this.InitItemList(itemClass);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000124E6 File Offset: 0x000106E6
		internal List<ItemData> ItemList
		{
			get
			{
				return this.itemList;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000124EE File Offset: 0x000106EE
		internal StoreObjectId DestinationFolderId
		{
			get
			{
				return this.destinationFolderId;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000124F6 File Offset: 0x000106F6
		internal bool SetDateWhileMoving
		{
			get
			{
				return this.setDateWhileMoving;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000124FE File Offset: 0x000106FE
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Move policy items for mailbox " + this.mailboxData.MailboxSession.MailboxOwner;
			}
			return this.toString;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001252E File Offset: 0x0001072E
		internal void AddItemToDestinationList(ItemData item)
		{
			this.itemList.Add(item);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001253C File Offset: 0x0001073C
		private void InitItemList(string itemClass)
		{
			StoreObjectId storeObjectId;
			string text;
			this.GetOriginalDestinationFolder(out storeObjectId, out text);
			this.CheckOriginalDestinationFolder(storeObjectId, text);
			StoreObjectId subfolderUnderTarget = this.mailboxData.FolderProcessor.GetSubfolderUnderTarget(storeObjectId, this.sourceFolder.Folder, this.elcPolicy.Name);
			this.destinationFolderId = subfolderUnderTarget;
			this.itemList = new List<ItemData>(2000);
			this.SetDestPolicyType(storeObjectId, itemClass);
			MovePolicyItems.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Policy: '{1}' on folder: '{2}'. Successfully initialized the list for destination folder '{3}'. The current source folder is '{4}'.", new object[]
			{
				this,
				this.elcPolicy.Name,
				this.elcPolicy.ManagedFolderName,
				text,
				this.sourceFolder.FullFolderPath
			});
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000125F4 File Offset: 0x000107F4
		private void GetOriginalDestinationFolder(out StoreObjectId originalDestFolderId, out string fullFolderPath)
		{
			originalDestFolderId = null;
			fullFolderPath = null;
			if (this.elcPolicy.RetentionAction == RetentionActionType.MoveToFolder)
			{
				if (this.elcPolicy.MoveToDestinationFolder == null)
				{
					MovePolicyItems.Tracer.TraceError<MovePolicyItems, string, string>((long)this.GetHashCode(), "{0}: Invalid policy: '{1}' on folder: '{2}'. Expiration action is MoveToFolder but destination folder is null.", this, this.elcPolicy.Name, this.sourceFolder.DisplayName);
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_NullDestinationFolder, null, new object[]
					{
						this.sourceFolder.DisplayName,
						this.mailboxData.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
						this.elcPolicy.Name,
						this.elcPolicy.ManagedFolderName
					});
					throw new InvalidExpiryDestinationException(Strings.descNullExpiryDestination(this.mailboxData.MailboxSmtpAddress, this.elcPolicy.Name));
				}
				Guid folderGuidFromObjectGuid = this.mailboxData.GetFolderGuidFromObjectGuid(this.elcPolicy.MoveToDestinationFolder);
				if (folderGuidFromObjectGuid == Guid.Empty)
				{
					MovePolicyItems.Tracer.TraceError((long)this.GetHashCode(), "{0}: Invalid policy: '{1}' on folder: '{2}'. Destination folder '{3}' is not in the cached list of ELC folders from AD.", new object[]
					{
						this,
						this.elcPolicy.Name,
						this.sourceFolder.DisplayName,
						this.elcPolicy.MoveToDestinationFolderName
					});
					throw new InvalidExpiryDestinationException(Strings.descExpiryDestNotProvisioned(this.mailboxData.MailboxSmtpAddress, this.elcPolicy.MoveToDestinationFolderName, this.elcPolicy.Name));
				}
				ProvisionedFolder folderFromId = this.mailboxData.FolderProcessor.GetFolderFromId(folderGuidFromObjectGuid);
				if (folderFromId != null)
				{
					originalDestFolderId = folderFromId.FolderId;
					fullFolderPath = folderFromId.FullFolderPath;
					return;
				}
			}
			else
			{
				originalDestFolderId = this.mailboxData.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
				if (originalDestFolderId != null)
				{
					fullFolderPath = this.mailboxData.FolderProcessor.GetFolderPathFromId(originalDestFolderId);
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000127E0 File Offset: 0x000109E0
		private void CheckOriginalDestinationFolder(StoreObjectId originalDestFolderId, string originalDestFolderPath)
		{
			string text = (this.elcPolicy.RetentionAction == RetentionActionType.MoveToFolder) ? this.elcPolicy.MoveToDestinationFolderName : "Deleted Items Folder";
			if (originalDestFolderId == null || string.IsNullOrEmpty(originalDestFolderPath))
			{
				MovePolicyItems.Tracer.TraceError((long)this.GetHashCode(), "{0}: Invalid policy: '{1}' on folder: '{2}'. The folderId or folder path of the destination folder '{3}' could not be found in the list of provisioned folders from mailbox. FolderId: '{4}'. Folder path: '{5}'", new object[]
				{
					this,
					this.elcPolicy.Name,
					this.sourceFolder.DisplayName,
					text,
					originalDestFolderId,
					originalDestFolderPath
				});
				throw new InvalidExpiryDestinationException(Strings.descMissingFolderIdOnExpiryDest(this.mailboxData.MailboxSmtpAddress, text, this.elcPolicy.Name));
			}
			this.CheckIfSourceEqualsDestination(originalDestFolderId, text);
			this.CheckIfSourceUnderDestination(originalDestFolderPath);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00012894 File Offset: 0x00010A94
		private void CheckIfSourceEqualsDestination(StoreObjectId originalDestFolderId, string originalDestinationFolderName)
		{
			if (!originalDestFolderId.Equals(this.sourceFolder.Folder.Id.ObjectId))
			{
				return;
			}
			if (this.sourceFolder.InheritedPolicy)
			{
				MovePolicyItems.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Policy: '{1}' on folder: '{2}'. The destination folder '{3}' is same as the current source folder '{4}'. Since this policy was inherited from a parent, skip this folder.", new object[]
				{
					this,
					this.elcPolicy.Name,
					this.elcPolicy.ManagedFolderName,
					originalDestinationFolderName,
					this.sourceFolder.DisplayName
				});
				throw new SkipFolderException(Strings.descExpiryDestSameAsSource(this.sourceFolder.DisplayName, this.mailboxData.MailboxSmtpAddress, this.elcPolicy.Name));
			}
			MovePolicyItems.Tracer.TraceError((long)this.GetHashCode(), "{0}: Invalid policy: '{1}' on folder: '{2}'. The destination folder '{3}' is same as the current source folder '{4}'.", new object[]
			{
				this,
				this.elcPolicy.Name,
				this.elcPolicy.ManagedFolderName,
				originalDestinationFolderName,
				this.sourceFolder.DisplayName
			});
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DestinationFolderSameAsSource, null, new object[]
			{
				this.sourceFolder.DisplayName,
				this.mailboxData.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
				this.elcPolicy.Name,
				this.elcPolicy.ManagedFolderName,
				originalDestinationFolderName
			});
			throw new InvalidExpiryDestinationException(Strings.descExpiryDestSameAsSource(this.sourceFolder.DisplayName, this.mailboxData.MailboxSmtpAddress, this.elcPolicy.Name));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012A38 File Offset: 0x00010C38
		private void CheckIfSourceUnderDestination(string originalDestFolderPath)
		{
			if (this.sourceFolder.FullFolderPath.StartsWith(originalDestFolderPath))
			{
				MovePolicyItems.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Policy: '{1}' on folder: '{2}'. The source folder '{3}' is already under the destination folder '{4}', so items from it will not be moved. Skip this folder.", new object[]
				{
					this,
					this.elcPolicy.Name,
					this.elcPolicy.ManagedFolderName,
					this.sourceFolder.FullFolderPath,
					originalDestFolderPath
				});
				throw new SkipFolderException(Strings.descSourceUnderExpiryDest(this.sourceFolder.DisplayName, this.mailboxData.MailboxSmtpAddress, this.elcPolicy.Name, this.sourceFolder.FullFolderPath, originalDestFolderPath));
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012AE4 File Offset: 0x00010CE4
		private void SetDestPolicyType(StoreObjectId destFolderId, string itemClass)
		{
			ContentSetting contentSetting = null;
			ProvisionedFolder folderFromId = this.mailboxData.FolderProcessor.GetFolderFromId(destFolderId);
			if (folderFromId != null)
			{
				contentSetting = ElcPolicySettings.GetApplyingPolicy(folderFromId.ElcPolicies, itemClass, folderFromId.ItemClassToPolicyMapping);
			}
			if (contentSetting != null && contentSetting.RetentionEnabled && contentSetting.TriggerForRetention == RetentionDateType.WhenMoved && contentSetting.AgeLimitForRetention != null && contentSetting.AgeLimitForRetention.Value.TotalDays > 0.0)
			{
				this.setDateWhileMoving = true;
			}
		}

		// Token: 0x04000270 RID: 624
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationEnforcerTracer;

		// Token: 0x04000271 RID: 625
		private ContentSetting elcPolicy;

		// Token: 0x04000272 RID: 626
		private ProvisionedFolder sourceFolder;

		// Token: 0x04000273 RID: 627
		private MailboxDataForFolders mailboxData;

		// Token: 0x04000274 RID: 628
		private List<ItemData> itemList;

		// Token: 0x04000275 RID: 629
		private StoreObjectId destinationFolderId;

		// Token: 0x04000276 RID: 630
		private bool setDateWhileMoving;

		// Token: 0x04000277 RID: 631
		private string toString;
	}
}

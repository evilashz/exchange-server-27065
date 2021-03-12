using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200061C RID: 1564
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class COWLogProtector : ICOWNotification
	{
		// Token: 0x06004071 RID: 16497 RVA: 0x0010E228 File Offset: 0x0010C428
		public bool SkipItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, bool onBeforeNotification, bool onDumpster, bool success, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
			if (onBeforeNotification && COWTriggerAction.Update == operation)
			{
				if (settings.CurrentFolderId != null)
				{
					if (settings.CurrentFolderId.Equals(dumpster.AuditsFolderId))
					{
						throw new AccessDeniedException(ServerStrings.ExAuditsUpdateDenied);
					}
					if (settings.CurrentFolderId.Equals(dumpster.AdminAuditLogsFolderId))
					{
						throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsUpdateDenied);
					}
					if (dumpster.IsAuditFolder(settings.CurrentFolderId))
					{
						throw new AccessDeniedException((dumpster.AuditsFolderId != null) ? ServerStrings.ExAuditsUpdateDenied : ServerStrings.ExAdminAuditLogsUpdateDenied);
					}
				}
				else if (itemId != null)
				{
					StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(itemId);
					if (parentIdFromMessageId.Equals(dumpster.AuditsFolderId))
					{
						throw new AccessDeniedException(ServerStrings.ExAuditsUpdateDenied);
					}
					if (parentIdFromMessageId.Equals(dumpster.AdminAuditLogsFolderId))
					{
						throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsUpdateDenied);
					}
					if (dumpster.IsAuditFolder(settings.CurrentFolderId))
					{
						throw new AccessDeniedException((dumpster.AuditsFolderId != null) ? ServerStrings.ExAuditsUpdateDenied : ServerStrings.ExAdminAuditLogsUpdateDenied);
					}
				}
			}
			return true;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x0010E330 File Offset: 0x0010C530
		public void ItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, CoreFolder folder, bool onBeforeNotification, OperationResult result, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<OperationResult>(result, "result");
			EnumValidator.ThrowIfInvalid<COWTriggerActionState>(state, "state");
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x0010E358 File Offset: 0x0010C558
		public CowClientOperationSensitivity SkipGroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, bool onBeforeNotification, bool onDumpster, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			MailboxSession mailboxSession = sourceSession as MailboxSession;
			if (mailboxSession == null)
			{
				return CowClientOperationSensitivity.Skip;
			}
			if (onBeforeNotification && (COWTriggerAction.Copy == operation || COWTriggerAction.HardDelete == operation || COWTriggerAction.Move == operation || COWTriggerAction.MoveToDeletedItems == operation || COWTriggerAction.SoftDelete == operation))
			{
				StoreObjectId auditsFolderId = dumpster.AuditsFolderId;
				StoreObjectId adminAuditLogsFolderId = dumpster.AdminAuditLogsFolderId;
				if (settings.CurrentFolderId != null && (COWTriggerAction.HardDelete != operation || LogonType.SystemService != sourceSession.LogonType || !settings.IsMrmAction()))
				{
					this.CheckAccessOnAuditFolders(mailboxSession, settings.CurrentFolderId, dumpster, false);
				}
				if (itemIds != null)
				{
					foreach (StoreObjectId storeObjectId in itemIds)
					{
						if (storeObjectId != null)
						{
							if (storeObjectId.IsMessageId)
							{
								if (settings.CurrentFolderId == null && (COWTriggerAction.HardDelete != operation || LogonType.SystemService != sourceSession.LogonType || !settings.IsMrmAction()))
								{
									StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(storeObjectId);
									if (parentIdFromMessageId.Equals(auditsFolderId))
									{
										throw new AccessDeniedException(ServerStrings.ExAuditsUpdateDenied);
									}
									if (parentIdFromMessageId.Equals(adminAuditLogsFolderId))
									{
										throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsUpdateDenied);
									}
									if (dumpster.IsAuditFolder(parentIdFromMessageId))
									{
										throw new AccessDeniedException((auditsFolderId != null) ? ServerStrings.ExAuditsUpdateDenied : ServerStrings.ExAdminAuditLogsUpdateDenied);
									}
								}
							}
							else if (storeObjectId.IsFolderId)
							{
								this.CheckAccessOnAuditFolders(mailboxSession, storeObjectId, dumpster, true);
							}
						}
					}
				}
			}
			return CowClientOperationSensitivity.Skip;
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x0010E4BC File Offset: 0x0010C6BC
		private void CheckAccessOnAuditFolders(MailboxSession mailboxSession, StoreObjectId folderId, IDumpsterItemOperations dumpster, bool checkAncestorFolders)
		{
			StoreObjectId auditsFolderId = dumpster.AuditsFolderId;
			StoreObjectId adminAuditLogsFolderId = dumpster.AdminAuditLogsFolderId;
			if (folderId.Equals(auditsFolderId))
			{
				throw new AccessDeniedException(ServerStrings.ExAuditsUpdateDenied);
			}
			if (folderId.Equals(adminAuditLogsFolderId))
			{
				throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsUpdateDenied);
			}
			if (dumpster.IsAuditFolder(folderId))
			{
				throw new AccessDeniedException((auditsFolderId != null) ? ServerStrings.ExAuditsUpdateDenied : ServerStrings.ExAdminAuditLogsUpdateDenied);
			}
			if (checkAncestorFolders && (folderId.Equals(dumpster.RecoverableItemsRootFolderId) || folderId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration))))
			{
				if (auditsFolderId != null)
				{
					throw new AccessDeniedException(ServerStrings.ExAuditsUpdateDenied);
				}
				if (adminAuditLogsFolderId != null)
				{
					throw new AccessDeniedException(ServerStrings.ExAdminAuditLogsUpdateDenied);
				}
			}
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0010E55B File Offset: 0x0010C75B
		public void GroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, GroupOperationResult result, bool onBeforeNotification, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
		}
	}
}

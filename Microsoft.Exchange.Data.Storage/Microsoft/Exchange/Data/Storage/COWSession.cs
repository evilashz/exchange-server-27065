using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Compliance.CrimsonEvents;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000636 RID: 1590
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class COWSession : COWSessionBase
	{
		// Token: 0x0600417F RID: 16767 RVA: 0x00113F80 File Offset: 0x00112180
		static COWSession()
		{
			COWSession.cowClients = new List<ICOWNotification>
			{
				new COWLogProtector(),
				new COWAudit(),
				new COWCalendarLogging(),
				new COWContactLogging(),
				new COWContactLinking(),
				new COWGroupMessageWSPublishing(),
				new COWDumpster(),
				new COWSiteMailboxMessageDedup(),
				new COWHardDelete()
			};
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x001140CC File Offset: 0x001122CC
		private COWSession(MailboxSession session)
		{
			bool flag = false;
			try
			{
				this.session = session;
				this.settings = new COWSettings(this.session);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.session = null;
				}
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x00114118 File Offset: 0x00112318
		public static MiddleTierStoragePerformanceCountersInstance PerfCounters
		{
			get
			{
				return COWSession.perfCounters;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x0011411F File Offset: 0x0011231F
		public override StoreSession StoreSession
		{
			get
			{
				this.CheckDisposed(null);
				return this.session;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x0011412E File Offset: 0x0011232E
		public COWSettings Settings
		{
			get
			{
				this.CheckDisposed("Settings");
				return this.settings;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x00114141 File Offset: 0x00112341
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x00114154 File Offset: 0x00112354
		public int CopyOnWriteRollbackCount
		{
			get
			{
				this.CheckDisposed("CopyOnWriteRollbackCount");
				return this.copyOnWriteRollbackCount;
			}
			set
			{
				this.CheckDisposed("CopyOnWriteRollbackCount");
				this.copyOnWriteRollbackCount = value;
				if (value > COWSession.copyOnWriteRollbackEventLogLimit && this.copyOnWriteRollbackEventUserLegacyDN == null)
				{
					this.copyOnWriteRollbackEventUserLegacyDN = this.session.UserLegacyDN;
					this.copyOnWriteRollbackEventSmtpAddress = this.session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
					this.copyOnWriteRollbackEventClientInfo = (this.session.ClientInfoString ?? string.Empty);
				}
			}
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x001141D7 File Offset: 0x001123D7
		public static COWSession Create(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			return new COWSession(session);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x001141EA File Offset: 0x001123EA
		public override bool IsAuditFolder(StoreObjectId folderId)
		{
			return folderId != null && IdConverter.IsFolderId(folderId) && this.auditLogFolders != null && this.auditLogFolders.Contains(folderId);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x00114210 File Offset: 0x00112410
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.copyOnWriteRollbackCount > COWSession.copyOnWriteRollbackEventLogLimit)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorMultipleSaveOperationFailed, this.copyOnWriteRollbackEventUserLegacyDN, new object[]
					{
						this.copyOnWriteRollbackEventSmtpAddress,
						this.copyOnWriteRollbackCount,
						this.copyOnWriteRollbackEventClientInfo
					});
				}
				this.settings.CurrentFolderId = null;
				this.settings.ResetCurrentFolder();
				if (this.folderIdState == FolderIdState.FolderIdSuccess)
				{
					COWSession.perfCounters.DumpsterSessionsActive.Decrement();
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x001142A4 File Offset: 0x001124A4
		public override StoreObjectId CopyItemToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, ICoreItem item)
		{
			this.CheckDisposed("CopyItemToDumpster");
			StoreObjectId storeObjectId = item.StoreObjectId;
			StoreObjectId[] itemIds = new StoreObjectId[]
			{
				storeObjectId
			};
			bool forceNonIPM = this.settings.IsCurrentFolderItemEnabled(sessionWithBestAccess, item);
			IList<StoreObjectId> list = this.InternalCopyOrMoveItemsToDumpster(sessionWithBestAccess, destinationFolderId, itemIds, false, forceNonIPM, true);
			if (list == null || list.Count < 1)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x00114300 File Offset: 0x00112500
		public bool OnBeforeItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnBeforeItemChange");
			EnumValidator.ThrowIfInvalid<ItemChangeOperation>(operation, "operation");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			this.InternalItemChange(COWSession.GetTriggerAction(operation), COWTriggerActionState.Flush, session, itemId, item, true, false, callbackContext);
			return false;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x00114348 File Offset: 0x00112548
		public void OnAfterItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnAfterItemChange");
			EnumValidator.ThrowIfInvalid<ItemChangeOperation>(operation, "operation");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			this.InternalItemChange(COWSession.GetTriggerAction(operation), COWTriggerActionState.Flush, session, itemId, item, false, result.SaveStatus == SaveResult.Success || result.SaveStatus == SaveResult.SuccessWithConflictResolution, callbackContext);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x001143A4 File Offset: 0x001125A4
		public bool OnBeforeItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnBeforeItemSave");
			EnumValidator.ThrowIfInvalid<ItemChangeOperation>(operation, "operation");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			this.InternalItemChange(COWSession.GetTriggerAction(operation), COWTriggerActionState.Save, session, itemId, item, true, false, callbackContext);
			return false;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x001143EC File Offset: 0x001125EC
		public void OnAfterItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnAfterItemSave");
			EnumValidator.ThrowIfInvalid<ItemChangeOperation>(operation, "operation");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			this.InternalItemChange(COWSession.GetTriggerAction(operation), COWTriggerActionState.Save, session, itemId, item, false, result.SaveStatus == SaveResult.Success || result.SaveStatus == SaveResult.SuccessWithConflictResolution, callbackContext);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x00114448 File Offset: 0x00112648
		public bool OnBeforeFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnBeforeFolderChange");
			EnumValidator.ThrowIfInvalid<FolderChangeOperation>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			if (this.InCallback)
			{
				return false;
			}
			base.Results = new COWResults(this.StoreSession, itemIds);
			if (!COWSettings.DumpsterEnabledOverwrite)
			{
				if (this.settings.HoldEnabled())
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.StoreSession.GetHashCode(), "Global dumpster disabled on user {0} while in hold", this.session.MailboxOwnerLegacyDN);
				}
				return false;
			}
			COWTriggerAction triggerAction = COWSessionBase.GetTriggerAction(operation);
			List<StoreObjectId> list = null;
			if (triggerAction == COWTriggerAction.HardDelete || triggerAction == COWTriggerAction.SoftDelete)
			{
				list = new List<StoreObjectId>(itemIds.Count);
				using (IEnumerator<StoreObjectId> enumerator = itemIds.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StoreObjectId storeObjectId = enumerator.Current;
						if (storeObjectId.ObjectType != StoreObjectType.SearchFolder && storeObjectId.ObjectType != StoreObjectType.OutlookSearchFolder)
						{
							list.Add(storeObjectId);
						}
					}
					goto IL_EF;
				}
			}
			list = new List<StoreObjectId>(itemIds);
			IL_EF:
			if (list.Count == 0)
			{
				return false;
			}
			base.CheckFolderInitState(callbackContext.SessionWithBestAccess);
			this.SetInCallback(callbackContext.SessionWithBestAccess, true);
			bool value;
			try
			{
				bool? flag = null;
				foreach (KeyValuePair<StoreObjectId, ICollection<StoreObjectId>> keyValuePair in this.GroupItemsPerParent(sourceFolderId, list))
				{
					this.settings.CurrentFolderId = null;
					if (sourceFolderId != null && !sourceFolderId.Equals(keyValuePair.Key))
					{
						ExTraceGlobals.SessionTracer.TraceWarning((long)this.StoreSession.GetHashCode(), "Source folder (id {0}) is search folder (type {1}). Processing {2} items as part of actual folder {3}", new object[]
						{
							sourceFolderId.ToHexEntryId(),
							sourceFolderId.ObjectType,
							keyValuePair.Value.Count,
							keyValuePair.Key.ToHexEntryId()
						});
					}
					bool flag2 = this.InternalBeforeFolderChange(triggerAction, flags, destinationSession, keyValuePair.Key, destinationFolderId, keyValuePair.Value, callbackContext);
					if (flag == null)
					{
						flag = new bool?(flag2);
					}
					else if (flag2 != flag.Value)
					{
						throw new InvalidOperationException("All split request for search folders should be processed the same way");
					}
				}
				value = flag.Value;
			}
			finally
			{
				this.settings.CurrentFolderId = null;
				this.SetInCallback(callbackContext.SessionWithBestAccess, false);
			}
			return value;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x001146EC File Offset: 0x001128EC
		public void OnAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, GroupOperationResult result, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnAfterFolderChange");
			EnumValidator.ThrowIfInvalid<FolderChangeOperation>(operation, "operation");
			EnumValidator.ThrowIfInvalid<FolderChangeOperationFlags>(flags, "flags");
			Util.ThrowOnNullArgument(sourceSession, "sourceSession");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			if (this.InCallback)
			{
				return;
			}
			if (!COWSettings.DumpsterEnabledOverwrite)
			{
				if (this.settings.HoldEnabled())
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.StoreSession.GetHashCode(), "Global dumpster disabled on user {0} while in hold", this.session.MailboxOwnerLegacyDN);
				}
				return;
			}
			base.CheckFolderInitState(callbackContext.SessionWithBestAccess);
			this.SetInCallback(callbackContext.SessionWithBestAccess, true);
			try
			{
				this.InternalAfterFolderChange(COWSessionBase.GetTriggerAction(operation), flags, destinationSession, sourceFolderId, destinationFolderId, itemIds, result, callbackContext);
			}
			finally
			{
				this.settings.CurrentFolderId = null;
				this.SetInCallback(callbackContext.SessionWithBestAccess, false);
			}
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x001147D8 File Offset: 0x001129D8
		public void OnBeforeFolderBind(StoreSession sourceSession, StoreObjectId folderId, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnBeforeFolderBind");
			Util.ThrowOnNullArgument(sourceSession, "sourceSession");
			Util.ThrowOnNullArgument(folderId, "folderId");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x001148EC File Offset: 0x00112AEC
		public void OnAfterFolderBind(StoreSession sourceSession, StoreObjectId folderId, CoreFolder folder, bool success, CallbackContext callbackContext)
		{
			this.CheckDisposed("OnAfterFolderBind");
			Util.ThrowOnNullArgument(sourceSession, "sourceSession");
			Util.ThrowOnNullArgument(folderId, "folderId");
			Util.ThrowOnNullArgument(callbackContext, "callbackContext");
			if (this.InCallback)
			{
				return;
			}
			if (!COWSettings.DumpsterEnabledOverwrite)
			{
				if (this.settings.HoldEnabled())
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.StoreSession.GetHashCode(), "Global dumpster disabled on user {0} while in hold", this.session.MailboxOwnerLegacyDN);
				}
				return;
			}
			this.SetInCallback(callbackContext.SessionWithBestAccess, true);
			try
			{
				callbackContext.SessionWithBestAccess.DoNothingIfBypassAuditing(delegate
				{
					this.Settings.CurrentFolderId = folderId;
					foreach (ICOWNotification icownotification in COWSession.cowClients)
					{
						if (!icownotification.SkipItemOperation(this.Settings, this, COWTriggerAction.FolderBind, COWTriggerActionState.Save, this.StoreSession, null, null, false, false, success, callbackContext))
						{
							icownotification.ItemOperation(this.Settings, this, COWTriggerAction.FolderBind, COWTriggerActionState.Save, this.StoreSession, null, null, folder, false, success ? OperationResult.Succeeded : OperationResult.Failed, callbackContext);
						}
					}
				});
			}
			finally
			{
				this.Settings.CurrentFolderId = null;
				this.SetInCallback(callbackContext.SessionWithBestAccess, false);
			}
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x00114A04 File Offset: 0x00112C04
		public override void RollbackItemVersion(MailboxSession sessionWithBestAccess, CoreItem itemUpdated, StoreObjectId itemIdToRollback)
		{
			this.CheckDisposed("RollbackItemVersion");
			if (itemIdToRollback == null)
			{
				ExTraceGlobals.SessionTracer.TraceError((long)this.session.GetHashCode(), "Rollback of item failed as rollback item id is empty");
				return;
			}
			if (itemUpdated == null)
			{
				ExTraceGlobals.SessionTracer.TraceError<StoreObjectId>((long)this.session.GetHashCode(), "Rollback of item failed as rollback item is missing. Rollback item Id: {0}", itemIdToRollback);
				return;
			}
			ExTraceGlobals.SessionTracer.TraceError<VersionedId, string, string>((long)this.session.GetHashCode(), "Update of Item Id {0}, class {1}, subject {2} failed. The dumpster copy will be deleted ", itemUpdated.Id, itemUpdated.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass), itemUpdated.PropertyBag.GetValueOrDefault<string>(InternalSchema.Subject));
			if (!base.IsDumpsterFolder(sessionWithBestAccess, IdConverter.GetParentIdFromMessageId(itemIdToRollback)))
			{
				ExTraceGlobals.SessionTracer.TraceError<StoreObjectId>((long)this.session.GetHashCode(), "Rollback of item failed as rollback identity is not in a Dumpster folder. Rollback item Id: {0}", itemIdToRollback);
				return;
			}
			COWSession.PerfCounters.DumpsterVersionRollback.Increment();
			this.copyOnWriteRollbackCount++;
			AggregateOperationResult aggregateOperationResult = sessionWithBestAccess.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				itemIdToRollback
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				GroupOperationResult groupOperationResult = null;
				if (aggregateOperationResult.GroupOperationResults != null)
				{
					groupOperationResult = aggregateOperationResult.GroupOperationResults[0];
				}
				if (groupOperationResult != null)
				{
					ExTraceGlobals.SessionTracer.TraceError<OperationResult, LocalizedException>((long)this.session.GetHashCode(), "Rollback of item failed to be deleted. Results: {0}, Exception: {1}", groupOperationResult.OperationResult, groupOperationResult.Exception);
					return;
				}
				ExTraceGlobals.SessionTracer.TraceError((long)this.session.GetHashCode(), "Rollback of item failed to be deleted. No group result");
			}
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x00114B60 File Offset: 0x00112D60
		public override bool IsDumpsterOverWarningQuota(COWSettings settings)
		{
			this.CheckDisposed("IsDumpsterOverWarningQuota");
			bool? flag = this.session.IsDumpsterOverQuota(settings.DumpsterWarningQuota);
			if (flag != null)
			{
				return flag.Value;
			}
			ExTraceGlobals.SessionTracer.TraceError((long)this.session.GetHashCode(), "The call to get dumpster size failed. Can't determine whether the dumpster size is over the warning quota.");
			return false;
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x00114BB8 File Offset: 0x00112DB8
		public override bool IsDumpsterOverCalendarLoggingQuota(MailboxSession sessionWithBestAccess, COWSettings settings)
		{
			this.CheckDisposed("IsDumpsterOverCalendarLoggingQuota");
			if (settings.CalendarLoggingQuota.IsUnlimited)
			{
				return false;
			}
			ulong? calendarLoggingFolderSize = settings.GetCalendarLoggingFolderSize(sessionWithBestAccess);
			if (calendarLoggingFolderSize != null)
			{
				return calendarLoggingFolderSize.Value > settings.CalendarLoggingQuota.Value.ToBytes();
			}
			ExTraceGlobals.SessionTracer.TraceError((long)this.session.GetHashCode(), "The call to get calendar logging folder size failed. Can't determine whether calendar log size exceeds the quota.");
			return false;
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x00114C30 File Offset: 0x00112E30
		internal static bool IsDelegateSession(StoreSession session)
		{
			switch (session.LogonType)
			{
			case LogonType.Owner:
			case LogonType.Admin:
			case LogonType.Transport:
			case LogonType.SystemService:
				return false;
			case LogonType.Delegated:
			case LogonType.DelegatedAdmin:
				return true;
			}
			throw new InvalidOperationException("Unexpected logon type");
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x00114C78 File Offset: 0x00112E78
		internal void InternalItemChange(COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreId itemId, CoreItem item, bool onBeforeNotification, bool success, CallbackContext callbackContext)
		{
			EnumValidator.ThrowIfInvalid<COWTriggerAction>(operation, "operation");
			if (this.InCallback)
			{
				return;
			}
			if (item != null && item.ItemLevel == ItemLevel.Attached)
			{
				return;
			}
			if (!COWSettings.DumpsterEnabledOverwrite)
			{
				if (this.settings.HoldEnabled())
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.StoreSession.GetHashCode(), "Global dumpster disabled on user {0} while in hold", this.session.MailboxOwnerLegacyDN);
				}
				return;
			}
			StoreObjectId storeObjectId = null;
			if (!onBeforeNotification && COWTriggerAction.Update == operation)
			{
				if (callbackContext.ItemOperationAuditInfo != null)
				{
					storeObjectId = callbackContext.ItemOperationAuditInfo.Id;
				}
			}
			else if (operation != COWTriggerAction.Create && COWTriggerAction.Submit != operation)
			{
				if (item != null)
				{
					storeObjectId = ((ICoreObject)item).StoreObjectId;
				}
				else if (itemId is StoreObjectId)
				{
					storeObjectId = (itemId as StoreObjectId);
				}
				else if (itemId is VersionedId)
				{
					storeObjectId = (itemId as VersionedId).ObjectId;
				}
			}
			if (!this.IsReadonlyOperation(operation))
			{
				base.CheckFolderInitState(callbackContext.SessionWithBestAccess);
			}
			this.SetInCallback(callbackContext.SessionWithBestAccess, true);
			try
			{
				List<ICOWNotification> list = new List<ICOWNotification>(COWSession.cowClients.Count);
				if (storeObjectId != null && IdConverter.IsMessageId(storeObjectId.ProviderLevelItemId))
				{
					this.settings.CurrentFolderId = IdConverter.GetParentIdFromMessageId(storeObjectId);
				}
				else
				{
					this.settings.CurrentFolderId = null;
				}
				bool onDumpster = false;
				if (!this.IsReadonlyOperation(operation))
				{
					if (this.settings.CurrentFolderId == null)
					{
						onDumpster = this.CheckOperationOnDumpster(callbackContext.SessionWithBestAccess, operation, item);
					}
					else
					{
						onDumpster = this.CheckOperationOnDumpster(callbackContext.SessionWithBestAccess, operation, this.settings.CurrentFolderId, item);
					}
				}
				if (onBeforeNotification)
				{
					base.Results = new COWResults(this.StoreSession, new StoreObjectId[]
					{
						storeObjectId
					});
				}
				foreach (ICOWNotification icownotification in COWSession.cowClients)
				{
					if (!icownotification.SkipItemOperation(this.Settings, this, operation, state, this.StoreSession, storeObjectId, item, onBeforeNotification, onDumpster, !onBeforeNotification && success, callbackContext))
					{
						list.Add(icownotification);
					}
				}
				if (list.Count != 0)
				{
					if (operation == COWTriggerAction.Create && storeObjectId == null && callbackContext.ItemOperationAuditInfo != null && callbackContext.ItemOperationAuditInfo.Id != null)
					{
						storeObjectId = callbackContext.ItemOperationAuditInfo.Id;
					}
					foreach (ICOWNotification icownotification2 in list)
					{
						icownotification2.ItemOperation(this.Settings, this, operation, state, this.StoreSession, storeObjectId, item, null, onBeforeNotification, success ? OperationResult.Succeeded : OperationResult.Failed, callbackContext);
					}
				}
			}
			finally
			{
				this.settings.CurrentFolderId = null;
				this.SetInCallback(callbackContext.SessionWithBestAccess, false);
				if (!onBeforeNotification && COWTriggerAction.Update == operation && COWTriggerActionState.Save == state)
				{
					item.ResetLegallyDirtyProperties();
				}
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x00114F74 File Offset: 0x00113174
		internal bool InternalBeforeFolderChange(COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, CallbackContext callbackContext)
		{
			List<StoreObjectId> list = COWSessionBase.InternalFilterFolders(itemIds);
			List<ICOWNotification> list2 = new List<ICOWNotification>(COWSession.cowClients.Count);
			CowClientOperationSensitivity cowClientOperationSensitivity = CowClientOperationSensitivity.Skip;
			bool onDumpster = this.CheckOperationOnDumpster(callbackContext.SessionWithBestAccess, operation, sourceFolderId) || this.CheckOperationOnDumpster(callbackContext.SessionWithBestAccess, operation, list);
			this.settings.CurrentFolderId = sourceFolderId;
			foreach (ICOWNotification icownotification in COWSession.cowClients)
			{
				CowClientOperationSensitivity cowClientOperationSensitivity2 = icownotification.SkipGroupOperation(this.Settings, this, operation, flags, this.StoreSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, true, onDumpster, callbackContext);
				if (cowClientOperationSensitivity2 != CowClientOperationSensitivity.Skip)
				{
					cowClientOperationSensitivity |= cowClientOperationSensitivity2;
					list2.Add(icownotification);
				}
			}
			bool flag = COWSessionBase.IsDeleteOperation(operation) && (cowClientOperationSensitivity & CowClientOperationSensitivity.PerformOperation) == CowClientOperationSensitivity.PerformOperation;
			List<StoreObjectId> list3 = COWSessionBase.InternalFilterItems(itemIds);
			ExTraceGlobals.SessionTracer.TraceDebug((long)this.StoreSession.GetHashCode(), "Processing operation {0} for {1} notification clients, {2} folders, {3} top level items", new object[]
			{
				operation.ToString(),
				list2.Count,
				list.Count,
				list3.Count
			});
			if (list2.Count == 0)
			{
				return false;
			}
			base.Results.ResetPartialResults();
			if (list3.Count > 0 && (flags & FolderChangeOperationFlags.IncludeItems) == FolderChangeOperationFlags.IncludeItems)
			{
				Util.ThrowOnNullArgument(sourceFolderId, "sourceFolderId");
				this.InternalFolderChangeItemGroup(list2, operation, flags, destinationSession, destinationFolderId, list3.ToArray(), true, callbackContext);
			}
			if (flag)
			{
				base.Results.AddResult(base.Results.GetPartialResults());
			}
			foreach (StoreObjectId storeObjectId in list)
			{
				bool flag2 = sourceFolderId != null && storeObjectId.Equals(sourceFolderId);
				List<StoreObjectId> list4 = new List<StoreObjectId>(0);
				long num = 0L;
				long num2 = 0L;
				long num3 = 0L;
				this.settings.CurrentFolderId = storeObjectId;
				Folder currentFolder = this.settings.GetCurrentFolder(callbackContext.SessionWithBestAccess);
				if (currentFolder == null)
				{
					using (List<ICOWNotification>.Enumerator enumerator3 = list2.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							ICOWNotification icownotification2 = enumerator3.Current;
							if (icownotification2 is COWAudit)
							{
								callbackContext.FolderAuditInfo[this.settings.CurrentFolderId] = new FolderAuditInfo(this.settings.CurrentFolderId, null);
								break;
							}
						}
						continue;
					}
				}
				if (flag && !flag2)
				{
					ELCFolderFlags valueOrDefault = currentFolder.PropertyBag.GetValueOrDefault<ELCFolderFlags>(InternalSchema.AdminFolderFlags, ELCFolderFlags.None);
					if (!this.IsDeleteOnFolderAllowed(this.settings.CurrentFolderId, valueOrDefault, currentFolder.DisplayName))
					{
						continue;
					}
				}
				if ((flags & FolderChangeOperationFlags.IncludeSubFolders) == FolderChangeOperationFlags.IncludeSubFolders)
				{
					list4.AddRange(this.InternalSubfolderList(currentFolder, operation));
				}
				list4.Reverse();
				if ((flags & FolderChangeOperationFlags.IncludeItems) == FolderChangeOperationFlags.IncludeItems)
				{
					list4.Add(storeObjectId);
				}
				base.Results.ResetPartialResults();
				foreach (StoreObjectId storeObjectId2 in list4)
				{
					this.settings.CurrentFolderId = storeObjectId2;
					if (!this.settings.IsCurrentFolderEnabled(callbackContext.SessionWithBestAccess))
					{
						ExTraceGlobals.SessionTracer.TraceDebug<StoreObjectId>((long)this.StoreSession.GetHashCode(), "InternalFolderChangeOneFolder processing skipped as source folder {0} is not-IPM.", this.settings.CurrentFolderId);
					}
					else
					{
						this.InternalFolderChangeOneFolder(list2, operation, flags, destinationSession, destinationFolderId, true, callbackContext);
						if (!storeObjectId2.Equals(storeObjectId) && flag)
						{
							this.settings.CurrentFolderId = null;
							this.settings.CurrentFolderId = storeObjectId2;
							currentFolder = this.settings.GetCurrentFolder(callbackContext.SessionWithBestAccess);
							if (currentFolder != null)
							{
								num += (long)((int)currentFolder[FolderSchema.ItemCount]);
								num2 += (long)((int)currentFolder[FolderSchema.AssociatedItemCount]);
								num3 += (long)((int)currentFolder[FolderSchema.ChildCount]);
							}
						}
					}
				}
				if (flag)
				{
					this.settings.CurrentFolderId = null;
					this.settings.CurrentFolderId = storeObjectId;
					currentFolder = this.settings.GetCurrentFolder(callbackContext.SessionWithBestAccess);
					if (currentFolder != null)
					{
						if (this.settings.IsCurrentFolderEnabled(callbackContext.SessionWithBestAccess) || (flags & FolderChangeOperationFlags.IncludeItems) == FolderChangeOperationFlags.None)
						{
							num += (long)((int)currentFolder[FolderSchema.ItemCount]);
						}
						if (this.settings.IsCurrentFolderEnabled(callbackContext.SessionWithBestAccess) || (flags & FolderChangeOperationFlags.IncludeAssociated) == FolderChangeOperationFlags.None)
						{
							num2 += (long)((int)currentFolder[FolderSchema.AssociatedItemCount]);
						}
						if (this.settings.IsCurrentFolderEnabled(callbackContext.SessionWithBestAccess) || (flags & FolderChangeOperationFlags.IncludeSubFolders) == FolderChangeOperationFlags.None)
						{
							num3 += (long)((int)currentFolder[FolderSchema.ChildCount]);
						}
						ExTraceGlobals.SessionTracer.TraceDebug((long)this.StoreSession.GetHashCode(), "About to delete/empty folder {0}, id {1}. Items remaining: i:{2}, a:{3},f:{4}. Folder enabled {5}", new object[]
						{
							currentFolder.DisplayName,
							this.settings.CurrentFolderId,
							num,
							num2,
							num3,
							this.settings.IsCurrentFolderEnabled(callbackContext.SessionWithBestAccess)
						});
						this.ProcessDeleteOnTopFolder(callbackContext.SessionWithBestAccess, storeObjectId, flag2, flags, num != 0L, num2 != 0L, num3 != 0L);
					}
				}
			}
			return flag;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0011554C File Offset: 0x0011374C
		internal void InternalAfterFolderChange(COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, GroupOperationResult result, CallbackContext callbackContext)
		{
			List<ICOWNotification> list = new List<ICOWNotification>(COWSession.cowClients.Count);
			CowClientOperationSensitivity cowClientOperationSensitivity = CowClientOperationSensitivity.Skip;
			this.settings.CurrentFolderId = sourceFolderId;
			foreach (ICOWNotification icownotification in COWSession.cowClients)
			{
				CowClientOperationSensitivity cowClientOperationSensitivity2 = icownotification.SkipGroupOperation(this.Settings, this, operation, flags, this.StoreSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, false, false, callbackContext);
				if (cowClientOperationSensitivity2 != CowClientOperationSensitivity.Skip)
				{
					cowClientOperationSensitivity |= cowClientOperationSensitivity2;
					list.Add(icownotification);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			try
			{
				foreach (ICOWNotification icownotification2 in list)
				{
					icownotification2.GroupOperation(this.Settings, this, operation, flags, this.StoreSession, destinationSession, destinationFolderId, new List<StoreObjectId>(itemIds).ToArray(), result, false, callbackContext);
				}
			}
			finally
			{
				this.settings.CurrentFolderId = null;
			}
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0011566C File Offset: 0x0011386C
		internal void InternalFolderChangeItemGroup(List<ICOWNotification> clients, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, bool onBeforeNotification, CallbackContext callbackContext)
		{
			foreach (ICOWNotification icownotification in clients)
			{
				icownotification.GroupOperation(this.Settings, this, operation, flags, this.StoreSession, destinationSession, destinationFolderId, itemIds, null, onBeforeNotification, callbackContext);
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x001156D4 File Offset: 0x001138D4
		internal void InternalFolderChangeOneFolder(List<ICOWNotification> clients, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession destinationSession, StoreObjectId destinationFolderId, bool onBeforeNotification, CallbackContext callbackContext)
		{
			int num = 0;
			StoragePermanentException ex = null;
			Folder currentFolder = this.settings.GetCurrentFolder(callbackContext.SessionWithBestAccess);
			if (currentFolder == null)
			{
				return;
			}
			try
			{
				num += this.GetProcessItemCount(currentFolder, clients, operation, flags, destinationSession, destinationFolderId, ItemQueryType.None, onBeforeNotification, callbackContext);
				if ((flags & FolderChangeOperationFlags.IncludeAssociated) == FolderChangeOperationFlags.IncludeAssociated)
				{
					num += this.GetProcessItemCount(currentFolder, clients, operation, flags, destinationSession, destinationFolderId, ItemQueryType.Associated, onBeforeNotification, callbackContext);
				}
				if (num == 0)
				{
					this.InternalFolderChangeItemGroup(clients, operation, flags, destinationSession, destinationFolderId, null, onBeforeNotification, callbackContext);
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (VirusDetectedException ex3)
			{
				ex = ex3;
			}
			catch (VirusMessageDeletedException ex4)
			{
				ex = ex4;
			}
			catch (VirusException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				this.settings.ResetCurrentFolder();
				ExTraceGlobals.SessionTracer.TraceWarning<string, StoragePermanentException, int>((long)this.StoreSession.GetHashCode(), "Folder ({0}) processing failure {1} ({2} processed).", this.settings.CurrentFolderId.ToString(), ex, num);
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x001157D0 File Offset: 0x001139D0
		internal List<StoreObjectId> InternalSubfolderList(Folder currentFolder, COWTriggerAction operation)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(0);
			StoragePermanentException ex = null;
			try
			{
				using (QueryResult queryResult = currentFolder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, COWSession.folderProperties))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(COWSession.folderQueryPageSize);
						if (rows == null || rows.Length <= 0)
						{
							break;
						}
						for (int i = 0; i < rows.Length; i++)
						{
							VersionedId versionedId = rows[i][0] as VersionedId;
							if (versionedId != null)
							{
								StoreObjectId objectId = versionedId.ObjectId;
								if (objectId.ObjectType != StoreObjectType.SearchFolder && objectId.ObjectType != StoreObjectType.OutlookSearchFolder)
								{
									if (COWSessionBase.IsDeleteOperation(operation))
									{
										string displayName = rows[i][2] as string;
										object obj = rows[i][1];
										ELCFolderFlags elcFolderFlags = (obj is int) ? ((ELCFolderFlags)obj) : ELCFolderFlags.None;
										if (!this.IsDeleteOnFolderAllowed(objectId, elcFolderFlags, displayName))
										{
											goto IL_AE;
										}
									}
									list.Add(objectId);
								}
							}
							IL_AE:;
						}
					}
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (VirusDetectedException ex3)
			{
				ex = ex3;
			}
			catch (VirusMessageDeletedException ex4)
			{
				ex = ex4;
			}
			catch (VirusException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<string, StoragePermanentException>((long)this.StoreSession.GetHashCode(), "Subfolders of folder {0} enumeration failure {1}.", this.settings.CurrentFolderId.ToString(), ex);
				base.Results.AddResult(new GroupOperationResult(OperationResult.PartiallySucceeded, new StoreObjectId[]
				{
					this.settings.CurrentFolderId
				}, ex));
			}
			return list;
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x00115968 File Offset: 0x00113B68
		internal bool IsReadonlyOperation(COWTriggerAction operation)
		{
			return operation == COWTriggerAction.FolderBind || operation == COWTriggerAction.ItemBind || operation == COWTriggerAction.Submit;
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x0011597C File Offset: 0x00113B7C
		internal bool SkipAuditingFolderOperations(MailboxAuditOperations operation, StoreObjectId folderId)
		{
			if (folderId != null)
			{
				if (this.skipFolderOperationsCache == null)
				{
					this.skipFolderOperationsCache = new Dictionary<StoreObjectId, MailboxAuditOperations>();
					foreach (COWSession.FolderOperationsPair folderOperationsPair in COWSession.skipFolderOperationsSetting)
					{
						StoreObjectId defaultFolderId = this.session.GetDefaultFolderId(folderOperationsPair.DefaultFolderType);
						if (defaultFolderId != null && !this.skipFolderOperationsCache.ContainsKey(defaultFolderId))
						{
							this.skipFolderOperationsCache.Add(defaultFolderId, folderOperationsPair.Operations);
						}
					}
				}
				if (this.skipFolderOperationsCache.ContainsKey(folderId) && (this.skipFolderOperationsCache[folderId] & operation) == operation)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x00115A14 File Offset: 0x00113C14
		private static COWTriggerAction GetTriggerAction(ItemChangeOperation operation)
		{
			EnumValidator.ThrowIfInvalid<ItemChangeOperation>(operation, "operation");
			switch (operation)
			{
			case ItemChangeOperation.Create:
				return COWTriggerAction.Create;
			case ItemChangeOperation.Update:
				return COWTriggerAction.Update;
			case ItemChangeOperation.ItemBind:
				return COWTriggerAction.ItemBind;
			case ItemChangeOperation.Submit:
				return COWTriggerAction.Submit;
			default:
				throw new NotSupportedException("Invalid item change operation");
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x00115A78 File Offset: 0x00113C78
		internal StoreObjectId CheckAndCreateAuditsFolder(MailboxSession sessionWithBestAccess)
		{
			sessionWithBestAccess.BypassAuditsFolderAccessChecking(delegate
			{
				this.AuditsFolderId = sessionWithBestAccess.GetAuditsFolderId();
			});
			if (base.AuditsFolderId == null)
			{
				if (base.RecoverableItemsRootFolderId == null)
				{
					base.CheckFolderInitState(sessionWithBestAccess);
				}
				base.AuditsFolderId = sessionWithBestAccess.CreateDefaultFolder(DefaultFolderType.Audits);
			}
			return base.AuditsFolderId;
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00115AE8 File Offset: 0x00113CE8
		public StoreObjectId CheckAndCreateDiscoveryHoldsFolder(MailboxSession sessionWithBestAccess)
		{
			if (base.RecoverableItemsDiscoveryHoldsFolderId == null)
			{
				if (base.RecoverableItemsRootFolderId == null)
				{
					base.CheckFolderInitState(sessionWithBestAccess);
				}
				base.RecoverableItemsDiscoveryHoldsFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDiscoveryHolds);
				if (base.RecoverableItemsDiscoveryHoldsFolderId == null)
				{
					base.RecoverableItemsDiscoveryHoldsFolderId = sessionWithBestAccess.CreateDefaultFolder(DefaultFolderType.RecoverableItemsDiscoveryHolds);
				}
			}
			return base.RecoverableItemsDiscoveryHoldsFolderId;
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x00115B38 File Offset: 0x00113D38
		public StoreObjectId CheckAndCreateMigratedMessagesFolder()
		{
			if (base.RecoverableItemsMigratedMessagesFolderId == null)
			{
				if (base.RecoverableItemsRootFolderId == null)
				{
					base.CheckFolderInitState(this.session);
				}
				base.RecoverableItemsMigratedMessagesFolderId = this.settings.Session.GetDefaultFolderId(DefaultFolderType.RecoverableItemsMigratedMessages);
				if (base.RecoverableItemsMigratedMessagesFolderId == null)
				{
					base.RecoverableItemsMigratedMessagesFolderId = this.settings.Session.CreateDefaultFolder(DefaultFolderType.RecoverableItemsMigratedMessages);
				}
			}
			return base.RecoverableItemsMigratedMessagesFolderId;
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x00115C34 File Offset: 0x00113E34
		protected override FolderIdState InternalInitFolders(MailboxSession sessionWithBestAccess)
		{
			DumpsterFolderHelper.CheckAndCreateFolder(sessionWithBestAccess);
			base.RecoverableItemsRootFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot);
			base.RecoverableItemsDeletionsFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions);
			base.RecoverableItemsVersionsFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsVersions);
			base.RecoverableItemsPurgesFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.RecoverableItemsPurges);
			base.CalendarLoggingFolderId = sessionWithBestAccess.GetDefaultFolderId(DefaultFolderType.CalendarLogging);
			sessionWithBestAccess.BypassAuditsFolderAccessChecking(delegate
			{
				this.AuditsFolderId = sessionWithBestAccess.GetAuditsFolderId();
				if (this.AuditsFolderId != null)
				{
					this.LoadAuditSubfolders(sessionWithBestAccess, this.AuditsFolderId);
				}
				this.AdminAuditLogsFolderId = sessionWithBestAccess.GetAdminAuditLogsFolderId();
				if (this.AdminAuditLogsFolderId != null)
				{
					this.LoadAuditSubfolders(sessionWithBestAccess, this.AdminAuditLogsFolderId);
				}
			});
			if (base.RecoverableItemsRootFolderId == null || base.RecoverableItemsDeletionsFolderId == null || base.RecoverableItemsVersionsFolderId == null || base.RecoverableItemsPurgesFolderId == null || base.CalendarLoggingFolderId == null)
			{
				return FolderIdState.FolderIdError;
			}
			COWSession.perfCounters.DumpsterSessionsActive.Increment();
			return FolderIdState.FolderIdSuccess;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x00115D14 File Offset: 0x00113F14
		private int GetProcessItemCount(Folder currentFolder, List<ICOWNotification> clients, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession destinationSession, StoreObjectId destinationFolderId, ItemQueryType type, bool onBeforeNotification, CallbackContext callbackContext)
		{
			int num = 0;
			COWSession.perfCounters.DumpsterFolderEnumRate.Increment();
			using (QueryResult queryResult = currentFolder.ItemQuery(type, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				for (;;)
				{
					object[][] rows = queryResult.GetRows(COWSession.itemQueryPageSize);
					if (rows == null || rows.Length <= 0)
					{
						break;
					}
					StoreObjectId[] array = new StoreObjectId[rows.Length];
					for (int i = 0; i < rows.Length; i++)
					{
						array[i] = (rows[i][0] as VersionedId).ObjectId;
					}
					this.InternalFolderChangeItemGroup(clients, operation, flags, destinationSession, destinationFolderId, array, onBeforeNotification, callbackContext);
					num += array.Length;
				}
			}
			return num;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x00115DD0 File Offset: 0x00113FD0
		protected override IList<StoreObjectId> InternalCopyOrMoveItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, bool moveRequest, bool forceNonIPM, bool returnNewItemIds)
		{
			StoragePermanentException ex = null;
			IList<StoreObjectId> result = null;
			if (!base.IsDumpsterFolder(sessionWithBestAccess, destinationFolderId))
			{
				throw new InvalidOperationException("destinationFolderId");
			}
			if (itemIds == null || itemIds.Length == 0)
			{
				return null;
			}
			Folder currentFolder = this.settings.GetCurrentFolder(sessionWithBestAccess);
			if (currentFolder == null)
			{
				return null;
			}
			try
			{
				if (COWSession.IsDelegateSession(this.session))
				{
					moveRequest = false;
					COWSession.perfCounters.DumpsterForceCopyItemsRate.IncrementBy((long)itemIds.Length);
					itemIds = this.RemoveSoftDeletedItems(itemIds);
					if (itemIds == null || itemIds.Length == 0)
					{
						return null;
					}
				}
				GroupOperationResult groupOperationResult;
				if (!forceNonIPM && !this.settings.IsCurrentFolderEnabled(sessionWithBestAccess))
				{
					ExTraceGlobals.SessionTracer.TraceDebug<StoreObjectId, int>((long)sessionWithBestAccess.GetHashCode(), "CopyOrMoveItemsToDumpster to dumpster skipped as source folder {0} is not-IPM ({1} items).", this.settings.CurrentFolderId, itemIds.Length);
					if (!moveRequest)
					{
						COWSession.perfCounters.DumpsterCopyNoKeepItemsRate.IncrementBy((long)itemIds.Length);
						return null;
					}
					DeleteItemFlags flags = DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt;
					groupOperationResult = currentFolder.InternalDeleteItems(flags, itemIds);
					COWSession.perfCounters.DumpsterMoveNoKeepItemsRate.IncrementBy((long)itemIds.Length);
				}
				else
				{
					if (destinationFolderId.Equals(base.RecoverableItemsDeletionsFolderId))
					{
						COWSession.perfCounters.DumpsterDeletionsItemsRate.IncrementBy((long)itemIds.Length);
					}
					else if (destinationFolderId.Equals(base.RecoverableItemsVersionsFolderId))
					{
						COWSession.perfCounters.DumpsterVersionsItemsRate.IncrementBy((long)itemIds.Length);
					}
					else if (destinationFolderId.Equals(base.RecoverableItemsPurgesFolderId))
					{
						COWSession.perfCounters.DumpsterPurgesItemsRate.IncrementBy((long)itemIds.Length);
					}
					using (Folder folder = Folder.Bind(sessionWithBestAccess, destinationFolderId))
					{
						if (moveRequest)
						{
							groupOperationResult = currentFolder.MoveItems(folder, itemIds, null, null, returnNewItemIds);
							COWSession.perfCounters.DumpsterMoveItemsRate.IncrementBy((long)itemIds.Length);
						}
						else
						{
							groupOperationResult = currentFolder.CopyItems(folder, itemIds, null, null, returnNewItemIds);
							COWSession.perfCounters.DumpsterCopyItemsRate.IncrementBy((long)itemIds.Length);
						}
						if (groupOperationResult.OperationResult == OperationResult.Succeeded && returnNewItemIds)
						{
							result = groupOperationResult.ResultObjectIds;
						}
					}
				}
				base.Results.AddPartialResult(groupOperationResult);
				if (groupOperationResult.OperationResult == OperationResult.Failed || (this.settings.LegalHoldEnabled() && groupOperationResult.OperationResult != OperationResult.Succeeded))
				{
					ExTraceGlobals.SessionTracer.TraceError<LocalizedException, int>((long)sessionWithBestAccess.GetHashCode(), "CopyOrMoveItemsToDumpster to dumpster failure {0} ({1} ids).", groupOperationResult.Exception, itemIds.Length);
					string error = string.Format("{0} {1} item(s) to the dumpster folder {2} failed on mailbox {3}, which is {4}on hold. This operation is from client {5} and its result is {6}", new object[]
					{
						moveRequest ? "Move" : "Copy",
						itemIds.Length,
						this.MapDumpsterFolderIdToDisplayName(destinationFolderId),
						this.session.MailboxOwnerLegacyDN,
						this.settings.LegalHoldEnabled() ? string.Empty : "not ",
						this.session.ClientInfoString,
						groupOperationResult.OperationResult.ToString()
					});
					LocalizedString message = ServerStrings.CopyToDumpsterFailure(error);
					throw new DumpsterOperationException(message, groupOperationResult.Exception);
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (VirusDetectedException ex3)
			{
				ex = ex3;
			}
			catch (VirusMessageDeletedException ex4)
			{
				ex = ex4;
			}
			catch (VirusException ex5)
			{
				ex = ex5;
			}
			catch (QuotaExceededException e)
			{
				this.LogException(e, new StackTrace(true).ToString());
				throw;
			}
			catch (Exception ex6)
			{
				this.LogException(ex6, new StackTrace(true).ToString());
				if (!(ex6.InnerException is PartialCompletionException))
				{
					this.PublishNotification(ex6, new StackTrace(true).ToString());
				}
				throw;
			}
			if (ex != null)
			{
				ExTraceGlobals.SessionTracer.TraceWarning<StoragePermanentException, int>((long)sessionWithBestAccess.GetHashCode(), "CopyOrMoveItemsToDumpster to dumpster failure {0} ({1} ids).", ex, itemIds.Length);
				base.Results.AddResult(new GroupOperationResult(OperationResult.PartiallySucceeded, itemIds, ex));
				this.settings.ResetCurrentFolder();
			}
			return result;
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00116200 File Offset: 0x00114400
		private string MapDumpsterFolderIdToDisplayName(StoreObjectId id)
		{
			if (id != null)
			{
				if (id.Equals(base.RecoverableItemsDeletionsFolderId))
				{
					return DefaultFolderType.RecoverableItemsDeletions.ToString();
				}
				if (id.Equals(base.RecoverableItemsDiscoveryHoldsFolderId))
				{
					return DefaultFolderType.RecoverableItemsDiscoveryHolds.ToString();
				}
				if (id.Equals(base.RecoverableItemsPurgesFolderId))
				{
					return DefaultFolderType.RecoverableItemsPurges.ToString();
				}
				if (id.Equals(base.RecoverableItemsVersionsFolderId))
				{
					return DefaultFolderType.RecoverableItemsVersions.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x00116284 File Offset: 0x00114484
		private bool IsDeleteOnFolderAllowed(StoreObjectId checkFolderId, ELCFolderFlags elcFolderFlags, string displayName)
		{
			if (!Folder.CanDeleteFolder(this.StoreSession, checkFolderId))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, StoreObjectId>((long)this.StoreSession.GetHashCode(), "IsDeleteOnFolderAllowed. Failed to delete folder {0}. It is a default folder. FolderId = {2}", displayName, checkFolderId);
				base.Results.AddResult(new GroupOperationResult(OperationResult.PartiallySucceeded, new StoreObjectId[]
				{
					checkFolderId
				}, new RecoverableItemsAccessDeniedException(displayName)));
				return false;
			}
			if (this.session.LogonType != LogonType.Admin && (elcFolderFlags & ELCFolderFlags.Protected) == ELCFolderFlags.Protected)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, StoreObjectId>((long)this.StoreSession.GetHashCode(), "IsDeleteOnFolderAllowed. Failed to delete folder {0}. It was a protected ELC folder. FolderId = {1}", displayName, checkFolderId);
				base.Results.AddResult(new GroupOperationResult(OperationResult.PartiallySucceeded, new StoreObjectId[]
				{
					checkFolderId
				}, new RecoverableItemsAccessDeniedException(displayName)));
				return false;
			}
			return true;
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00116338 File Offset: 0x00114538
		private void ProcessDeleteOnTopFolder(MailboxSession sessionWithBestAccess, StoreObjectId toDeleteFolderId, bool isEmptyFolder, FolderChangeOperationFlags flags, bool itemsFound, bool associatedFound, bool foldersFound)
		{
			bool flag = false;
			StoragePermanentException storageException = null;
			if (base.Results.AnyPartialResultFailure())
			{
				return;
			}
			if (base.Results.AnyResultNotSucceeded())
			{
				return;
			}
			if (itemsFound || associatedFound || foldersFound)
			{
				if (foldersFound && (flags & FolderChangeOperationFlags.IncludeSubFolders) != FolderChangeOperationFlags.IncludeSubFolders)
				{
					flag = true;
				}
				if (isEmptyFolder)
				{
					if (associatedFound && (flags & FolderChangeOperationFlags.IncludeAssociated) == FolderChangeOperationFlags.IncludeAssociated)
					{
						flag = true;
					}
					if (itemsFound && (flags & FolderChangeOperationFlags.IncludeItems) == FolderChangeOperationFlags.IncludeItems)
					{
						flag = true;
					}
				}
				else if (itemsFound)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Folder currentFolder = this.settings.GetCurrentFolder(sessionWithBestAccess);
				StoreObjectId parentId = currentFolder.ParentId;
				try
				{
					DeleteItemFlags deleteItemFlags = DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt;
					if (isEmptyFolder)
					{
						bool deleteAssociated = false;
						if ((flags & FolderChangeOperationFlags.IncludeAssociated) == FolderChangeOperationFlags.IncludeAssociated)
						{
							deleteAssociated = true;
						}
						base.Results.AddResult(currentFolder.DeleteAllObjects(deleteItemFlags, deleteAssociated));
					}
					else
					{
						using (Folder folder = Folder.Bind(this.StoreSession, parentId))
						{
							base.Results.AddResult(folder.DeleteObjects(deleteItemFlags, new StoreId[]
							{
								toDeleteFolderId
							}).GroupOperationResults);
						}
					}
				}
				catch (ObjectNotFoundException ex)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<string, ObjectNotFoundException>((long)this.StoreSession.GetHashCode(), "Source folder {0} not found {1}.", parentId.ToString(), ex);
					flag = true;
					storageException = ex;
				}
			}
			if (flag)
			{
				base.Results.AddResult(new GroupOperationResult(OperationResult.PartiallySucceeded, new StoreObjectId[]
				{
					toDeleteFolderId
				}, storageException));
			}
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x001164A0 File Offset: 0x001146A0
		private bool CheckOperationOnDumpster(MailboxSession sessionWithBestAccess, COWTriggerAction operation, StoreObjectId folderId)
		{
			return this.CheckOperationOnDumpster(sessionWithBestAccess, operation, folderId, null);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x001164AC File Offset: 0x001146AC
		private bool CheckOperationOnDumpster(MailboxSession sessionWithBestAccess, COWTriggerAction operation, StoreObjectId folderId, CoreItem item)
		{
			if (!base.IsDumpsterFolder(sessionWithBestAccess, folderId))
			{
				return false;
			}
			switch (operation)
			{
			case COWTriggerAction.Create:
			case COWTriggerAction.ItemBind:
			case COWTriggerAction.Submit:
			case COWTriggerAction.Copy:
			case COWTriggerAction.FolderBind:
				return true;
			case COWTriggerAction.Move:
			case COWTriggerAction.MoveToDeletedItems:
			case COWTriggerAction.HardDelete:
				return false;
			case COWTriggerAction.DoneWithMessageDelete:
				return true;
			}
			if (this.settings.HoldEnabled() && !this.IsUpdateAllowed(folderId) && !this.settings.IsMrmAction() && !this.settings.IsPermissibleInferenceAction(item))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string>((long)this.StoreSession.GetHashCode(), "Attempt to {0} items in the dumpster folder", operation.ToString());
				throw new RecoverableItemsAccessDeniedException("Recoverable Items");
			}
			return true;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x00116568 File Offset: 0x00114768
		private bool IsUpdateAllowed(StoreObjectId folderId)
		{
			return (this.StoreSession.LogonType == LogonType.Admin || this.StoreSession.LogonType == LogonType.SystemService) && folderId.Equals(base.RecoverableItemsPurgesFolderId);
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x00116594 File Offset: 0x00114794
		private bool CheckOperationOnDumpster(MailboxSession sessionWithBestAccess, COWTriggerAction operation, ICollection<StoreObjectId> folderIds)
		{
			if (folderIds == null)
			{
				return false;
			}
			foreach (StoreObjectId folderId in folderIds)
			{
				if (this.CheckOperationOnDumpster(sessionWithBestAccess, operation, folderId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x001165EC File Offset: 0x001147EC
		internal bool CheckOperationOnDumpster(MailboxSession sessionWithBestAccess, COWTriggerAction operation, CoreItem item)
		{
			if (item == null)
			{
				return false;
			}
			StoreObjectId internalStoreObjectId = ((ICoreObject)item).InternalStoreObjectId;
			return internalStoreObjectId != null && IdConverter.IsMessageId(internalStoreObjectId.ProviderLevelItemId) && this.CheckOperationOnDumpster(sessionWithBestAccess, operation, IdConverter.GetParentIdFromMessageId(internalStoreObjectId), item);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0011662C File Offset: 0x0011482C
		internal StoreObjectId[] RemoveSoftDeletedItems(StoreObjectId[] itemIds)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(itemIds.Length);
			PropertyDefinition[] propsToReturn = new PropertyDefinition[]
			{
				StoreObjectSchema.DeletedOnTime
			};
			foreach (StoreObjectId storeObjectId in itemIds)
			{
				bool flag = false;
				bool flag2 = true;
				try
				{
					using (Item item = Item.Bind(this.StoreSession, storeObjectId, ItemBindOption.SoftDeletedItem, propsToReturn))
					{
						object propertyValue = item.TryGetProperty(StoreObjectSchema.DeletedOnTime);
						flag = !PropertyError.IsPropertyError(propertyValue);
					}
				}
				catch (ObjectNotFoundException)
				{
					flag2 = false;
				}
				catch (VirusMessageDeletedException)
				{
					flag2 = false;
				}
				catch (VirusDetectedException)
				{
				}
				catch (VirusException)
				{
				}
				if (flag2 && !flag)
				{
					list.Add(storeObjectId);
				}
			}
			if (itemIds.Length > list.Count)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<int>((long)this.StoreSession.GetHashCode(), "RemoveSoftDeletedItems found {0} soft-deleted items which were removed.", itemIds.Length - list.Count);
				return list.ToArray();
			}
			return itemIds;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00116748 File Offset: 0x00114948
		private void SetInCallback(MailboxSession sessionWithBestAccess, bool inCallback)
		{
			this.InCallback = inCallback;
			if (sessionWithBestAccess != this.session)
			{
				sessionWithBestAccess.CowSession.SetInCallback(sessionWithBestAccess, inCallback);
			}
			if (!this.InCallback)
			{
				this.settings.ResetMailboxInfo();
			}
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00116A94 File Offset: 0x00114C94
		private IEnumerable<KeyValuePair<StoreObjectId, ICollection<StoreObjectId>>> GroupItemsPerParent(StoreObjectId parentFolderId, ICollection<StoreObjectId> itemIds)
		{
			if (parentFolderId == null || (parentFolderId.ObjectType != StoreObjectType.SearchFolder && parentFolderId.ObjectType != StoreObjectType.OutlookSearchFolder))
			{
				yield return new KeyValuePair<StoreObjectId, ICollection<StoreObjectId>>(parentFolderId, itemIds);
			}
			else
			{
				Dictionary<StoreObjectId, ICollection<StoreObjectId>> results = new Dictionary<StoreObjectId, ICollection<StoreObjectId>>();
				foreach (StoreObjectId storeObjectId in itemIds)
				{
					StoreObjectId key = parentFolderId;
					StoreObjectId item = storeObjectId;
					if (!storeObjectId.IsFolderId)
					{
						using (Item item2 = Item.Bind(this.StoreSession, storeObjectId, new PropertyDefinition[]
						{
							ItemSchema.Fid
						}))
						{
							this.settings.GetRealParentFolderForItem(item2.CoreItem, out key, out item);
						}
					}
					ICollection<StoreObjectId> collection;
					if (!results.ContainsKey(key))
					{
						collection = new List<StoreObjectId>(1);
					}
					else
					{
						collection = results[key];
					}
					collection.Add(item);
					results[key] = collection;
				}
				foreach (KeyValuePair<StoreObjectId, ICollection<StoreObjectId>> entry in results)
				{
					yield return entry;
				}
			}
			yield break;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x00116AC0 File Offset: 0x00114CC0
		private void LoadAuditSubfolders(MailboxSession sessionWithBestAccess, StoreObjectId auditRoot)
		{
			if (this.auditLogFolders == null)
			{
				this.auditLogFolders = new HashSet<StoreObjectId>();
			}
			try
			{
				foreach (StoreObjectId storeObjectId in this.InternalGetAuditSubfolders(sessionWithBestAccess, auditRoot))
				{
					if (storeObjectId != null && !this.auditLogFolders.Contains(storeObjectId))
					{
						this.auditLogFolders.Add(storeObjectId);
					}
				}
			}
			catch (StorageTransientException ex)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string>((long)sessionWithBestAccess.GetHashCode(), "Unable to load the list of audit subfolders. {0}", ex.ToString());
			}
			catch (StoragePermanentException ex2)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<string>((long)sessionWithBestAccess.GetHashCode(), "Unable to load the list of audit subfolders. {0}", ex2.ToString());
			}
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x00116C64 File Offset: 0x00114E64
		public override void DisableCalendarLogging()
		{
			this.CheckDisposed("DisableCalendarLogging");
			ExTraceGlobals.SessionTracer.TraceDebug((long)this.session.GetHashCode(), "Disabling the calendar logging");
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IRecipientSession adrecipientSession = this.session.GetADRecipientSession(false, ConsistencyMode.IgnoreInvalid);
				ADRecipient adrecipient;
				if (object.Equals(this.session.MailboxGuid, Guid.Empty))
				{
					adrecipient = adrecipientSession.FindByLegacyExchangeDN(this.session.MailboxOwnerLegacyDN);
				}
				else
				{
					adrecipient = adrecipientSession.FindByExchangeGuidIncludingAlternate(this.session.MailboxGuid);
				}
				ADUser aduser = adrecipient as ADUser;
				if (aduser == null)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.session.GetHashCode(), "User not found in AD.");
					return;
				}
				aduser.CalendarVersionStoreDisabled = true;
				adrecipientSession.Save(aduser);
				this.settings.RemoveMailboxInfoCache(this.session.MailboxGuid);
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_COWCalendarLoggingDisabled, null, new object[]
				{
					aduser.LegacyExchangeDN
				});
			});
			if (adoperationResult.Succeeded)
			{
				return;
			}
			ExTraceGlobals.SessionTracer.TraceError<Exception>((long)this.session.GetHashCode(), "DisableCalendarLogging failed due to directory exception {0}.", adoperationResult.Exception);
			LocalizedException ex = adoperationResult.Exception as LocalizedException;
			if (ex != null)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "DisableCalendarLogging failed due to directory exception {0}.", new object[]
				{
					ex
				});
			}
			throw adoperationResult.Exception;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x00116D08 File Offset: 0x00114F08
		private void PublishNotification(Exception e, string stackTrace)
		{
			if (e != null)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Compliance.Name, "Hold.HoldErrors.Monitor", string.Empty, ResultSeverityLevel.Error);
				string stateAttribute = e.GetType().ToString();
				string stateAttribute2 = this.session.MailboxGuid.ToString();
				string stateAttribute3 = (this.session.OrganizationId != null) ? this.session.OrganizationId.ToString() : string.Empty;
				eventNotificationItem.StateAttribute1 = stateAttribute;
				eventNotificationItem.StateAttribute2 = stateAttribute2;
				eventNotificationItem.StateAttribute3 = stateAttribute3;
				eventNotificationItem.Exception = string.Format("{0}\nFull StackTrace:\n{1}", e.ToString(), stackTrace);
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x00116DBC File Offset: 0x00114FBC
		private void LogException(Exception e, string stackTrace)
		{
			if (e != null)
			{
				string text = e.GetType().ToString();
				string text2 = this.session.MailboxGuid.ToString();
				string text3 = (this.session.OrganizationId != null) ? this.session.OrganizationId.ToString() : string.Empty;
				string text4 = string.Format("{0}\nFull StackTrace:\n{1}", e.ToString(), stackTrace);
				ComplianceCrimsonEvents.EvtComplianceAnalytics.Log<string, string, string, string, string, string, string, string, string, string, string, string, string, string, string, string, string, string>(DateTime.UtcNow.ToString(), ExchangeComponent.Compliance.Name, string.Empty, string.Empty, string.Empty, string.Empty, text, text4, string.Empty, text, text2, text3, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
			}
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00117078 File Offset: 0x00115278
		private IEnumerable<StoreObjectId> InternalGetAuditSubfolders(MailboxSession sessionWithBestAccess, StoreObjectId auditsRoot)
		{
			AuditLogCollection auditLogs = new AuditLogCollection(sessionWithBestAccess, auditsRoot, null);
			foreach (IAuditLog auditLog2 in auditLogs.GetAuditLogs())
			{
				AuditLog auditLog = (AuditLog)auditLog2;
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(auditLog.LogFolderId);
				if (storeObjectId != null)
				{
					yield return storeObjectId;
				}
			}
			yield break;
		}

		// Token: 0x0400240E RID: 9230
		private const int FolderPropertiesIdIndex = 0;

		// Token: 0x0400240F RID: 9231
		private const int FolderPropertiesFolderFlagsIndex = 1;

		// Token: 0x04002410 RID: 9232
		private const int FolderPropertiesDisplayNameIndex = 2;

		// Token: 0x04002411 RID: 9233
		private static readonly PropertyDefinition[] folderProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.FolderFlags,
			FolderSchema.DisplayName
		};

		// Token: 0x04002412 RID: 9234
		private readonly MailboxSession session;

		// Token: 0x04002413 RID: 9235
		private readonly COWSettings settings;

		// Token: 0x04002414 RID: 9236
		private static readonly MiddleTierStoragePerformanceCountersInstance perfCounters = DumpsterFolderHelper.GetPerfCounters();

		// Token: 0x04002415 RID: 9237
		private static readonly PropertyDefinition[] queryProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id
		};

		// Token: 0x04002416 RID: 9238
		private static List<ICOWNotification> cowClients = new List<ICOWNotification>();

		// Token: 0x04002417 RID: 9239
		private static int folderQueryPageSize = 100;

		// Token: 0x04002418 RID: 9240
		private static int itemQueryPageSize = 1000;

		// Token: 0x04002419 RID: 9241
		private static int copyOnWriteRollbackEventLogLimit = 10;

		// Token: 0x0400241A RID: 9242
		private HashSet<StoreObjectId> auditLogFolders;

		// Token: 0x0400241B RID: 9243
		private static COWSession.FolderOperationsPair[] skipFolderOperationsSetting = new COWSession.FolderOperationsPair[]
		{
			new COWSession.FolderOperationsPair
			{
				DefaultFolderType = DefaultFolderType.Calendar,
				Operations = MailboxAuditOperations.FolderBind
			},
			new COWSession.FolderOperationsPair
			{
				DefaultFolderType = DefaultFolderType.ConversationActions,
				Operations = MailboxAuditOperations.FolderBind
			},
			new COWSession.FolderOperationsPair
			{
				DefaultFolderType = DefaultFolderType.Root,
				Operations = MailboxAuditOperations.FolderBind
			}
		};

		// Token: 0x0400241C RID: 9244
		private int copyOnWriteRollbackCount;

		// Token: 0x0400241D RID: 9245
		private string copyOnWriteRollbackEventUserLegacyDN;

		// Token: 0x0400241E RID: 9246
		private string copyOnWriteRollbackEventSmtpAddress;

		// Token: 0x0400241F RID: 9247
		private string copyOnWriteRollbackEventClientInfo;

		// Token: 0x04002420 RID: 9248
		private IDictionary<StoreObjectId, MailboxAuditOperations> skipFolderOperationsCache;

		// Token: 0x02000637 RID: 1591
		private class FolderOperationsPair
		{
			// Token: 0x17001365 RID: 4965
			// (get) Token: 0x060041B6 RID: 16822 RVA: 0x001170A3 File Offset: 0x001152A3
			// (set) Token: 0x060041B7 RID: 16823 RVA: 0x001170AB File Offset: 0x001152AB
			public DefaultFolderType DefaultFolderType { get; set; }

			// Token: 0x17001366 RID: 4966
			// (get) Token: 0x060041B8 RID: 16824 RVA: 0x001170B4 File Offset: 0x001152B4
			// (set) Token: 0x060041B9 RID: 16825 RVA: 0x001170BC File Offset: 0x001152BC
			public MailboxAuditOperations Operations { get; set; }
		}
	}
}

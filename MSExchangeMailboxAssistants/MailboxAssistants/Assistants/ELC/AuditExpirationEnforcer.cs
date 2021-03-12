using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A3 RID: 163
	internal class AuditExpirationEnforcer : SysCleanupEnforcerBase, IAuditRecordStrategy<ItemData>
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0002FC19 File Offset: 0x0002DE19
		internal AuditExpirationEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002FC2E File Offset: 0x0002DE2E
		SortBy[] IAuditRecordStrategy<ItemData>.QuerySortOrder
		{
			get
			{
				return AuditExpirationEnforcer.SortByCreationTime;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0002FC35 File Offset: 0x0002DE35
		PropertyDefinition[] IAuditRecordStrategy<ItemData>.Columns
		{
			get
			{
				return AuditExpirationEnforcer.PropertyColumns;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002FC3C File Offset: 0x0002DE3C
		bool IAuditRecordStrategy<ItemData>.RecordFilter(IReadOnlyPropertyBag propertyBag, out bool stopNow)
		{
			stopNow = false;
			VersionedId versionedId = propertyBag[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceError<AuditExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				}
				return false;
			}
			if (ElcGlobals.ExpireDumpsterRightNow)
			{
				return true;
			}
			object obj = propertyBag[StoreObjectSchema.CreationTime];
			if (!ElcMailboxHelper.Exists(obj))
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceDebug<AuditExpirationEnforcer>((long)this.GetHashCode(), "{0}: CreationTime date is missing. Skipping the item.", this);
				}
				return false;
			}
			DateTime dateTime = (DateTime)((ExDateTime)obj).ToUtc();
			if (dateTime > this.expirationTime)
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Item {1} newer than minAgeLimitForFolder. Item:{2}, Limit:{3}.", new object[]
					{
						this,
						versionedId,
						dateTime,
						this.expirationTime
					});
				}
				stopNow = true;
				return false;
			}
			if (this.oldestExpiringLog.CompareTo(DateTime.MinValue) == 0)
			{
				this.oldestExpiringLog = dateTime;
			}
			else if (dateTime.CompareTo(this.oldestExpiringLog) < 0)
			{
				this.oldestExpiringLog = dateTime;
			}
			return true;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002FD74 File Offset: 0x0002DF74
		ItemData IAuditRecordStrategy<ItemData>.Convert(IReadOnlyPropertyBag propertyBag)
		{
			VersionedId itemId = propertyBag[ItemSchema.Id] as VersionedId;
			object obj = propertyBag[ItemSchema.Size];
			int messageSize = 0;
			if (obj != null && obj is int)
			{
				messageSize = (int)obj;
			}
			return new ItemData(itemId, messageSize);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002FDBC File Offset: 0x0002DFBC
		protected override void InvokeInternal()
		{
			if (!base.IsEnabled)
			{
				return;
			}
			MailboxSession mailboxSession = base.MailboxDataForTags.MailboxSession;
			MailboxSession mailboxSession2 = null;
			try
			{
				mailboxSession2 = MailboxSession.OpenAsSystemService(mailboxSession.MailboxOwner, CultureInfo.InvariantCulture, mailboxSession.ClientInfoString);
			}
			catch (ObjectNotFoundException arg)
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceDebug<IExchangePrincipal, ObjectNotFoundException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService because the mailbox is missing. {1}", mailboxSession.MailboxOwner, arg);
				}
			}
			catch (StorageTransientException arg2)
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.WarningTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceWarning<IExchangePrincipal, MailboxSession, StorageTransientException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService: {1}.\nError:\n{2}", mailboxSession.MailboxOwner, mailboxSession, arg2);
				}
			}
			catch (StoragePermanentException arg3)
			{
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceError<IExchangePrincipal, MailboxSession, StoragePermanentException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService: {1}.\nError:\n{2}", mailboxSession.MailboxOwner, mailboxSession, arg3);
				}
			}
			if (mailboxSession2 != null)
			{
				try
				{
					base.MailboxDataForTags.MailboxSession = mailboxSession2;
					base.InvokeInternal();
				}
				finally
				{
					base.MailboxDataForTags.MailboxSession = mailboxSession;
					mailboxSession2.Dispose();
					mailboxSession2 = null;
				}
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		private void CheckAndLogAdminAuditsWarningQuota(Folder folder)
		{
			object obj = folder[FolderSchema.ExtendedSize];
			if (obj is long)
			{
				ulong num = (ulong)((long)obj);
				if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditExpirationEnforcer.Tracer.TraceDebug<AuditExpirationEnforcer, ulong>((long)this.GetHashCode(), "{0}: AdminAudit FolderExtendedSize is {1}. ", this, num);
				}
				bool? useDatabaseQuotaDefaults = base.MailboxDataForTags.ElcUserInformation.ADUser.UseDatabaseQuotaDefaults;
				Unlimited<ByteQuantifiedSize> unlimited;
				if (useDatabaseQuotaDefaults != null && !useDatabaseQuotaDefaults.Value)
				{
					unlimited = base.MailboxDataForTags.ElcUserInformation.ADUser.RecoverableItemsQuota;
					if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AuditExpirationEnforcer.Tracer.TraceDebug<AuditExpirationEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. RecoverableItemsQuota from mailbox object = {2}.", this, useDatabaseQuotaDefaults, unlimited);
					}
				}
				else
				{
					if (base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota == null)
					{
						if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							AuditExpirationEnforcer.Tracer.TraceError<AuditExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get RecoverableItemsWarningQuota of this mailbox database. Couldnot check if admin audits are reaching the warning quota.", this);
						}
						return;
					}
					unlimited = base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota.Value;
					if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AuditExpirationEnforcer.Tracer.TraceDebug<AuditExpirationEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. RecoverableItemsWarningQuota from database object = {2}.", this, useDatabaseQuotaDefaults, unlimited);
					}
				}
				if (!unlimited.IsUnlimited)
				{
					double num2 = AdminAuditFolderStrategy.AdminAuditsWarningPercentage * unlimited.Value.ToBytes();
					if (num > num2)
					{
						if (AuditExpirationEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							AuditExpirationEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: AdminAudits Folder Size is {1}. Mailbox RecoverableItemsQuota is = {2}. Admin Audits are consuming {3}% or more of dumpster quota", new object[]
							{
								this,
								num,
								unlimited,
								AdminAuditFolderStrategy.AdminAuditsWarningPercentage * 100.0
							});
						}
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_AdminAuditsQuotaWarning, null, new object[]
						{
							base.MailboxDataForTags.ElcUserTagInformation.ADUser.OrganizationId,
							num,
							unlimited,
							base.MailboxDataForTags.MailboxSession.MailboxOwner
						});
					}
				}
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00030110 File Offset: 0x0002E310
		protected override bool QueryIsEnabled()
		{
			return true;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00030114 File Offset: 0x0002E314
		protected override void CollectItemsToExpire()
		{
			AuditFolderStrategy[] array = new AuditFolderStrategy[]
			{
				new MailboxAuditFolderStrategy(base.MailboxDataForTags, AuditExpirationEnforcer.Tracer),
				new AdminAuditFolderStrategy(base.MailboxDataForTags, AuditExpirationEnforcer.Tracer)
			};
			foreach (AuditFolderStrategy auditFolderStrategy in array)
			{
				StoreId folderId = auditFolderStrategy.GetFolderId(base.MailboxDataForTags.MailboxSession);
				if (folderId != null)
				{
					EnhancedTimeSpan auditRecordAgeLimit = auditFolderStrategy.AuditRecordAgeLimit;
					if (auditFolderStrategy is AdminAuditFolderStrategy)
					{
						using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, folderId, new PropertyDefinition[]
						{
							FolderSchema.ExtendedSize
						}))
						{
							this.CheckAndLogAdminAuditsWarningQuota(folder);
						}
						base.MailboxDataForTags.StatisticsLogEntry.AdminAuditRecordAgeLimit = auditRecordAgeLimit.ToString();
						base.MailboxDataForTags.StatisticsLogEntry.IsAdminAuditLog = true;
					}
					else
					{
						base.MailboxDataForTags.StatisticsLogEntry.MailboxAuditRecordAgeLimit = auditRecordAgeLimit.ToString();
					}
					AuditLogCollection auditLogCollection = new AuditLogCollection(base.MailboxDataForTags.MailboxSession, folderId, AuditExpirationEnforcer.Tracer);
					this.expirationTime = DateTime.MinValue;
					if (ElcGlobals.ExpireDumpsterRightNow)
					{
						this.expirationTime = DateTime.MaxValue;
					}
					else if (auditRecordAgeLimit != EnhancedTimeSpan.MaxValue)
					{
						this.expirationTime = base.MailboxDataForTags.UtcNow.Subtract(auditRecordAgeLimit);
					}
					if (this.expirationTime != DateTime.MinValue)
					{
						List<VersionedId> list = new List<VersionedId>();
						int num = 0;
						foreach (AuditLog auditLog in auditLogCollection.GetExpiringAuditLogs(this.expirationTime))
						{
							foreach (ItemData itemData in auditLog.FindAuditRecords<ItemData>(this))
							{
								base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData, false);
								this.itemsExpired++;
								num++;
								if (num % 100 == 0)
								{
									base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
								}
							}
							if (auditLog.EstimatedLogEndTime < this.expirationTime && auditLog.LogFolderId is VersionedId)
							{
								list.Add(auditLog.LogFolderId as VersionedId);
							}
						}
						if (list.Count > 0)
						{
							foreach (VersionedId itemId in list)
							{
								base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData(itemId, 0), false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00030410 File Offset: 0x0002E610
		protected override void StartPerfCounterCollect()
		{
			this.itemsExpired = 0;
			this.oldestExpiringLog = DateTime.MinValue;
			base.MailboxDataForTags.StatisticsLogEntry.IsAdminAuditLog = false;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00030438 File Offset: 0x0002E638
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			ELCPerfmon.TotalExpiredDumpsterItems.IncrementBy((long)this.itemsExpired);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByAuditExpirationEnforcer += (long)this.itemsExpired;
			base.MailboxDataForTags.StatisticsLogEntry.AuditExpirationEnforcerProcessingTime = timeElapsed;
			base.MailboxDataForTags.StatisticsLogEntry.OldestExpiringAuditLog = this.oldestExpiringLog.ToString();
		}

		// Token: 0x04000497 RID: 1175
		private static readonly Trace Tracer = ExTraceGlobals.AuditExpirationEnforcerTracer;

		// Token: 0x04000498 RID: 1176
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000499 RID: 1177
		private static readonly PropertyDefinition[] PropertyColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.CreationTime,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId
		};

		// Token: 0x0400049A RID: 1178
		private static readonly SortBy[] SortByCreationTime = new SortBy[]
		{
			new SortBy(StoreObjectSchema.CreationTime, SortOrder.Ascending)
		};

		// Token: 0x0400049B RID: 1179
		private int itemsExpired;

		// Token: 0x0400049C RID: 1180
		private DateTime expirationTime;

		// Token: 0x0400049D RID: 1181
		private DateTime oldestExpiringLog = DateTime.MinValue;
	}
}

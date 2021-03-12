using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000A7 RID: 167
	internal class AuditQuotaEnforcer : SysCleanupEnforcerBase, IAuditRecordStrategy<ItemData>
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00030730 File Offset: 0x0002E930
		internal AuditQuotaEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0003078D File Offset: 0x0002E98D
		SortBy[] IAuditRecordStrategy<ItemData>.QuerySortOrder
		{
			get
			{
				return AuditQuotaEnforcer.SortByCreationTime;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00030794 File Offset: 0x0002E994
		PropertyDefinition[] IAuditRecordStrategy<ItemData>.Columns
		{
			get
			{
				return AuditQuotaEnforcer.PropertyColumns;
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003079C File Offset: 0x0002E99C
		bool IAuditRecordStrategy<ItemData>.RecordFilter(IReadOnlyPropertyBag propertyBag, out bool stopNow)
		{
			stopNow = false;
			VersionedId versionedId = propertyBag[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceError<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				}
				return false;
			}
			if (propertyBag[ItemSchema.Size] == null)
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceError<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get size of this item. Skipping it.", this);
				}
				return false;
			}
			if (this.dumpsterSize - this.deletedItemsSize <= this.dumpsterQuota.Value.ToBytes())
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Collected sufficient items. dumspterSize={1}, deletedItemsSize={2}, itemId={3}.", new object[]
					{
						this,
						this.dumpsterSize,
						this.deletedItemsSize,
						versionedId
					});
				}
				stopNow = true;
				return false;
			}
			return true;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0003089C File Offset: 0x0002EA9C
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

		// Token: 0x06000660 RID: 1632 RVA: 0x000308E1 File Offset: 0x0002EAE1
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by AuditQuotaEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0003091C File Offset: 0x0002EB1C
		protected override bool QueryIsEnabled()
		{
			if (!this.IsSystemArbitrationMailbox())
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, string>((long)this.GetHashCode(), "{0}: This is not system arbitration mailbox '{1}'. This mailbox's dumpster will be skipped.", this, AuditQuotaEnforcer.SystemArbitrationMailboxName);
				}
				return false;
			}
			if (base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot) == null)
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: This user has no Dumpster root folder. The mailbox will be skipped.", this);
				}
				return false;
			}
			if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: This is system arbitration mailbox.The audits folder will be processed.", this);
			}
			return true;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000309BC File Offset: 0x0002EBBC
		protected override void InvokeInternal()
		{
			if (this.IsSystemArbitrationMailbox() && this.IsDumpsterOverQuota())
			{
				this.isOverQuota = true;
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: The system arbitration mailbox is over dumpster warning quota. Processing the adminaudits folder.", this);
				}
				MailboxSession mailboxSession = base.MailboxDataForTags.MailboxSession;
				MailboxSession mailboxSession2 = null;
				try
				{
					mailboxSession2 = MailboxSession.OpenAsSystemService(mailboxSession.MailboxOwner, CultureInfo.InvariantCulture, mailboxSession.ClientInfoString);
				}
				catch (ObjectNotFoundException arg)
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceDebug<IExchangePrincipal, ObjectNotFoundException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService because the mailbox is missing. {1}", mailboxSession.MailboxOwner, arg);
					}
				}
				catch (StorageTransientException arg2)
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.WarningTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceWarning<IExchangePrincipal, MailboxSession, StorageTransientException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService: {1}.\nError:\n{2}", mailboxSession.MailboxOwner, mailboxSession, arg2);
					}
				}
				catch (StoragePermanentException arg3)
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceError<IExchangePrincipal, MailboxSession, StoragePermanentException>((long)mailboxSession.GetHashCode(), "{0}: Failed to reopen the session with SystemService: {1}.\nError:\n{2}", mailboxSession.MailboxOwner, mailboxSession, arg3);
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
				if (base.IsEnabled)
				{
					this.LogAuditsCleanupEvent();
				}
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00030B30 File Offset: 0x0002ED30
		private bool IsSystemArbitrationMailbox()
		{
			return base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString().Contains(AuditQuotaEnforcer.SystemArbitrationMailboxName);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00030B74 File Offset: 0x0002ED74
		protected override void CollectItemsToExpire()
		{
			DefaultFolderType[] foldersToProcessForQuota = AuditQuotaEnforcer.FoldersToProcessForQuota;
			int i = 0;
			while (i < foldersToProcessForQuota.Length)
			{
				DefaultFolderType defaultFolderType = foldersToProcessForQuota[i];
				if (this.CollectItemsInFolder(defaultFolderType))
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: CollectItemsToExpire returned false for folderType {1}.", this, defaultFolderType);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00030BC8 File Offset: 0x0002EDC8
		private bool CollectItemsInFolder(DefaultFolderType folderToCollect)
		{
			return this.ProcessFolderContents(folderToCollect, ItemQueryType.None);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00030BD8 File Offset: 0x0002EDD8
		private bool ProcessFolderContents(DefaultFolderType folderToCollect, ItemQueryType itemQueryType)
		{
			if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, DefaultFolderType, ItemQueryType>((long)this.GetHashCode(), "{0}: ProcessFolderContents: folderToCollect={1}, itemQueryType={2}.", this, folderToCollect, itemQueryType);
			}
			StoreId adminAuditLogsFolderId = base.MailboxDataForTags.MailboxSession.GetAdminAuditLogsFolderId();
			int num = 0;
			AuditLogCollection auditLogCollection = new AuditLogCollection(base.MailboxDataForTags.MailboxSession, adminAuditLogsFolderId, AuditQuotaEnforcer.Tracer);
			foreach (IAuditLog auditLog in auditLogCollection.GetAuditLogs())
			{
				AuditLog auditLog2 = (AuditLog)auditLog;
				this.itemsInDumpster += auditLog2.EstimatedItemCount;
				foreach (ItemData itemData in auditLog2.FindAuditRecords<ItemData>(this))
				{
					base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData, false);
					this.deletedItemsSize += (ulong)((long)itemData.MessageSize);
					this.itemsExpired++;
					num++;
					if (num % 100 == 0)
					{
						base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
					}
				}
				if (this.dumpsterSize - this.deletedItemsSize <= this.dumpsterQuota.Value.ToBytes())
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, ulong, ulong>((long)this.GetHashCode(), "{0}: Collected sufficient items. dumspterSize={1}, deletedItemsSize={2}.", this, this.dumpsterSize, this.deletedItemsSize);
						break;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00030D98 File Offset: 0x0002EF98
		protected override void ExpireItemsAlready()
		{
			this.SetAdminAuditsFolderStats(this.beforeFolderSize, this.beforeFolderCount);
			base.ExpireItemsAlready();
			this.SetAdminAuditsFolderStats(this.afterFolderSize, this.afterFolderCount);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		private bool IsDumpsterOverQuota()
		{
			base.MailboxDataForTags.MailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.DumpsterQuotaUsedExtended
			});
			object obj = base.MailboxDataForTags.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended);
			if (!(obj is long))
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceError<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get size of this mailbox. Skipping it.", this);
				}
				return false;
			}
			this.dumpsterSize = (ulong)((long)base.MailboxDataForTags.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended));
			bool? useDatabaseQuotaDefaults = base.MailboxDataForTags.ElcUserInformation.ADUser.UseDatabaseQuotaDefaults;
			if (useDatabaseQuotaDefaults != null && !useDatabaseQuotaDefaults.Value)
			{
				this.dumpsterQuota = base.MailboxDataForTags.ElcUserInformation.ADUser.RecoverableItemsWarningQuota;
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. RecoverableItemsWarningQuota from mailbox object = {2}.", this, useDatabaseQuotaDefaults, this.dumpsterQuota);
				}
			}
			else
			{
				if (base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota == null)
				{
					if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						AuditQuotaEnforcer.Tracer.TraceError<AuditQuotaEnforcer>((long)this.GetHashCode(), "{0}: We could not get RecoverableItemsWarningQuota of this mailbox database. Skipping it.", this);
					}
					return false;
				}
				this.dumpsterQuota = base.SysCleanupSubAssistant.DatabaseConfig.DatabaseDumpsterWarningQuota.Value;
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. Mailbox.RecoverableItemsWarningQuota from database object = {2}.", this, useDatabaseQuotaDefaults, this.dumpsterQuota);
				}
			}
			if (this.dumpsterQuota.IsUnlimited)
			{
				if (AuditQuotaEnforcer.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AuditQuotaEnforcer.Tracer.TraceDebug<AuditQuotaEnforcer, bool?, Unlimited<ByteQuantifiedSize>>((long)this.GetHashCode(), "{0}: useDatabaseQuotaDefaults is {1}. RecoverableItemsWarningQuota is set to Unlimited, enforcing a warning quota to 20 GB.", this, useDatabaseQuotaDefaults, this.dumpsterQuota);
				}
				this.dumpsterQuota = AuditQuotaEnforcer.DefaultWarningQuota;
			}
			return this.dumpsterSize > this.dumpsterQuota.Value.ToBytes();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00030FC8 File Offset: 0x0002F1C8
		private void SetAdminAuditsFolderStats(string[] folderSize, string[] folderCount)
		{
			StoreId adminAuditLogsFolderId = base.MailboxDataForTags.MailboxSession.GetAdminAuditLogsFolderId();
			if (adminAuditLogsFolderId == null)
			{
				folderSize[0] = "-";
				folderCount[0] = "-";
				return;
			}
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, adminAuditLogsFolderId, new PropertyDefinition[]
			{
				FolderSchema.ExtendedSize
			}))
			{
				folderSize[0] = folder[FolderSchema.ExtendedSize].ToString();
				folderCount[0] = folder.ItemCount.ToString();
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0003105C File Offset: 0x0002F25C
		private void LogAuditsCleanupEvent()
		{
			base.MailboxDataForTags.MailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.DumpsterQuotaUsedExtended
			});
			object obj = base.MailboxDataForTags.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.DumpsterQuotaUsedExtended);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_DumpsterOverQuotaDeletedAuditLogs, null, new object[]
			{
				base.MailboxDataForTags.MailboxSession.MailboxOwner,
				this.dumpsterQuota,
				this.dumpsterSize,
				obj,
				string.Join(", ", this.beforeFolderSize),
				string.Join(", ", this.beforeFolderCount),
				string.Join(", ", this.afterFolderSize),
				string.Join(", ", this.afterFolderCount)
			});
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00031141 File Offset: 0x0002F341
		protected override void StartPerfCounterCollect()
		{
			this.itemsInDumpster = 0;
			this.itemsExpired = 0;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00031154 File Offset: 0x0002F354
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			if (this.isOverQuota)
			{
				ELCPerfmon.TotalOverQuotaDumpsters.Increment();
			}
			ELCPerfmon.TotalOverQuotaDumpsterItems.IncrementBy((long)this.itemsInDumpster);
			ELCPerfmon.TotalOverQuotaDumpsterItemsDeleted.IncrementBy((long)this.itemsExpired);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByDumpsterQuotaEnforcer += (long)this.itemsExpired;
			base.MailboxDataForTags.StatisticsLogEntry.DumpsterQuotaEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x040004A2 RID: 1186
		private const string CommaSeparator = ", ";

		// Token: 0x040004A3 RID: 1187
		private static readonly string SystemArbitrationMailboxName = "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}";

		// Token: 0x040004A4 RID: 1188
		private static Unlimited<ByteQuantifiedSize> DefaultWarningQuota = ProvisioningHelper.DefaultRecoverableItemsWarningQuota;

		// Token: 0x040004A5 RID: 1189
		private static readonly Trace Tracer = ExTraceGlobals.DumpsterQuotaEnforcerTracer;

		// Token: 0x040004A6 RID: 1190
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040004A7 RID: 1191
		protected static readonly DefaultFolderType[] FoldersToProcessForQuota = new DefaultFolderType[]
		{
			DefaultFolderType.AdminAuditLogs
		};

		// Token: 0x040004A8 RID: 1192
		private static readonly PropertyDefinition[] PropertyColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size
		};

		// Token: 0x040004A9 RID: 1193
		private static readonly SortBy[] SortByCreationTime = new SortBy[]
		{
			new SortBy(StoreObjectSchema.CreationTime, SortOrder.Ascending)
		};

		// Token: 0x040004AA RID: 1194
		private ulong deletedItemsSize;

		// Token: 0x040004AB RID: 1195
		private Unlimited<ByteQuantifiedSize> dumpsterQuota;

		// Token: 0x040004AC RID: 1196
		private ulong dumpsterSize;

		// Token: 0x040004AD RID: 1197
		private string[] beforeFolderSize = new string[AuditQuotaEnforcer.FoldersToProcessForQuota.Length];

		// Token: 0x040004AE RID: 1198
		private string[] afterFolderSize = new string[AuditQuotaEnforcer.FoldersToProcessForQuota.Length];

		// Token: 0x040004AF RID: 1199
		private string[] beforeFolderCount = new string[AuditQuotaEnforcer.FoldersToProcessForQuota.Length];

		// Token: 0x040004B0 RID: 1200
		private string[] afterFolderCount = new string[AuditQuotaEnforcer.FoldersToProcessForQuota.Length];

		// Token: 0x040004B1 RID: 1201
		private string toString;

		// Token: 0x040004B2 RID: 1202
		private bool isOverQuota;

		// Token: 0x040004B3 RID: 1203
		private int itemsInDumpster;

		// Token: 0x040004B4 RID: 1204
		private int itemsExpired;
	}
}

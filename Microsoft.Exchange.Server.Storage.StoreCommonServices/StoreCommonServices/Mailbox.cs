using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000A2 RID: 162
	public abstract class Mailbox : SharedObjectPropertyBag, IMailboxContext, ISchemaVersion, IStateObject
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0002061C File Offset: 0x0001E81C
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00020623 File Offset: 0x0001E823
		public static TimeSpan MaintenanceRunInterval { get; private set; }

		// Token: 0x06000611 RID: 1553 RVA: 0x0002062C File Offset: 0x0001E82C
		protected Mailbox(StoreDatabase database, MailboxState mailboxState, Context context) : this(database, DatabaseSchema.MailboxTable(database), mailboxState, context, false, new ColumnValue[]
		{
			new ColumnValue(DatabaseSchema.MailboxTable(database).MailboxNumber, mailboxState.MailboxNumber)
		})
		{
			if (!base.IsDead)
			{
				this.valid = true;
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxTracer.TraceDebug<int>(0L, "Mailbox:Mailbox(): Mailbox {0} has been opened", this.MailboxNumber);
					return;
				}
			}
			else if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceDebug<Guid>(0L, "Mailbox:Mailbox(): Mailbox {0} has not been opened - does not exist", mailboxState.MailboxGuid);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000206D0 File Offset: 0x0001E8D0
		protected Mailbox(StoreDatabase database, Context context, MailboxState mailboxState, MailboxInfo mailboxDirectoryInfo, Guid mailboxInstanceGuid, Guid localIdGuid, Guid mappingSignatureGuid, ulong nextIdCounter, uint? reservedIdCounterRange, ulong nextCnCounter, uint? reservedCnCounterRange, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap, Dictionary<ushort, Guid> replidGuidMap, Guid defaultFoldersReplGuid, bool createdByMove) : this(database, DatabaseSchema.MailboxTable(database), mailboxState, context, true, new ColumnValue[]
		{
			new ColumnValue(DatabaseSchema.MailboxTable(database).MailboxGuid, mailboxDirectoryInfo.MailboxGuid)
		})
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				bool flag = numberToNameMap != null && replidGuidMap != null;
				base.SetColumn(context, this.mailboxTable.DeletedOn, null);
				base.SetColumn(context, this.mailboxTable.OofState, false);
				base.SetColumn(context, this.mailboxTable.MailboxInstanceGuid, mailboxInstanceGuid);
				base.SetColumn(context, this.mailboxTable.MappingSignatureGuid, mappingSignatureGuid);
				base.SetColumn(context, this.mailboxTable.Lcid, CultureHelper.GetLcidFromCulture(CultureHelper.DefaultCultureInfo));
				base.SetColumn(context, this.mailboxTable.MessageCount, 0L);
				base.SetColumn(context, this.mailboxTable.MessageSize, 0L);
				base.SetColumn(context, this.mailboxTable.HiddenMessageCount, 0L);
				base.SetColumn(context, this.mailboxTable.HiddenMessageSize, 0L);
				base.SetColumn(context, this.mailboxTable.MessageDeletedCount, 0L);
				base.SetColumn(context, this.mailboxTable.MessageDeletedSize, 0L);
				base.SetColumn(context, this.mailboxTable.HiddenMessageDeletedCount, 0L);
				base.SetColumn(context, this.mailboxTable.HiddenMessageDeletedSize, 0L);
				base.SetColumn(context, this.mailboxTable.MailboxDatabaseVersion, StoreDatabase.GetMinimumSchemaVersion().Value);
				base.SetColumn(context, this.mailboxTable.PreservingMailboxSignature, flag);
				base.SetColumn(context, this.mailboxTable.NextMessageDocumentId, 1);
				this.SetLastQuotaCheckTime(context, DateTime.MinValue);
				this.SetLastMailboxMaintenanceTime(context, DateTime.MinValue);
				base.SetColumn(context, this.mailboxTable.ConversationEnabled, false);
				DateTime utcNow = this.sharedState.UtcNow;
				base.SetColumn(context, this.mailboxTable.Status, (short)this.sharedState.Status);
				base.SetColumn(context, this.mailboxTable.MailboxNumber, this.sharedState.MailboxNumber);
				base.SetPrimaryKey(new ColumnValue[]
				{
					new ColumnValue(this.mailboxTable.MailboxNumber, this.sharedState.MailboxNumber)
				});
				if (UnifiedMailbox.IsReady(context, context.Database))
				{
					base.SetColumn(context, this.mailboxTable.MailboxPartitionNumber, this.sharedState.MailboxPartitionNumber);
					if (this.sharedState.UnifiedState != null)
					{
						base.SetColumn(context, this.mailboxTable.UnifiedMailboxGuid, this.sharedState.UnifiedState.UnifiedMailboxGuid);
					}
				}
				MailboxMiscFlags mailboxMiscFlags = MailboxMiscFlags.None;
				if (mailboxDirectoryInfo.IsArchiveMailbox)
				{
					mailboxMiscFlags |= MailboxMiscFlags.ArchiveMailbox;
				}
				if (createdByMove)
				{
					mailboxMiscFlags |= MailboxMiscFlags.CreatedByMove;
					if (flag)
					{
						mailboxMiscFlags |= MailboxMiscFlags.MRSPreservingMailboxSignature;
					}
				}
				this.SetProperty(context, PropTag.Mailbox.MailboxFlags, (int)mailboxMiscFlags);
				if (flag)
				{
					NamedPropertyMap.CreateCacheForNewMailbox(context, this.sharedState, numberToNameMap);
					ReplidGuidMap.CreateCacheForNewMailbox(context, this.sharedState, replidGuidMap);
				}
				if (reservedIdCounterRange != null)
				{
					ulong num = nextIdCounter + (ulong)reservedIdCounterRange.Value;
					this.SetProperty(context, PropTag.Mailbox.ReservedIdCounterRangeUpperLimit, (long)num);
				}
				if (reservedCnCounterRange != null)
				{
					ulong num2 = nextCnCounter + (ulong)reservedCnCounterRange.Value;
					this.SetProperty(context, PropTag.Mailbox.ReservedCnCounterRangeUpperLimit, (long)num2);
				}
				this.SetProperty(context, PropTag.Mailbox.CreationTime, this.sharedState.UtcNow);
				this.SetProperty(context, PropTag.Mailbox.MailboxType, (int)mailboxDirectoryInfo.Type);
				this.sharedState.MailboxType = mailboxDirectoryInfo.Type;
				uint num3;
				bool flag2;
				if (Mailbox.GetMailboxTypeVersion(context, mailboxDirectoryInfo.Type, mailboxDirectoryInfo.TypeDetail, out num3, out flag2))
				{
					if (!flag2)
					{
						DiagnosticContext.TraceDword((LID)64988U, (uint)mailboxDirectoryInfo.Type);
						DiagnosticContext.TraceDword((LID)40412U, (uint)mailboxDirectoryInfo.TypeDetail);
						throw new StoreException((LID)64348U, ErrorCodeValue.NotSupported, "Mailbox type is not supported on this database.");
					}
					this.SetProperty(context, PropTag.Mailbox.MailboxTypeVersion, (int)num3);
				}
				this.SetProperty(context, PropTag.Mailbox.MailboxTypeDetail, (int)mailboxDirectoryInfo.TypeDetail);
				this.sharedState.MailboxTypeDetail = mailboxDirectoryInfo.TypeDetail;
				this.SetDirectoryPersonalInfoOnMailbox(context, mailboxDirectoryInfo);
				base.Flush(context);
				using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, this.mailboxIdentityTable.Table, true, new ColumnValue[]
				{
					new ColumnValue(this.mailboxIdentityTable.MailboxPartitionNumber, this.sharedState.MailboxPartitionNumber)
				}))
				{
					if (dataRow == null)
					{
						ColumnValue[] initialValues;
						if (this.sharedState.UnifiedState == null)
						{
							initialValues = new ColumnValue[]
							{
								new ColumnValue(this.mailboxIdentityTable.MailboxPartitionNumber, this.sharedState.MailboxPartitionNumber),
								new ColumnValue(this.mailboxIdentityTable.LocalIdGuid, localIdGuid),
								new ColumnValue(this.mailboxIdentityTable.IdCounter, (long)nextIdCounter),
								new ColumnValue(this.mailboxIdentityTable.CnCounter, (long)nextCnCounter),
								new ColumnValue(this.mailboxIdentityTable.LastCounterPatchingTime, utcNow)
							};
						}
						else
						{
							initialValues = new ColumnValue[]
							{
								new ColumnValue(this.mailboxIdentityTable.MailboxPartitionNumber, this.sharedState.MailboxPartitionNumber),
								new ColumnValue(this.mailboxIdentityTable.LocalIdGuid, localIdGuid),
								new ColumnValue(this.mailboxIdentityTable.IdCounter, (long)nextIdCounter),
								new ColumnValue(this.mailboxIdentityTable.CnCounter, (long)nextCnCounter),
								new ColumnValue(this.mailboxIdentityTable.LastCounterPatchingTime, utcNow),
								new ColumnValue(this.mailboxIdentityTable.NextMessageDocumentId, 1)
							};
						}
						using (DataRow dataRow2 = Factory.CreateDataRow(context.Culture, context, this.mailboxIdentityTable.Table, true, initialValues))
						{
							dataRow2.Flush(context);
							goto IL_721;
						}
					}
					this.ChangeNumberAndIdCounters.UpdateMailboxGlobalIDs(context, this, context.Database, false);
					IL_721:;
				}
				this.ReplidGuidMap.GetReplidFromGuid(context, defaultFoldersReplGuid);
				MailboxState.UnifiedMailboxState unifiedState = this.sharedState.UnifiedState;
				this.ReplidGuidMap.GetReplidFromGuid(context, localIdGuid);
				short[] value = null;
				short[] value2 = null;
				if (context.DatabaseType != DatabaseType.Sql)
				{
					value = PropertyPromotionHelper.BuildDefaultPromotedPropertyIds(context, this);
					value2 = PropertyPromotionHelper.BuildAlwaysPromotedPropertyIds(context, this);
				}
				base.SetColumn(context, this.mailboxTable.DefaultPromotedMessagePropertyIds, value);
				base.SetColumn(context, this.mailboxTable.AlwaysPromotedMessagePropertyIds, value2);
				this.valid = true;
				this.mailboxInfo = mailboxDirectoryInfo;
				this.ChangeNumberAndIdCounters.InitializeCounterCaches(context, this);
				this.ReserveFolderIdRange(context, 100U);
				if (!flag)
				{
					this.ReplidGuidMap.RegisterReservedGuidValues(context);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00020F18 File Offset: 0x0001F118
		private Mailbox(StoreDatabase database, MailboxTable mailboxTable, MailboxState mailboxState, Context context, bool isNew, params ColumnValue[] initialValues) : base(context, mailboxTable.Table, null, isNew, true, SharedObjectPropertyBagDataCache.GetCacheForMailbox(mailboxState), Mailbox.MailboxPropertyBagCacheKey(mailboxState.MailboxNumber), initialValues)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.sharedState = mailboxState;
				this.sharedState.AddReference();
				this.database = database;
				this.context = context;
				this.context.RegisterMailboxContext(this);
				if (this.GetPropertyValue(context, PropTag.Mailbox.CreationTime) == null)
				{
					this.SetProperty(context, PropTag.Mailbox.CreationTime, this.sharedState.UtcNow);
				}
				MailboxShape mailboxShape = MailboxShapeAppConfig.Get(this.MailboxGuid);
				UnlimitedItems mailboxMessagesPerFolderCountWarningQuota;
				UnlimitedItems mailboxMessagesPerFolderCountReceiveQuota;
				UnlimitedItems dumpsterMessagesPerFolderCountWarningQuota;
				UnlimitedItems dumpsterMessagesPerFolderCountReceiveQuota;
				UnlimitedItems folderHierarchyChildCountWarningQuota;
				UnlimitedItems folderHierarchyChildCountReceiveQuota;
				UnlimitedItems folderHierarchyDepthWarningQuota;
				UnlimitedItems folderHierarchyDepthReceiveQuota;
				UnlimitedItems foldersCountWarningQuota;
				UnlimitedItems foldersCountReceiveQuota;
				UnlimitedItems namedPropertiesCountQuota;
				if (mailboxShape != null)
				{
					mailboxMessagesPerFolderCountWarningQuota = new UnlimitedItems(mailboxShape.MessagesPerFolderCountWarningQuota);
					mailboxMessagesPerFolderCountReceiveQuota = new UnlimitedItems(mailboxShape.MessagesPerFolderCountReceiveQuota);
					dumpsterMessagesPerFolderCountWarningQuota = new UnlimitedItems(mailboxShape.DumpsterMessagesPerFolderCountWarningQuota);
					dumpsterMessagesPerFolderCountReceiveQuota = new UnlimitedItems(mailboxShape.DumpsterMessagesPerFolderCountReceiveQuota);
					folderHierarchyChildCountWarningQuota = new UnlimitedItems(mailboxShape.FolderHierarchyChildrenCountWarningQuota);
					folderHierarchyChildCountReceiveQuota = new UnlimitedItems(mailboxShape.FolderHierarchyChildrenCountReceiveQuota);
					folderHierarchyDepthWarningQuota = new UnlimitedItems(mailboxShape.FolderHierarchyDepthWarningQuota);
					folderHierarchyDepthReceiveQuota = new UnlimitedItems(mailboxShape.FolderHierarchyDepthReceiveQuota);
					foldersCountWarningQuota = new UnlimitedItems(mailboxShape.FoldersCountWarningQuota);
					foldersCountReceiveQuota = new UnlimitedItems(mailboxShape.FoldersCountReceiveQuota);
					namedPropertiesCountQuota = new UnlimitedItems(mailboxShape.NamedPropertiesCountQuota);
				}
				else
				{
					int? num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota);
					mailboxMessagesPerFolderCountWarningQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.MailboxMessagesPerFolderCountWarningQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota);
					mailboxMessagesPerFolderCountReceiveQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.MailboxMessagesPerFolderCountReceiveQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota);
					dumpsterMessagesPerFolderCountWarningQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.DumpsterMessagesPerFolderCountWarningQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota);
					dumpsterMessagesPerFolderCountReceiveQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.DumpsterMessagesPerFolderCountReceiveQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota);
					folderHierarchyChildCountWarningQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FolderHierarchyChildrenCountWarningQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota);
					folderHierarchyChildCountReceiveQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FolderHierarchyChildrenCountReceiveQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FolderHierarchyDepthWarningQuota);
					folderHierarchyDepthWarningQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FolderHierarchyDepthWarningQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FolderHierarchyDepthReceiveQuota);
					folderHierarchyDepthReceiveQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FolderHierarchyDepthReceiveQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FoldersCountWarningQuota);
					foldersCountWarningQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FoldersCountWarningQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.FoldersCountReceiveQuota);
					foldersCountReceiveQuota = ((num != null) ? new UnlimitedItems((long)num.Value) : ConfigurationSchema.FoldersCountReceiveQuota.Value);
					num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.NamedPropertiesCountQuota);
					namedPropertiesCountQuota = new UnlimitedItems((long)((num != null) ? num.Value : ((int)ConfigurationSchema.MAPINamedPropsQuota.Value)));
				}
				this.QuotaInfo = new QuotaInfo(mailboxMessagesPerFolderCountWarningQuota, mailboxMessagesPerFolderCountReceiveQuota, dumpsterMessagesPerFolderCountWarningQuota, dumpsterMessagesPerFolderCountReceiveQuota, folderHierarchyChildCountWarningQuota, folderHierarchyChildCountReceiveQuota, folderHierarchyDepthWarningQuota, folderHierarchyDepthReceiveQuota, foldersCountWarningQuota, foldersCountReceiveQuota, namedPropertiesCountQuota);
				this.mailboxTable = mailboxTable;
				this.mailboxIdentityTable = DatabaseSchema.MailboxIdentityTable(database);
				disposeGuard.Success();
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00021340 File Offset: 0x0001F540
		public static IMailboxMaintenance CleanupHardDeletedMailboxesMaintenance
		{
			get
			{
				return Mailbox.cleanupHardDeletedMailboxesMaintenance;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00021347 File Offset: 0x0001F547
		public static IMailboxMaintenance SynchronizeWithDSMailboxMaintenance
		{
			get
			{
				return Mailbox.synchronizeWithDSMailboxMaintenance;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0002134E File Offset: 0x0001F54E
		public static ReadOnlyCollection<Mailbox.TableSizeStatistics> TableSizeStatisticsDefinitions
		{
			get
			{
				return new ReadOnlyCollection<Mailbox.TableSizeStatistics>(Mailbox.tableSizeStatistics);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0002135A File Offset: 0x0001F55A
		public override Context CurrentOperationContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00021362 File Offset: 0x0001F562
		public bool IsConnected
		{
			get
			{
				return this.CurrentOperationContext != null;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00021370 File Offset: 0x0001F570
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return ReplidGuidMap.GetCacheForMailbox(this.context, this.SharedState);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00021383 File Offset: 0x0001F583
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0002138B File Offset: 0x0001F58B
		public MailboxInfo MailboxInfo
		{
			get
			{
				return this.mailboxInfo;
			}
			set
			{
				this.mailboxInfo = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00021394 File Offset: 0x0001F594
		public NamedPropertyMap NamedPropertyMap
		{
			get
			{
				return NamedPropertyMap.GetCacheForMailbox(this.context, this.SharedState);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000213A7 File Offset: 0x0001F5A7
		public ChangeNumberAndIdCounters ChangeNumberAndIdCounters
		{
			get
			{
				return ChangeNumberAndIdCounters.GetCacheForMailbox(this);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000213AF File Offset: 0x0001F5AF
		public MailboxIdentityTable MailboxIdentityTable
		{
			get
			{
				return this.mailboxIdentityTable;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000213B7 File Offset: 0x0001F5B7
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000213BF File Offset: 0x0001F5BF
		public Guid MdbGuid
		{
			get
			{
				return this.database.MdbGuid;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x000213CC File Offset: 0x0001F5CC
		public MailboxState SharedState
		{
			get
			{
				return this.sharedState;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000213D4 File Offset: 0x0001F5D4
		public int MailboxNumber
		{
			get
			{
				return this.sharedState.MailboxNumber;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x000213E1 File Offset: 0x0001F5E1
		public int MailboxPartitionNumber
		{
			get
			{
				return this.sharedState.MailboxPartitionNumber;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x000213EE File Offset: 0x0001F5EE
		public Guid MailboxGuid
		{
			get
			{
				return this.sharedState.MailboxGuid;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000213FB File Offset: 0x0001F5FB
		public Guid MailboxInstanceGuid
		{
			get
			{
				return this.sharedState.MailboxInstanceGuid;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00021408 File Offset: 0x0001F608
		public bool IsUnifiedMailbox
		{
			get
			{
				return this.sharedState.UnifiedState != null;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0002141B File Offset: 0x0001F61B
		public DateTime UtcNow
		{
			get
			{
				return this.sharedState.UtcNow;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00021428 File Offset: 0x0001F628
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00021430 File Offset: 0x0001F630
		public ExchangeId ConversationFolderId { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00021439 File Offset: 0x0001F639
		public override ObjectPropertySchema Schema
		{
			get
			{
				if (this.propertySchema == null)
				{
					this.propertySchema = PropertySchema.GetObjectSchema(this.Database, ObjectType.Mailbox);
				}
				return this.propertySchema;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0002145B File Offset: 0x0001F65B
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00021463 File Offset: 0x0001F663
		public QuotaInfo QuotaInfo
		{
			get
			{
				return this.quotaInfo;
			}
			set
			{
				this.quotaInfo = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0002146C File Offset: 0x0001F66C
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00021474 File Offset: 0x0001F674
		public QuotaStyle QuotaStyle
		{
			get
			{
				return this.quotaStyle;
			}
			set
			{
				this.quotaStyle = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0002147D File Offset: 0x0001F67D
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00021485 File Offset: 0x0001F685
		public UnlimitedBytes MaxItemSize
		{
			get
			{
				return this.maxItemSize;
			}
			set
			{
				this.maxItemSize = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0002148E File Offset: 0x0001F68E
		public bool IsPublicFolderMailbox
		{
			get
			{
				return this.SharedState.IsPublicFolderMailbox;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002149B File Offset: 0x0001F69B
		public bool IsGroupMailbox
		{
			get
			{
				return this.SharedState.IsGroupMailbox;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000214A8 File Offset: 0x0001F6A8
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000214BF File Offset: 0x0001F6BF
		private HashSet<ushort> DefaultPromotedPropertyIds
		{
			get
			{
				return (HashSet<ushort>)this.sharedState.GetComponentData(Mailbox.defaultPromotedPropertyIdsDataSlot);
			}
			set
			{
				this.sharedState.SetComponentData(Mailbox.defaultPromotedPropertyIdsDataSlot, value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000214D2 File Offset: 0x0001F6D2
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x000214E9 File Offset: 0x0001F6E9
		private HashSet<ushort> AlwaysPromotedPropertyIds
		{
			get
			{
				return (HashSet<ushort>)this.sharedState.GetComponentData(Mailbox.alwaysPromotedPropertyIdsDataSlot);
			}
			set
			{
				this.sharedState.SetComponentData(Mailbox.alwaysPromotedPropertyIdsDataSlot, value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000214FC File Offset: 0x0001F6FC
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00021513 File Offset: 0x0001F713
		private HashSet<ushort> StoreDefaultPromotedPropertyIds
		{
			get
			{
				return (HashSet<ushort>)this.sharedState.GetComponentData(Mailbox.storeDefaultPromotedPropertyIdsDataSlot);
			}
			set
			{
				this.sharedState.SetComponentData(Mailbox.storeDefaultPromotedPropertyIdsDataSlot, value);
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00021528 File Offset: 0x0001F728
		internal static void Initialize()
		{
			Mailbox.defaultPromotedPropertyIdsDataSlot = MailboxState.AllocateComponentDataSlot(true);
			Mailbox.alwaysPromotedPropertyIdsDataSlot = MailboxState.AllocateComponentDataSlot(true);
			Mailbox.storeDefaultPromotedPropertyIdsDataSlot = MailboxState.AllocateComponentDataSlot(true);
			Mailbox.folderIdAllocationCacheDataSlot = MailboxState.AllocateComponentDataSlot(false);
			Mailbox.MaintenanceRunInterval = TimeSpan.FromMinutes((double)ConfigurationSchema.MailboxMaintenanceIntervalMinutes.Value);
			SchemaUpgradeService.Register(SchemaUpgradeService.SchemaCategory.Mailbox, new SchemaUpgrader[]
			{
				SchemaUpgrader.Null(0, 121, 0, 122),
				SchemaUpgrader.Null(0, 122, 0, 123),
				SchemaUpgrader.Null(0, 123, 0, 124),
				SchemaUpgrader.Null(0, 124, 0, 125),
				SchemaUpgrader.Null(0, 125, 0, 126),
				SchemaUpgrader.Null(0, 126, 0, 127),
				SchemaUpgrader.Null(0, 127, 0, 128),
				SchemaUpgrader.Null(0, 128, 0, 129),
				SchemaUpgrader.Null(0, 129, 0, 130),
				SchemaUpgrader.Null(0, 130, 0, 131)
			});
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00021627 File Offset: 0x0001F827
		protected static void SetMailboxInitializationDelegates(Mailbox.OpenMailboxDelegate openMailboxDelegate, Mailbox.CreateMailboxDelegate createMailboxDelegate)
		{
			Mailbox.openMailboxDelegate = openMailboxDelegate;
			Mailbox.createMailboxDelegate = createMailboxDelegate;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00021635 File Offset: 0x0001F835
		internal static void RegisterTableSizeStatistics(IEnumerable<Mailbox.TableSizeStatistics> tableSizeStatistics)
		{
			Mailbox.tableSizeStatistics.AddRange(tableSizeStatistics);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00021644 File Offset: 0x0001F844
		internal static ulong GetFirstAvailableIdGlobcount(MailboxInfo mailboxDirectoryInfo)
		{
			ulong result = 256UL;
			if (mailboxDirectoryInfo.Type == MailboxInfo.MailboxType.PublicFolderPrimary || mailboxDirectoryInfo.Type == MailboxInfo.MailboxType.PublicFolderSecondary)
			{
				result = 1UL;
			}
			return result;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00021670 File Offset: 0x0001F870
		internal static Mailbox OpenMailbox(Context context, MailboxState mailboxState)
		{
			if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceDebug<Guid, SecurityIdentifier>(0L, "Mailbox:OpenMailbox(Mailbox:{0},User:{1})", mailboxState.MailboxGuid, context.SecurityContext.UserSid);
			}
			Mailbox result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Mailbox mailbox = disposeGuard.Add<Mailbox>(Mailbox.openMailboxDelegate(context.Database, mailboxState, context));
				if (!mailbox.IsDead)
				{
					mailbox.ChangeNumberAndIdCounters.UpdateMailboxGlobalIDs(context, mailbox, context.Database, true);
					result = mailbox;
					disposeGuard.Success();
				}
			}
			return result;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00021718 File Offset: 0x0001F918
		internal static Mailbox CreateMailbox(Context context, MailboxState mailboxState, MailboxInfo mailboxDirectoryInfo, Guid mailboxInstanceGuid, Guid localIdGuid, Guid mappingSignatureGuid, ulong nextIdCounter, uint? reservedIdCounterRange, ulong nextCnCounter, uint? reservedCnCounterRange, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap, Dictionary<ushort, Guid> replidGuidMap, Guid defaultFoldersReplGuid, bool createdByMove)
		{
			if ((numberToNameMap != null && replidGuidMap == null) || (numberToNameMap == null && replidGuidMap != null))
			{
				throw new CorruptDataException((LID)43856U, "Invalid correlation between preserving mailbox signature specific argument values.");
			}
			bool flag = numberToNameMap != null && replidGuidMap != null;
			if ((flag || Mailbox.GetFirstAvailableIdGlobcount(mailboxDirectoryInfo) != nextIdCounter || reservedIdCounterRange != null || 1UL != nextCnCounter || reservedCnCounterRange != null) && (!flag || nextIdCounter < 1UL || reservedIdCounterRange == null || nextCnCounter < 1UL || reservedCnCounterRange == null))
			{
				throw new CorruptDataException((LID)52296U, "Invalid correlation between preserving mailbox signature specific argument values.");
			}
			if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceDebug<Guid>(0L, "Mailbox:Create(Guid={0})", mailboxDirectoryInfo.MailboxGuid);
			}
			return Mailbox.createMailboxDelegate(context.Database, context, mailboxState, mailboxDirectoryInfo, mailboxInstanceGuid, localIdGuid, mappingSignatureGuid, nextIdCounter, reservedIdCounterRange, nextCnCounter, reservedCnCounterRange, numberToNameMap, replidGuidMap, defaultFoldersReplGuid, createdByMove);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00021804 File Offset: 0x0001FA04
		internal static Mailbox CreateMailbox(Context context, MailboxState mailboxState, MailboxInfo mailboxDirectoryInfo, Guid mailboxInstanceGuid, Guid localIdGuid)
		{
			return Mailbox.CreateMailbox(context, mailboxState, mailboxDirectoryInfo, mailboxInstanceGuid, localIdGuid, Guid.NewGuid(), Mailbox.GetFirstAvailableIdGlobcount(mailboxDirectoryInfo), null, 1UL, null, null, null, localIdGuid, false);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00021840 File Offset: 0x0001FA40
		internal static void RegisterOnPostDisposeAction(Action<Context, StoreDatabase> action)
		{
			Mailbox.onPostDisposeActions.Add(action);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002184D File Offset: 0x0001FA4D
		internal static void RegisterOnDisconnectAction(Action<Context, Mailbox> action)
		{
			Mailbox.onDisconnectActions.Add(action);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002185C File Offset: 0x0001FA5C
		internal static object PostProcessMailboxPropValue(object value, StorePropTag propTag)
		{
			switch (propTag.PropId)
			{
			case 26283:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.MailboxMessagesPerFolderCountWarningQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26284:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.MailboxMessagesPerFolderCountReceiveQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26285:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.DumpsterMessagesPerFolderCountWarningQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26286:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.DumpsterMessagesPerFolderCountReceiveQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26287:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FolderHierarchyChildrenCountWarningQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26288:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FolderHierarchyChildrenCountReceiveQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26289:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FolderHierarchyDepthWarningQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26290:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FolderHierarchyDepthReceiveQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26293:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FoldersCountWarningQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26294:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = ConfigurationSchema.FoldersCountReceiveQuota.Value;
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			case 26295:
				if (value == null)
				{
					UnlimitedItems unlimitedItems = new UnlimitedItems((long)((ulong)ConfigurationSchema.MAPINamedPropsQuota.Value));
					value = (unlimitedItems.IsUnlimited ? null : new int?((int)unlimitedItems.Value));
				}
				break;
			}
			return value;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00021B4C File Offset: 0x0001FD4C
		public int SerializeMailboxShape(byte[] buffer, int offset)
		{
			List<uint> list = new List<uint>();
			List<object> list2 = new List<object>();
			int? num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FolderHierarchyDepthWarningQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FolderHierarchyDepthWarningQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FolderHierarchyDepthReceiveQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FolderHierarchyDepthReceiveQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FoldersCountWarningQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FoldersCountWarningQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.FoldersCountReceiveQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.FoldersCountReceiveQuota.PropTag);
				list2.Add(num.Value);
			}
			num = (int?)this.GetPropertyValue(this.context, PropTag.Mailbox.NamedPropertiesCountQuota);
			if (num != null)
			{
				list.Add(PropTag.Mailbox.NamedPropertiesCountQuota.PropTag);
				list2.Add(num.Value);
			}
			if (list.Count == 0)
			{
				return 0;
			}
			byte[] array = PropertyBlob.BuildBlob(list, list2);
			if (buffer != null)
			{
				Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			}
			return array.Length;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00021E8C File Offset: 0x0002008C
		public ErrorCode SetMailboxShape(Context context, PropertyBlob.BlobReader mailboxShapePropertyBlobReader)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			for (int i = 0; i < mailboxShapePropertyBlobReader.PropertyCount; i++)
			{
				if (!mailboxShapePropertyBlobReader.IsPropertyValueNull(i) && !mailboxShapePropertyBlobReader.IsPropertyValueReference(i))
				{
					StorePropTag propTag = this.MapPropTag(context, mailboxShapePropertyBlobReader.GetPropertyTag(i));
					if (propTag.PropType != PropertyType.Error && propTag.PropType != PropertyType.Invalid)
					{
						object propertyValue = mailboxShapePropertyBlobReader.GetPropertyValue(i);
						errorCode = this.SetProperty(context, propTag, propertyValue);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)44636U);
						}
					}
				}
			}
			return errorCode;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00021F24 File Offset: 0x00020124
		internal static void ValidateMailboxTypeVersion(Context context, MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, Mailbox.MailboxTypeVersion mailboxTypeVersion)
		{
			if (mailboxTypeVersion != null && (mailboxTypeVersion.MailboxType != mailboxType || mailboxTypeVersion.MailboxTypeDetail != mailboxTypeDetail))
			{
				DiagnosticContext.TraceDword((LID)51804U, (uint)mailboxTypeVersion.MailboxType);
				DiagnosticContext.TraceDword((LID)55900U, (uint)mailboxTypeVersion.MailboxTypeDetail);
				DiagnosticContext.TraceDword((LID)43612U, (uint)mailboxType);
				DiagnosticContext.TraceDword((LID)39516U, (uint)mailboxTypeDetail);
				throw new StoreException((LID)35676U, ErrorCodeValue.CorruptData, "MailboxTypeVersion types don't match our current mailbox types");
			}
			uint num;
			bool flag;
			if (Mailbox.GetMailboxTypeVersion(context, mailboxType, mailboxTypeDetail, out num, out flag))
			{
				if (!flag)
				{
					DiagnosticContext.TraceDword((LID)57948U, (uint)mailboxType);
					DiagnosticContext.TraceDword((LID)38460U, (uint)mailboxTypeDetail);
					throw new StoreException((LID)48604U, ErrorCodeValue.NotSupported, "Current mailbox type is versioned, but we don't allow it on this server");
				}
				if (mailboxTypeVersion == null)
				{
					DiagnosticContext.TraceDword((LID)62044U, (uint)mailboxType);
					DiagnosticContext.TraceDword((LID)41564U, (uint)mailboxTypeDetail);
					DiagnosticContext.TraceDword((LID)33372U, num);
					throw new StoreException((LID)62300U, ErrorCodeValue.NotSupported, "Current mailbox type is versioned. Cannot accept unversioned data");
				}
				if (mailboxTypeVersion.Version > num)
				{
					DiagnosticContext.TraceDword((LID)35420U, (uint)mailboxTypeVersion.MailboxType);
					DiagnosticContext.TraceDword((LID)41820U, (uint)mailboxTypeVersion.MailboxTypeDetail);
					DiagnosticContext.TraceDword((LID)59996U, mailboxTypeVersion.Version);
					DiagnosticContext.TraceDword((LID)64092U, num);
					throw new StoreException((LID)52060U, ErrorCodeValue.NotSupported, "RequestedVersion is higher than supported");
				}
			}
			else if (mailboxTypeVersion != null)
			{
				DiagnosticContext.TraceDword((LID)49756U, (uint)mailboxTypeVersion.MailboxType);
				DiagnosticContext.TraceDword((LID)53852U, (uint)mailboxTypeVersion.MailboxTypeDetail);
				DiagnosticContext.TraceDword((LID)37468U, mailboxTypeVersion.Version);
				throw new StoreException((LID)37724U, ErrorCodeValue.NotSupported, "Current mailbox type is unversioned. Cannot accept versioned data");
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00022116 File Offset: 0x00020316
		internal static bool GetMailboxTypeVersion(Context context, MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, out uint version, out bool mailboxTypeIsAllowed)
		{
			if (mailboxType == MailboxInfo.MailboxType.Private && mailboxTypeDetail == MailboxInfo.MailboxTypeDetail.GroupMailbox)
			{
				if (AddGroupMailboxType.IsReady(context, context.Database))
				{
					mailboxTypeIsAllowed = true;
					version = 1U;
				}
				else
				{
					mailboxTypeIsAllowed = false;
					version = 0U;
				}
				return true;
			}
			mailboxTypeIsAllowed = true;
			version = 0U;
			return false;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00022148 File Offset: 0x00020348
		public void CheckMailboxVersionAndUpgrade(Context context)
		{
			ComponentVersion currentSchemaVersion = this.database.GetCurrentSchemaVersion(context);
			SchemaUpgradeService.Upgrade(context, this, SchemaUpgradeService.SchemaCategory.Mailbox, currentSchemaVersion);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002216C File Offset: 0x0002036C
		public ComponentVersion GetCurrentSchemaVersion(Context context)
		{
			ComponentVersion result = new ComponentVersion((int)base.GetColumnValue(context, this.mailboxTable.MailboxDatabaseVersion));
			return result;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00022198 File Offset: 0x00020398
		public void SetCurrentSchemaVersion(Context context, ComponentVersion version)
		{
			base.SetColumn(context, this.mailboxTable.MailboxDatabaseVersion, version.Value);
			this.Save(context);
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000221C0 File Offset: 0x000203C0
		public string Identifier
		{
			get
			{
				return this.MailboxGuid.ToString();
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000221E1 File Offset: 0x000203E1
		private static ExchangeId MailboxPropertyBagCacheKey(int mailboxNumber)
		{
			return ExchangeId.Create(new Guid("{2a41116e-36de-4d31-8554-e2ee843b250a}"), (ulong)((long)mailboxNumber), 0);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002225C File Offset: 0x0002045C
		private void DeleteMailbox(Context context, MailboxStatus newMailboxStatus)
		{
			if (!base.IsDead)
			{
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxTracer.TraceDebug(0L, "Mailbox.DeleteMailbox(" + this.MailboxNumber + ")");
				}
				this.IsValid();
				if (MailboxStatus.Tombstone != newMailboxStatus && MailboxStatus.HardDeleted != newMailboxStatus && MailboxStatus.Disabled != newMailboxStatus && MailboxStatus.SoftDeleted != newMailboxStatus)
				{
					throw new InvalidOperationException("Invalid mailbox delete operation.");
				}
				this.RemoveCrossMailboxReferences(context);
				this.SetStatus(context, newMailboxStatus);
				this.UpdateTableSizeStatistics(context);
				base.Flush(context);
				MailboxStatus capturedNewMailboxStatusForDelete = newMailboxStatus;
				MailboxState capturedMailboxState = this.SharedState;
				context.RegisterStateAction(delegate(Context ctx)
				{
					MailboxState mailboxState = MailboxStateCache.ResetMailboxState(ctx, capturedMailboxState, capturedNewMailboxStatusForDelete);
					if (capturedNewMailboxStatusForDelete == MailboxStatus.HardDeleted && Mailbox.cleanupHardDeletedMailboxesMaintenance != null)
					{
						mailboxState.AddReference();
						try
						{
							Mailbox.cleanupHardDeletedMailboxesMaintenance.MarkForMaintenance(ctx, mailboxState);
						}
						finally
						{
							mailboxState.ReleaseReference();
						}
					}
				}, null);
				base.MarkAsDeleted(context);
			}
			this.DisposeOwnedPropertyBags();
			this.SharedState.CleanupAsNonActive(context, false);
			this.deleted = true;
		}

		// Token: 0x0600064D RID: 1613
		protected abstract void RemoveCrossMailboxReferences(Context context);

		// Token: 0x0600064E RID: 1614 RVA: 0x00022334 File Offset: 0x00020534
		public void Disconnect()
		{
			if (!base.IsDead)
			{
				this.IsValid();
				foreach (SharedObjectPropertyBag sharedObjectPropertyBag in this.activePropertyBags)
				{
					sharedObjectPropertyBag.OnMailboxDisconnect();
				}
				this.activePropertyBags.Clear();
				foreach (Action<Context, Mailbox> action in Mailbox.onDisconnectActions)
				{
					action(this.context, this);
				}
			}
			this.context.UnregisterMailboxContext(this);
			this.context = null;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000223FC File Offset: 0x000205FC
		public void Reconnect(Context context)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.IsDirty, "Somebody left mailbox dirty");
			this.context = context;
			this.context.RegisterMailboxContext(this);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00022424 File Offset: 0x00020624
		public SharedObjectPropertyBag GetOpenedPropertyBag(Context context, ExchangeId id)
		{
			SharedObjectPropertyBag sharedObjectPropertyBag = null;
			if (this.openedPropertyBags.TryGetValue(id, out sharedObjectPropertyBag) && sharedObjectPropertyBag != null && !sharedObjectPropertyBag.CheckAlive(context))
			{
				this.openedPropertyBags.Remove(id);
				if (sharedObjectPropertyBag != this && sharedObjectPropertyBag != null)
				{
					((IDisposable)sharedObjectPropertyBag).Dispose();
				}
				sharedObjectPropertyBag = null;
			}
			return sharedObjectPropertyBag;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002246C File Offset: 0x0002066C
		public void AddPropertyBag(ExchangeId id, SharedObjectPropertyBag propertyBag)
		{
			this.openedPropertyBags.Add(id, propertyBag);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002247B File Offset: 0x0002067B
		public void RemovePropertyBag(ExchangeId id)
		{
			this.openedPropertyBags.Remove(id);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002248A File Offset: 0x0002068A
		public void OnPropertyBagActive(SharedObjectPropertyBag propertyBag)
		{
			this.activePropertyBags.Add(propertyBag);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00022498 File Offset: 0x00020698
		[Conditional("DEBUG")]
		public void AssertPropertyBagIsActive(SharedObjectPropertyBag propertyBag)
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0002249C File Offset: 0x0002069C
		public override object GetPropertyValue(Context context, StorePropTag propTag)
		{
			uint propTag2 = propTag.PropTag;
			if (propTag2 == 1746534411U)
			{
				return this.SharedState.Quarantined;
			}
			return base.GetPropertyValue(context, propTag);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000224D4 File Offset: 0x000206D4
		private IdSet GetIdSet(Context context, StorePropTag propTag)
		{
			byte[] array = (byte[])this.GetPropertyValue(context, propTag);
			if (array != null)
			{
				return IdSet.Parse(context, array);
			}
			return new IdSet();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000224FF File Offset: 0x000206FF
		public IdSet GetFolderCnsetIn(Context context)
		{
			return this.GetIdSet(context, PropTag.Mailbox.CnsetIn);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00022510 File Offset: 0x00020710
		public void SetFolderCnsetIn(Context context, IdSet cnSet)
		{
			cnSet.IdealPack();
			byte[] value = cnSet.Serialize();
			this.SetProperty(context, PropTag.Mailbox.CnsetIn, value);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00022538 File Offset: 0x00020738
		public IdSet GetFolderIdsetIn(Context context)
		{
			if (RemoveFolderIdsetIn.IsReady(context, context.Database))
			{
				return new IdSet();
			}
			return this.GetIdSet(context, PropTag.Mailbox.FolderIdsetIn);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0002255C File Offset: 0x0002075C
		public void SetFolderIdsetIn(Context context, IdSet idSet)
		{
			if (RemoveFolderIdsetIn.IsReady(context, context.Database))
			{
				this.SetProperty(context, PropTag.Mailbox.FolderIdsetIn, null);
				return;
			}
			byte[] value = idSet.Serialize();
			this.SetProperty(context, PropTag.Mailbox.FolderIdsetIn, value);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002259C File Offset: 0x0002079C
		public void SetDirectoryPersonalInfoOnMailbox(Context context, MailboxInfo mailboxDirectoryInfo)
		{
			base.SetColumn(context, this.mailboxTable.OwnerADGuid, mailboxDirectoryInfo.OwnerGuid);
			ErrorCode errorCode = this.SetProperty(context, PropTag.Mailbox.MailboxOwnerDN, mailboxDirectoryInfo.OwnerLegacyDN);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)60544U, errorCode, "Failed to set MailboxOwnerDN");
			}
			errorCode = this.SetProperty(context, PropTag.Mailbox.MailboxOwnerName, mailboxDirectoryInfo.OwnerDisplayName);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)35968U, errorCode, "Failed to set MailboxOwnerName");
			}
			errorCode = this.SetProperty(context, PropTag.Mailbox.DisplayName, mailboxDirectoryInfo.OwnerDisplayName);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)62592U, errorCode, "Failed to set DisplayName");
			}
			errorCode = this.SetProperty(context, PropTag.Mailbox.SimpleDisplayName, mailboxDirectoryInfo.SimpleDisplayName);
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)38016U, errorCode, "Failed to set SimpleDisplayName");
			}
			this.SetProperty(context, PropTag.Mailbox.MailboxLastUpdated, this.sharedState.UtcNow);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000226CF File Offset: 0x000208CF
		public void MakeUserAccessible(Context context)
		{
			if (this.SharedState.IsUserAccessible)
			{
				return;
			}
			this.SetStatus(context, MailboxStatus.UserAccessible);
			this.SharedState.SetUserAccessible();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000226F2 File Offset: 0x000208F2
		public void MakeTombstone(Context context)
		{
			this.DeleteMailbox(context, MailboxStatus.Tombstone);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000226FC File Offset: 0x000208FC
		public void MakeRoomForNewMailbox(Context context)
		{
			base.SetColumn(context, this.mailboxTable.MailboxGuid, null);
			base.Flush(context);
			if (!this.sharedState.IsRemoved)
			{
				this.HardDelete(context);
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0002272C File Offset: 0x0002092C
		public void RemoveMailboxEntriesFromTable(Context context, Table table)
		{
			StartStopKey empty;
			if (table.PrimaryKeyIndex.Columns[0].Name == this.mailboxTable.MailboxNumber.Name)
			{
				empty = new StartStopKey(true, new object[]
				{
					this.MailboxNumber
				});
			}
			else if (table.PrimaryKeyIndex.Columns[0].Name == this.mailboxIdentityTable.MailboxPartitionNumber.Name)
			{
				empty = new StartStopKey(true, new object[]
				{
					this.MailboxPartitionNumber
				});
			}
			else
			{
				empty = StartStopKey.Empty;
			}
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, table, table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(empty, empty), false, false), false))
			{
				deleteOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0002282C File Offset: 0x00020A2C
		public void Save(Context context)
		{
			if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceDebug<int>(0L, "Mailbox:Save({0})", this.MailboxNumber);
			}
			this.IsValid();
			int count = this.activePropertyBags.Count;
			for (int i = 0; i < count; i++)
			{
				ObjectPropertyBag objectPropertyBag = this.activePropertyBags[i];
				if (objectPropertyBag != this && (objectPropertyBag.IsDirty || objectPropertyBag.NeedsToPublishNotification))
				{
					objectPropertyBag.AutoSave(context);
				}
			}
			base.Flush(context);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000228AA File Offset: 0x00020AAA
		public override void Delete(Context context)
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000228AC File Offset: 0x00020AAC
		public void Disable(Context context)
		{
			this.DeleteMailbox(context, MailboxStatus.Disabled);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000228B6 File Offset: 0x00020AB6
		public void SoftDelete(Context context)
		{
			this.DeleteMailbox(context, MailboxStatus.SoftDeleted);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000228C0 File Offset: 0x00020AC0
		public void HardDelete(Context context)
		{
			if (this.SharedState.IsSoftDeleted)
			{
				PropertyBagHelpers.AdjustPropertyFlags(context, this, PropTag.Mailbox.MailboxFlags, 256, 0);
			}
			this.DeleteMailbox(context, MailboxStatus.HardDeleted);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000228EC File Offset: 0x00020AEC
		public ExchangeId GetNextChangeNumber(Context context)
		{
			this.IsValid();
			ulong itemNbr = this.AllocateChangeNumberCounter(context);
			return ExchangeId.Create(context, this.ReplidGuidMap, this.GetLocalIdGuid(context), itemNbr);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002291C File Offset: 0x00020B1C
		public ExchangeId GetLastChangeNumber(Context context)
		{
			this.IsValid();
			ulong lastChangeNumber = this.ChangeNumberAndIdCounters.GetLastChangeNumber(context, this);
			return ExchangeId.Create(context, this.ReplidGuidMap, this.GetLocalIdGuid(context), lastChangeNumber - 1UL);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00022954 File Offset: 0x00020B54
		public ulong GetNextIdCounterAndReserveRange(Context context, uint reservedRange)
		{
			this.IsValid();
			return this.ChangeNumberAndIdCounters.GetNextIdCounterAndReserveRange(context, this, reservedRange);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002296A File Offset: 0x00020B6A
		public ulong GetNextCnCounterAndReserveRange(Context context, uint reservedRange)
		{
			this.IsValid();
			return this.ChangeNumberAndIdCounters.GetNextCnCounterAndReserveRange(context, this, reservedRange);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00022980 File Offset: 0x00020B80
		public ExchangeId GetNextObjectId(Context context)
		{
			this.IsValid();
			ulong itemNbr = this.AllocateObjectIdCounter(context);
			return ExchangeId.Create(context, this.ReplidGuidMap, this.GetLocalIdGuid(context), itemNbr);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000229B0 File Offset: 0x00020BB0
		public ExchangeId GetNextFolderId(Context context)
		{
			this.IsValid();
			ulong itemNbr = this.AllocateFolderIdCounter(context);
			return ExchangeId.Create(context, this.ReplidGuidMap, this.GetLocalIdGuid(context), itemNbr);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000229DF File Offset: 0x00020BDF
		public ExchangeId GetLocalRepids(Context context, uint countOfIds)
		{
			return this.GetLocalRepids(context, countOfIds, true);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000229EC File Offset: 0x00020BEC
		public ExchangeId GetLocalRepids(Context context, uint countOfIds, bool separateTransaction)
		{
			this.IsValid();
			ulong nextIdCounterAndReserveRange = this.ChangeNumberAndIdCounters.GetNextIdCounterAndReserveRange(context, this, countOfIds, separateTransaction);
			return ExchangeId.Create(context, this.ReplidGuidMap, this.GetLocalIdGuid(context), nextIdCounterAndReserveRange);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00022A23 File Offset: 0x00020C23
		public int GetNextMessageDocumentId(Context context)
		{
			return this.ReserveMessageDocumentIdRange(context, 1);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00022A30 File Offset: 0x00020C30
		public int ReserveMessageDocumentIdRange(Context context, int count)
		{
			this.IsValid();
			int num;
			if (this.sharedState.UnifiedState == null)
			{
				num = (int)base.GetColumnValue(context, this.mailboxTable.NextMessageDocumentId);
				if (num == 2147483647)
				{
					throw new StoreException((LID)42581U, ErrorCodeValue.CallFailed, "DocumentId overflow");
				}
				base.SetColumn(context, this.mailboxTable.NextMessageDocumentId, num + count);
			}
			else
			{
				bool flag = false;
				context.PushConnection();
				try
				{
					using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, this.mailboxIdentityTable.Table, true, new ColumnValue[]
					{
						new ColumnValue(this.mailboxIdentityTable.MailboxPartitionNumber, this.sharedState.MailboxPartitionNumber)
					}))
					{
						num = (int)dataRow.GetValue(context, this.mailboxIdentityTable.NextMessageDocumentId);
						if (num == 2147483647)
						{
							throw new StoreException((LID)59100U, ErrorCodeValue.CallFailed, "DocumentId overflow");
						}
						dataRow.SetValue(context, this.mailboxIdentityTable.NextMessageDocumentId, num + count);
						dataRow.Flush(context);
					}
					context.Commit();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						context.Abort();
					}
					context.PopConnection();
				}
			}
			return num;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00022B9C File Offset: 0x00020D9C
		public void IsValid()
		{
			if (!this.valid || this.deleted)
			{
				throw new InvalidOperationException("This mailbox is invalid");
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00022BB9 File Offset: 0x00020DB9
		public Guid GetMappingSignatureGuid(Context context)
		{
			return (Guid)base.GetColumnValue(context, this.mailboxTable.MappingSignatureGuid);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00022BD2 File Offset: 0x00020DD2
		public Guid GetLocalIdGuid(Context context)
		{
			return this.ChangeNumberAndIdCounters.GetLocalIdGuid(context, this);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00022BE1 File Offset: 0x00020DE1
		public int GetLCID(Context context)
		{
			return (int)base.GetColumnValue(context, this.mailboxTable.Lcid);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00022BFA File Offset: 0x00020DFA
		public void SetLCID(Context context, int lcid)
		{
			base.SetColumn(context, this.mailboxTable.Lcid, lcid);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00022C14 File Offset: 0x00020E14
		public void SetComment(Context context, string value)
		{
			base.SetColumn(context, this.mailboxTable.Comment, value);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00022C29 File Offset: 0x00020E29
		public string GetComment(Context context)
		{
			return (string)base.GetColumnValue(context, this.mailboxTable.Comment);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00022C42 File Offset: 0x00020E42
		public string GetDisplayName(Context context)
		{
			return (string)base.GetColumnValue(context, this.mailboxTable.DisplayName);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00022C5B File Offset: 0x00020E5B
		public long GetMessageCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.MessageCount);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00022C74 File Offset: 0x00020E74
		public long GetHiddenMessageCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.HiddenMessageCount);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00022C8D File Offset: 0x00020E8D
		public long GetDeletedMessageCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.MessageDeletedCount);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00022CA6 File Offset: 0x00020EA6
		public long GetHiddenDeletedMessageCount(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.HiddenMessageDeletedCount);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022CBF File Offset: 0x00020EBF
		public string GetSimpleDisplayName(Context context)
		{
			return (string)base.GetColumnValue(context, this.mailboxTable.SimpleDisplayName);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00022CD8 File Offset: 0x00020ED8
		public bool GetOofState(Context context)
		{
			return (bool)base.GetColumnValue(context, this.mailboxTable.OofState);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022CF1 File Offset: 0x00020EF1
		public void SetOofState(Context context, bool value)
		{
			base.SetColumn(context, this.mailboxTable.OofState, value);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00022D0B File Offset: 0x00020F0B
		public DateTime GetLastQuotaCheckTime(Context context)
		{
			return this.SharedState.LastQuotaCheckTime;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022D18 File Offset: 0x00020F18
		public void SetLastQuotaCheckTime(Context context, DateTime value)
		{
			base.SetColumn(context, this.mailboxTable.LastQuotaNotificationTime, value);
			this.SharedState.LastQuotaCheckTime = value;
			if (!AddLastMaintenanceTimeToMailbox.IsReady(context, this.database))
			{
				this.SharedState.LastMailboxMaintenanceTime = value;
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00022D58 File Offset: 0x00020F58
		public DateTime GetLastMailboxMaintenanceTime(Context context)
		{
			return this.SharedState.LastMailboxMaintenanceTime;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00022D65 File Offset: 0x00020F65
		public void SetLastMailboxMaintenanceTime(Context context, DateTime value)
		{
			if (AddLastMaintenanceTimeToMailbox.IsReady(context, this.database))
			{
				base.SetColumn(context, this.mailboxTable.LastMailboxMaintenanceTime, value);
			}
			else
			{
				this.SetLastQuotaCheckTime(context, value);
			}
			this.SharedState.LastMailboxMaintenanceTime = value;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00022DA3 File Offset: 0x00020FA3
		public DateTime? GetISIntegScheduledLast(Context context)
		{
			return (DateTime?)this.GetPropertyValue(context, PropTag.Mailbox.ScheduledISIntegLastFinished);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00022DB8 File Offset: 0x00020FB8
		public void SetISIntegScheduledLast(Context context, DateTime valueCurrentTime, int? executionTimeMs, int? corruptionCount)
		{
			if (corruptionCount != null && corruptionCount.Value > 0)
			{
				int? num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.ScheduledISIntegCorruptionCount);
				DateTime? dateTime = (DateTime?)this.GetPropertyValue(context, PropTag.Mailbox.ScheduledISIntegLastFinished);
				if (dateTime != null && num != null && corruptionCount.Value > num.Value)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_IntegrityCheckScheduledNewCorruption, new object[]
					{
						this.Identifier,
						num.Value,
						corruptionCount.Value,
						dateTime.Value,
						context.Database.MdbName
					});
				}
			}
			this.SetProperty(context, PropTag.Mailbox.ScheduledISIntegLastFinished, valueCurrentTime);
			this.SetProperty(context, PropTag.Mailbox.ScheduledISIntegExecutionTime, executionTimeMs);
			this.SetProperty(context, PropTag.Mailbox.ScheduledISIntegCorruptionCount, corruptionCount);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00022EB4 File Offset: 0x000210B4
		public long GetMessageSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.MessageSize);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00022ECD File Offset: 0x000210CD
		public long GetHiddenMessageSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.HiddenMessageSize);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00022EE6 File Offset: 0x000210E6
		public long GetDeletedMessageSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.MessageDeletedSize);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00022EFF File Offset: 0x000210FF
		public long GetHiddenDeletedMessageSize(Context context)
		{
			return (long)base.GetColumnValue(context, this.mailboxTable.HiddenMessageDeletedSize);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00022F18 File Offset: 0x00021118
		public MailboxStatus GetStatus(Context context)
		{
			return (MailboxStatus)base.GetColumnValue(context, this.mailboxTable.Status);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00022F40 File Offset: 0x00021140
		public void SetStatus(Context context, MailboxStatus newMailboxStatus)
		{
			bool flag = MailboxStatus.SoftDeleted == newMailboxStatus || MailboxStatus.Disabled == newMailboxStatus || MailboxStatus.HardDeleted == newMailboxStatus || MailboxStatus.Tombstone == newMailboxStatus;
			if (flag)
			{
				base.SetColumn(context, this.mailboxTable.DeletedOn, this.sharedState.UtcNow);
			}
			else
			{
				base.SetColumn(context, this.mailboxTable.DeletedOn, null);
			}
			base.SetColumn(context, this.mailboxTable.Status, (short)newMailboxStatus);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00022FB4 File Offset: 0x000211B4
		public bool GetPreservingMailboxSignature(Context context)
		{
			return (bool)base.GetColumnValue(context, this.mailboxTable.PreservingMailboxSignature);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00022FDA File Offset: 0x000211DA
		public void SetPreservingMailboxSignature(Context context, bool newPreservingMailboxSignature)
		{
			base.SetColumn(context, this.mailboxTable.PreservingMailboxSignature, newPreservingMailboxSignature);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00022FF4 File Offset: 0x000211F4
		public bool GetMRSPreservingMailboxSignature(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Mailbox.MailboxFlags, 512, 512);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002300C File Offset: 0x0002120C
		public void SetMRSPreservingMailboxSignature(Context context, bool newMRSPreservingMailboxSignature)
		{
			PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Mailbox.MailboxFlags, newMRSPreservingMailboxSignature, 512);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00023026 File Offset: 0x00021226
		public bool GetCreatedByMove(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Mailbox.MailboxFlags, 16, 16);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00023038 File Offset: 0x00021238
		public DateTime GetCreationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Mailbox.CreationTime);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0002304B File Offset: 0x0002124B
		public DateTime? GetDeletedOn(Context context)
		{
			return (DateTime?)base.GetColumnValue(context, this.mailboxTable.DeletedOn);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00023064 File Offset: 0x00021264
		public bool GetConversationEnabled(Context context)
		{
			return (bool)base.GetColumnValue(context, this.mailboxTable.ConversationEnabled);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002307D File Offset: 0x0002127D
		public void SetConversationEnabled(Context context)
		{
			base.SetColumn(context, this.mailboxTable.ConversationEnabled, true);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002309C File Offset: 0x0002129C
		public HashSet<ushort> GetDefaultPromotedMessagePropertyIds(Context context)
		{
			this.IsValid();
			HashSet<ushort> hashSet = this.DefaultPromotedPropertyIds;
			if (hashSet == null)
			{
				short[] array = (short[])base.GetColumnValue(context, this.mailboxTable.DefaultPromotedMessagePropertyIds);
				if (array != null && array.Length != 0)
				{
					hashSet = new HashSet<ushort>(from id in array
					select (ushort)id);
				}
				else
				{
					hashSet = new HashSet<ushort>();
				}
				this.DefaultPromotedPropertyIds = hashSet;
			}
			return hashSet;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00023118 File Offset: 0x00021318
		public HashSet<ushort> GetAlwaysPromotedMessagePropertyIds(Context context)
		{
			this.IsValid();
			HashSet<ushort> hashSet = this.AlwaysPromotedPropertyIds;
			if (hashSet == null)
			{
				short[] array = (short[])base.GetColumnValue(context, this.mailboxTable.AlwaysPromotedMessagePropertyIds);
				if (array != null && array.Length != 0)
				{
					hashSet = new HashSet<ushort>(from id in array
					select (ushort)id);
					this.AlwaysPromotedPropertyIds = hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002318C File Offset: 0x0002138C
		public HashSet<ushort> GetStoreDefaultPromotedMessagePropertyIds(Context context)
		{
			this.IsValid();
			if (this.StoreDefaultPromotedPropertyIds == null)
			{
				this.StoreDefaultPromotedPropertyIds = new HashSet<ushort>(from x in PropertyPromotionHelper.BuildDefaultPromotedPropertyIds(context, this)
				select (ushort)x);
			}
			return this.StoreDefaultPromotedPropertyIds;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000231E1 File Offset: 0x000213E1
		public void ResetSharedCache()
		{
			this.StoreDefaultPromotedPropertyIds = null;
			this.AlwaysPromotedPropertyIds = null;
			this.DefaultPromotedPropertyIds = null;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000231F8 File Offset: 0x000213F8
		internal static INotificationSubscriptionList GetMailboxSubscriptions(Context context, int mailboxNumber)
		{
			return MailboxStateCache.Get(context, mailboxNumber);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00023204 File Offset: 0x00021404
		public static StorePropTag GetStorePropTag(Context context, Mailbox mailbox, uint propTag, ObjectType objectType)
		{
			if (propTag < 2147483648U)
			{
				return WellKnownProperties.GetPropTag(propTag, objectType);
			}
			ushort propId = (ushort)((propTag & 4294901760U) >> 16);
			PropertyType propertyType = (PropertyType)(propTag & 65535U);
			PropertyType propType = PropertyTypeHelper.MapToInternalPropertyType(propertyType);
			StorePropInfo storePropInfo = null;
			if (mailbox != null)
			{
				storePropInfo = mailbox.NamedPropertyMap.GetNameFromNumber(context, propId);
			}
			if (storePropInfo == null)
			{
				return StorePropTag.CreateWithoutInfo(propId, propType, propertyType, WellKnownProperties.BaseObjectType[(int)objectType]);
			}
			return new StorePropTag(propId, propType, storePropInfo, propertyType, WellKnownProperties.BaseObjectType[(int)objectType]);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00023274 File Offset: 0x00021474
		public static void MakeRoomForNewPartition(Context context, Guid existingUnifiedMailboxGuid, Guid newUnifiedMailboxGuid)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				existingUnifiedMailboxGuid
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, mailboxTable.UnifiedMailboxGuidIndex, new Column[]
			{
				mailboxTable.MailboxNumber
			}, Factory.CreateSearchCriteriaTrue(), null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						int @int = reader.GetInt32(mailboxTable.MailboxNumber);
						MailboxState mailboxState = MailboxStateCache.Get(context, @int);
						if (!mailboxState.IsRemoved)
						{
							IMailboxContext mailboxContext = context.GetMailboxContext(@int);
							if (!mailboxState.IsSoftDeleted && !mailboxContext.GetCreatedByMove(context))
							{
								DiagnosticContext.TraceDword((LID)47356U, (uint)mailboxState.Status);
								throw new StoreException((LID)42236U, ErrorCodeValue.UnexpectedMailboxState);
							}
							((Mailbox)mailboxContext).MakeRoomForNewMailbox(context);
						}
					}
				}
				using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(context.Culture, context, tableOperator, new Column[]
				{
					mailboxTable.UnifiedMailboxGuid
				}, new object[]
				{
					newUnifiedMailboxGuid
				}, true))
				{
					updateOperator.ExecuteScalar();
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00023400 File Offset: 0x00021600
		internal static void InitializeHardDeletedMailboxMaintenance(Guid maintenaceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate, string maintenanceTaskName)
		{
			Mailbox.cleanupHardDeletedMailboxesMaintenance = MaintenanceHandler.RegisterMailboxMaintenance(maintenaceId, requiredMaintenanceResourceType, false, mailboxMaintenanceDelegate, maintenanceTaskName, true);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00023412 File Offset: 0x00021612
		internal static void InitializeSynchronizeWithDSMailboxMaintenance(Guid maintenaceId, RequiredMaintenanceResourceType requiredMaintenanceResourceType, MaintenanceHandler.MailboxMaintenanceDelegate mailboxMaintenanceDelegate, string maintenanceTaskName)
		{
			Mailbox.synchronizeWithDSMailboxMaintenance = MaintenanceHandler.RegisterMailboxMaintenance(maintenaceId, requiredMaintenanceResourceType, false, mailboxMaintenanceDelegate, maintenanceTaskName, false);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00023424 File Offset: 0x00021624
		internal static byte[] CreateMailboxSecurityDescriptorBlob(SecurityDescriptor databaseOrServerADSecurityDescriptor, SecurityDescriptor mailboxADSecurityDescriptor)
		{
			SecurityDescriptor securityDescriptor = MailboxSecurity.CreateMailboxSecurityDescriptor(databaseOrServerADSecurityDescriptor, mailboxADSecurityDescriptor);
			if (securityDescriptor == null && ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxTracer.TraceDebug(0L, "MailboxSecurity.CreateMailboxSecurityDescriptor failed");
			}
			return securityDescriptor.BinaryForm;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00023460 File Offset: 0x00021660
		public StorePropTag GetStorePropTag(Context context, uint propTag, ObjectType objectType)
		{
			return Mailbox.GetStorePropTag(context, this, propTag, objectType);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0002346B File Offset: 0x0002166B
		public StorePropTag GetStorePropTag(Context context, ushort propId, PropertyType propType, ObjectType objectType)
		{
			return Mailbox.GetStorePropTag(context, this, (uint)((int)propId << 16 | (int)propType), objectType);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002347C File Offset: 0x0002167C
		public StorePropTag GetNamedPropStorePropTag(Context context, StorePropName propName, PropertyType propType, ObjectType objectType)
		{
			ushort propId;
			StoreNamedPropInfo propertyInfo;
			this.NamedPropertyMap.GetNumberFromName(context, propName, true, this.QuotaInfo, out propId, out propertyInfo);
			return new StorePropTag(propId, propType, propertyInfo, objectType);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000234AC File Offset: 0x000216AC
		public void UpdateMessagesAggregateCountAndSize(Context context, bool hidden, bool deleted, int countChange, long sizeChange)
		{
			PhysicalColumn column;
			PhysicalColumn column2;
			if (hidden)
			{
				column = (deleted ? this.mailboxTable.HiddenMessageDeletedCount : this.mailboxTable.HiddenMessageCount);
				column2 = (deleted ? this.mailboxTable.HiddenMessageDeletedSize : this.mailboxTable.HiddenMessageSize);
			}
			else
			{
				column = (deleted ? this.mailboxTable.MessageDeletedCount : this.mailboxTable.MessageCount);
				column2 = (deleted ? this.mailboxTable.MessageDeletedSize : this.mailboxTable.MessageSize);
			}
			this.UpdateAggregateColumn(context, column, (long)countChange);
			this.UpdateAggregateColumn(context, column2, sizeChange);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00023544 File Offset: 0x00021744
		internal int GetNextFolderInternetId(Context context)
		{
			int? num = (int?)this.GetPropertyValue(context, PropTag.Mailbox.HighestFolderInternetId);
			int num2 = (num != null) ? (num.Value + 1) : 1;
			this.SetProperty(context, PropTag.Mailbox.HighestFolderInternetId, num2);
			return num2;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002358D File Offset: 0x0002178D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Mailbox>(this);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00023598 File Offset: 0x00021798
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace) && this.sharedState != null)
				{
					ExTraceGlobals.MailboxTracer.TraceDebug<Guid>(0L, "Mailbox.Dispose(): {0}", this.MailboxGuid);
				}
				if (this.openedPropertyBags != null)
				{
					this.DisposeOwnedPropertyBags();
				}
				if (this.context != null)
				{
					this.context.UnregisterMailboxContext(this);
				}
				this.context = null;
				if (this.sharedState != null)
				{
					this.sharedState.ReleaseReference();
				}
			}
			else if (this.sharedState != null)
			{
				this.sharedState.DangerousReleaseReference();
			}
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				using (Context context = Context.CreateForSystem())
				{
					foreach (Action<Context, StoreDatabase> action in Mailbox.onPostDisposeActions)
					{
						action(context, this.database);
					}
				}
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00023698 File Offset: 0x00021898
		internal void ResetConversationEnabled(Context context)
		{
			base.SetColumn(context, this.mailboxTable.ConversationEnabled, false);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000236B4 File Offset: 0x000218B4
		private void UpdateAggregateColumn(Context context, PhysicalColumn column, long change)
		{
			if (change == 0L)
			{
				return;
			}
			long num = (long)base.GetColumnValue(context, column);
			long num2 = num + change;
			base.SetColumn(context, column, num2);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000236E7 File Offset: 0x000218E7
		private ulong AllocateObjectIdCounter(Context context)
		{
			return this.ChangeNumberAndIdCounters.AllocateObjectIdCounter(context, this);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000236F6 File Offset: 0x000218F6
		private ulong AllocateChangeNumberCounter(Context context)
		{
			return this.ChangeNumberAndIdCounters.AllocateChangeNumberCounter(context, this);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00023708 File Offset: 0x00021908
		private ulong AllocateFolderIdCounter(Context context)
		{
			GlobcntAllocationCache globcntAllocationCache = (GlobcntAllocationCache)this.sharedState.GetComponentData(Mailbox.folderIdAllocationCacheDataSlot);
			if (globcntAllocationCache == null || globcntAllocationCache.CountAvailable < 1U)
			{
				globcntAllocationCache = this.ReserveFolderIdRange(context, 1000U);
			}
			return globcntAllocationCache.Allocate(1U);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002374C File Offset: 0x0002194C
		private GlobcntAllocationCache ReserveFolderIdRange(Context context, uint reservedRangeSize)
		{
			ulong nextIdCounterAndReserveRange = this.GetNextIdCounterAndReserveRange(context, reservedRangeSize);
			ulong maxReserved = nextIdCounterAndReserveRange + (ulong)reservedRangeSize;
			GlobcntAllocationCache globcntAllocationCache = new GlobcntAllocationCache(nextIdCounterAndReserveRange, maxReserved);
			this.sharedState.SetComponentData(Mailbox.folderIdAllocationCacheDataSlot, globcntAllocationCache);
			return globcntAllocationCache;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00023781 File Offset: 0x00021981
		private void ThrowIfDeleted()
		{
			if (this.deleted)
			{
				throw new InvalidOperationException("This mailbox has been deleted");
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00023796 File Offset: 0x00021996
		protected override ObjectType GetObjectType()
		{
			return ObjectType.Mailbox;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00023799 File Offset: 0x00021999
		internal void GetGlobalCounters(Context context, out ulong idCounter, out ulong cnCounter)
		{
			this.ChangeNumberAndIdCounters.GetGlobalCounters(context, this, out idCounter, out cnCounter);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000237AA File Offset: 0x000219AA
		internal void SetGlobalCounters(Context context, ulong newIdCounter, ulong newCnCounter, Guid newLocalIdGuid)
		{
			this.ChangeNumberAndIdCounters.SetGlobalCounters(context, this, newIdCounter, newCnCounter, new Guid?(newLocalIdGuid));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000237C2 File Offset: 0x000219C2
		internal void SetGlobalCounters(Context context, ulong newIdCounter, ulong newCnCounter)
		{
			this.ChangeNumberAndIdCounters.SetGlobalCounters(context, this, newIdCounter, newCnCounter);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000237D4 File Offset: 0x000219D4
		public void GetReservedCounterRangesForDestinationMailbox(Context context, out ulong nextIdCounter, out uint reservedIdCounterRange, out ulong nextCnCounter, out uint reservedCnCounterRange)
		{
			checked
			{
				long num;
				try
				{
					num = (long)base.GetColumnValue(context, this.mailboxTable.MessageCount) + (long)base.GetColumnValue(context, this.mailboxTable.HiddenMessageCount);
				}
				catch (OverflowException ex)
				{
					context.OnExceptionCatch(ex);
					throw new CannotPreserveMailboxSignature((LID)48168U, "Too many messages in the mailbox.", ex);
				}
				try
				{
					reservedIdCounterRange = (uint)(unchecked((long)ConfigurationSchema.DestinationMailboxReservedCounterRangeConstant.Value) + unchecked((long)ConfigurationSchema.DestinationMailboxReservedCounterRangeGradient.Value) * num);
				}
				catch (OverflowException ex2)
				{
					context.OnExceptionCatch(ex2);
					throw new CannotPreserveMailboxSignature((LID)64552U, "Too many messages in the mailbox.", ex2);
				}
				reservedCnCounterRange = reservedIdCounterRange;
				nextIdCounter = this.GetNextIdCounterAndReserveRange(context, reservedIdCounterRange);
				nextCnCounter = this.GetNextCnCounterAndReserveRange(context, reservedCnCounterRange);
			}
			this.SetGlobalCounters(context, nextIdCounter + (ulong)reservedIdCounterRange, nextCnCounter + (ulong)reservedCnCounterRange);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000238BC File Offset: 0x00021ABC
		public void VerifyAndUpdateGlobalCounters(Context context, Guid localIdGuidSource, ulong newIdCounter, ulong newCnCounter)
		{
			if (!this.ReplidGuidMap.IsGuidInMap(context, localIdGuidSource))
			{
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.MailboxTracer.TraceDebug<string, int, Guid>(36552L, "Database {0} : Mailbox {1} : Local Id Guid source {2} is not registered in replid/GUID mapping.", this.Database.MdbName, this.MailboxNumber, localIdGuidSource);
				}
				throw new CorruptDataException((LID)42696U, "Local Id guid values of the source and destination mailboxes do not match.");
			}
			if (!localIdGuidSource.Equals(this.GetLocalIdGuid(context)))
			{
				if (ExTraceGlobals.MailboxTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailboxTracer.TraceDebug(36552L, "Database {0} : Mailbox {1} : Local Id Guid source {2} differs from local Id Guid destination {3}.", new object[]
					{
						this.Database.MdbName,
						this.MailboxNumber,
						localIdGuidSource,
						this.GetLocalIdGuid(context)
					});
				}
				this.SetGlobalCounters(context, newIdCounter, newCnCounter, localIdGuidSource);
				return;
			}
			this.SetGlobalCounters(context, newIdCounter, newCnCounter);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000239A8 File Offset: 0x00021BA8
		protected override void OnDirty(Context context)
		{
			if (this.sharedState != null)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.context != null, "Mailbox should be in Connected state");
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.sharedState.IsMailboxLockedExclusively(), "Making Mailbox dirty requires exclusive lock");
			}
			if (!context.IsStateObjectRegistered(this))
			{
				context.RegisterStateObject(this);
			}
			base.OnDirty(context);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000239FF File Offset: 0x00021BFF
		public void OnBeforeCommit(Context context)
		{
			bool isDisposed = base.IsDisposed;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00023A08 File Offset: 0x00021C08
		public void OnCommit(Context context)
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00023A0A File Offset: 0x00021C0A
		public void OnAbort(Context context)
		{
			if (!base.IsDisposed)
			{
				this.DiscardPrivateCache(context);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00023A1C File Offset: 0x00021C1C
		public void UpdateTableSizeStatistics(Context context)
		{
			object[] partitionValues = new object[]
			{
				this.MailboxPartitionNumber
			};
			Dictionary<StorePropTag, int> dictionary = new Dictionary<StorePropTag, int>(10);
			foreach (Mailbox.TableSizeStatistics tableSizeStatistics in Mailbox.tableSizeStatistics)
			{
				Table table = tableSizeStatistics.TableAccessor(context);
				int num;
				int num2;
				table.GetTableSize(context, partitionValues, out num, out num2);
				int num3;
				dictionary.TryGetValue(tableSizeStatistics.TotalPagesProperty, out num3);
				dictionary[tableSizeStatistics.TotalPagesProperty] = num3 + num;
				int num4;
				dictionary.TryGetValue(tableSizeStatistics.AvailablePagesProperty, out num4);
				dictionary[tableSizeStatistics.AvailablePagesProperty] = num4 + num2;
			}
			foreach (KeyValuePair<StorePropTag, int> keyValuePair in dictionary)
			{
				this.SetProperty(context, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00023B3C File Offset: 0x00021D3C
		private void DisposeOwnedPropertyBags()
		{
			SharedObjectPropertyBag[] array = this.openedPropertyBags.Values.ToArray<SharedObjectPropertyBag>();
			foreach (SharedObjectPropertyBag sharedObjectPropertyBag in array)
			{
				if (sharedObjectPropertyBag != null && sharedObjectPropertyBag != this)
				{
					sharedObjectPropertyBag.Dispose();
				}
			}
		}

		// Token: 0x040003FF RID: 1023
		internal const uint DefaultCnReserveChunk = 500U;

		// Token: 0x04000400 RID: 1024
		internal const uint CounterPatchMultiplier = 128U;

		// Token: 0x04000401 RID: 1025
		internal const ulong InitialIdGlobcountForNewPrivateMailbox = 256UL;

		// Token: 0x04000402 RID: 1026
		internal const uint CounterPatchingSafeDelta = 3840U;

		// Token: 0x04000403 RID: 1027
		internal const uint ReservedFidRangeSize = 1000U;

		// Token: 0x04000404 RID: 1028
		internal const uint ReservedFidRangeSizeForNewMailbox = 100U;

		// Token: 0x04000405 RID: 1029
		private static IMailboxMaintenance cleanupHardDeletedMailboxesMaintenance;

		// Token: 0x04000406 RID: 1030
		private static IMailboxMaintenance synchronizeWithDSMailboxMaintenance;

		// Token: 0x04000407 RID: 1031
		private static int defaultPromotedPropertyIdsDataSlot = -1;

		// Token: 0x04000408 RID: 1032
		private static int alwaysPromotedPropertyIdsDataSlot = -1;

		// Token: 0x04000409 RID: 1033
		private static int storeDefaultPromotedPropertyIdsDataSlot = -1;

		// Token: 0x0400040A RID: 1034
		private static int folderIdAllocationCacheDataSlot = -1;

		// Token: 0x0400040B RID: 1035
		private static List<Action<Context, StoreDatabase>> onPostDisposeActions = new List<Action<Context, StoreDatabase>>();

		// Token: 0x0400040C RID: 1036
		private static List<Action<Context, Mailbox>> onDisconnectActions = new List<Action<Context, Mailbox>>();

		// Token: 0x0400040D RID: 1037
		private static List<Mailbox.TableSizeStatistics> tableSizeStatistics = new List<Mailbox.TableSizeStatistics>();

		// Token: 0x0400040E RID: 1038
		private static Mailbox.OpenMailboxDelegate openMailboxDelegate;

		// Token: 0x0400040F RID: 1039
		private static Mailbox.CreateMailboxDelegate createMailboxDelegate;

		// Token: 0x04000410 RID: 1040
		private MailboxInfo mailboxInfo;

		// Token: 0x04000411 RID: 1041
		private Context context;

		// Token: 0x04000412 RID: 1042
		private bool deleted;

		// Token: 0x04000413 RID: 1043
		private bool valid;

		// Token: 0x04000414 RID: 1044
		private StoreDatabase database;

		// Token: 0x04000415 RID: 1045
		private MailboxState sharedState;

		// Token: 0x04000416 RID: 1046
		private MailboxTable mailboxTable;

		// Token: 0x04000417 RID: 1047
		private MailboxIdentityTable mailboxIdentityTable;

		// Token: 0x04000418 RID: 1048
		private ObjectPropertySchema propertySchema;

		// Token: 0x04000419 RID: 1049
		private QuotaInfo quotaInfo;

		// Token: 0x0400041A RID: 1050
		private QuotaStyle quotaStyle;

		// Token: 0x0400041B RID: 1051
		private UnlimitedBytes maxItemSize;

		// Token: 0x0400041C RID: 1052
		private Dictionary<ExchangeId, SharedObjectPropertyBag> openedPropertyBags = new Dictionary<ExchangeId, SharedObjectPropertyBag>(25);

		// Token: 0x0400041D RID: 1053
		private List<SharedObjectPropertyBag> activePropertyBags = new List<SharedObjectPropertyBag>(5);

		// Token: 0x020000A3 RID: 163
		// (Invoke) Token: 0x060006BC RID: 1724
		protected delegate Mailbox OpenMailboxDelegate(StoreDatabase database, MailboxState mailboxState, Context context);

		// Token: 0x020000A4 RID: 164
		// (Invoke) Token: 0x060006C0 RID: 1728
		protected delegate Mailbox CreateMailboxDelegate(StoreDatabase database, Context context, MailboxState mailboxState, MailboxInfo mailboxDirectoryInfo, Guid mailboxInstanceGuid, Guid localIdGuid, Guid mappingSignatureGuid, ulong nextIdCounter, uint? reservedIdCounterRange, ulong nextCnCounter, uint? reservedCnCounterRange, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap, Dictionary<ushort, Guid> replidGuidMap, Guid defaultFoldersReplGuid, bool createdByMove);

		// Token: 0x020000A5 RID: 165
		public struct TableSizeStatistics
		{
			// Token: 0x04000423 RID: 1059
			public Func<Context, Table> TableAccessor;

			// Token: 0x04000424 RID: 1060
			public StorePropTag TotalPagesProperty;

			// Token: 0x04000425 RID: 1061
			public StorePropTag AvailablePagesProperty;
		}

		// Token: 0x020000A6 RID: 166
		internal class MailboxTypeVersion
		{
			// Token: 0x060006C3 RID: 1731 RVA: 0x00023BB3 File Offset: 0x00021DB3
			internal MailboxTypeVersion(MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, uint version)
			{
				this.mailboxType = mailboxType;
				this.mailboxTypeDetail = mailboxTypeDetail;
				this.version = version;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00023BD0 File Offset: 0x00021DD0
			internal MailboxInfo.MailboxType MailboxType
			{
				get
				{
					return this.mailboxType;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00023BD8 File Offset: 0x00021DD8
			internal MailboxInfo.MailboxTypeDetail MailboxTypeDetail
			{
				get
				{
					return this.mailboxTypeDetail;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00023BE0 File Offset: 0x00021DE0
			internal uint Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x04000426 RID: 1062
			private readonly MailboxInfo.MailboxType mailboxType;

			// Token: 0x04000427 RID: 1063
			private readonly MailboxInfo.MailboxTypeDetail mailboxTypeDetail;

			// Token: 0x04000428 RID: 1064
			private readonly uint version;
		}
	}
}

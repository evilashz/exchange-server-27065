using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000083 RID: 131
	[DataContract]
	internal class PhysicalMailbox : IPhysicalMailbox, IExtensibleDataObject
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public PhysicalMailbox(DirectoryIdentity identity, ByteQuantifiedSize totalLogicalSize, ByteQuantifiedSize totalPhysicalSize, bool isQuarantined, MailboxMiscFlags mailboxFlags, StoreMailboxType mailboxType, ulong itemCount, DateTime? lastLogonTimestamp) : this(identity, totalLogicalSize, totalPhysicalSize, isQuarantined, mailboxType, itemCount, lastLogonTimestamp, null, false, mailboxFlags.HasFlag(MailboxMiscFlags.SoftDeletedMailbox) || mailboxFlags.HasFlag(MailboxMiscFlags.MRSSoftDeletedMailbox), mailboxFlags.HasFlag(MailboxMiscFlags.ArchiveMailbox), mailboxFlags.HasFlag(MailboxMiscFlags.DisabledMailbox), mailboxFlags.HasFlag(MailboxMiscFlags.CreatedByMove))
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C248 File Offset: 0x0000A448
		public PhysicalMailbox(DirectoryIdentity identity, ByteQuantifiedSize totalLogicalSize, ByteQuantifiedSize totalPhysicalSize, bool isQuarantined, StoreMailboxType mailboxType, ulong itemCount, DateTime? lastLogonTimestamp, DateTime? disconnectDate, bool isConsumer, bool isSoftDeleted, bool isArchive, bool isDisabled, bool isMoveDestination)
		{
			this.Identity = identity;
			this.TotalLogicalSize = totalLogicalSize;
			this.TotalPhysicalSize = totalPhysicalSize;
			this.IsQuarantined = isQuarantined;
			this.IsArchive = isArchive;
			this.IsSoftDeleted = isSoftDeleted;
			this.IsMoveDestination = isMoveDestination;
			this.IsDisabled = isDisabled;
			this.MailboxType = mailboxType;
			this.ItemCount = itemCount;
			this.LastLogonAge = ((lastLogonTimestamp == null) ? TimeSpan.MaxValue : (DateTime.UtcNow - lastLogonTimestamp.Value));
			this.LastLogonTimestamp = lastLogonTimestamp;
			this.DisconnectDate = disconnectDate;
			this.IsConsumer = isConsumer;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000C2E7 File Offset: 0x0000A4E7
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000C2EF File Offset: 0x0000A4EF
		[DataMember]
		public ByteQuantifiedSize AttachmentTableTotalSize { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000C300 File Offset: 0x0000A500
		[DataMember]
		public string DatabaseName { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000C309 File Offset: 0x0000A509
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000C311 File Offset: 0x0000A511
		[DataMember]
		public ulong DeletedItemCount { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000C31A File Offset: 0x0000A51A
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000C322 File Offset: 0x0000A522
		[DataMember]
		public DateTime? DisconnectDate { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000C32B File Offset: 0x0000A52B
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000C333 File Offset: 0x0000A533
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000C33C File Offset: 0x0000A53C
		public Guid Guid
		{
			get
			{
				return this.Identity.Guid;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000C349 File Offset: 0x0000A549
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000C351 File Offset: 0x0000A551
		[DataMember]
		public DirectoryIdentity Identity { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000C35A File Offset: 0x0000A55A
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000C362 File Offset: 0x0000A562
		[DataMember]
		public bool IsArchive { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000C36B File Offset: 0x0000A56B
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000C373 File Offset: 0x0000A573
		[DataMember]
		public bool IsConsumer { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000C37C File Offset: 0x0000A57C
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000C384 File Offset: 0x0000A584
		[DataMember]
		public bool IsDisabled { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000C38D File Offset: 0x0000A58D
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000C395 File Offset: 0x0000A595
		[DataMember]
		public bool IsMoveDestination { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000C39E File Offset: 0x0000A59E
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		[DataMember]
		public bool IsQuarantined { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000C3AF File Offset: 0x0000A5AF
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		[DataMember]
		public bool IsSoftDeleted { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		[DataMember]
		public ulong ItemCount { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000C3D1 File Offset: 0x0000A5D1
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		[DataMember]
		public TimeSpan LastLogonAge { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000C3E2 File Offset: 0x0000A5E2
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000C3EA File Offset: 0x0000A5EA
		[DataMember]
		public DateTime? LastLogonTimestamp { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000C3F3 File Offset: 0x0000A5F3
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000C3FB File Offset: 0x0000A5FB
		[DataMember]
		public StoreMailboxType MailboxType { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000C404 File Offset: 0x0000A604
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000C40C File Offset: 0x0000A60C
		[DataMember]
		public ByteQuantifiedSize MessageTableTotalSize { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000C415 File Offset: 0x0000A615
		public string Name
		{
			get
			{
				return this.Identity.Name;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000C422 File Offset: 0x0000A622
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000C42A File Offset: 0x0000A62A
		[DataMember]
		public Guid OrganizationId { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000C433 File Offset: 0x0000A633
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000C43B File Offset: 0x0000A63B
		[DataMember]
		public ByteQuantifiedSize OtherTablesTotalSize { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000C444 File Offset: 0x0000A644
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000C44C File Offset: 0x0000A64C
		[DataMember]
		public ByteQuantifiedSize TotalDeletedItemSize { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C455 File Offset: 0x0000A655
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000C45D File Offset: 0x0000A65D
		[DataMember]
		public ByteQuantifiedSize TotalItemSize { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000C466 File Offset: 0x0000A666
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000C473 File Offset: 0x0000A673
		public ByteQuantifiedSize TotalLogicalSize
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(this.totalLogicalSizeBytes);
			}
			private set
			{
				this.totalLogicalSizeBytes = value.ToBytes();
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000C48F File Offset: 0x0000A68F
		public ByteQuantifiedSize TotalPhysicalSize
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(this.totalPhysicalSizeBytes);
			}
			private set
			{
				this.totalPhysicalSizeBytes = value.ToBytes();
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000C49E File Offset: 0x0000A69E
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		[DataMember]
		public DateTime CreationTimestamp { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000C4AF File Offset: 0x0000A6AF
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000C4B7 File Offset: 0x0000A6B7
		[DataMember]
		public int ItemsPendingUpgrade { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		private MailboxState MailboxState
		{
			get
			{
				if (this.IsSoftDeleted)
				{
					return MailboxState.SoftDeleted;
				}
				if (this.IsDisabled)
				{
					return MailboxState.Disabled;
				}
				return MailboxState.Connected;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public void PopulateLogEntry(MailboxStatisticsLogEntry logEntry)
		{
			logEntry[MailboxStatisticsLogEntrySchema.AttachmentTableTotalSizeInBytes] = this.AttachmentTableTotalSize.ToBytes();
			logEntry[MailboxStatisticsLogEntrySchema.DatabaseName] = this.DatabaseName;
			logEntry[MailboxStatisticsLogEntrySchema.DeletedItemCount] = this.DeletedItemCount;
			logEntry[MailboxStatisticsLogEntrySchema.DisconnectDate] = this.DisconnectDate;
			logEntry[MailboxStatisticsLogEntrySchema.MailboxState] = this.MailboxState;
			logEntry[MailboxStatisticsLogEntrySchema.ExternalDirectoryOrganizationId] = this.OrganizationId;
			logEntry[MailboxStatisticsLogEntrySchema.IsArchiveMailbox] = this.IsArchive;
			logEntry[MailboxStatisticsLogEntrySchema.IsMoveDestination] = this.IsMoveDestination;
			logEntry[MailboxStatisticsLogEntrySchema.IsQuarantined] = this.IsQuarantined;
			logEntry[MailboxStatisticsLogEntrySchema.ItemCount] = this.ItemCount;
			logEntry[MailboxStatisticsLogEntrySchema.LastLogonTime] = this.LastLogonTimestamp;
			logEntry[MailboxStatisticsLogEntrySchema.LogicalSizeInM] = this.TotalLogicalSize.ToMB();
			logEntry[MailboxStatisticsLogEntrySchema.MailboxGuid] = this.Guid;
			logEntry[MailboxStatisticsLogEntrySchema.MailboxType] = this.ComputeMailboxType((LoadBalanceMailboxType)logEntry[MailboxStatisticsLogEntrySchema.MailboxType]);
			logEntry[MailboxStatisticsLogEntrySchema.MessageTableTotalSizeInBytes] = this.MessageTableTotalSize.ToBytes();
			logEntry[MailboxStatisticsLogEntrySchema.OtherTablesTotalSizeInBytes] = this.OtherTablesTotalSize.ToBytes();
			logEntry[MailboxStatisticsLogEntrySchema.PhysicalSizeInM] = this.TotalPhysicalSize.ToMB();
			logEntry[MailboxStatisticsLogEntrySchema.TotalDeletedItemSizeInBytes] = this.TotalDeletedItemSize.ToBytes();
			logEntry[MailboxStatisticsLogEntrySchema.TotalItemSizeInBytes] = this.TotalItemSize.ToBytes();
			logEntry[MailboxStatisticsLogEntrySchema.CreationTimestamp] = this.CreationTimestamp;
			logEntry[MailboxStatisticsLogEntrySchema.ItemsPendingUpgrade] = this.ItemsPendingUpgrade;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		private LoadBalanceMailboxType ComputeMailboxType(LoadBalanceMailboxType directoryMailboxType)
		{
			switch (this.MailboxType)
			{
			case StoreMailboxType.PublicFolderPrimary:
				return LoadBalanceMailboxType.PublicFolderPrimary;
			case StoreMailboxType.PublicFolderSecondary:
				return LoadBalanceMailboxType.PublicFolderSecondary;
			default:
				if (this.IsConsumer)
				{
					return LoadBalanceMailboxType.Consumer;
				}
				return directoryMailboxType;
			}
		}

		// Token: 0x04000174 RID: 372
		[DataMember]
		private ulong totalLogicalSizeBytes;

		// Token: 0x04000175 RID: 373
		[DataMember]
		private ulong totalPhysicalSizeBytes;
	}
}

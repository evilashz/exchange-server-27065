using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000084 RID: 132
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class VirtualPhysicalMailbox : IPhysicalMailbox
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000C734 File Offset: 0x0000A934
		public VirtualPhysicalMailbox(IClientFactory clientFactory, DirectoryDatabase database, Guid mailboxGuid, ILogger logger, bool isArchive)
		{
			this.clientFactory = clientFactory;
			this.database = database;
			this.logger = logger;
			this.physicalMailbox = new Lazy<IPhysicalMailbox>(new Func<IPhysicalMailbox>(this.LoadMailboxData));
			this.Guid = mailboxGuid;
			this.IsArchive = isArchive;
			this.IsConsumer = false;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000C78A File Offset: 0x0000A98A
		public ByteQuantifiedSize AttachmentTableTotalSize
		{
			get
			{
				return this.PhysicalMailbox.AttachmentTableTotalSize;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000C797 File Offset: 0x0000A997
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000C79F File Offset: 0x0000A99F
		public string DatabaseName { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public ulong DeletedItemCount
		{
			get
			{
				return this.PhysicalMailbox.DeletedItemCount;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		public DateTime? DisconnectDate
		{
			get
			{
				return this.PhysicalMailbox.DisconnectDate;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		[DataMember]
		public Guid Guid { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		public DirectoryIdentity Identity
		{
			get
			{
				return this.PhysicalMailbox.Identity;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public bool IsArchive { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000C7F1 File Offset: 0x0000A9F1
		public bool IsDisabled
		{
			get
			{
				return this.PhysicalMailbox.IsDisabled;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		public bool IsMoveDestination
		{
			get
			{
				return this.PhysicalMailbox.IsMoveDestination;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000C80B File Offset: 0x0000AA0B
		public bool IsQuarantined
		{
			get
			{
				return this.PhysicalMailbox.IsQuarantined;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000C818 File Offset: 0x0000AA18
		public bool IsSoftDeleted
		{
			get
			{
				return this.PhysicalMailbox.IsSoftDeleted;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000C825 File Offset: 0x0000AA25
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000C82D File Offset: 0x0000AA2D
		[DataMember]
		public bool IsConsumer { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000C836 File Offset: 0x0000AA36
		public ulong ItemCount
		{
			get
			{
				return this.PhysicalMailbox.ItemCount;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000C843 File Offset: 0x0000AA43
		public TimeSpan LastLogonAge
		{
			get
			{
				return this.PhysicalMailbox.LastLogonAge;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000C850 File Offset: 0x0000AA50
		public DateTime? LastLogonTimestamp
		{
			get
			{
				return this.PhysicalMailbox.LastLogonTimestamp;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000C85D File Offset: 0x0000AA5D
		public StoreMailboxType MailboxType
		{
			get
			{
				return this.PhysicalMailbox.MailboxType;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000C86A File Offset: 0x0000AA6A
		public ByteQuantifiedSize MessageTableTotalSize
		{
			get
			{
				return this.PhysicalMailbox.MessageTableTotalSize;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000C877 File Offset: 0x0000AA77
		public string Name
		{
			get
			{
				return this.PhysicalMailbox.Name;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000C884 File Offset: 0x0000AA84
		public Guid OrganizationId
		{
			get
			{
				return this.PhysicalMailbox.OrganizationId;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000C891 File Offset: 0x0000AA91
		public ByteQuantifiedSize OtherTablesTotalSize
		{
			get
			{
				return this.PhysicalMailbox.OtherTablesTotalSize;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000C89E File Offset: 0x0000AA9E
		public ByteQuantifiedSize TotalDeletedItemSize
		{
			get
			{
				return this.PhysicalMailbox.TotalDeletedItemSize;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000C8AB File Offset: 0x0000AAAB
		public ByteQuantifiedSize TotalItemSize
		{
			get
			{
				return this.PhysicalMailbox.TotalItemSize;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public ByteQuantifiedSize TotalLogicalSize
		{
			get
			{
				return this.PhysicalMailbox.TotalLogicalSize;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000C8C5 File Offset: 0x0000AAC5
		public ByteQuantifiedSize TotalPhysicalSize
		{
			get
			{
				return this.PhysicalMailbox.TotalPhysicalSize;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000C8D2 File Offset: 0x0000AAD2
		public DateTime CreationTimestamp
		{
			get
			{
				return this.PhysicalMailbox.CreationTimestamp;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000C8DF File Offset: 0x0000AADF
		public int ItemsPendingUpgrade
		{
			get
			{
				return this.PhysicalMailbox.ItemsPendingUpgrade;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private IPhysicalMailbox PhysicalMailbox
		{
			get
			{
				return this.physicalMailbox.Value;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
		public void PopulateLogEntry(MailboxStatisticsLogEntry logEntry)
		{
			throw new InvalidOperationException("Virtual mailboxes should not be logged.");
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C908 File Offset: 0x0000AB08
		private IPhysicalMailbox LoadMailboxData()
		{
			IPhysicalMailbox result;
			using (OperationTracker.Create(this.logger, "Retrieving single mailbox {0} data from database {1}", new object[]
			{
				this.Guid,
				this.database.Identity
			}))
			{
				using (IPhysicalDatabase physicalDatabaseConnection = this.clientFactory.GetPhysicalDatabaseConnection(this.database))
				{
					result = (physicalDatabaseConnection.GetMailbox(this.Guid) ?? EmptyPhysicalMailbox.Instance);
				}
			}
			return result;
		}

		// Token: 0x0400018D RID: 397
		private readonly IClientFactory clientFactory;

		// Token: 0x0400018E RID: 398
		[DataMember]
		private readonly DirectoryDatabase database;

		// Token: 0x0400018F RID: 399
		private readonly ILogger logger;

		// Token: 0x04000190 RID: 400
		private readonly Lazy<IPhysicalMailbox> physicalMailbox;
	}
}

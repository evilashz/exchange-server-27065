using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EmptyPhysicalMailbox : IPhysicalMailbox
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0000B5B7 File Offset: 0x000097B7
		private EmptyPhysicalMailbox()
		{
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000B5BF File Offset: 0x000097BF
		public ByteQuantifiedSize AttachmentTableTotalSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000B5C6 File Offset: 0x000097C6
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000B5CE File Offset: 0x000097CE
		public string DatabaseName { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000B5D7 File Offset: 0x000097D7
		public ulong DeletedItemCount
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000B5DC File Offset: 0x000097DC
		public DateTime? DisconnectDate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000B5F2 File Offset: 0x000097F2
		public Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000B5F9 File Offset: 0x000097F9
		public DirectoryIdentity Identity
		{
			get
			{
				return DirectoryIdentity.NullIdentity;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000B600 File Offset: 0x00009800
		public bool IsArchive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B603 File Offset: 0x00009803
		public bool IsDisabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000B606 File Offset: 0x00009806
		public bool IsMoveDestination
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000B609 File Offset: 0x00009809
		public bool IsQuarantined
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000B60C File Offset: 0x0000980C
		public bool IsSoftDeleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000B60F File Offset: 0x0000980F
		public bool IsConsumer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000B612 File Offset: 0x00009812
		public ulong ItemCount
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B616 File Offset: 0x00009816
		public TimeSpan LastLogonAge
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000B620 File Offset: 0x00009820
		public DateTime? LastLogonTimestamp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B636 File Offset: 0x00009836
		public StoreMailboxType MailboxType
		{
			get
			{
				return StoreMailboxType.Private;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B639 File Offset: 0x00009839
		public ByteQuantifiedSize MessageTableTotalSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B640 File Offset: 0x00009840
		public string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000B647 File Offset: 0x00009847
		public Guid OrganizationId
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000B64E File Offset: 0x0000984E
		public ByteQuantifiedSize OtherTablesTotalSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000B655 File Offset: 0x00009855
		public ByteQuantifiedSize TotalDeletedItemSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000B65C File Offset: 0x0000985C
		public ByteQuantifiedSize TotalItemSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000B663 File Offset: 0x00009863
		public ByteQuantifiedSize TotalLogicalSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000B66A File Offset: 0x0000986A
		public ByteQuantifiedSize TotalPhysicalSize
		{
			get
			{
				return ByteQuantifiedSize.Zero;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000B671 File Offset: 0x00009871
		public DateTime CreationTimestamp
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B678 File Offset: 0x00009878
		public int ItemsPendingUpgrade
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000B67B File Offset: 0x0000987B
		public void PopulateLogEntry(MailboxStatisticsLogEntry logEntry)
		{
			throw new InvalidOperationException("Empty mailboxes should not be logged.");
		}

		// Token: 0x04000161 RID: 353
		public static readonly IPhysicalMailbox Instance = new EmptyPhysicalMailbox();
	}
}

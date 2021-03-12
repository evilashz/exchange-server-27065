using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class DirectoryMailbox : DirectoryObject
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000AA91 File Offset: 0x00008C91
		public DirectoryMailbox(IDirectoryProvider directory, DirectoryIdentity identity, IEnumerable<IPhysicalMailbox> physicalMailboxes, DirectoryMailboxType mailboxType = DirectoryMailboxType.Organization) : base(directory, identity)
		{
			this.physicalMailboxes = physicalMailboxes.ToList<IPhysicalMailbox>();
			this.MailboxType = mailboxType;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000AAB7 File Offset: 0x00008CB7
		public virtual bool IsArchiveOnly
		{
			get
			{
				if (this.physicalMailboxes.Count > 0)
				{
					return this.physicalMailboxes.All((IPhysicalMailbox pm) => pm.IsArchive);
				}
				return false;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000AAF1 File Offset: 0x00008CF1
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000AAF9 File Offset: 0x00008CF9
		[DataMember]
		public bool IsBeingLoadBalanced { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public virtual long ItemCount
		{
			get
			{
				return this.physicalMailboxes.Sum((IPhysicalMailbox mbx) => (long)mbx.ItemCount);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000AB3E File Offset: 0x00008D3E
		public int ItemsPendingUpgrade
		{
			get
			{
				return this.physicalMailboxes.Aggregate(0, (int current, IPhysicalMailbox mailbox) => mailbox.ItemsPendingUpgrade + current);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000AB77 File Offset: 0x00008D77
		public virtual ByteQuantifiedSize LogicalSize
		{
			get
			{
				return this.physicalMailboxes.Aggregate(ByteQuantifiedSize.Zero, (ByteQuantifiedSize current, IPhysicalMailbox mailbox) => current + mailbox.TotalLogicalSize);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000ABA6 File Offset: 0x00008DA6
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000ABAE File Offset: 0x00008DAE
		public IMailboxProvisioningConstraints MailboxProvisioningConstraints { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000ABBF File Offset: 0x00008DBF
		[DataMember]
		public DirectoryMailboxType MailboxType { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public Guid OrganizationId
		{
			get
			{
				return base.Identity.OrganizationId;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		public IEnumerable<IPhysicalMailbox> PhysicalMailboxes
		{
			get
			{
				return this.physicalMailboxes;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000ABEB File Offset: 0x00008DEB
		public virtual ByteQuantifiedSize PhysicalSize
		{
			get
			{
				return this.physicalMailboxes.Aggregate(ByteQuantifiedSize.Zero, (ByteQuantifiedSize current, IPhysicalMailbox mailbox) => current + mailbox.TotalPhysicalSize);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000AC1A File Offset: 0x00008E1A
		public override bool SupportsMoving
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000AC1D File Offset: 0x00008E1D
		public virtual long TotalCpu
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000AC33 File Offset: 0x00008E33
		public TimeSpan MinimumAgeInDatabase
		{
			get
			{
				if (this.physicalMailboxes.Count == 0)
				{
					return TimeSpan.Zero;
				}
				return this.physicalMailboxes.Min((IPhysicalMailbox pm) => TimeProvider.UtcNow - pm.CreationTimestamp);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000AC70 File Offset: 0x00008E70
		public override IRequest CreateRequestToMove(DirectoryIdentity target, string batchName, ILogger logger)
		{
			if (target != null && target.ObjectType != DirectoryObjectType.Database)
			{
				throw new NotSupportedException("Mailboxes can only be moved into databases.");
			}
			return base.Directory.CreateRequestToMove(this, target, batchName, logger);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public void EmitLogEntry(ObjectLogCollector logCollector)
		{
			MailboxStatisticsLogEntry mailboxStatisticsLogEntry = new MailboxStatisticsLogEntry();
			mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.RecipientGuid] = base.Guid;
			mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.ExternalDirectoryOrganizationId] = this.OrganizationId;
			mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.MailboxType] = LoadBalanceMailboxType.OrgIdMailbox;
			mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.MailboxState] = MailboxState.AdOnly;
			if (this.MailboxProvisioningConstraints != null)
			{
				mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.MailboxProvisioningConstraint] = string.Format("{0}", this.MailboxProvisioningConstraints.HardConstraint);
				mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.MailboxProvisioningPreferences] = string.Join(";", from sc in this.MailboxProvisioningConstraints.SoftConstraints
				select sc.Value);
			}
			if (base.Parent != null)
			{
				mailboxStatisticsLogEntry[MailboxStatisticsLogEntrySchema.DatabaseName] = base.Parent.Name;
			}
			bool flag = false;
			foreach (IPhysicalMailbox physicalMailbox in this.PhysicalMailboxes.OfType<PhysicalMailbox>())
			{
				MailboxStatisticsLogEntry mailboxStatisticsLogEntry2 = new MailboxStatisticsLogEntry();
				mailboxStatisticsLogEntry2.CopyChangesFrom(mailboxStatisticsLogEntry);
				physicalMailbox.PopulateLogEntry(mailboxStatisticsLogEntry2);
				logCollector.LogObject<MailboxStatisticsLogEntry>(mailboxStatisticsLogEntry2);
				flag = true;
			}
			if (!flag)
			{
				logCollector.LogObject<MailboxStatisticsLogEntry>(mailboxStatisticsLogEntry);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public DirectoryDatabase GetDatabaseForMailbox()
		{
			return base.Directory.GetDatabaseForMailbox(base.Identity);
		}

		// Token: 0x04000135 RID: 309
		[DataMember]
		private readonly List<IPhysicalMailbox> physicalMailboxes;
	}
}

using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000068 RID: 104
	internal class StoreMailboxData : MailboxData
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000F0C1 File Offset: 0x0000D2C1
		public StoreMailboxData(Guid guid, Guid databaseGuid, string displayName, OrganizationId organizationId) : this(guid, databaseGuid, displayName, organizationId, null)
		{
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F0CF File Offset: 0x0000D2CF
		public StoreMailboxData(Guid guid, Guid databaseGuid, string displayName, OrganizationId organizationId, TenantPartitionHint tenantPartitionHint) : base(guid, databaseGuid, displayName)
		{
			this.organizationId = organizationId;
			this.TenantPartitionHint = tenantPartitionHint;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000F0EA File Offset: 0x0000D2EA
		public Guid Guid
		{
			get
			{
				return base.MailboxGuid;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F0F2 File Offset: 0x0000D2F2
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000F0FA File Offset: 0x0000D2FA
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000F102 File Offset: 0x0000D302
		public bool IsPublicFolderMailbox { get; internal set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000F10B File Offset: 0x0000D30B
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000F113 File Offset: 0x0000D313
		public TenantPartitionHint TenantPartitionHint { get; set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F11C File Offset: 0x0000D31C
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			StoreMailboxData storeMailboxData = other as StoreMailboxData;
			return storeMailboxData != null && this.Equals(storeMailboxData);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F141 File Offset: 0x0000D341
		public bool Equals(StoreMailboxData other)
		{
			return other != null && !(base.MailboxGuid != other.MailboxGuid) && base.Equals(other);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F164 File Offset: 0x0000D364
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Guid.GetHashCode();
		}

		// Token: 0x040001CD RID: 461
		private readonly OrganizationId organizationId;
	}
}

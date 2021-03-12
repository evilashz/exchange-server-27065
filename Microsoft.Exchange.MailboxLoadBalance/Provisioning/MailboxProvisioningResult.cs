using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000D4 RID: 212
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class MailboxProvisioningResult : IExtensibleDataObject
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00012E48 File Offset: 0x00011048
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00012E50 File Offset: 0x00011050
		[DataMember]
		public DirectoryIdentity Database { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00012E59 File Offset: 0x00011059
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00012E61 File Offset: 0x00011061
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00012E6A File Offset: 0x0001106A
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00012E72 File Offset: 0x00011072
		[DataMember]
		public IMailboxProvisioningConstraints MailboxProvisioningConstraints { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00012E7B File Offset: 0x0001107B
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00012E83 File Offset: 0x00011083
		public MailboxProvisioningResultStatus Status { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00012E8C File Offset: 0x0001108C
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00012E94 File Offset: 0x00011094
		[DataMember]
		private int StatusInt
		{
			get
			{
				return (int)this.Status;
			}
			set
			{
				this.Status = (MailboxProvisioningResultStatus)value;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00012EA0 File Offset: 0x000110A0
		public void ValidateSelection()
		{
			switch (this.Status)
			{
			case MailboxProvisioningResultStatus.Valid:
				return;
			case MailboxProvisioningResultStatus.InsufficientCapacity:
				throw new InsufficientCapacityProvisioningException();
			case MailboxProvisioningResultStatus.ConstraintCouldNotBeSatisfied:
				throw new ConstraintCouldNotBeSatisfiedProvisioningException(string.Format("{0}", this.MailboxProvisioningConstraints));
			default:
				throw new UnknownProvisioningStatusException();
			}
		}
	}
}

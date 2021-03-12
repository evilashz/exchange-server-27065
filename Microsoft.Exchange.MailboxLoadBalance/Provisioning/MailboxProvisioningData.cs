using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000D3 RID: 211
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class MailboxProvisioningData : IExtensibleDataObject
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x00012D7C File Offset: 0x00010F7C
		public MailboxProvisioningData(ByteQuantifiedSize totalDataSize, IMailboxProvisioningConstraints mailboxProvisioningConstraintses = null, LoadMetricStorage consumedLoad = null)
		{
			this.TotalDataSize = totalDataSize;
			this.MailboxProvisioningConstraints = mailboxProvisioningConstraintses;
			if (consumedLoad == null)
			{
				this.ConsumedLoad = new LoadMetricStorage();
				this.ConsumedLoad[PhysicalSize.Instance] = PhysicalSize.Instance.GetUnitsForSize(totalDataSize);
				return;
			}
			this.ConsumedLoad = consumedLoad;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00012DCE File Offset: 0x00010FCE
		public MailboxProvisioningData()
		{
			this.ConsumedLoad = new LoadMetricStorage();
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00012DE1 File Offset: 0x00010FE1
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00012DE9 File Offset: 0x00010FE9
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00012DF2 File Offset: 0x00010FF2
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00012DFA File Offset: 0x00010FFA
		[DataMember]
		public ByteQuantifiedSize TotalDataSize { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00012E03 File Offset: 0x00011003
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00012E0B File Offset: 0x0001100B
		[DataMember]
		public IMailboxProvisioningConstraints MailboxProvisioningConstraints { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00012E14 File Offset: 0x00011014
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00012E1C File Offset: 0x0001101C
		[DataMember]
		public LoadMetricStorage ConsumedLoad { get; set; }

		// Token: 0x060006A6 RID: 1702 RVA: 0x00012E25 File Offset: 0x00011025
		public override string ToString()
		{
			return string.Format("ProvisioningData['{0}' bytes, '{1}' consumed load, '{2}' constraint.", this.TotalDataSize, this.ConsumedLoad, this.MailboxProvisioningConstraints);
		}
	}
}

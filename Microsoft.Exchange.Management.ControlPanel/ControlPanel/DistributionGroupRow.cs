using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023B RID: 571
	[KnownType(typeof(DistributionGroupRow))]
	[DataContract]
	public class DistributionGroupRow : RecipientRow
	{
		// Token: 0x060027DE RID: 10206 RVA: 0x0007D300 File Offset: 0x0007B500
		public DistributionGroupRow(ReducedRecipient distributionGroup) : base(distributionGroup)
		{
			this.Initalize(distributionGroup.RecipientTypeDetails);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0007D315 File Offset: 0x0007B515
		public DistributionGroupRow(MailEnabledRecipient distributionGroup) : base(distributionGroup)
		{
			this.Initalize(distributionGroup.RecipientTypeDetails);
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x0007D32A File Offset: 0x0007B52A
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x0007D332 File Offset: 0x0007B532
		[DataMember]
		public string GroupType { get; private set; }

		// Token: 0x060027E2 RID: 10210 RVA: 0x0007D33B File Offset: 0x0007B53B
		private void Initalize(RecipientTypeDetails recipientTypeDetails)
		{
			this.GroupType = DistributionGroupHelper.GenerateGroupTypeText(recipientTypeDetails);
		}
	}
}

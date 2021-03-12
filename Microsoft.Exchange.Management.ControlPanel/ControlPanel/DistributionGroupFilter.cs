using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004ED RID: 1261
	[DataContract]
	public class DistributionGroupFilter : RecipientFilter
	{
		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000B82DF File Offset: 0x000B64DF
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000B82E8 File Offset: 0x000B64E8
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup
				};
			}
		}
	}
}

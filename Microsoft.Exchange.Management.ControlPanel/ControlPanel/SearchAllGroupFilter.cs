using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023E RID: 574
	[DataContract]
	public class SearchAllGroupFilter : RecipientFilter
	{
		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0007D82C File Offset: 0x0007BA2C
		public override string RbacScope
		{
			get
			{
				return "@R:MyGAL";
			}
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0007D834 File Offset: 0x0007BA34
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

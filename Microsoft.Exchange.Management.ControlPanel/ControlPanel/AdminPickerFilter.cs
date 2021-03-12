using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000318 RID: 792
	[DataContract]
	public class AdminPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x0008E970 File Offset: 0x0008CB70
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup,
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.DynamicDistributionGroup
				};
			}
		}
	}
}

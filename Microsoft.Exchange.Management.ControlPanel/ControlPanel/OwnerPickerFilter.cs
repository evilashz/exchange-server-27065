using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000344 RID: 836
	[DataContract]
	public class OwnerPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x0009054C File Offset: 0x0008E74C
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.MailUser,
					(RecipientTypeDetails)((ulong)int.MinValue)
				};
			}
		}
	}
}

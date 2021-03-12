using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200034E RID: 846
	[DataContract]
	public class TeamMailboxUserPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x00091178 File Offset: 0x0008F378
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox
				};
			}
		}
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000336 RID: 822
	[DataContract]
	public class MailUserAndMailboxPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x0008F418 File Offset: 0x0008D618
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.UserMailbox
				};
			}
		}
	}
}

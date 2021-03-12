using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200032A RID: 810
	[DataContract]
	public class DiscoveryMailboxPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x0008F1B4 File Offset: 0x0008D3B4
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.DiscoveryMailbox
				};
			}
		}
	}
}

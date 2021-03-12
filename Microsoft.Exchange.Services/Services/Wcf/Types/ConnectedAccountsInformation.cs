using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A9B RID: 2715
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectedAccountsInformation : OptionsPropertyChangeTracker
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x001067E1 File Offset: 0x001049E1
		// (set) Token: 0x06004CC2 RID: 19650 RVA: 0x001067E9 File Offset: 0x001049E9
		[DataMember]
		public Identity DefaultReplyAddress { get; set; }

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x001067F2 File Offset: 0x001049F2
		// (set) Token: 0x06004CC4 RID: 19652 RVA: 0x001067FA File Offset: 0x001049FA
		[DataMember]
		public SendAddressData[] SendAddresses { get; set; }

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x00106803 File Offset: 0x00104A03
		// (set) Token: 0x06004CC6 RID: 19654 RVA: 0x0010680B File Offset: 0x00104A0B
		[DataMember]
		public Subscription[] Subscriptions { get; set; }
	}
}

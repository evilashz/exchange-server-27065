using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA7 RID: 2727
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetImapSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x001069D7 File Offset: 0x00104BD7
		// (set) Token: 0x06004CED RID: 19693 RVA: 0x001069DF File Offset: 0x00104BDF
		[DataMember(IsRequired = true)]
		public ImapSubscription ImapSubscription { get; set; }

		// Token: 0x06004CEE RID: 19694 RVA: 0x001069E8 File Offset: 0x00104BE8
		public override string ToString()
		{
			return string.Format("GetImapSubscriptionResponse: {0}", this.ImapSubscription);
		}
	}
}

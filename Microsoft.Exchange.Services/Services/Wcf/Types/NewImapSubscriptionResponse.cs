using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC6 RID: 2758
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewImapSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x00107E26 File Offset: 0x00106026
		// (set) Token: 0x06004E7A RID: 20090 RVA: 0x00107E2E File Offset: 0x0010602E
		[DataMember(IsRequired = true)]
		public ImapSubscription ImapSubscription { get; set; }

		// Token: 0x06004E7B RID: 20091 RVA: 0x00107E37 File Offset: 0x00106037
		public override string ToString()
		{
			return string.Format("NewImapSubscriptionResponse: {0}", this.ImapSubscription);
		}
	}
}

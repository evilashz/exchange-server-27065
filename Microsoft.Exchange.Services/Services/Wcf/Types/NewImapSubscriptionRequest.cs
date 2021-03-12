using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC5 RID: 2757
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewImapSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00107DFB File Offset: 0x00105FFB
		// (set) Token: 0x06004E76 RID: 20086 RVA: 0x00107E03 File Offset: 0x00106003
		[DataMember(IsRequired = true)]
		public NewImapSubscriptionData ImapSubscription { get; set; }

		// Token: 0x06004E77 RID: 20087 RVA: 0x00107E0C File Offset: 0x0010600C
		public override string ToString()
		{
			return string.Format("NewImapSubscriptionRequest: {0}", this.ImapSubscription);
		}
	}
}

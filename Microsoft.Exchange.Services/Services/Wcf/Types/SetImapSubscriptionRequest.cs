using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ADB RID: 2779
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImapSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0010860D File Offset: 0x0010680D
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x00108615 File Offset: 0x00106815
		[DataMember(IsRequired = true)]
		public SetImapSubscriptionData ImapSubscription { get; set; }

		// Token: 0x06004F21 RID: 20257 RVA: 0x0010861E File Offset: 0x0010681E
		public override string ToString()
		{
			return string.Format("SetImapSubscriptionRequest: {0}", this.ImapSubscription);
		}
	}
}

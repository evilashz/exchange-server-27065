using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACA RID: 2762
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewPopSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06004E9E RID: 20126 RVA: 0x00108008 File Offset: 0x00106208
		// (set) Token: 0x06004E9F RID: 20127 RVA: 0x00108010 File Offset: 0x00106210
		[DataMember(IsRequired = true)]
		public NewPopSubscriptionData PopSubscription { get; set; }

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00108019 File Offset: 0x00106219
		public override string ToString()
		{
			return string.Format("NewPopSubscriptionRequest: {0}", this.PopSubscription);
		}
	}
}

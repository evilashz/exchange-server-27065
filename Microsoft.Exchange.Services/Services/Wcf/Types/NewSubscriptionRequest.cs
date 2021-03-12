using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACD RID: 2765
	[KnownType(typeof(Subscription))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewSubscriptionRequest : BaseJsonRequest
	{
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00108146 File Offset: 0x00106346
		// (set) Token: 0x06004EB8 RID: 20152 RVA: 0x0010814E File Offset: 0x0010634E
		[DataMember(IsRequired = true)]
		public NewSubscriptionData Subscription { get; set; }

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00108157 File Offset: 0x00106357
		public override string ToString()
		{
			return string.Format("NewSubscriptionRequest: {0}", this.Subscription);
		}
	}
}

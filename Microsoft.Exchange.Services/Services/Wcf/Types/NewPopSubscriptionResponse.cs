using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ACB RID: 2763
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewPopSubscriptionResponse : OptionsResponseBase
	{
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x00108033 File Offset: 0x00106233
		// (set) Token: 0x06004EA3 RID: 20131 RVA: 0x0010803B File Offset: 0x0010623B
		[DataMember(IsRequired = true)]
		public PopSubscription PopSubscription { get; set; }

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00108044 File Offset: 0x00106244
		public override string ToString()
		{
			return string.Format("NewPopSubscriptionResponse: {0}", this.PopSubscription);
		}
	}
}

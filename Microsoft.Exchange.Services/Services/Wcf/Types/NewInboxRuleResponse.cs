using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC8 RID: 2760
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewInboxRuleResponse : OptionsResponseBase
	{
		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x00107E87 File Offset: 0x00106087
		// (set) Token: 0x06004E82 RID: 20098 RVA: 0x00107E8F File Offset: 0x0010608F
		[DataMember(IsRequired = true)]
		public InboxRule InboxRule { get; set; }

		// Token: 0x06004E83 RID: 20099 RVA: 0x00107E98 File Offset: 0x00106098
		public override string ToString()
		{
			return string.Format("NewInboxRuleResponse: {0}", this.InboxRule);
		}
	}
}

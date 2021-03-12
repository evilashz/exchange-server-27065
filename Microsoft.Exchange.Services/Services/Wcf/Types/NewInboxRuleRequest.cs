using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC7 RID: 2759
	[KnownType(typeof(InboxRule))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewInboxRuleRequest : UpdateInboxRuleRequestBase
	{
		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x00107E51 File Offset: 0x00106051
		// (set) Token: 0x06004E7E RID: 20094 RVA: 0x00107E59 File Offset: 0x00106059
		[DataMember(IsRequired = true)]
		public NewInboxRuleData InboxRule { get; set; }

		// Token: 0x06004E7F RID: 20095 RVA: 0x00107E62 File Offset: 0x00106062
		public override string ToString()
		{
			return string.Format(base.ToString() + ", InboxRule = {0}", this.InboxRule);
		}
	}
}

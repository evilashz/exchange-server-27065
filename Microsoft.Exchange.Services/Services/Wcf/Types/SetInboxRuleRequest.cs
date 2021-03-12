using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ADC RID: 2780
	[KnownType(typeof(InboxRule))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetInboxRuleRequest : UpdateInboxRuleRequestBase
	{
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x00108638 File Offset: 0x00106838
		// (set) Token: 0x06004F24 RID: 20260 RVA: 0x00108640 File Offset: 0x00106840
		[DataMember(IsRequired = true)]
		public SetInboxRuleData InboxRule { get; set; }

		// Token: 0x06004F25 RID: 20261 RVA: 0x00108649 File Offset: 0x00106849
		public override string ToString()
		{
			return string.Format(base.ToString() + ", InboxRule = {0}", this.InboxRule);
		}
	}
}
